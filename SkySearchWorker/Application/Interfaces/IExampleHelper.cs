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
        List<Task<FlightOfferDto>> GetFlightOfferTasks();
        List<string> GetDateRange(string fromDate, string toDate);
        List<List<Task<FlightOfferDto>>> GetGroupedFlightOfferTasks();
    }
}
