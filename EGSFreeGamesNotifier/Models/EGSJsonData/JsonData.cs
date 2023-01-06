using System.Text.Json.Serialization;

namespace EGSFreeGamesNotifier.Models.EGSJsonData {
	public class JsonData {
		[JsonPropertyName("data")]
		public Data_ Data { get; set; }
	}

	public class Data_ {
		[JsonPropertyName("Catalog")]
		public Catalog_ Catalog { get; set; }
	}

	public class Catalog_ {
		[JsonPropertyName("searchStore")]
		public SearchStore_ SearchStore { get; set; }
	}

	public class SearchStore_ {
		[JsonPropertyName("elements")]
		public List<Element_> Elements { get; set; }
	}

	public class Element_ {
		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("id")]
		public string ID { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("productSlug")]
		public string ProductSlug { get; set; }

		[JsonPropertyName("customAttributes")]
		public List<CustomAttribute> CustomAttributes { get; set; }

		[JsonPropertyName("catalogNs")]
		public CatalogNs_ CatalogNs { get; set; }

		[JsonPropertyName("offerMappings")]
		public List<Mapping> OfferMappings { get; set; }

		[JsonPropertyName("promotions")]
		public Promotions_ Promotions { get; set; }
	}

	public class CustomAttribute {
		[JsonPropertyName("key")]
		public string Key { get; set; }

		[JsonPropertyName("value")]
		public string Value { get; set; }
	}

	public class CatalogNs_ {
		[JsonPropertyName("mappings")]
		public List<Mapping> Mappings { get; set; }
	}

	public class Mapping {
		[JsonPropertyName("pageSlug")]
		public string PageSlug { get; set; }

		[JsonPropertyName("pageType")]
		public string PageType { get; set; }
	}

	public class Promotions_ {
		[JsonPropertyName("promotionalOffers")]
		public List<PromotionOffers_> PromotionalOffers { get; set; }

		[JsonPropertyName("upcomingPromotionalOffers")]
		public List<PromotionOffers_> UpcomingPromotionalOffers { get; set; }
	}

	public class PromotionOffers_ {
		[JsonPropertyName("promotionalOffers")]
		public List<PromotionOffer> PromotionalOffers { get; set; }
	}

	public class PromotionOffer {
		[JsonPropertyName("startDate")]
		public DateTime StartDate { get; set; }

		[JsonPropertyName("endDate")]
		public DateTime EndDate { get; set; }

		[JsonPropertyName("discountSetting")]
		public DiscountSetting_ DiscountSetting { get; set; }
	}

	public class DiscountSetting_ {
		[JsonPropertyName("discountType")]
		public string DiscountType { get; set; }

		[JsonPropertyName("discountPercentage")]
		public int DiscountPercentage { get; set; }
	}
}
