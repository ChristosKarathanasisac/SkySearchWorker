using SkySearchWorker.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.Interfaces
{
    public interface IAirportRepository : IRepository<Airport>
    {
        Task<bool> AirportCodeExistsAsync(string code);
        Task<Airport> GetAirportAsync(string code);
    }
}
