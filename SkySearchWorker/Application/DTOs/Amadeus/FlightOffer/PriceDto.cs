using System.Text.Json.Serialization;

namespace SkySearchWorker.Application.DTOs.Amadeus.FlightOffer
{
    public class PriceDto
    {
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("total")]
        public string? Total { get; set; }

        [JsonPropertyName("base")]
        public string? Base { get; set; }

        [JsonPropertyName("fees")]
        public List<FeeDto>? Fees { get; set; }

        [JsonPropertyName("grandTotal")]
        public string? GrandTotal { get; set; }
    }
}
