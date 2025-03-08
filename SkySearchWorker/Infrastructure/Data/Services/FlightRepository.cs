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

        public async Task<bool> FlightExistAsync(DateTime departureTime, string departureAirportCode, DateTime arrivalTime, string arrivalAirportCode)
        {
            var existingFlight = await FindAsync(a =>
                 a.DepartureTime == departureTime &&
                 a.DepartureAirport!.Code.Equals(departureAirportCode) &&
                 a.ArrivalTime == arrivalTime &&
                 a.ArrivalAirport!.Code.Equals(arrivalAirportCode));

            return existingFlight.Any();
        }

        public async Task<Flight> GetFlight(DateTime departureTime, string departureAirportCode, DateTime arrivalTime, string arrivalAirportCode)
        {
            var FlightExist = await FlightExistAsync(departureTime,departureAirportCode,arrivalTime,arrivalAirportCode);
            if (FlightExist)
            {
                var flight = await FindAsync(a =>
                 a.DepartureTime == departureTime &&
                 a.DepartureAirport!.Code.Equals(departureAirportCode) &&
                 a.ArrivalTime == arrivalTime &&
                 a.ArrivalAirport!.Code.Equals(arrivalAirportCode));
                return flight.FirstOrDefault()!;
            }
            throw new KeyNotFoundException($"Flight from {departureAirportCode} to {arrivalAirportCode} not found.");
        }
    }
}
