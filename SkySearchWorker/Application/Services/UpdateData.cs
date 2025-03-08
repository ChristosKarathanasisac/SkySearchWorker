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
        private readonly IAirlineRepository _airlineRepository;
        private readonly IAirportRepository _airportRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IFlightPriceRepository _flightPriceRepository;

        public UpdateData(ILogger<UpdateData> logger,
            IUnitOfWork unitOfWork,
            IAirlineRepository airlineRepository,
            IAirportRepository airportRepository,
            IFlightRepository flightRepository,
            IFlightPriceRepository flightPriceRepository)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _airlineRepository = airlineRepository;
            _airportRepository = airportRepository;
            _flightRepository = flightRepository;
            _flightPriceRepository = flightPriceRepository;
        }
        public async Task<bool> UpdateAirlines(Dictionary<string, string> carriers)
        {
            try
            {
                var addedAirline = false;
                foreach (var airlineCode in carriers.Keys)
                {
                    var existingAirline = await _airlineRepository.AirlineCodeExistsAsync(airlineCode);
                    if (!existingAirline)
                    {
                        addedAirline = true;
                        var value = carriers.GetValueOrDefault(airlineCode);
                        var airline = new Airline
                        {
                            Name = value!,
                            Code = airlineCode
                        };
                        await _airlineRepository.AddAsync(airline);
                    }
                }
                if (addedAirline)
                    await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Airlines updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating airlines.");
                return false;
            }
        }
        public async Task<bool> UpdateAirports(Dictionary<string, DictionaryLocationDto> locations)
        {
            try
            {
                var addedAirports = false;
                foreach (var airportCode in locations.Keys)
                {
                    var existingAirport = await _airportRepository.AirportCodeExistsAsync(airportCode);
                    if (!existingAirport)
                    {
                        addedAirports = true;
                        var value = locations.GetValueOrDefault(airportCode);
                        var airport = new Airport
                        {
                            Code = airportCode,
                            CityCode = value!.CityCode,
                            CountryCode = value!.CountryCode
                        };
                        await _airportRepository.AddAsync(airport);
                    }
                }
                if (addedAirports)
                    await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Airports updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating airports.");
                return false;
            }
        }
        public async Task<bool> UpdateFlights(List<DataDto> dataDtos)
        {
            try
            {
                var addedFlight = false;
                foreach (var data in dataDtos)
                {
                    var itenary = data.Itineraries!.FirstOrDefault();
                    var segment = itenary!.Segments!.FirstOrDefault();
                    var existingFlight = await _flightRepository.FlightExistAsync(
                        segment!.Departure!.At,
                        segment!.Departure!.IataCode!,
                        segment!.Arrival!.At,
                        segment!.Arrival!.IataCode!);

                    if (!existingFlight)
                    {
                        addedFlight = true;
                        var departureAirport = await _airportRepository.GetAirportAsync(segment!.Departure!.IataCode!);
                        var arrivalAirport = await _airportRepository.GetAirportAsync(segment!.Arrival!.IataCode!);
                        var airline = await _airlineRepository.GetAirlineAsync(segment!.Operating!.CarrierCode!);
                        var flight = new Flight
                        {
                            DepartureTime = segment!.Departure!.At,
                            DepartureAirportId = departureAirport.Id,
                            ArrivalTime = segment!.Arrival!.At,
                            ArrivalAirportId = arrivalAirport.Id,
                            AirlineId = airline.Id,
                            NumberOfAvailableSeats = data.NumberOfBookableSeats
                        };

                        await _flightRepository.AddAsync(flight);
                    }
                }
                if (addedFlight)
                    await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Flights updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating flights.");
                return false;
            }
        }
        private async Task UpdateFlightPrices()
        {

        }
    }
}
