﻿using SkySearchWorker.Infrastructure.Data;
using SkySearchWorker.Infrastructure.Data.Entities;
using SkySearchWorker.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.Services
{
    public class AirlineRepository : Repository<Airline>,IAirlineRepository
    {
        public AirlineRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<bool> AirlineCodeExistsAsync(string code)
        {
            var existingAirline = await FindAsync(a => a.Code.ToUpper().Trim().Equals(code.ToUpper().Trim()));
            return existingAirline.Any();
        }

        public async Task<Airline> GetAirlineAsync(string code)
        {
            var airlineExist = await AirlineCodeExistsAsync(code);
            if (airlineExist) 
            {
                var airline = await FindAsync(a => a.Code.ToUpper().Trim().Equals(code.ToUpper().Trim()));
                return airline.FirstOrDefault()!;
            }
            throw new KeyNotFoundException($"Airline with code {code} not found.");
        }
    }
}
