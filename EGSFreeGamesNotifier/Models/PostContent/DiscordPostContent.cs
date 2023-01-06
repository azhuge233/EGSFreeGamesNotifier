﻿using System.Text.Json.Serialization;

namespace EGSFreeGamesNotifier.Models.PostContent {
	public class Footer {
		[JsonPropertyName("text")]
		public string Text { get; set; }

	}
	public class Embed {
		[JsonPropertyName("title")]
		public string Title { get; set; }
		[JsonPropertyName("url")]
		public string Url { get; set; }
		[JsonPropertyName("description")]
		public string Description { get; set; }
		[JsonPropertyName("color")]
		public int Color { get; set; } = 2763306;
		[JsonPropertyName("footer")]
		public Footer Footer { get; set; }
	}
	public class DiscordPostContent {
		[JsonPropertyName("content")]
		public string Content { get; set; }
		[JsonPropertyName("embeds")]
		public List<Embed> Embeds { get; set; } = new List<Embed>();
	}
}
