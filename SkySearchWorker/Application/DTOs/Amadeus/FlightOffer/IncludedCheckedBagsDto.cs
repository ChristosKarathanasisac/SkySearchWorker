using System.Text.Json.Serialization;

namespace SkySearchWorker.Application.DTOs.Amadeus.FlightOffer
{
    public class IncludedCheckedBagsDto
    {
        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("weightUnit")]
        public string WeightUnit { get; set; }
    }
}
