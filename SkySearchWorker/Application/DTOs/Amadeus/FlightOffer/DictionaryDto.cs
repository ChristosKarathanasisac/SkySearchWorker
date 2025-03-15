using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.DTOs.Amadeus.FlightOffer
{
    public class DictionaryDto
    {
        [JsonPropertyName("locations")]
        public Dictionary<string, DictionaryLocationDto>? Locations { get; set; }

        [JsonPropertyName("aircraft")]
        public Dictionary<string, string>? Aircraft { get; set; }

        [JsonPropertyName("currencies")]
        public Dictionary<string, string>? Currencies { get; set; }

        [JsonPropertyName("carriers")]
        public Dictionary<string, string>? Carriers { get; set; }
    }
}
