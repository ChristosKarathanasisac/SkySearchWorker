using Microsoft.EntityFrameworkCore;
using SkySearchWorker.Infrastructure.Data.Entities;
using SkySearchWorker.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Data.Services
{
    public class FlightPriceRepository : Repository<FlightPrice>, IFlightPriceRepository
    {
        public FlightPriceRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
