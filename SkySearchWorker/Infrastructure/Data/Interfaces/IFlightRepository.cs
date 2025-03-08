using SkySearchWorker.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.Interfaces
{
    public interface IFlightRepository : IRepository<Flight>
    {
        Task<bool> FlightExistAsync(DateTime departureTime, string departureAirportCode,
            DateTime arrivalTime, string arrivalAirportCode);
        Task<Flight> GetFlight(DateTime departureTime, string departureAirportCode,
            DateTime arrivalTime, string arrivalAirportCode);
    }
}
