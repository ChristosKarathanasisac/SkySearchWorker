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
        private readonly IAmadeusFlightProvider _amadeusFlightProvider;

        public SkySearchSyncService(ILogger<SkySearchSyncService> logger,
            IAmadeusAuthentication amadeusAuthenticate,
            IExampleHelper exampleHelper,
            IOptions<AppSettings> appSettings,
            IAmadeusFlightProvider amadeusFlightProvider)
        {
            _logger = logger;
            _amadeusAuthenticate = amadeusAuthenticate;
            _appSettings = appSettings.Value;
            _exampleHelper = exampleHelper;
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
            var dictionaryBatches = _exampleHelper.GetGroupedFlightOfferDictionaries();
            var fligthOffers = new List<FlightOfferDto>();

            foreach (var dictionaryBatche in dictionaryBatches)
            {
                var tasks = new List<Task<FlightOfferDto?>>();
                foreach (var dictionary in dictionaryBatche)
                {
                    var task = _amadeusFlightProvider.GetFlightOffers<FlightOfferDto>(dictionary);
                    tasks.Add(task);
                }
                var batchResults = await Task.WhenAll(tasks);
                fligthOffers.AddRange(batchResults.Where(result => result != null).Cast<FlightOfferDto>());

                _logger.LogInformation($"Delay before next call: {_appSettings.TestData.DelayBetweenCalls} ms");
                await Task.Delay(_appSettings.TestData.DelayBetweenCalls);
            }
            var test = fligthOffers;
            return true;
        }
    }
}
