using System.Text.Json.Serialization;

namespace SkySearchWorker.Application.DTOs.Amadeus.FlightOffer
{
    public class OperatingDto
    {
        [JsonPropertyName("carrierCode")]
        public string? CarrierCode { get; set; }
    }
}
