using SkySearchWorker.Application.Interfaces;
using SkySearchWorker.Application.Services;
using SkySearchWorker.Infrastructure.Configuration;
using SkySearchWorker.Infrastructure.Data.Interfaces;
using SkySearchWorker.Infrastructure.Data.Services;
using SkySearchWorker.Infrastructure.Interfaces;
using SkySearchWorker.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Startup
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection RegisterRepositorieServices(this IServiceCollection services)
        {
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IAirportRepository, AirportRepository>();
            services.AddScoped<IAirlineRepository, AirlineRepository>();
            services.AddScoped<IFlightPriceRepository, FlightPriceRepository>();

            return services;
        }
    }
}
