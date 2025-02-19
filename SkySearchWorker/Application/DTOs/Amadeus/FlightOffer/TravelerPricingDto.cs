using System.Text.Json.Serialization;

namespace SkySearchWorker.Application.DTOs.Amadeus.FlightOffer
{
    public class TravelerPricingDto
    {
        [JsonPropertyName("travelerId")]
        public string? TravelerId { get; set; }

        [JsonPropertyName("fareOption")]
        public string? FareOption { get; set; }

        [JsonPropertyName("travelerType")]
        public string? TravelerType { get; set; }

        [JsonPropertyName("price")]
        public PriceDto? Price { get; set; }

        [JsonPropertyName("fareDetailsBySegment")]
        public List<FareDetailsBySegmentDto>? FareDetailsBySegment { get; set; }
    }
}
