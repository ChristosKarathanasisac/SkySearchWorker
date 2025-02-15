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

        public SkySearchSyncService(ILogger<SkySearchSyncService> logger,
            IAmadeusAuthentication amadeusAuthenticate)
        {
            _logger = logger;
            _amadeusAuthenticate = amadeusAuthenticate;
        }
        public async Task<bool> Sync()
        {
            var authenticate = await _amadeusAuthenticate.Authenticate();

            if (!authenticate)
            {
                _logger.LogError("Failed to authenticate with Amadeus");
                return false;
            }

            return true;

        }
    }
}
