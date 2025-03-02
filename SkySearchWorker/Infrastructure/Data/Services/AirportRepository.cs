using SkySearchWorker.Infrastructure.Data.Entities;
using SkySearchWorker.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.Services
{
    public class AirportRepository : Repository<Airport>,IAirportRepository
    {
        public AirportRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<bool> AirportCodeExistsAsync(string code)
        {
            var existingAirport = await FindAsync(a => a.Code.ToUpper().Trim().Equals(code.ToUpper().Trim()));
            return existingAirport.Any();
        }
    }
}
