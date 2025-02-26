using Azure.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SkySearchWorker.Application.Interfaces;
using SkySearchWorker.Application.Services;
using SkySearchWorker.Infrastructure.Configuration;
using SkySearchWorker.Infrastructure.Interfaces;
using SkySearchWorker.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Startup
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ICustomHttpClient, CustomHttpClientService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IAmadeusAuthentication, AmadeusAuthenticationService>();
            services.AddSingleton<IAmadeusFlightProvider, AmadeusFlightProviderService>();
            services.AddSingleton<IExampleHelper, ExampleHelper>();
            services.AddSingleton<ISkySearchSync, SkySearchSyncService>();

            return services;
        }
    }
}
