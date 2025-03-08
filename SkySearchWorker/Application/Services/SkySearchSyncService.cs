using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
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
        private readonly IAmadeusFlightProvider _amadeusFlightProvider;
        private readonly IUpdateData _updateData;
        private readonly IUpdateDataHelper _updateDataHelper;
        private readonly AppSettings _appSettings;

        public SkySearchSyncService(ILogger<SkySearchSyncService> logger,
            IAmadeusAuthentication amadeusAuthenticate,
            IExampleHelper exampleHelper,
            IOptions<AppSettings> appSettings,
            IAmadeusFlightProvider amadeusFlightProvider,
            IUpdateData updateData,
            IUpdateDataHelper updateDataHelper)
        {
            _logger = logger;
            _amadeusAuthenticate = amadeusAuthenticate;
            _appSettings = appSettings.Value;
            _exampleHelper = exampleHelper;
            _amadeusFlightProvider = amadeusFlightProvider;
            _updateData = updateData;
            _updateDataHelper = updateDataHelper;
        }
        public async Task<bool> Sync()
        {

            var authenticate = await _amadeusAuthenticate.Authenticate();

            if (!authenticate)
            {
                _logger.LogError("Failed to authenticate with Amadeus");
                return false;
            }

            var fligthOffers = await GetFlightOffersAsync();
            if (fligthOffers.Count < 1) 
            {
                _logger.LogInformation("No offers found.");
                return false;
            }

            var uniqueAirports = _updateDataHelper.GetUniqueAirports(fligthOffers);
            if(!(await _updateData.UpdateAirports(uniqueAirports)))
                return false;

            var uniqueCarriers = _updateDataHelper.GetUniqueCarriers(fligthOffers);
            if(!(await _updateData.UpdateAirlines(uniqueCarriers)))
                return false;

            var uniqueFlights = _updateDataHelper.GetUniqueFlights(fligthOffers);
            if (!(await _updateData.UpdateFlights(uniqueFlights)))
                return false;

            return true;
        }
        private async Task<List<FlightOfferDto>> GetFlightOffersAsync()
        {
            try
            {
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
                _logger.LogInformation("Flight offers retrieved successfully.");
                return fligthOffers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving flight offers.");
                return new List<FlightOfferDto>();
            }
        }
    }
}
