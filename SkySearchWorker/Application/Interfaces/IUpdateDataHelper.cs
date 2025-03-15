using SkySearchWorker.Application.DTOs.Amadeus.FlightOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.Interfaces
{
    public interface IUpdateDataHelper
    {
        Dictionary<string, string> GetUniqueCarriers(List<FlightOfferDto> flightOffers);
        Dictionary<string, DictionaryLocationDto> GetUniqueAirports(List<FlightOfferDto> flightOffers);
        List<DataDto> GetUniqueFlights(List<FlightOfferDto> flightOffers);
    }
}
