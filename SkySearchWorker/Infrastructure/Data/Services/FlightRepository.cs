using SkySearchWorker.Infrastructure.Data.Entities;
using SkySearchWorker.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.Services
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        public FlightRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> FlightExistAsynch(DateTime departureTime, string departureAirportCode, DateTime arrivalTime, string arrivalAirportCode)
        {
            var existingFlight = await FindAsync(a =>
                a.DepartureTime == departureTime &&
                a.DepartureAirport!.Code.Equals(departureAirportCode) &&
                a.ArrivalTime == arrivalTime &&
                a.ArrivalAirport!.Code.Equals(departureAirportCode));

            return existingFlight.Any();
        }
    }
}
