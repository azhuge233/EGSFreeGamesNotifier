using System.Text;
using System.Text.RegularExpressions;
using EGSFreeGamesNotifier.Strings;

namespace EGSFreeGamesNotifier.Models.Record {
	public class NotifyRecord: FreeGameRecord {

		private new string StartTime { get; set; }
		private new string EndTime { get; set; }

		public NotifyRecord() { }

		public NotifyRecord(FreeGameRecord record) {
			#region FreeGameRecord
			Name = record.Name;
			Title = record.Title;
			Description = record.Description;
			Url = record.Url;
			PurchaseUrl = record.PurchaseUrl;
			ID = record.ID;
			Namespace = record.Namespace;

			StartTime = $"{record.StartTime} CST";
			EndTime = $"{record.EndTime} CST";

			IsUpcomingPromotion = record.IsUpcomingPromotion;
			IsMysteryGame = record.IsMysteryGame;
			IsDevAccount = record.IsDevAccount;
			#endregion
			}
		}

		private static string RemoveSpecialCharacters(string str) {
			return Regex.Replace(str, NotifyStrings.removeSpecialCharsRegex, string.Empty);
		}

		public string ToTelegramMessage() {
			return string.Format(NotifyFormatStrings.telegramPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Description, Url, PurchaseUrl, $"{StartTime} CST", $"{EndTime} CST", RemoveSpecialCharacters(Title));
		}

		public string ToBarkMessage() {
			return string.Format(NotifyFormatStrings.barkPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, PurchaseUrl, $"{StartTime} CST", $"{EndTime} CST");
		}

		public string ToEmailMessage() {
			return string.Format(NotifyFormatStrings.emailPushHtmlFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, PurchaseUrl, $"{StartTime} CST", $"{EndTime} CST");
		}

		public string ToQQMessage() {
			return string.Format(NotifyFormatStrings.qqPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, PurchaseUrl, $"{StartTime} CST", $"{EndTime} CST");
		}

		public string ToPushPlusMessage() {
			return string.Format(NotifyFormatStrings.pushPlusPushHtmlFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, PurchaseUrl, $"{StartTime} CST", $"{EndTime} CST");
		}

		public string ToDingTalkMessage() {
			return string.Format(NotifyFormatStrings.dingTalkPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, PurchaseUrl, $"{StartTime} CST", $"{EndTime} CST");
		}

		public string ToPushDeerMessage() {
			return string.Format(NotifyFormatStrings.pushDeerPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, PurchaseUrl, $"{StartTime} CST", $"{EndTime} CST");
		}

		public string ToDiscordMessage() {
			return string.Format(NotifyFormatStrings.discordPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, PurchaseUrl, $"{StartTime} CST", $"{EndTime} CST");
		}

		public string ToMeowMessage() {
			return string.Format(NotifyFormatStrings.meowPushFormat[IsUpcomingPromotion ? 1 : 0], Title, Url, PurchaseUrl, $"{StartTime} CST", $"{EndTime} CST");
		}
	}
}
