using System.Text.Json.Serialization;

namespace SkySearchWorker.Application.DTOs.Amadeus.FlightOffer
{
    public class DataDto
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("source")]
        public string? Source { get; set; }

        [JsonPropertyName("instantTicketingRequired")]
        public bool InstantTicketingRequired { get; set; }

        [JsonPropertyName("nonHomogeneous")]
        public bool NonHomogeneous { get; set; }

        [JsonPropertyName("oneWay")]
        public bool OneWay { get; set; }

        [JsonPropertyName("isUpsellOffer")]
        public bool IsUpsellOffer { get; set; }

        [JsonPropertyName("lastTicketingDate")]
        public string? LastTicketingDate { get; set; }

        [JsonPropertyName("lastTicketingDateTime")]
        public string? LastTicketingDateTime { get; set; }

        [JsonPropertyName("numberOfBookableSeats")]
        public int NumberOfBookableSeats { get; set; }

        [JsonPropertyName("itineraries")]
        public List<ItineraryDto>? Itineraries { get; set; }

        [JsonPropertyName("price")]
        public PriceDto? Price { get; set; }

        [JsonPropertyName("pricingOptions")]
        public PricingOptionsDto? PricingOptions { get; set; }

        [JsonPropertyName("validatingAirlineCodes")]
        public List<string>? ValidatingAirlineCodes { get; set; }

        [JsonPropertyName("travelerPricings")]
        public List<TravelerPricingDto>? TravelerPricings { get; set; }
    }
}
