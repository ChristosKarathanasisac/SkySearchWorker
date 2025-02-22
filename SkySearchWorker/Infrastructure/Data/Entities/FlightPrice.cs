using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.Entities
{
    public class FlightPrice
    {
        public int Id { get; set; }
        public Decimal Price { get; set; }
        public string? Class { get; set; }
        public int AvailableSeats {get;set;}
        public DateTime Date { get; set; }
        public string? currency { get; set; }
        public int FlightId { get; set; }
        public Flight? Flight { get; set; }
    }
}
