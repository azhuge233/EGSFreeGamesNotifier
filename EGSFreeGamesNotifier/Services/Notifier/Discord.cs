﻿using EGSFreeGamesNotifier.Models.Config;
using EGSFreeGamesNotifier.Models.PostContent;
using EGSFreeGamesNotifier.Models.Record;
using EGSFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace EGSFreeGamesNotifier.Services.Notifier {
	internal class Discord(ILogger<Discord> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<Discord> _logger = logger;
		private readonly Config config = config.Value;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to Discord";
		private readonly string debugGeneratePostContent = "Generating Discord POST content";
		#endregion

		public async Task SendMessage(List<NotifyRecord> records) {
			try {
				_logger.LogDebug(debugSendMessage);

				var url = config.DiscordWebhookURL;
				var contents = GeneratePostContent(records);
				var client = new HttpClient();

				foreach (var content in contents) {
					var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
					var resp = await client.PostAsync(url, data);
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

		private List<DiscordPostContent> GeneratePostContent(List<NotifyRecord> records) {
			_logger.LogDebug(debugGeneratePostContent);

			var contents = new List<DiscordPostContent>();
			var content = new DiscordPostContent() {
				Content = records.Count > 1 ? "New Free Games - Epic Game Store" : "New Free Game - Epic Game Store"
			};

			for (int i = 0; i < records.Count; i++) {
				if (content.Embeds.Count == 2) {
					contents.Add(content);
					content = new DiscordPostContent() {
						Content = records.Count - i - 1 > 1 ? "New Free Games - Epic Game Store" : "New Free Game - Epic Game Store"
					};
				}

				content.Embeds.Add(
					new Embed() {
						Title = records[i].Title,
						// Url = records[i].Url,
						Description = records[i].ToDiscordMessage(),
						Footer = new Footer() {
							Text = NotifyFormatStrings.projectLink
						}
					}
				);

				if (i == records.Count - 1) contents.Add(content);
			}

			_logger.LogDebug($"Done: {debugGeneratePostContent}");
			return contents;
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
