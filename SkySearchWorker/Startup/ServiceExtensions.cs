using Azure.Core;
using SkySearchWorker.Application.Interfaces;
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
            services.AddHttpClient("amadeus", client =>
            {
                client.BaseAddress = new Uri("https://test.api.amadeus.com/v2/");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {"sTsreNWsEolzxTVgNbIBLFP5RTW1"}");
            });
            services.AddSingleton<IHttpClientService, HttpClientService>();
            return services;
        }
    }
}
