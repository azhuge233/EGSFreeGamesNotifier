using EGSFreeGamesNotifier.Models.Record;

namespace EGSFreeGamesNotifier.Models {
	public class ParseResult {
		public List<FreeGameRecord> Records { get; set; } = new List<FreeGameRecord>();

		public List<FreeGameRecord> NotifyRecords { get; set; } = new List<FreeGameRecord>();
	}
}
