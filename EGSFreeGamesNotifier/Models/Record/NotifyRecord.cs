using System.Text.RegularExpressions;
using EGSFreeGamesNotifier.Strings;

namespace EGSFreeGamesNotifier.Models.Record {
	public class NotifyRecord: FreeGameRecord {
		private int FormatIndex { get; set; }

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
			OfferType = record.OfferType;

			StartTime = $"{record.StartTime} CST";
			EndTime = $"{record.EndTime} CST";

			IsUpcomingPromotion = record.IsUpcomingPromotion;
			IsMysteryGame = record.IsMysteryGame;
			IsDevAccount = record.IsDevAccount;
			#endregion

			#region decide format index
			if (IsUpcomingPromotion) {
				FormatIndex = 3;
				if (IsDevAccount) FormatIndex = 4;
				if (IsMysteryGame) FormatIndex = 5;
			} else {
				FormatIndex = 0;
				if (IsDevAccount) FormatIndex = 1;
				if (IsMysteryGame) FormatIndex = 2;
			}
			#endregion
		}

		public string ToTelegramMessage() {
			string formatString = NotifyFormatStrings.telegramPushFormat[FormatIndex];
			string telegramTag = RemoveSpecialCharacters(Title);

			if (IsMysteryGame) return string.Format(formatString, Title, Description, StartTime, EndTime, telegramTag);
			else if (IsDevAccount) return string.Format(formatString, Title, Description, Url, StartTime, EndTime, telegramTag);
			else return string.Format(formatString, Title, Description, Url, PurchaseUrl, StartTime, EndTime, telegramTag);
		}

		public string ToBarkMessage() {
			return GeneralFormat(NotifyFormatStrings.barkPushFormat[FormatIndex]);
		}

		public string ToEmailMessage() {
			return GeneralFormat(NotifyFormatStrings.emailPushHtmlFormat[FormatIndex]);
		}

		public string ToQQMessage() {
			return GeneralFormat(NotifyFormatStrings.qqPushFormat[FormatIndex]);
		}

		public string ToPushPlusMessage() {
			return GeneralFormat(NotifyFormatStrings.pushPlusPushHtmlFormat[FormatIndex]);
		}

		public string ToDingTalkMessage() {
			return GeneralFormat(NotifyFormatStrings.dingTalkPushFormat[FormatIndex]);
		}

		public string ToPushDeerMessage() {
			return GeneralFormat(NotifyFormatStrings.pushDeerPushFormat[FormatIndex]);
		}

		public string ToDiscordMessage() {
			string formatString = NotifyFormatStrings.discordPushFormat[FormatIndex];

			if (IsMysteryGame) return string.Format(formatString, StartTime, EndTime);
			else if (IsDevAccount) return string.Format(formatString, Url, StartTime, EndTime);
			else return string.Format(formatString, Url, PurchaseUrl, StartTime, EndTime);
		}

		public string ToMeowMessage() {
			return GeneralFormat(NotifyFormatStrings.meowPushFormat[FormatIndex]);
		}

		private string GeneralFormat(string formatString) {
			if (IsMysteryGame) return string.Format(formatString, Title, StartTime, EndTime);
			else if (IsDevAccount) return string.Format(formatString, Title, Url, StartTime, EndTime);
			else return string.Format(formatString, Title, Url, PurchaseUrl, StartTime, EndTime);
		}

		private static string RemoveSpecialCharacters(string str) {
			return Regex.Replace(str, NotifyStrings.removeSpecialCharsRegex, string.Empty);
		}
	}
}
