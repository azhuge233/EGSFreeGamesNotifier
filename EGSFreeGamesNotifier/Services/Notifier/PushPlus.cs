using EGSFreeGamesNotifier.Models.Config;
using EGSFreeGamesNotifier.Models.PostContent;
using EGSFreeGamesNotifier.Models.Record;
using EGSFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace EGSFreeGamesNotifier.Services.Notifier {
	internal class PushPlus(ILogger<PushPlus> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<PushPlus> _logger = logger;
		private readonly Config config = config.Value;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to PushPlus";
		private readonly string debugCreateMessage = "Create notification message";
		#endregion

		public async Task SendMessage(List<NotifyRecord> records) {
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

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
