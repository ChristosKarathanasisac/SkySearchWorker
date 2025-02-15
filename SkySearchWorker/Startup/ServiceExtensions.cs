using Azure.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SkySearchWorker.Application.Interfaces;
using SkySearchWorker.Infrastructure.Configuration;
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
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("Amadeus"));
            services.AddHttpClient("amadeus", (serviceProvider, client) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
                client.BaseAddress = new Uri(settings.BaseUri);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {settings.AccessToken}");
            });
            services.AddSingleton<IHttpClientService, HttpClientService>();
            return services;
        }
    }
}
