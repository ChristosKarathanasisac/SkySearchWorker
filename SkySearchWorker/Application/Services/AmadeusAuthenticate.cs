using Microsoft.Extensions.Options;
using SkySearchWorker.Application.DTOs.Amadeus.Authentication;
using SkySearchWorker.Application.Interfaces;
using SkySearchWorker.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.Services
{
    public class AmadeusAuthenticate : IAmadeusAuthenticate
    {
        private readonly ILogger<AmadeusAuthenticate> _logger;
        private readonly IHttpClientService _httpClientService;
        private readonly AppSettings _appSettings;

        public AmadeusAuthenticate(ILogger<AmadeusAuthenticate> logger,
            IHttpClientService httpClientService,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _httpClientService = httpClientService;
            _appSettings = appSettings.Value;
        }
        public async Task<bool> Authenticate()
        {
            var values = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", _appSettings.credentials.clientΙd },
                { "client_secret", _appSettings.credentials.clientSecret }
            };

            var response = await _httpClientService.PostUrlEncodedAsync<AuthenticationResponseDto>(_appSettings.urls.authenticate, _appSettings.amadeusClient, values);

            if (response != null)
            {
                _appSettings.credentials.accessToken = response.access_token!;
                return true;
            }

            return false;
        }
    }
}
