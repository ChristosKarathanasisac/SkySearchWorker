using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.DTOs.Amadeus.FlightOffer
{
    public class FlightOfferDto
    {
        [JsonPropertyName("data")]
        public required List<DataDto> Data { get; set; }
        [JsonPropertyName("dictionaries")]
        public required DictionaryDto Dictionary { get; set; }
    }
}
