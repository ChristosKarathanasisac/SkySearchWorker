using SkySearchWorker.Application.Interfaces;

namespace SkySearchWorker.Worker
{
    public class UpdateDbWorker : BackgroundService
    {
        private readonly ILogger<UpdateDbWorker> _logger;
        private readonly IHttpClientService _httpClientService;

        public UpdateDbWorker(ILogger<UpdateDbWorker> logger, IHttpClientService httpClientService)
        {
            _logger = logger;
            _httpClientService = httpClientService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var url = "shopping/flight-offers?originLocationCode=DXB&destinationLocationCode=IST&departureDate=2025-02-14&adults=1&nonStop=false&max=2";
            var data = await _httpClientService.GetAsync<List<String>>(url,"amadeus");
        }
    }
}
