﻿using EGSFreeGamesNotifier.Modules;
using EGSFreeGamesNotifier.Services;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace EGSFreeGamesNotifier {
	internal class Program {
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		static async Task Main() {
			try {
				var servicesProvider = DI.BuildDiAll();

				logger.Info(" - Start Job -");

				using (servicesProvider as IDisposable) {
					var jsonOp = servicesProvider.GetRequiredService<JsonOP>();
					var notifyOP = servicesProvider.GetRequiredService<NotifyOP>();

					var oldRecord = jsonOp.LoadData();
					servicesProvider.GetRequiredService<ConfigValidator>().CheckValid();

					// Get page source
					var source = await servicesProvider.GetRequiredService<Scraper>().GetSource();
					// var source = System.IO.File.ReadAllText("test.json");

					// Parse page source
					var parseResult = servicesProvider.GetRequiredService<Parser>().Parse(source, oldRecord);

					// Notify first, then write records
					await notifyOP.Notify(parseResult.NotifyRecords);

					// Write new records
					jsonOp.WriteData(parseResult.Records);
				}

				logger.Info(" - Job End -\n");
			} catch (Exception ex) {
				logger.Error($"{ex.Message}\n");
			} finally {
				LogManager.Shutdown();
			}
		}
	}
}