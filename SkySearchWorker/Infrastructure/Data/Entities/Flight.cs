using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int AirlineId { get; set; }
        public Airline? Airline { get; set; }
        public int DepartureAirportId { get; set; }
        public Airport? DepartureAirport { get; set; }
        public int ArrivalAirportId { get; set; }
        public Airport? ArrivalAirport { get; set; }
        public List<FlightPrice>? FlightPrices { get; set; }
        public int NumberOfAvailableSeats { get; set; }
    }
}
