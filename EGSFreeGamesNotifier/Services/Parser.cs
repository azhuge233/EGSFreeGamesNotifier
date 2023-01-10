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

						newRecord.Url = $"{ParseStrings.EGSUrlPre}{newRecord.Name}";

						if (game.Promotions.PromotionalOffers.Count > 0) {
							var offer = game.Promotions.PromotionalOffers.First().PromotionalOffers.First(offer => offer.DiscountSetting.DiscountPercentage == 0);
							newRecord.StartTime = offer.StartDate.AddHours(8);
							newRecord.EndTime = offer.EndDate.AddHours(8);
						} else if (game.Promotions.UpcomingPromotionalOffers.Count > 0) {
							newRecord.IsUpcomingPromotion = true;
							var offer = game.Promotions.UpcomingPromotionalOffers.First().PromotionalOffers.First(offer => offer.DiscountSetting.DiscountPercentage == 0);
							newRecord.StartTime = offer.StartDate.AddHours(8);
							newRecord.EndTime = offer.EndDate.AddHours(8);
						} else {
							_logger.LogDebug(ParseStrings.debugGameNoPromotion, game.Title);
							continue;
						}

						result.Records.Add(newRecord);

						if (!oldRecords.Any(record => record.ID == newRecord.ID)) {
							_logger.LogInformation(ParseStrings.infoFoundNewGame, newRecord.Title);
							result.NotifyRecords.Add(newRecord);
						} else if (oldRecords.First(record => record.ID == newRecord.ID).IsUpcomingPromotion != newRecord.IsUpcomingPromotion) {
							_logger.LogInformation(ParseStrings.infoUpcomingGameIsLive, newRecord.Title);
							result.NotifyRecords.Add(newRecord);
						} else _logger.LogDebug(ParseStrings.debugFoundInOldRecords, game.Title);
					}
				} else _logger.LogDebug(ParseStrings.debugJsonDataNull);

				if(result.NotifyRecords.Count > 0)
					result.NotifyRecords = result.NotifyRecords.OrderBy(record => record.IsUpcomingPromotion).ToList();

				_logger.LogDebug($"Done: {ParseStrings.debugParse}");
				return result;
			} catch (Exception) {
				_logger.LogError($"Error: {ParseStrings.debugParse}");
				throw;
			} finally {
				Dispose();
			}
		}

		private static string GetProductSlug(Element_ game) {
			if(!string.IsNullOrEmpty(game.ProductSlug)) return game.ProductSlug;
			if (game.CatalogNs.Mappings.Any(map => map.PageType == ParseStrings.UrlProductSlugPageType))
				return game.CatalogNs.Mappings.First(map => map.PageType == ParseStrings.UrlProductSlugPageType).PageSlug;
			if (game.OfferMappings.Any(map => map.PageType == ParseStrings.UrlProductSlugPageType))
				return game.OfferMappings.First(map => map.PageType == ParseStrings.UrlProductSlugPageType).PageSlug;
			if (game.CustomAttributes.Any(pair => pair.Key == ParseStrings.CustomAttrProductSlugKey))
				return game.CustomAttributes.First(pair => pair.Key == ParseStrings.CustomAttrProductSlugKey).Value;
			return string.Empty;
		}

		public void Dispose() { 
			GC.SuppressFinalize(this);
		}
	}
}
