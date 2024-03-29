﻿namespace EGSFreeGamesNotifier.Models.Record {
	public class FreeGameRecord {
		public string Name { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public string ID { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public bool IsUpcomingPromotion { get; set; } = false;

		public FreeGameRecord() { }
		public FreeGameRecord(FreeGameRecord record) {
			Name = record.Name;
			Title = record.Title;
			Description = record.Description;
			Url = record.Url;
			ID = record.ID;
			IsUpcomingPromotion = record.IsUpcomingPromotion;
			StartTime = record.StartTime;
			EndTime = record.EndTime;
		}
	}
}
