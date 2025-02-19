using Microsoft.Extensions.Options;
using SkySearchWorker.Application.DTOs.Amadeus.FlightOffer;
using SkySearchWorker.Application.Interfaces;
using SkySearchWorker.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.Services
{
    public class SkySearchSyncService : ISkySearchSync
    {
        private readonly ILogger<SkySearchSyncService> _logger;
        private readonly IAmadeusAuthentication _amadeusAuthenticate;
        private readonly IExampleHelper _exampleHelper;
        private readonly AppSettings _appSettings;

        public SkySearchSyncService(ILogger<SkySearchSyncService> logger,
            IAmadeusAuthentication amadeusAuthenticate,
            IExampleHelper exampleHelper,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _amadeusAuthenticate = amadeusAuthenticate;
            _appSettings = appSettings.Value;
            _exampleHelper = exampleHelper;
        }
        public async Task<bool> Sync()
        {
            var authenticate = await _amadeusAuthenticate.Authenticate();

            if (!authenticate)
            {
                _logger.LogError("Failed to authenticate with Amadeus");
                return false;
            }

            var tasks = _exampleHelper.GetFlightOfferTasks();

            try
            {
                var fligthOffers = await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                var test = ex.ToString();
            }
          
            return true;

        }
    }
}
