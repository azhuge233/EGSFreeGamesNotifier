using EGSFreeGamesNotifier.Models.Config;
using EGSFreeGamesNotifier.Models.Record;
using EGSFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using SteamDBFreeGames.Models.PostContent;
using System.Text.Json;
using System.Text;

namespace EGSFreeGamesNotifier.Services.Notifier {
	internal class Meow: INotifiable {
		private readonly ILogger<Meow> _logger;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to Meow";
		#endregion

		public Meow(ILogger<Meow> logger) {
			_logger = logger;
		}

		public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records) {
			try {
				_logger.LogDebug(debugSendMessage);

				var url = string.Format(NotifyFormatStrings.meowUrlFormat, config.MeowAddress, config.MeowNickname);

				var content = new MeowPostContent() {
					Title = NotifyFormatStrings.meowUrlTitle
				};

				var client = new HttpClient();

				foreach (var record in records) {
					content.Message = record.ToMeowMessage();
					content.Url = record.Url;

					var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
					var resp = await client.PostAsync(url, data);

					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
					await Task.Delay(3000); // Meow has a rate limit of 1 message per 3 seconds
				}

				_logger.LogDebug($"Done: {debugSendMessage}");
			} catch (Exception) {
				_logger.LogDebug($"Done: {debugSendMessage}");
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
