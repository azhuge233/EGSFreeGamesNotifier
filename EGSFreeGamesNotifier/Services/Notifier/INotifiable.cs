using EGSFreeGamesNotifier.Models.Record;

namespace EGSFreeGamesNotifier.Services.Notifier {
	internal interface INotifiable : IDisposable {
		public Task SendMessage(List<NotifyRecord> records);
	}
}
