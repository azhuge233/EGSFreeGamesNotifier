namespace EGSFreeGamesNotifier.Models.Record {
	public class FreeGameRecord {
		public string Name { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public string PurchaseUrl { get; set; }
		public string ID { get; set; }
		public string Namespace { get; set; }
		public string OfferType { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public bool IsUpcomingPromotion { get; set; } = false;
		public bool IsMysteryGame { get; set; } = false;
		public bool IsDevAccount { get; set; } = false;

		public FreeGameRecord() { }
	}
}
