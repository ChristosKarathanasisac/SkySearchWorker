using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAirlineRepository Airlines { get; }
        IAirportRepository Airports { get; }
        IFlightRepository Flights { get; }
        IFlightPriceRepository FlightPrices { get; }
        Task<int> SaveChangesAsync();
    }
}
