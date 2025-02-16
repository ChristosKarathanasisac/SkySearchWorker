using SkySearchWorker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.Services
{
    public class SkySearchSyncService : ISkySearchSync
    {
        private readonly ILogger<SkySearchSyncService> _logger;
        private readonly IAmadeusAuthentication _amadeusAuthenticate;
        private readonly IAmadeusFlightProvider _amadeusFlightProvider;

        public SkySearchSyncService(ILogger<SkySearchSyncService> logger,
            IAmadeusAuthentication amadeusAuthenticate,
            IAmadeusFlightProvider amadeusFlightProvider)
        {
            _logger = logger;
            _amadeusAuthenticate = amadeusAuthenticate;
            _amadeusFlightProvider = amadeusFlightProvider;
        }
        public async Task<bool> Sync()
        {
            var authenticate = await _amadeusAuthenticate.Authenticate();

            if (!authenticate)
            {
                _logger.LogError("Failed to authenticate with Amadeus");
                return false;
            }

            var flights = await _amadeusFlightProvider.GetFlightOffers<string>(new Dictionary<string, string>
            {
                { "originLocationCode", "LHR" },
                { "destinationLocationCode", "DXB" },
                { "departureDate", "2025-04-04" },
                { "adults", "1" },
                { "nonStop", "false" },
                { "max", "2" }
            });

            return true;

        }
    }
}
