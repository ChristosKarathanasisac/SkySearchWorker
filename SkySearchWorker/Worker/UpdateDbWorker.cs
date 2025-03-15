using Microsoft.Extensions.Options;
using SkySearchWorker.Application.Interfaces;
using SkySearchWorker.Application.Services;
using SkySearchWorker.Infrastructure.Configuration;

namespace SkySearchWorker.Worker
{
    public class UpdateDbWorker : BackgroundService
    {
        private readonly ILogger<UpdateDbWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _appSettings;

        public UpdateDbWorker(ILogger<UpdateDbWorker> logger,
            IServiceProvider serviceProvider,
            IOptions<AppSettings> appSettings)

        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _appSettings = appSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var skySearchSync = scope.ServiceProvider.GetRequiredService<ISkySearchSync>();

                if (!_appSettings.IsDevelopment)
                {
                    _logger.LogInformation("Sync with Amadeus DB");
                    await skySearchSync.Sync();
                }
                else
                {
                    _logger.LogInformation("Test Enviroment - Dummy Data");
                }
            }
        }
    }
}

