using System.Text.Json.Serialization;

namespace SkySearchWorker.Application.DTOs.Amadeus.FlightOffer
{
    public class AircraftDto
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}
