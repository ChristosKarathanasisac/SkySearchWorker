using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.DTOs.Amadeus.FlightOffer
{
    public class DictionaryLocationDto
    {
        [JsonPropertyName("cityCode")]
        public required string CityCode { get; set; }

        [JsonPropertyName("countryCode")]
        public required string CountryCode { get; set; }
    }
}
