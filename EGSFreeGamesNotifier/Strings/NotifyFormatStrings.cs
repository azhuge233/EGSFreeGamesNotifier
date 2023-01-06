namespace EGSFreeGamesNotifier.Strings {
	internal class NotifyFormatStrings {
		#region record strings
		internal static readonly List<string> telegramPushFormat = new() {
			"<b>New EGS Free Game</b>\n\n" +
			"<b>{0}</b>\n\n" + // Title
			"<i>{1}</i>\n\n" + // Description
			"EGS 链接: <a href=\"{2}\" >{0}</a>\n" + // Url
			"开始时间: {3}\n" + // StartTime
			"结束时间: {4}\n\n" + // EndTime
			"#EGS #{5}",

			"<b>Upcoming EGS Free Game</b>\n\n" +
			"<b>{0}</b>\n\n" + // Title
			"<i>{1}</i>\n\n" + // Description
			"EGS 链接: <a href=\"{2}\" >{0}</a>\n" + // Url
			"开始时间: {3}\n" + // StartTime
			"结束时间: {4}\n\n" + // EndTime
			"#EGS #Upcoming #{5}",
		};

		internal static readonly List<string> barkPushFormat = new() {
			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"EGS 链接: {1}\n" +
			"开始时间: {2}\n" +
			"结束时间: {3}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"EGS 链接: {1}\n" +
			"开始时间: {2}\n" +
			"结束时间: {3}\n"
		};

		internal static readonly List<string> emailPushHtmlFormat = new() {
			"<p><b>{0}</b><br>" +
			"EGS 链接: <a href=\"{1}\" > {0}</a><br><br>" +
			"开始时间: {2}<br>" +
			"结束时间: {3}<br>",

			"<b><i>Upcoming</i></b>" +
			"<p><b>{0}</b><br>" +
			"EGS 链接: <a href=\"{1}\" > {0}</a><br><br>" +
			"开始时间: {2}<br>" +
			"结束时间: {3}<br>"
		};

		internal static readonly List<string> qqPushFormat = new() {
			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"EGS 链接: {1}\n" +
			"开始时间: {2}\n" +
			"结束时间: {3}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"EGS 链接: {1}\n" +
			"开始时间: {2}\n" +
			"结束时间: {3}\n"
		};

		internal static readonly List<string> pushPlusPushHtmlFormat = new() {
			"<p><b>{0}</b><br>" +
			"EGS 链接: <a href=\"{1}\" > {0}</a><br><br>" +
			"开始时间: {2}<br>" +
			"结束时间: {3}<br>",

			"<b><i>Upcoming</i></b>" +
			"<p><b>{0}</b><br>" +
			"EGS 链接: <a href=\"{1}\" > {0}</a><br><br>" +
			"开始时间: {2}<br>" +
			"结束时间: {3}<br>"
		};

		internal static readonly List<string> dingTalkPushFormat = new() {
			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"EGS 链接: {1}\n" +
			"开始时间: {2}\n" +
			"结束时间: {3}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"EGS 链接: {1}\n" +
			"开始时间: {2}\n" +
			"结束时间: {3}\n"
		};

		internal static readonly List<string> pushDeerPushFormat = new() {
			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"EGS 链接: {1}\n\n" +
			"开始时间: {2}\n\n" +
			"结束时间: {3}\n\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"EGS 链接: {1}\n\n" +
			"开始时间: {2}\n\n" +
			"结束时间: {3}\n\n"
		};

		internal static readonly List<string> discordPushFormat = new() {
			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"EGS 链接: {1}\n" +
			"开始时间: {2}\n" +
			"结束时间: {3}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"EGS 链接: {1}\n" +
			"开始时间: {2}\n" +
			"结束时间: {3}\n"
		};
		#endregion

		#region url, title format string
		internal static readonly string barkUrlFormat = "{0}/{1}/";
		internal static readonly string barkUrlTitle = "EGSFreeGamesNotifier/";
		internal static readonly string barkUrlArgs =
			"?group=EGSfreegames" +
			"&copy={0}" +
			"&isArchive=1" +
			"&sound=calypso";

		internal static readonly string emailTitleFormat = "{0} new free game(s) - EGSFreeGamesNotifier";
		internal static readonly string emailBodyFormat = "<br>{0}";

		internal static readonly string qqUrlFormat = "http://{0}:{1}/send_private_msg?user_id={2}&message=";

		internal static readonly string pushPlusTitleFormat = "{0} new free game(s) - EGSFreeGamesNotifier";
		internal static readonly string pushPlusBodyFormat = "<br>{0}";
		internal static readonly string pushPlusGetUrlFormat = "http://www.pushplus.plus/send?token={0}&template=html&title={1}&content=";
		internal static readonly string pushPlusPostUrl = "http://www.pushplus.plus/send";

		internal static readonly string dingTalkUrlFormat = "https://oapi.dingtalk.com/robot/send?access_token={0}";

		internal static readonly string pushDeerUrlFormat = "https://api2.pushdeer.com/message/push?pushkey={0}&&text={1}";
		#endregion

		internal static readonly string projectLink = "\n\nFrom https://github.com/azhuge233/EGSFreeGamesNotifier";
		internal static readonly string projectLinkHTML = "<br><br>From <a href=\"https://github.com/azhuge233/EGSFreeGamesNotifier\">EGSFreeGamesNotifier</a>";
	}
}
