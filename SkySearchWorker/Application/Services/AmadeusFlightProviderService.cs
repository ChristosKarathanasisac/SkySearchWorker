using Microsoft.Extensions.Options;
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
        public Task<Τ?> GetFlightOffers<Τ>(Dictionary<string, string> keyValueParams)
        {
            throw new NotImplementedException();
        }

        private string BuildUrl(Dictionary<string, string> keyValueParams)
        {
            var url = _appSettings.urls.flightOffers;
            foreach (var key in keyValueParams.Keys)
            {
                url = url.Replace($"{{{key}}}", keyValueParams[key]);
            }
            return url;
        }
    }
}
