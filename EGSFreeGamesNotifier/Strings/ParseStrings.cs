namespace EGSFreeGamesNotifier.Strings {
	internal class ParseStrings {
		internal static readonly string UrlProductSlugPageType = "productHome";
		internal static readonly string UrlPageSlugPageType = "offer";
		internal static readonly string CustomAttrProductSlugKey = "com.epicgames.app.productSlug";

		#region Free game offerTypes name
		internal static readonly string OfferTypeBaseGame = "BASE_GAME";
		internal static readonly string OfferTypeBundle = "BUNDLE";
		internal static readonly string OfferTypeAddOn = "ADD_ON";
		internal static readonly string OfferTypeEdition = "EDITION";
		internal static readonly string OfferTypeOthers = "OTHERS";
		#endregion

		internal static readonly string SellerDevAccountID = "o-ufmrk5furrrxgsp5tdngefzt5rxdcn";
		internal static readonly string SellerDevAccountName = "Epic Dev Test Account";

		internal static readonly string MysteryGameName = "Mystery Game";

		#region select url prefix by offerType
		internal static readonly string[] EGSUrlPres = [
			"https://store.epicgames.com/",
			"https://store.epicgames.com/p/",
			"https://store.epicgames.com/bundles/"
		];
		internal static readonly Dictionary<string, int> OfferTypesToUrlPrefix = new() { 
			{ OfferTypeBaseGame, 1 }, 
			{ OfferTypeBundle, 2 }, 
			{ OfferTypeAddOn, 1 },
			{ OfferTypeEdition, 1 },
			{ OfferTypeOthers, 1}
		};
		#endregion

		internal static readonly string PurchaseBaseUrl = @"https://store.epicgames.com/purchase?offers=1-{0}-{1}";

		#region debug strings
		internal static readonly string debugParse = "Parse";
		internal static readonly string debugParseWithUrl = "Parsing: {0}";

		internal static readonly string debugJsonDataNull = "Json Data Null";

		internal static readonly string debugGameNullPromotion = "Null promotion for game {0}";
		internal static readonly string debugGameNoPromotion = "No promotion for game {0}";

		internal static readonly string infoUpcomingGameIsLive = "Upcoming game is live: {0} | offerType: {1}";

		internal static readonly string infoFoundNewGame = "Found new free game: {0} | offerType: {1}";
		internal static readonly string debugFoundInOldRecords = "Found {0} in old records, stop adding to push list | offerType: {1}";

		internal static readonly string debugMysteryGameFound = "Found mystery game: {0}";
		internal static readonly string debugDevAccountFound = "Found dev account game: {0}";
		#endregion
	}
}
