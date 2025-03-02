using Microsoft.EntityFrameworkCore;
using SkySearchWorker.Infrastructure.Data;
using SkySearchWorker.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private  IAirlineRepository? _airlineRepository;
        private  IAirportRepository? _airportRepository;
        private  IFlightPriceRepository? _flightPriceRepository;
        private  IFlightRepository? _flightRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAirlineRepository Airlines => _airlineRepository ??= new AirlineRepository(_context);

        public IAirportRepository Airports => _airportRepository ??= new AirportRepository(_context);

        public IFlightRepository FlightRepository => _flightRepository ??= new FlightRepository(_context);

        public IFlightPriceRepository FlightPrices => _flightPriceRepository ??= new FlightPriceRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
