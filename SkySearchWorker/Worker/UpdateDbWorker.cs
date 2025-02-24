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
        private readonly AppSettings _appSettings;

        public UpdateDbWorker(ILogger<UpdateDbWorker> logger,
            ISkySearchSync skySearchSync,
            IOptions<AppSettings> appSettings)

        {
            _logger = logger;
            _skySearchSync = skySearchSync;
            _appSettings = appSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_appSettings.IsDevelopment)
            {
                _logger.LogInformation("Sync with Amadeus DB");
                await _skySearchSync.Sync();
            }
            else 
            {
                _logger.LogInformation("Test Enviroment - Dummy Data");
            }
        }
    }
}

