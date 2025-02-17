using System.Text.Json.Serialization;

namespace SkySearchWorker.Application.DTOs.Amadeus.FlightOffer
{
    public class FareDetailsBySegmentDto
    {
        [JsonPropertyName("segmentId")]
        public string SegmentId { get; set; }

        [JsonPropertyName("cabin")]
        public string Cabin { get; set; }

        [JsonPropertyName("fareBasis")]
        public string FareBasis { get; set; }

        [JsonPropertyName("brandedFare")]
        public string BrandedFare { get; set; }

        [JsonPropertyName("brandedFareLabel")]
        public string BrandedFareLabel { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("includedCheckedBags")]
        public IncludedCheckedBagsDto IncludedCheckedBags { get; set; }
    }
}
