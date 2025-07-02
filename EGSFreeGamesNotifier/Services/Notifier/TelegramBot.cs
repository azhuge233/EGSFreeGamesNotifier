using EGSFreeGamesNotifier.Models.Config;
using EGSFreeGamesNotifier.Models.Record;
using EGSFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace EGSFreeGamesNotifier.Services.Notifier {
	internal class TelegramBot(ILogger<TelegramBot> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<TelegramBot> _logger = logger;
		private readonly Config config = config.Value;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to Telegram";
		#endregion

		public async Task SendMessage(List<NotifyRecord> records) {
			var BotClient = new TelegramBotClient(token: config.TelegramToken);

			try {
				foreach (var record in records) {
					_logger.LogDebug($"{debugSendMessage} : {record.Name}");
					await BotClient.SendMessage(
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
