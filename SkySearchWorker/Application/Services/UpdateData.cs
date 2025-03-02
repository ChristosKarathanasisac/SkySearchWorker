using SkySearchWorker.Application.DTOs.Amadeus.FlightOffer;
using SkySearchWorker.Application.Interfaces;
using SkySearchWorker.Infrastructure.Data.Entities;
using SkySearchWorker.Infrastructure.Data.Interfaces;
using SkySearchWorker.Infrastructure.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.Services
{
    public class UpdateData : IUpdateData
    {
        private readonly ILogger<UpdateData> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateData(ILogger<UpdateData> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> UpdateDatabase(List<FlightOfferDto> flightOffers)
        {
            try
            {
                _logger.LogInformation("Starting database update.");

                var uniqueCarriers = flightOffers
                    .SelectMany(fo => fo.Dictionary.Carriers ?? new Dictionary<string, string>())
                    .GroupBy(c => c.Key)
                    .Select(g => g.First())
                    .ToDictionary(c => c.Key, c => c.Value);
                await UpdateAirlines(uniqueCarriers);


                var uniqueAirports = flightOffers
                    .SelectMany(fo => fo.Dictionary.Locations ?? new Dictionary<string, DictionaryLocationDto>())
                    .GroupBy(c => c.Key)
                    .Select(g => g.First())
                    .ToDictionary(c => c.Key, c => c.Value);
                await UpdateAirports(uniqueAirports);
                //await UpdateFlights();
                //await UpdateFlightPrices();

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Database update completed successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the database.");
                return false;
            }
            
        }
        private async Task UpdateAirlines(Dictionary<string, string> carriers) 
        {
            var airlineRepo = _unitOfWork.Airlines;
            foreach (var airlineCode in carriers.Keys) 
            {
                var existingAirline = await airlineRepo.AirlineCodeExistsAsync(airlineCode);
                if (!existingAirline)
                {
                    var airline = new Airline { 
                        Name = carriers.GetValueOrDefault(airlineCode), 
                        Code = airlineCode 
                    };
                    await airlineRepo.AddAsync(airline);
                }
            }
        }

        private async Task UpdateAirports(Dictionary<string, DictionaryLocationDto> locations)
        {
            var airportRepo = _unitOfWork.Airports;
            foreach (var airportCode in locations.Keys) 
            {
                var existingAirport = await airportRepo.AirportCodeExistsAsync(airportCode);
                if (!existingAirport) 
                {
                    var value = locations.GetValueOrDefault(airportCode);
                    var airport = new Airport{
                        Code = airportCode,
                        CityCode = value!.CityCode,
                        CountryCode = value!.CountryCode
                    };
                    await airportRepo.AddAsync(airport);
                }
            }
        }

        private async Task UpdateFlights() 
        {

        }
        private async Task UpdateFlightPrices()
        {

        }
    }
}
