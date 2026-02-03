namespace EGSFreeGamesNotifier.Strings {
	internal class ScrapeStrings {
		internal static readonly string EGSUrl = "https://store-site-backend-static.ak.epicgames.com/freeGamesPromotions";
		internal static readonly string EGSGraphQLUrl = "https://store.epicgames.com/graphql?";

		internal static readonly string EGSGraphQLQueryParam = @"operationName=searchStoreQuery&variables={""allowCountries"":""US"",""category"":""games/edition/base"",""comingSoon"":false,""count"":40,""country"":""US"",""freeGame"":true,""locale"":""en-US"",""sortBy"":""releaseDate"",""sortDir"":""DESC"",""start"":0,""onSale"":true,""withPrice"":false,""withPromotions"":true}&extensions={""persistedQuery"":{""version"":1,""sha256Hash"":""29d49ab31d438cd90be2d554d2d54704951e4223a8fcd290fcf68308841a1979""}}";

		#region debug strings
		internal static readonly string debugGetSource = "Get source";
		internal static readonly string debugGetSourceWithUrl = "Getting source: {0}";
		#endregion
	}
}
