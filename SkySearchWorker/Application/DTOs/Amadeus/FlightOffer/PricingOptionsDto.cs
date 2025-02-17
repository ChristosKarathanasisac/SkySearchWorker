using System.Text.Json.Serialization;

namespace SkySearchWorker.Application.DTOs.Amadeus.FlightOffer
{
    public class PricingOptionsDto
    {
        [JsonPropertyName("fareType")]
        public List<string> FareType { get; set; }

        [JsonPropertyName("includedCheckedBagsOnly")]
        public bool IncludedCheckedBagsOnly { get; set; }
    }
}
