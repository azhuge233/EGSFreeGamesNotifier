using Microsoft.Extensions.Logging;
using EGSFreeGamesNotifier.Strings;

namespace EGSFreeGamesNotifier.Services {
	internal class Scraper(ILogger<Scraper> logger) : IDisposable {
		private readonly ILogger<Scraper> _logger = logger;

		internal HttpClient Client { get; set; } = new HttpClient();

		internal async Task<Tuple<string, string>> GetSource() {
			try {
				_logger.LogDebug(ScrapeStrings.debugGetWeeklyFreeGameSource);
				var weeklyFreeGameSource = await GetSourceWithHttpClient(ScrapeStrings.EGSUrl);
				_logger.LogDebug($"Done: {ScrapeStrings.debugGetWeeklyFreeGameSource}");

				_logger.LogDebug(ScrapeStrings.debugGetGraphQLSource);
				var graphQLUrl = $"{ScrapeStrings.EGSGraphQLUrl}?{ScrapeStrings.EGSGraphQLQueryParam}";
				var graphQLSource =  await GetSourceWithHttpClient(graphQLUrl);
				_logger.LogDebug($"Done: {ScrapeStrings.debugGetGraphQLSource}");

				return new (weeklyFreeGameSource, graphQLSource);
			} catch (Exception) {
				_logger.LogError($"Error: {ScrapeStrings.debugGetSource}");
				throw;
			} finally {
				Dispose();
			}
		}

		private async Task<string> GetSourceWithHttpClient(string url) {
			try {
				_logger.LogDebug(ScrapeStrings.debugGetSourceWithUrl, url);

				var response = await Client.GetAsync(url);
				var result = await response.Content.ReadAsStringAsync();

				_logger.LogDebug($"Done: {ScrapeStrings.debugGetSourceWithUrl}", url);
				return result;
			} catch (Exception) {
				_logger.LogError($"Error: {ScrapeStrings.debugGetSourceWithUrl}", url);
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
