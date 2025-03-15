using Microsoft.EntityFrameworkCore.Update;
using SkySearchWorker.Application.DTOs.Amadeus.FlightOffer;
using SkySearchWorker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.Services
{
    public class UpdateDataHelper : IUpdateDataHelper
    {
        public Dictionary<string, DictionaryLocationDto> GetUniqueAirports(List<FlightOfferDto> flightOffers) =>
            flightOffers
                    .SelectMany(fo => fo.Dictionary.Locations ?? new Dictionary<string, DictionaryLocationDto>())
                    .GroupBy(c => c.Key)
                    .Select(g => g.First())
                    .ToDictionary(c => c.Key, c => c.Value);
        public Dictionary<string, string> GetUniqueCarriers(List<FlightOfferDto> flightOffers) =>
            flightOffers
                    .SelectMany(fo => fo.Dictionary.Carriers ?? new Dictionary<string, string>())
                    .GroupBy(c => c.Key)
                    .Select(g => g.First())
                    .ToDictionary(c => c.Key, c => c.Value);
        public List<DataDto> GetUniqueFlights(List<FlightOfferDto> flightOffers) =>
            flightOffers
                    .SelectMany(fo => fo.Data)
                    .ToList();
    }
}
