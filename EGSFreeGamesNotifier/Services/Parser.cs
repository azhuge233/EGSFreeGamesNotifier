using Microsoft.Extensions.Logging;
using EGSFreeGamesNotifier.Models;
using EGSFreeGamesNotifier.Models.EGSJsonData;
using EGSFreeGamesNotifier.Models.Record;
using EGSFreeGamesNotifier.Strings;
using System.Text.Json;

namespace EGSFreeGamesNotifier.Services {
	internal class Parser: IDisposable {
		private readonly ILogger<Parser> _logger;

		public Parser(ILogger<Parser> logger) {
			_logger = logger;
		}

		internal ParseResult Parse(string source, List<FreeGameRecord> oldRecords) {
			try {
				_logger.LogDebug(ParseStrings.debugParse);
				var result = new ParseResult();

				var jsonData = JsonSerializer.Deserialize<JsonData>(source);

				if (jsonData != null) {
					foreach (var game in jsonData.Data.Catalog.SearchStore.Elements) {
						if (game.Promotions == null) {
							_logger.LogDebug(ParseStrings.debugGameNullPromotion, game.Title);
							continue;
						}

						var newRecord = new FreeGameRecord() { 
							Title = game.Title,
							Name = GetProductSlug(game),
							Description = game.Description,
							ID = game.ID
						};

						newRecord.Url = $"{ParseStrings.EGSUrlPres[ParseStrings.OfferTypesToUrlPrefix.GetValueOrDefault(game.OfferType, 0)]}{newRecord.Name}";

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

						result.Records.Add(newRecord);

						if (!oldRecords.Any(record => record.ID == newRecord.ID)) {
							_logger.LogInformation(ParseStrings.infoFoundNewGame, newRecord.Title, game.OfferType);
							result.NotifyRecords.Add(newRecord);
						} else if (oldRecords.First(record => record.ID == newRecord.ID).IsUpcomingPromotion != newRecord.IsUpcomingPromotion) {
							_logger.LogInformation(ParseStrings.infoUpcomingGameIsLive, newRecord.Title, game.OfferType);
							result.NotifyRecords.Add(newRecord);
						} else _logger.LogDebug(ParseStrings.debugFoundInOldRecords, game.Title, game.OfferType);
					}
				} else _logger.LogDebug(ParseStrings.debugJsonDataNull);

				if(result.NotifyRecords.Count > 0)
					result.NotifyRecords = [.. result.NotifyRecords.OrderBy(record => record.IsUpcomingPromotion)];

				_logger.LogDebug($"Done: {ParseStrings.debugParse}");
				return result;
			} catch (Exception) {
				_logger.LogError($"Error: {ParseStrings.debugParse}");
				throw;
			} finally {
				Dispose();
			}
		}

		private string GetProductSlug(Element_ game) {
			string gameName = string.Empty;

			if (game.OfferType == ParseStrings.OfferTypeAddOn) { // return offerMappings value if free game is add on
				if (game.OfferMappings != null && game.OfferMappings.Any(map => map.PageType == ParseStrings.UrlProductSlugPageType || map.PageType == ParseStrings.UrlPageSlugPageType))
					gameName = game.OfferMappings.First(map => map.PageType == ParseStrings.UrlProductSlugPageType || map.PageType == ParseStrings.UrlPageSlugPageType).PageSlug;
			} else {
				if (gameName == ParseStrings.MisteryGameName) {
					gameName = game.UrlSlug;
					_logger.LogDebug(ParseStrings.debugMisteryGameFound, gameName);
				} else if (!string.IsNullOrEmpty(game.UrlSlug)) gameName = game.UrlSlug;
				else if (!string.IsNullOrEmpty(game.ProductSlug)) gameName = game.ProductSlug;
				else if (game.CatalogNs.Mappings != null && game.CatalogNs.Mappings.Any(map => map.PageType == ParseStrings.UrlProductSlugPageType))
					gameName = game.CatalogNs.Mappings.First(map => map.PageType == ParseStrings.UrlProductSlugPageType).PageSlug;
				else if (game.OfferMappings != null && game.OfferMappings.Any(map => map.PageType == ParseStrings.UrlProductSlugPageType))
					gameName = game.OfferMappings.First(map => map.PageType == ParseStrings.UrlProductSlugPageType).PageSlug;
				else if (game.CustomAttributes != null && game.CustomAttributes.Any(pair => pair.Key == ParseStrings.CustomAttrProductSlugKey))
					gameName = game.CustomAttributes.First(pair => pair.Key == ParseStrings.CustomAttrProductSlugKey).Value;
			}

			return gameName;
		}

		public void Dispose() { 
			GC.SuppressFinalize(this);
		}
	}
}
