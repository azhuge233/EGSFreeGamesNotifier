using Microsoft.Extensions.Logging;
using EGSFreeGamesNotifier.Models;
using EGSFreeGamesNotifier.Models.EGSJsonData;
using EGSFreeGamesNotifier.Models.Record;
using EGSFreeGamesNotifier.Strings;
using System.Text.Json;
using EGSFreeGamesNotifier.Models.GraphQL;

namespace EGSFreeGamesNotifier.Services {
	internal class Parser(ILogger<Parser> logger) : IDisposable {
		private readonly ILogger<Parser> _logger = logger;

		internal ParseResult Parse(Tuple<string, string> source, List<FreeGameRecord> oldRecords) {
			try {
				_logger.LogDebug(ParseStrings.debugParse);

				var result = new ParseResult();

				ParseWeeklyFreeGame(source.Item1, oldRecords, result);
				ParseGraphQLFreeGame(source.Item2, oldRecords, result);

				_logger.LogDebug($"Done: {ParseStrings.debugParse}");
				return result;
			} catch (Exception) {
				_logger.LogError($"Error: {ParseStrings.debugParse}");
				throw;
			} finally {
				Dispose();
			}
		}

		private void ParseWeeklyFreeGame(string source, List<FreeGameRecord> oldRecords, ParseResult result) {
			try {
				_logger.LogDebug(ParseStrings.debugParseWeekly);

				var jsonData = JsonSerializer.Deserialize<EGSJsonData>(source);

				if (jsonData != null) {
					foreach (var game in jsonData.Data.Catalog.SearchStore.Elements) {
						if (game.Promotions == null) {
							_logger.LogDebug(ParseStrings.debugGameNullPromotion, game.Title);
							continue;
						}

						#region get new record basic info
						var newRecord = new FreeGameRecord() {
							Title = game.Title,
							Name = GetProductSlug(game),
							Description = game.Description,
							ID = game.ID,
							Namespace = game.Namespace,
							OfferType = game.OfferType
						};

						newRecord.Url = $"{ParseStrings.EGSUrlPres[ParseStrings.OfferTypesToUrlPrefix.GetValueOrDefault(game.OfferType, 1)]}{newRecord.Name}";
						newRecord.PurchaseUrl = string.Format(ParseStrings.PurchaseBaseUrl, newRecord.Namespace, newRecord.ID);
						#endregion

						#region mystery game 
						if (newRecord.Title.StartsWith(ParseStrings.MysteryGameName)) {
							newRecord.IsMysteryGame = true;
							newRecord.Url = ParseStrings.EGSUrlPres.FirstOrDefault();
							newRecord.PurchaseUrl = string.Empty;
							_logger.LogDebug(ParseStrings.debugMysteryGameFound, newRecord.Title);
						}
						#endregion

						#region dev account
						if (game.Seller.ID == ParseStrings.SellerDevAccountID || string.Equals(game.Seller.Name, ParseStrings.SellerDevAccountName, StringComparison.OrdinalIgnoreCase)) {
							newRecord.IsDevAccount = true;
							newRecord.PurchaseUrl = string.Empty;
							_logger.LogDebug(ParseStrings.debugDevAccountFound, game.Title);
						}
						#endregion

						#region deal with promotion time and upcoming
						if (game.Promotions.PromotionalOffers.Count > 0 && game.Promotions.PromotionalOffers.First().PromotionalOffers.Any(offer => offer.DiscountSetting.DiscountPercentage == 0)) {
							var offer = game.Promotions.PromotionalOffers.First().PromotionalOffers.First(offer => offer.DiscountSetting.DiscountPercentage == 0);

							newRecord.StartTime = offer.StartDate.AddHours(8);
							// handles end time null
							newRecord.EndTime = !offer.EndDate.HasValue ? DateTime.MaxValue : offer.EndDate.Value.AddHours(8);
						} else if (game.Promotions.UpcomingPromotionalOffers.Count > 0 && game.Promotions.UpcomingPromotionalOffers.First().PromotionalOffers.Any(offer => offer.DiscountSetting.DiscountPercentage == 0)) {
							newRecord.IsUpcomingPromotion = true;
							var offer = game.Promotions.UpcomingPromotionalOffers.First().PromotionalOffers.First(offer => offer.DiscountSetting.DiscountPercentage == 0);

							newRecord.StartTime = offer.StartDate.AddHours(8);
							// handles end time null
							newRecord.EndTime = !offer.EndDate.HasValue ? DateTime.MaxValue : offer.EndDate.Value.AddHours(8);
						} else {
							_logger.LogDebug(ParseStrings.debugGameNoPromotion, game.Title);
							continue;
						}
						#endregion

						result.Records.Add(newRecord);

						#region decide if notify
						if (!oldRecords.Any(record => record.ID == newRecord.ID)) {
							_logger.LogInformation(ParseStrings.infoFoundNewGame, newRecord.Title, game.OfferType);
							result.NotifyRecords.Add(new NotifyRecord(newRecord));
						} else if (oldRecords.First(record => record.ID == newRecord.ID).IsUpcomingPromotion != newRecord.IsUpcomingPromotion) {
							_logger.LogInformation(ParseStrings.infoUpcomingGameIsLive, newRecord.Title, game.OfferType);
							result.NotifyRecords.Add(new NotifyRecord(newRecord));
						} else _logger.LogDebug(ParseStrings.debugFoundInOldRecords, game.Title, game.OfferType);
						#endregion
					}
				} else _logger.LogDebug(ParseStrings.debugJsonDataNull);

				if (result.NotifyRecords.Count > 0)
					result.NotifyRecords = [.. result.NotifyRecords.OrderByDescending(record => record.IsUpcomingPromotion)];

				_logger.LogDebug($"Done: {ParseStrings.debugParseWeekly}");
			} catch (Exception) {
				_logger.LogError($"Error: {ParseStrings.debugParseWeekly}");
				throw;
			} finally {
				Dispose();
			}
		}

		private void ParseGraphQLFreeGame(string source, List<FreeGameRecord> oldRecords, ParseResult result) {
			try {
				_logger.LogDebug(ParseStrings.debugParseGraphQL);

				var jsonData = JsonSerializer.Deserialize<GraphQLJsonData>(source);

				if (jsonData != null) {
					foreach (var game in jsonData.Data.Catalog.SearchStore.Elements) { 
						if (game.Promotions == null) {
							_logger.LogDebug(ParseStrings.debugGameNullPromotion, game.Title);
							continue;
						}

						#region get new record basic info
						var newRecord = new FreeGameRecord() {
							Title = game.Title,
							Name = GetProductSlug(game),
							Description = game.Description,
							ID = game.ID,
							Namespace = game.Namespace,
							OfferType = ParseStrings.OfferTypeGraphQLFreeGame
						};

						newRecord.Url = $"{ParseStrings.EGSUrlPres[ParseStrings.OfferTypesToUrlPrefix.GetValueOrDefault(newRecord.OfferType, 1)]}{newRecord.Name}";
						newRecord.PurchaseUrl = string.Format(ParseStrings.PurchaseBaseUrl, newRecord.Namespace, newRecord.ID);
						#endregion

						#region check if appears in weekly free games
						if(result.Records.Any(rec => rec.ID == newRecord.ID) || result.NotifyRecords.Any(rec => rec.ID == newRecord.ID)) {
							_logger.LogDebug(ParseStrings.debugFoundInWeeklyGames, game.Title);
							continue;
						}
						#endregion

						#region dev account
						if (game.Seller.ID == ParseStrings.SellerDevAccountID || string.Equals(game.Seller.Name, ParseStrings.SellerDevAccountName, StringComparison.OrdinalIgnoreCase)) {
							newRecord.IsDevAccount = true;
							newRecord.PurchaseUrl = string.Empty;
							_logger.LogDebug(ParseStrings.debugDevAccountFound, game.Title);
						}
						#endregion

						#region deal with promotion time
						if (game.Promotions.PromotionalOffers.Count > 0 && game.Promotions.PromotionalOffers.First().PromotionalOffers.Any(offer => offer.DiscountSetting.DiscountPercentage == 0)) {
							var offer = game.Promotions.PromotionalOffers.First().PromotionalOffers.First(offer => offer.DiscountSetting.DiscountPercentage == 0);
							newRecord.StartTime = offer.StartDate.AddHours(8);
							// handles end time null
							newRecord.EndTime = !offer.EndDate.HasValue ? DateTime.MaxValue : offer.EndDate.Value.AddHours(8);
						} else {
							_logger.LogDebug(ParseStrings.debugGameNoPromotion, game.Title);
							continue;
						}
						#endregion

						result.Records.Add(newRecord);

						#region decide if notify
						if (!oldRecords.Any(record => record.ID == newRecord.ID)) {
							_logger.LogInformation(ParseStrings.infoFoundNewGame, newRecord.Title, newRecord.OfferType);
							result.NotifyRecords.Add(new NotifyRecord(newRecord));
						} else _logger.LogDebug(ParseStrings.debugFoundInOldRecords, game.Title, newRecord.OfferType);
						#endregion
					}
				} else _logger.LogDebug(ParseStrings.debugJsonDataNull);

				_logger.LogDebug($"Done: {ParseStrings.debugParseGraphQL}");
			} catch (Exception) {
				_logger.LogError($"Error: {ParseStrings.debugParseGraphQL}");
				throw;
			} finally {
				Dispose();
			}
		}

		private static string GetProductSlug(Models.EGSJsonData.Element_ game) {
			string gameName = string.Empty;

			// return offerMappings value if free game is add on or type "edition"
			if (game.OfferType == ParseStrings.OfferTypeAddOn || game.OfferType == ParseStrings.OfferTypeEdition) { 
				if (game.OfferMappings != null && game.OfferMappings.Any(map => map.PageType == ParseStrings.UrlProductSlugPageType || map.PageType == ParseStrings.UrlPageSlugPageType))
					gameName = game.OfferMappings.First(map => map.PageType == ParseStrings.UrlProductSlugPageType || map.PageType == ParseStrings.UrlPageSlugPageType).PageSlug;
			} else {
				List<string> gameNameList = [];

				if (!string.IsNullOrEmpty(game.UrlSlug)) gameNameList.Add(game.UrlSlug);
				if (!string.IsNullOrEmpty(game.ProductSlug)) gameNameList.Add(game.ProductSlug);
				if (game.CatalogNs.Mappings != null && game.CatalogNs.Mappings.Any(map => map.PageType == ParseStrings.UrlProductSlugPageType))
					gameNameList.Add(game.CatalogNs.Mappings.First(map => map.PageType == ParseStrings.UrlProductSlugPageType).PageSlug);
				if (game.OfferMappings != null && game.OfferMappings.Any(map => map.PageType == ParseStrings.UrlProductSlugPageType))
					gameNameList.Add(game.OfferMappings.First(map => map.PageType == ParseStrings.UrlProductSlugPageType).PageSlug);
				if (game.CustomAttributes != null && game.CustomAttributes.Any(pair => pair.Key == ParseStrings.CustomAttrProductSlugKey))
					gameNameList.Add(game.CustomAttributes.First(pair => pair.Key == ParseStrings.CustomAttrProductSlugKey).Value);

				gameName = gameNameList.GroupBy(name => name).MaxBy(gp => gp.Count()).Key;
			}

			return gameName;
		}

		private static string GetProductSlug(Models.GraphQL.Element_ game) {
			string gameName = string.Empty;

			// GraphQL data does not have offerType, so always get the most frequent value
			List<string> gameNameList = [];

			if (!string.IsNullOrEmpty(game.UrlSlug)) gameNameList.Add(game.UrlSlug);
			if (!string.IsNullOrEmpty(game.ProductSlug)) gameNameList.Add(game.ProductSlug);
			if (game.CatalogNs.Mappings != null && game.CatalogNs.Mappings.Any(map => map.PageType == ParseStrings.UrlProductSlugPageType))
				gameNameList.Add(game.CatalogNs.Mappings.First(map => map.PageType == ParseStrings.UrlProductSlugPageType).PageSlug);
			if (game.OfferMappings != null && game.OfferMappings.Any(map => map.PageType == ParseStrings.UrlProductSlugPageType))
				gameNameList.Add(game.OfferMappings.First(map => map.PageType == ParseStrings.UrlProductSlugPageType).PageSlug);
			if (game.CustomAttributes != null && game.CustomAttributes.Any(pair => pair.Key == ParseStrings.CustomAttrProductSlugKey))
				gameNameList.Add(game.CustomAttributes.First(pair => pair.Key == ParseStrings.CustomAttrProductSlugKey).Value);

			gameName = gameNameList.GroupBy(name => name).MaxBy(gp => gp.Count()).Key;

			return gameName;
		}

		public void Dispose() { 
			GC.SuppressFinalize(this);
		}
	}
}
