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
    }
}
