using EGSFreeGamesNotifier.Models.Config;
using EGSFreeGamesNotifier.Models.Record;
using EGSFreeGamesNotifier.Services.Notifier;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EGSFreeGamesNotifier.Services {
	internal class NotifyOP(ILogger<NotifyOP> logger, IOptions<Config> config, TelegramBot tgBot, Bark bark, QQHttp qqHttp, QQWebSocket qqWS, PushPlus pushPlus, DingTalk dingTalk, PushDeer pushDeer, Discord discord, Email email, Meow meow) : IDisposable {
		private readonly ILogger<NotifyOP> _logger = logger;
		private readonly Config config = config.Value;

		#region debug strings
		private readonly string debugNotify = "Notify";
		private readonly string debugEnabledFormat = "Sending notifications to {0}";
		private readonly string debugDisabledFormat = "{0} notify is disabled, skipping";
		private readonly string debugNoNewNotifications = "No new notifications! Skipping";
		#endregion

		internal async Task Notify(List<NotifyRecord> pushList) {
			if (pushList.Count == 0) {
				_logger.LogInformation(debugNoNewNotifications);
				return;
			}

			try {
				_logger.LogDebug(debugNotify);

				var notifyTasks = new List<Task>();

				// Telegram notifications
				if (config.EnableTelegram) {
					_logger.LogInformation(debugEnabledFormat, "Telegram");
					notifyTasks.Add(tgBot.SendMessage(pushList));
				} else _logger.LogInformation(debugDisabledFormat, "Telegram");

				// Bark notifications
				if (config.EnableBark) {
					_logger.LogInformation(debugEnabledFormat, "Bark");
					notifyTasks.Add(bark.SendMessage(pushList));
				} else _logger.LogInformation(debugDisabledFormat, "Bark");

				// QQ Http notifications
				if (config.EnableQQHttp) {
					_logger.LogInformation(debugEnabledFormat, "QQ Http");
					notifyTasks.Add(qqHttp.SendMessage(pushList));
				} else _logger.LogInformation(debugDisabledFormat, "QQ Http");

				// QQ WebSocket notifications
				if (config.EnableQQWebSocket) {
					_logger.LogInformation(debugEnabledFormat, "QQ WebSocket");
					notifyTasks.Add(qqWS.SendMessage(pushList));
				} else _logger.LogInformation(debugDisabledFormat, "QQ WebSocket");

				// PushPlus notifications
				if (config.EnablePushPlus) {
					_logger.LogInformation(debugEnabledFormat, "PushPlus");
					notifyTasks.Add(pushPlus.SendMessage(pushList));
				} else _logger.LogInformation(debugDisabledFormat, "PushPlus");

				// DingTalk notifications
				if (config.EnableDingTalk) {
					_logger.LogInformation(debugEnabledFormat, "DingTalk");
					notifyTasks.Add(dingTalk.SendMessage(pushList));
				} else _logger.LogInformation(debugDisabledFormat, "DingTalk");

				// PushDeer notifications
				if (config.EnablePushDeer) {
					_logger.LogInformation(debugEnabledFormat, "PushDeer");
					notifyTasks.Add(pushDeer.SendMessage(pushList));
				} else _logger.LogInformation(debugDisabledFormat, "PushDeer");

				// Discord notifications
				if (config.EnableDiscord) {
					_logger.LogInformation(debugEnabledFormat, "Discord");
					notifyTasks.Add(discord.SendMessage(pushList));
				} else _logger.LogInformation(debugDisabledFormat, "Discord");

				// Email notifications
				if (config.EnableEmail) {
					_logger.LogInformation(debugEnabledFormat, "Email");
					notifyTasks.Add(email.SendMessage(pushList));
				} else _logger.LogInformation(debugDisabledFormat, "Email");

				// Meow notifications
				if (config.EnableMeow) {
					_logger.LogInformation(debugEnabledFormat, "Meow");
					notifyTasks.Add(meow.SendMessage(pushList));
				} else _logger.LogInformation(debugDisabledFormat, "Meow");

				await Task.WhenAll(notifyTasks);

				_logger.LogDebug($"Done: {debugNotify}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugNotify}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
