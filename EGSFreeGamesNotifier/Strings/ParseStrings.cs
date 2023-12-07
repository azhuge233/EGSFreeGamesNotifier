﻿namespace EGSFreeGamesNotifier.Strings {
	internal class ParseStrings {
		internal static readonly string UrlProductSlugPageType = "productHome";
		internal static readonly string CustomAttrProductSlugKey = "com.epicgames.app.productSlug";

		internal static readonly string EGSUrlPre = "https://store.epicgames.com/p/";

		internal static readonly string MisteryGameName = "[]";

		#region debug strings
		internal static readonly string debugParse = "Parse";
		internal static readonly string debugParseWithUrl = "Parsing: {0}";

		internal static readonly string debugJsonDataNull = "Json Data Null";

		internal static readonly string debugGameNullPromotion = "Null promotion for game {0}";
		internal static readonly string debugGameNoPromotion = "No promotion for game {0}";

		internal static readonly string infoUpcomingGameIsLive = "Upcoming game is live: {0}";

		internal static readonly string infoFoundNewGame = "Found new free game: {0}";
		internal static readonly string debugFoundInOldRecords = "Found {0} in old records, stop adding to push list";

		internal static readonly string debugMisteryGameFound = "Found Mistery Game: {0}";
		#endregion
	}
}
