using System.Text.Json.Serialization;

namespace SkySearchWorker.Application.DTOs.Amadeus.FlightOffer
{
    public class SegmentDto
    {
        [JsonPropertyName("departure")]
        public LocationDto Departure { get; set; }

        [JsonPropertyName("arrival")]
        public LocationDto Arrival { get; set; }

        [JsonPropertyName("carrierCode")]
        public string CarrierCode { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("aircraft")]
        public AircraftDto Aircraft { get; set; }

        [JsonPropertyName("operating")]
        public OperatingDto Operating { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("numberOfStops")]
        public int NumberOfStops { get; set; }

        [JsonPropertyName("blacklistedInEU")]
        public bool BlacklistedInEU { get; set; }
    }
}
