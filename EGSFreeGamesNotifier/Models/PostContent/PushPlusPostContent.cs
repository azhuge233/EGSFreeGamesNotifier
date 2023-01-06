using System.Text.Json.Serialization;

namespace EGSFreeGamesNotifier.Models.PostContent {
	public class PushPlusPostContent {
		[JsonPropertyName("token")]
		public string Token { get; set; }
		[JsonPropertyName("title")]
		public string Title { get; set; }
		[JsonPropertyName("content")]
		public string Content { get; set; }
	}
}
