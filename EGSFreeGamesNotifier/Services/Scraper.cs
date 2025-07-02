using Microsoft.Extensions.Logging;
using EGSFreeGamesNotifier.Strings;

namespace EGSFreeGamesNotifier.Services {
	internal class Scraper(ILogger<Scraper> logger) : IDisposable {
		private readonly ILogger<Scraper> _logger = logger;

		internal HttpClient Client { get; set; } = new HttpClient();

		internal async Task<string> GetSource() {
			try {
				_logger.LogDebug(ScrapeStrings.debugGetSource);

				_logger.LogDebug(ScrapeStrings.debugGetSourceWithUrl, ScrapeStrings.EGSUrl);
				var response = await Client.GetAsync(ScrapeStrings.EGSUrl);
				var result = await response.Content.ReadAsStringAsync();

				_logger.LogDebug($"Done: {ScrapeStrings.debugGetSource}");
				return result;
			} catch (Exception) {
				_logger.LogError($"Error: {ScrapeStrings.debugGetSource}");
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
