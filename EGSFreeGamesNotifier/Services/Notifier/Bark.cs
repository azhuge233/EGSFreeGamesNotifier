using EGSFreeGamesNotifier.Models.Config;
using EGSFreeGamesNotifier.Models.Record;
using EGSFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Web;

namespace EGSFreeGamesNotifier.Services.Notifier {
	internal class Bark(ILogger<Bark> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<Bark> _logger = logger;
		private readonly Config config = config.Value;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to Bark";
		#endregion

		public async Task SendMessage(List<NotifyRecord> records) {
			try {
				string url = new StringBuilder().AppendFormat(NotifyFormatStrings.barkUrlFormat, config.BarkAddress, config.BarkToken).ToString();
				using var client = new HttpClient();

				foreach (var record in records) {
					_logger.LogDebug($"{debugSendMessage} : {record.Name}");
					var resp = await client.GetAsync(
						new StringBuilder()
							.Append(url)
							.Append(NotifyFormatStrings.barkUrlTitle)
							.Append(HttpUtility.UrlEncode(record.ToBarkMessage()))
							.Append(HttpUtility.UrlEncode(NotifyFormatStrings.projectLink))
							.Append(new StringBuilder().AppendFormat(NotifyFormatStrings.barkUrlArgs, record.Url))
							.ToString()
					);
					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
				}

				_logger.LogDebug($"Done: {debugSendMessage}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {debugSendMessage}");
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
