using EGSFreeGamesNotifier.Models.Config;
using EGSFreeGamesNotifier.Models.Record;

namespace EGSFreeGamesNotifier.Services.Notifier {
	internal interface INotifiable : IDisposable {
		public Task SendMessage(NotifyConfig config, List<NotifyRecord> records);
	}
}
