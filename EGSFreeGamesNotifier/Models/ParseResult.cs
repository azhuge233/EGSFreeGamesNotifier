using EGSFreeGamesNotifier.Models.Record;

namespace EGSFreeGamesNotifier.Models {
	public class ParseResult {
		public List<FreeGameRecord> Records { get; set; } = [];

		public List<NotifyRecord> NotifyRecords { get; set; } = [];
	}
}
