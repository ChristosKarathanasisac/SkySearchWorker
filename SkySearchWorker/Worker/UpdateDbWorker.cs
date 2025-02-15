using Microsoft.Extensions.Options;
using SkySearchWorker.Application.Interfaces;
using SkySearchWorker.Infrastructure.Configuration;

namespace SkySearchWorker.Worker
{
    public class UpdateDbWorker : BackgroundService
    {
        private readonly ILogger<UpdateDbWorker> _logger;
        private readonly IAmadeusAuthenticate _amadeusAuthenticate;
        private readonly AppSettings _appSettings;

        public UpdateDbWorker(ILogger<UpdateDbWorker> logger,
            IAmadeusAuthenticate amadeusAuthenticate,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _amadeusAuthenticate = amadeusAuthenticate;
            _appSettings = appSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var url = "shopping/flight-offers?originLocationCode=DXB&destinationLocationCode=IST&departureDate=2025-02-18&adults=1&nonStop=false&max=2";
            var authenticate = await _amadeusAuthenticate.Authenticate();
        }
    }
}
