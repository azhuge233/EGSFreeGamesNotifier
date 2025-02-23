using System.Text;
using System.Text.RegularExpressions;
using EGSFreeGamesNotifier.Strings;

namespace EGSFreeGamesNotifier.Models.Record {
	public class NotifyRecord: FreeGameRecord {
		public NotifyRecord() { }

		public NotifyRecord(FreeGameRecord record): base(record) {
			if (Title == NotifyStrings.MysteryGameName && Title == Description) Url = NotifyStrings.EGSUrlPre;
		}

		private static string RemoveSpecialCharacters(string str) {
			return Regex.Replace(str, NotifyStrings.removeSpecialCharsRegex, string.Empty);
		}

		public string ToTelegramMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatStrings.telegramPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Description, Url, $"{StartTime} CST", $"{EndTime} CST", RemoveSpecialCharacters(Title)).ToString();
		}

		public string ToBarkMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatStrings.barkPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, $"{StartTime} CST", $"{EndTime} CST").ToString();
		}

		public string ToEmailMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatStrings.emailPushHtmlFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, $"{StartTime} CST", $"{EndTime} CST").ToString();
		}

		public string ToQQMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatStrings.qqPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, $"{StartTime} CST", $"{EndTime} CST").ToString();
		}

		public string ToPushPlusMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatStrings.pushPlusPushHtmlFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, $"{StartTime} CST", $"{EndTime} CST").ToString();
		}

		public string ToDingTalkMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatStrings.dingTalkPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, $"{StartTime} CST", $"{EndTime} CST").ToString();
		}

		public string ToPushDeerMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatStrings.pushDeerPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, $"{StartTime} CST", $"{EndTime} CST").ToString();
		}

		public string ToDiscordMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatStrings.discordPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, $"{StartTime} CST", $"{EndTime} CST").ToString();
		}

		public string ToMeowMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatStrings.meowPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, $"{StartTime} CST", $"{EndTime} CST").ToString();
		}
	}
}
