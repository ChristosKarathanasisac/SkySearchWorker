using SkySearchWorker.Application.DTOs.Amadeus.FlightOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.Interfaces
{
    public interface IExampleHelper
    {
        List<Dictionary<string, string>> GetFlightOfferDictionaries();
        List<string> GetDateRange(string fromDate, string toDate);
        public List<List<Dictionary<string, string>>> GetGroupedFlightOfferDictionaries();
    }
}
