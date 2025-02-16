using Azure.Core;
using Microsoft.Extensions.Options;
using SkySearchWorker.Application.Interfaces;
using SkySearchWorker.Infrastructure.Configuration;
using SkySearchWorker.Infrastructure.Interfaces;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SkySearchWorker.Application.Services
{
    public class AmadeusFlightProviderService : IAmadeusFlightProvider
    {
        private readonly ICustomHttpClient _httpClientService;
        private readonly AppSettings _appSettings;

        public AmadeusFlightProviderService(ICustomHttpClient httpClientService,
            IOptions<AppSettings> appSettings)
        {
            _httpClientService = httpClientService;
            _appSettings = appSettings.Value;
        }
        public async Task<Τ?> GetFlightOffers<Τ>(Dictionary<string, string> keyValueParams)
        {
            var queryParams = HttpUtility.ParseQueryString(string.Empty);
            foreach (var param in keyValueParams)
            {
                queryParams[param.Key] = param.Value;
            }

            string fullUrl = new Uri(new Uri(_appSettings.urls.shoppingBase), _appSettings.urls.flightOffers).ToString();
            var uriBuilder = new UriBuilder(fullUrl)
            {
                Port = -1,
                Query = queryParams.ToString()
            };

            var flights = await _httpClientService.GetAsyncWithBearerAuth<String>(uriBuilder.ToString(),
                _appSettings.amadeusClient,
                _appSettings.credentials.accessToken);

            return default;
        }
    }

}
