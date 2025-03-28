namespace EGSFreeGamesNotifier.Strings {
	internal class NotifyFormatStrings {
		#region record strings
		internal static readonly List<string> telegramPushFormat = [
			// All
			"<b>New EGS Free Game</b>\n\n" +
			"<b>{0}</b>\n\n" + // Title
			"<i>{1}</i>\n\n" + // Description
			"Store Link: <a href=\"{2}\" >{0}</a>\n" + // Url
			"Claim Link: <a href=\"{3}\" >Purchase</a>\n\n" + // Purchase Url
			"Start Time: {4}\n" + // StartTime
			"End Time: {5}\n\n" + // EndTime
			"#EGS #{6}",

			// Dev Account
			"<b>New EGS Free Game</b>\n\n" +
			"<b>{0}</b>\n\n" + // Title
			"<i>{1}</i>\n\n" + // Description
			"Store Link: <a href=\"{2}\" >{0}</a>\n\n" + // Url
			"Start Time: {3}\n" + // StartTime
			"End Time: {4}\n\n" + // EndTime
			"#EGS #{5}",

			// Mystery Game
			"<b>New EGS Free Game</b>\n\n" +
			"<b>{0}</b>\n\n" + // Title
			"<i>{1}</i>\n\n" + // Description
			"Start Time: {2}\n" + // StartTime
			"End Time: {3}\n\n" + // EndTime
			"#EGS #{4}",

			// All
			"<b>Upcoming EGS Free Game</b>\n\n" +
			"<b>{0}</b>\n\n" + // Title
			"<i>{1}</i>\n\n" + // Description
			"Store Link: <a href=\"{2}\" >{0}</a>\n" + // Url
			"Claim Link: <a href=\"{3}\" >Purchase</a>\n\n" + // Purchase Url
			"Start Time: {4}\n" + // StartTime
			"End Time: {5}\n\n" + // EndTime
			"#EGS #Upcoming #{6}",

			// Dev Account
			"<b>Upcoming EGS Free Game</b>\n\n" +
			"<b>{0}</b>\n\n" + // Title
			"<i>{1}</i>\n\n" + // Description
			"Store Link: <a href=\"{2}\" >{0}</a>\n\n" + // Url
			"Start Time: {3}\n" + // StartTime
			"End Time: {4}\n\n" + // EndTime
			"#EGS #Upcoming #{5}",

			// Mystery Game
			"<b>Upcoming EGS Free Game</b>\n\n" +
			"<b>{0}</b>\n\n" + // Title
			"<i>{1}</i>\n\n" + // Description
			"Start Time: {2}\n" + // StartTime
			"End Time: {3}\n\n" + // EndTime
			"#EGS #Upcoming #{4}"
		];

		internal static readonly List<string> barkPushFormat = [
			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n" +
			"Claim Link: {2}\n\n" +
			"Start Time: {3}\n" +
			"End Time: {4}\n",

			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n\n" +
			"Start Time: {2}\n" +
			"End Time: {3}\n",

			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Start Time: {1}\n" +
			"End Time: {2}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n" +
			"Claim Link: {2}\n\n" +
			"Start Time: {3}\n" +
			"End Time: {4}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n\n" +
			"Start Time: {2}\n" +
			"End Time: {3}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Start Time: {1}\n" +
			"End Time: {2}\n"
		];

		internal static readonly List<string> emailPushHtmlFormat = [
			"<p><b>{0}</b><br>" +
			"Store Link: <a href=\"{1}\" >{0}</a><br>" +
			"Claim Link: <a href=\"{2}\" >Purchase</a><br>" +
			"Start Time: {3}<br>" +
			"End Time: {4}<br>",

			"<p><b>{0}</b><br>" +
			"Store Link: <a href=\"{1}\" >{0}</a><br>" +
			"Start Time: {2}<br>" +
			"End Time: {3}<br>",

			"<p><b>{0}</b><br>" +
			"Start Time: {1}<br>" +
			"End Time: {2}<br>",

			"<b><i>Upcoming</i></b>" +
			"<p><b>{0}</b><br>" +
			"Store Link: <a href=\"{1}\" >{0}</a><br>" +
			"Claim Link: <a href=\"{2}\" >Purchase</a><br>" +
			"Start Time: {3}<br>" +
			"End Time: {4}<br>",

			"<b><i>Upcoming</i></b>" +
			"<p><b>{0}</b><br>" +
			"Store Link: <a href=\"{1}\" >{0}</a><br>" +
			"Start Time: {2}<br>" +
			"End Time: {3}<br>",

			"<b><i>Upcoming</i></b>" +
			"<p><b>{0}</b><br>" +
			"Start Time: {1}<br>" +
			"End Time: {2}<br>"
		];

		internal static readonly List<string> qqPushFormat = [
			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n" +
			"Claim Link: {2}\n\n" +
			"Start Time: {3}\n" +
			"End Time: {4}\n",

			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n\n" +
			"Start Time: {2}\n" +
			"End Time: {3}\n",

			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Start Time: {1}\n" +
			"End Time: {2}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n" +
			"Claim Link: {2}\n\n" +
			"Start Time: {3}\n" +
			"End Time: {4}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n\n" +
			"Start Time: {2}\n" +
			"End Time: {3}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Start Time: {1}\n" +
			"End Time: {2}\n"
		];

		internal static readonly List<string> pushPlusPushHtmlFormat = [
			"<p><b>{0}</b><br>" +
			"Store Link: <a href=\"{1}\" >{0}</a><br>" +
			"Claim Link: <a href=\"{2}\" >Purchase</a><br>" +
			"Start Time: {3}<br>" +
			"End Time: {4}<br>",

			"<p><b>{0}</b><br>" +
			"Store Link: <a href=\"{1}\" >{0}</a><br>" +
			"Start Time: {2}<br>" +
			"End Time: {3}<br>",

			"<p><b>{0}</b><br>" +
			"Start Time: {1}<br>" +
			"End Time: {2}<br>",

			"<b><i>Upcoming</i></b>" +
			"<p><b>{0}</b><br>" +
			"Store Link: <a href=\"{1}\" >{0}</a><br>" +
			"Claim Link: <a href=\"{2}\" >Purchase</a><br>" +
			"Start Time: {3}<br>" +
			"End Time: {4}<br>",

			"<b><i>Upcoming</i></b>" +
			"<p><b>{0}</b><br>" +
			"Store Link: <a href=\"{1}\" >{0}</a><br>" +
			"Start Time: {2}<br>" +
			"End Time: {3}<br>",

			"<b><i>Upcoming</i></b>" +
			"<p><b>{0}</b><br>" +
			"Start Time: {1}<br>" +
			"End Time: {2}<br>"
		];

		internal static readonly List<string> dingTalkPushFormat = [
			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n" +
			"Claim Link: {2}\n\n" +
			"Start Time: {3}\n" +
			"End Time: {4}\n",

			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n\n" +
			"Start Time: {2}\n" +
			"End Time: {3}\n",

			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Start Time: {1}\n" +
			"End Time: {2}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n" +
			"Claim Link: {2}\n\n" +
			"Start Time: {3}\n" +
			"End Time: {4}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n\n" +
			"Start Time: {2}\n" +
			"End Time: {3}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Start Time: {1}\n" +
			"End Time: {2}\n"
		];

		internal static readonly List<string> pushDeerPushFormat = [
			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n\n" +
			"Claim Link: {2}\n\n" +
			"Start Time: {3}\n" +
			"End Time: {4}\n\n",

			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n\n" +
			"Start Time: {2}\n" +
			"End Time: {3}\n\n",

			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Start Time: {1}\n" +
			"End Time: {2}\n\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n\n" +
			"Claim Link: {2}\n\n" +
			"Start Time: {3}\n" +
			"End Time: {4}\n\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n\n" +
			"Start Time: {2}\n" +
			"End Time: {3}\n\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Start Time: {1}\n" +
			"End Time: {2}\n\n"
		];

		internal static readonly List<string> discordPushFormat = [
			"\n\n" +
			"Store Link: {0}\n" +
			"Claim Link: {1}\n\n" +
			"Start Time: {2}\n" +
			"End Time: {3}\n",

			"Store Link: {0}\n\n" +
			"Start Time: {1}\n" +
			"End Time: {2}\n",

			"Start Time: {0}\n" +
			"End Time: {1}\n",

			"Upcoming\n\n" +
			"Store Link: {0}\n" +
			"Claim Link: {1}\n\n" +
			"Start Time: {2}\n" +
			"End Time: {3}\n",

			"Upcoming\n\n" +
			"Store Link: {0}\n\n" +
			"Start Time: {1}\n" +
			"End Time: {2}\n",

			"Upcoming\n\n" +
			"Start Time: {0}\n" +
			"End Time: {1}\n"
		];

		internal static readonly List<string> meowPushFormat = [
			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n" +
			"Claim Link: {2}\n\n" +
			"Start Time: {3}\n" +
			"End Time: {4}\n",

			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n\n" +
			"Start Time: {2}\n" +
			"End Time: {3}\n",

			"New EGS Free Game\n\n" +
			"{0}\n\n" +
			"Start Time: {1}\n" +
			"End Time: {2}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n" +
			"Claim Link: {2}\n\n" +
			"Start Time: {3}\n" +
			"End Time: {4}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Store Link: {1}\n\n" +
			"Start Time: {2}\n" +
			"End Time: {3}\n",

			"Upcoming EGS Free Game\n\n" +
			"{0}\n\n" +
			"Start Time: {1}\n" +
			"End Time: {2}\n"
		];
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

		internal static readonly string qqHttpUrlFormat = "http://{0}:{1}/send_private_msg?access_token={2}";
		internal static readonly string qqWebSocketUrlFormat = "ws://{0}:{1}/?access_token={2}";
		internal static readonly string qqWebSocketSendAction = "send_private_msg";

		internal static readonly string pushPlusTitleFormat = "{0} new free game(s) - EGSFreeGamesNotifier";
		internal static readonly string pushPlusBodyFormat = "<br>{0}";
		internal static readonly string pushPlusGetUrlFormat = "http://www.pushplus.plus/send?token={0}&template=html&title={1}&content=";
		internal static readonly string pushPlusPostUrl = "http://www.pushplus.plus/send";

		internal static readonly string dingTalkUrlFormat = "https://oapi.dingtalk.com/robot/send?access_token={0}";

		internal static readonly string pushDeerUrlFormat = "https://api2.pushdeer.com/message/push?pushkey={0}&&text={1}";

		internal static readonly string meowUrlFormat = "{0}/{1}";
		internal static readonly string meowUrlTitle = "EGSFreeGamesNotifier";
		#endregion

		internal static readonly string projectLink = "\n\nFrom https://github.com/azhuge233/EGSFreeGamesNotifier";
		internal static readonly string projectLinkHTML = "<br><br>From <a href=\"https://github.com/azhuge233/EGSFreeGamesNotifier\">EGSFreeGamesNotifier</a>";
	}
}
