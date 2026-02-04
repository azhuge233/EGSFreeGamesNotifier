using EGSFreeGamesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;

namespace EGSFreeGamesNotifier.Services {
	internal class Scraper : IDisposable {
		private readonly ILogger<Scraper> _logger;

		internal HttpClient Client { get; set; } = new HttpClient();

		public Scraper(ILogger<Scraper> logger) { 
			_logger = logger;

			Microsoft.Playwright.Program.Main(["install", "firefox"]);
		}

		internal async Task<Tuple<string, string>> GetSource() {
			try {
				_logger.LogDebug(ScrapeStrings.debugGetWeeklyFreeGameSource);
				var weeklyFreeGameSource = await GetSourceWithHttpClient(ScrapeStrings.EGSUrl);
				_logger.LogDebug($"Done: {ScrapeStrings.debugGetWeeklyFreeGameSource}");

				_logger.LogDebug(ScrapeStrings.debugGetGraphQLSource);
				var graphQLUrl = $"{ScrapeStrings.EGSGraphQLUrl}?{ScrapeStrings.EGSGraphQLQueryParam}";
				var graphQLSource =  await GetSourceWithPlaywright(graphQLUrl);
				_logger.LogDebug($"Done: {ScrapeStrings.debugGetGraphQLSource}");

				File.WriteAllText("debug_grapql.json", graphQLSource);

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
				_logger.LogDebug(ScrapeStrings.debugGetSourceWithHttpClient, url);

				var response = await Client.GetAsync(url);
				var result = await response.Content.ReadAsStringAsync();

				_logger.LogDebug($"Done: {ScrapeStrings.debugGetSourceWithHttpClient}", url);
				return result;
			} catch (Exception) {
				_logger.LogError($"Error: {ScrapeStrings.debugGetSourceWithHttpClient}", url);
				throw;
			}
		}

		private async Task<string> GetSourceWithPlaywright(string url) {
			try {
				_logger.LogDebug(ScrapeStrings.debugGetSourceWithPlaywright, url);
				var page = await GetNewPageInstance();

				await page.GotoAsync(url, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

				var source = await page.Locator("body > pre").InnerTextAsync();

				await page.CloseAsync();

				_logger.LogDebug($"Done: {ScrapeStrings.debugGetSourceWithPlaywright}", url);
				return source;
			} catch (Exception) {
				_logger.LogError($"Error: {ScrapeStrings.debugGetSourceWithPlaywright}", url);
				throw;
			}
		}

		private static async Task<IPage> GetNewPageInstance() {
			var playwright = await Playwright.CreateAsync();
			var browser = await playwright.Firefox.LaunchAsync(new() { Headless = true });
			var context = await browser.NewContextAsync();

			var page = await context.NewPageAsync();
			page.SetDefaultTimeout(10000);
			page.SetDefaultNavigationTimeout(10000);
			await page.RouteAsync("**/*", async route => {
				var blockList = new List<string> { "stylesheet", "image", "font" };
				if (blockList.Contains(route.Request.ResourceType)) await route.AbortAsync();
				else await route.ContinueAsync();
			});

			return page;
		}

		public void Dispose() {
			Client?.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
