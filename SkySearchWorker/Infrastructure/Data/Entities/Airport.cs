using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.Entities
{
    public class Airport
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string CityCode { get; set; }
        public required string CountryCode { get; set; }
        public List<Flight>? Flights { get; set; }
    }
}
