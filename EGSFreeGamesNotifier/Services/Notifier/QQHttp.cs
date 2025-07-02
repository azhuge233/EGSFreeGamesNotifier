using EGSFreeGamesNotifier.Models.Config;
using EGSFreeGamesNotifier.Models.PostContent;
using EGSFreeGamesNotifier.Models.Record;
using EGSFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace EGSFreeGamesNotifier.Services.Notifier {
	internal class QQHttp(ILogger<QQHttp> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<QQHttp> _logger = logger;
		private readonly Config config = config.Value;

		#region debug strings
		private readonly string debugSendMessage = "Send notifications to QQ Http";
		#endregion

		public async Task SendMessage(List<NotifyRecord> records) {
			try {
				_logger.LogDebug(debugSendMessage);

				string url = string.Format(NotifyFormatStrings.qqHttpUrlFormat, config.QQHttpAddress, config.QQHttpPort, config.QQHttpToken);

				var client = new HttpClient();

				var content = new QQHttpPostContent {
					UserID = config.ToQQID
				};

				var data = new StringContent(string.Empty);
				var resp = new HttpResponseMessage();

				foreach (var record in records) {
					_logger.LogDebug($"{debugSendMessage} : {record.Name}");

					content.Message = $"{record.ToQQMessage()}{NotifyFormatStrings.projectLink}";

					data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
					resp = await client.PostAsync(url, data);

					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
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
