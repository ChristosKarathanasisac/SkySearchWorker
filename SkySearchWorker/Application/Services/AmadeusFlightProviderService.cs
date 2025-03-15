using Azure.Core;
using Microsoft.Extensions.Options;
using SkySearchWorker.Application.DTOs.Amadeus.FlightOffer;
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
        private readonly ILogger<AmadeusFlightProviderService> _logger;
        private readonly ICustomHttpClient _httpClientService;
        private readonly AppSettings _appSettings;

        public AmadeusFlightProviderService(ILogger<AmadeusFlightProviderService> logger,
            ICustomHttpClient httpClientService,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _httpClientService = httpClientService;
            _appSettings = appSettings.Value;
        }
        public async Task<T?> GetFlightOffers<T>(Dictionary<string, string> keyValueParams)
        {
            try
            {
                var queryParams = HttpUtility.ParseQueryString(string.Empty);
                foreach (var param in keyValueParams)
                {
                    queryParams[param.Key] = param.Value;
                }

                string fullUrl = new Uri(new Uri(_appSettings.Urls.ShoppingBase), _appSettings.Urls.FlightOffers).ToString();
                var uriBuilder = new UriBuilder(fullUrl)
                {
                    Port = -1,
                    Query = queryParams.ToString()
                };

                _logger.LogInformation("Sending flight offers request to {Url}", uriBuilder.ToString());
                var flights = await _httpClientService.GetAsyncWithBearerAuth<T>(uriBuilder.ToString(),
                    _appSettings.AmadeusClient,
                    _appSettings.Credentials.AccessToken);

                if (flights != null)
                {
                    _logger.LogInformation("Flight offers retrieved successfully.");
                }
                else
                {
                    _logger.LogWarning("No flight offers retrieved.");
                }

                return flights;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving flight offers.");
                return default;
            }
        }
    }

}
