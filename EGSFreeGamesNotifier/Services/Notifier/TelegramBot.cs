using Microsoft.Extensions.Logging;
using EGSFreeGamesNotifier.Models.Config;
using EGSFreeGamesNotifier.Models.Record;
using EGSFreeGamesNotifier.Strings;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;

namespace EGSFreeGamesNotifier.Services.Notifier {
	internal class TelegramBot: INotifiable {
		private readonly ILogger<TelegramBot> _logger;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to Telegram";
		#endregion

		public TelegramBot(ILogger<TelegramBot> logger) {
			_logger = logger;
		}

		public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records) {
			var BotClient = new TelegramBotClient(token: config.TelegramToken);

			try {
				foreach (var record in records) {
					_logger.LogDebug($"{debugSendMessage} : {record.Name}");
					await BotClient.SendTextMessageAsync(
						chatId: config.TelegramChatID,
						text: $"{record.ToTelegramMessage()}{NotifyFormatStrings.projectLinkHTML.Replace("<br>", "\n")}",
						parseMode: ParseMode.Html
					);
				}

				_logger.LogDebug($"Done: {debugSendMessage}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugSendMessage}");
				throw;
			} finally {
				Dispose();
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
