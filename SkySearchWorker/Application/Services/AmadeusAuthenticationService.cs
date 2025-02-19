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
                { "client_id", _appSettings.Credentials.ClientΙd },
                { "client_secret", _appSettings.Credentials.ClientSecret }
            };

            var url = new Uri(new Uri(_appSettings.Urls.AuthBase), _appSettings.Urls.Authenticate).ToString();
            var response = await _httpClientService.PostUrlEncodedAsync<AuthenticationResponseDto>(url, _appSettings.AmadeusClient, values);

            if (response != null)
            {
                _appSettings.Credentials.AccessToken = response.AccessToken!;
                return true;
            }

            return false;
        }
    }
}
