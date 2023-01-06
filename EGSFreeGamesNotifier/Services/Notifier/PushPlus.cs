using Microsoft.Extensions.Logging;
using EGSFreeGamesNotifier.Models.Config;
using EGSFreeGamesNotifier.Models.PostContent;
using EGSFreeGamesNotifier.Models.Record;
using EGSFreeGamesNotifier.Strings;
using System.Text;
using System.Web;
using System.Text.Json;

namespace EGSFreeGamesNotifier.Services.Notifier {
	internal class PushPlus: INotifiable {
		private readonly ILogger<PushPlus> _logger;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to PushPlus";
		private readonly string debugCreateMessage = "Create notification message";
		#endregion

		public PushPlus(ILogger<PushPlus> logger) {
			_logger = logger;
		}

		private string CreateMessage(List<NotifyRecord> records) {
			try {
				_logger.LogDebug(debugCreateMessage);

				var sb = new StringBuilder();

				records.ForEach(record => sb.AppendFormat(NotifyFormatStrings.pushPlusBodyFormat, record.ToPushPlusMessage()));

				sb.Append(NotifyFormatStrings.projectLinkHTML);

				_logger.LogDebug($"Done: {debugCreateMessage}");
				return sb.ToString();
			} catch (Exception) {
				_logger.LogError($"Error: {debugCreateMessage}");
				throw;
			}
		}

		public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records) {
			try {
				_logger.LogDebug(debugSendMessage);

				var client = new HttpClient();

				var title = new StringBuilder().AppendFormat(NotifyFormatStrings.pushPlusTitleFormat, records.Count).ToString();

				var postContent = new PushPlusPostContent() {
					Token = config.PushPlusToken,
					Title = title,
					Content = CreateMessage(records)
				};

				var resp = await client.PostAsync(NotifyFormatStrings.pushPlusPostUrl, new StringContent(JsonSerializer.Serialize(postContent), Encoding.UTF8, "application/json"));
				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

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
