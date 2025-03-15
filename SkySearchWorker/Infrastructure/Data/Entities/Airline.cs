using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.Entities
{
    public class Airline
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public List<Flight>? Flights { get; set; }
    }
}
