﻿using SkySearchWorker.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.Interfaces
{
    public interface IAirlineRepository : IRepository<Airline>
    {
        Task<bool> AirlineCodeExistsAsync(string code);
        Task<Airline> GetAirlineAsync(string code);
    }
}
