using Microsoft.Extensions.Options;
using SkySearchWorker.Application.Interfaces;
using SkySearchWorker.Application.Services;
using SkySearchWorker.Infrastructure.Configuration;

namespace SkySearchWorker.Worker
{
    public class UpdateDbWorker : BackgroundService
    {
        private readonly ILogger<UpdateDbWorker> _logger;
        private readonly ISkySearchSync _skySearchSync;

        public UpdateDbWorker(ILogger<UpdateDbWorker> logger,
            ISkySearchSync skySearchSync
            )
        {
            _logger = logger;
            _skySearchSync = skySearchSync;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var url = "shopping/flight-offers?originLocationCode=DXB&destinationLocationCode=IST&departureDate=2025-02-18&adults=1&nonStop=false&max=2";
            //var authenticate = await _amadeusAuthenticate.Authenticate();
            await _skySearchSync.Sync();
        }
    }
}
