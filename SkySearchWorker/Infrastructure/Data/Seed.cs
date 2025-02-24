using Microsoft.EntityFrameworkCore;
using SkySearchWorker.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Airlines.Any() || context.Airports.Any() || context.Flights.Any() || context.FlightPrices.Any())
                {
                    return;   
                }

                // Seed Airlines
                var airlines = new List<Airline>
                {
                    new Airline { Name = "Airline A", Code = "AA" },
                    new Airline { Name = "Airline B", Code = "AB" }
                };
                context.Airlines.AddRange(airlines);

                // Seed Airports
                var airports = new List<Airport>
                {
                    new Airport { Name = "Airport X", Code = "AX", CityCode = "CX", CountryCode = "US" },
                    new Airport { Name = "Airport Y", Code = "AY", CityCode = "CY", CountryCode = "US" }
                };
                context.Airports.AddRange(airports);

                // Seed Flights
                var flights = new List<Flight>
                {
                    new Flight { DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(2), Airline = airlines[0], Airport = airports[0] },
                    new Flight { DepartureTime = DateTime.Now.AddHours(3), ArrivalTime = DateTime.Now.AddHours(5), Airline = airlines[1], Airport = airports[1] }
                };
                context.Flights.AddRange(flights);

                // Seed FlightPrices
                var flightPrices = new List<FlightPrice>
                {
                    new FlightPrice { Price = 100, Class = "Economy", AvailableSeats = 50, Date = DateTime.Now, Currency = "EUR", Flight = flights[0] },
                    new FlightPrice { Price = 200, Class = "Business", AvailableSeats = 20, Date = DateTime.Now, Currency = "EUR", Flight = flights[1] }
                };
                context.FlightPrices.AddRange(flightPrices);

                context.SaveChanges();
            }
        }
    }
}
