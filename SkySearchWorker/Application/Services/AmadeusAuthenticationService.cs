using Microsoft.Extensions.Options;
using SkySearchWorker.Application.DTOs.Amadeus.Authentication;
using SkySearchWorker.Application.Interfaces;
using SkySearchWorker.Infrastructure.Configuration;
using SkySearchWorker.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.Services
{
    public class AmadeusAuthenticationService : IAmadeusAuthentication
    {
        private readonly ICustomHttpClient _httpClientService;
        private readonly AppSettings _appSettings;

        public AmadeusAuthenticationService(ICustomHttpClient httpClientService,
            IOptions<AppSettings> appSettings)
        {
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

            var url = new Uri(new Uri(_appSettings.urls.authBase), _appSettings.urls.authenticate).ToString();
            var response = await _httpClientService.PostUrlEncodedAsync<AuthenticationResponseDto>(url, _appSettings.amadeusClient, values);

            if (response != null)
            {
                _appSettings.credentials.accessToken = response.access_token!;
                return true;
            }

            return false;
        }
    }
}
