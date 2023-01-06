using Microsoft.Extensions.Logging;
using EGSFreeGamesNotifier.Models.Config;
using EGSFreeGamesNotifier.Models.Record;
using EGSFreeGamesNotifier.Strings;
using System.Text;
using System.Web;

namespace EGSFreeGamesNotifier.Services.Notifier {
	internal class QQ: INotifiable {
		private readonly ILogger<QQ> _logger;

		#region debug strings
		private readonly string debugSendMessage = "Send notifications to QQ";
		#endregion

		public QQ(ILogger<QQ> logger) {
			_logger = logger;
		}

		public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records) {
			try {
				_logger.LogDebug(debugSendMessage);

				string url = new StringBuilder().AppendFormat(NotifyFormatStrings.qqUrlFormat, config.QQAddress, config.QQPort.ToString(), config.ToQQID).ToString();
				var sb = new StringBuilder();
				using var client = new HttpClient();

				foreach (var record in records) {
					_logger.LogDebug($"{debugSendMessage} : {record.Name}");
					var resp = await client.GetAsync(
						new StringBuilder()
							.Append(url)
							.Append(HttpUtility.UrlEncode(record.ToQQMessage()))
							.Append(HttpUtility.UrlEncode(NotifyFormatStrings.projectLink))
							.ToString()
					);
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
