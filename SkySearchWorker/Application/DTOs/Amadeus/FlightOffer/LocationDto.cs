using System.Text.Json.Serialization;

namespace SkySearchWorker.Application.DTOs.Amadeus.FlightOffer
{
    public class LocationDto
    {
        [JsonPropertyName("iataCode")]
        public string? IataCode { get; set; }

        [JsonPropertyName("terminal")]
        public string? Terminal { get; set; }

        [JsonPropertyName("at")]
        public DateTime At { get; set; }
    }
}
