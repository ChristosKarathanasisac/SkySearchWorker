using Azure.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomHttpClient, CustomHttpClientService>();
            return services;
        }

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAmadeusAuthentication, AmadeusAuthenticationService>();
            services.AddScoped<IAmadeusFlightProvider, AmadeusFlightProviderService>();
            services.AddScoped<IExampleHelper, ExampleHelper>();
            services.AddScoped<ISkySearchSync, SkySearchSyncService>();
            services.AddScoped<IUpdateData, UpdateData>();

            return services;
        }
    }
}
