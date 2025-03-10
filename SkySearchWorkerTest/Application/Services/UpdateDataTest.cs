using Microsoft.Extensions.Logging;
using NSubstitute;
using SkySearchWorker.Application.DTOs.Amadeus.FlightOffer;
using SkySearchWorker.Application.Services;
using SkySearchWorker.Infrastructure.Data.Entities;
using SkySearchWorker.Infrastructure.Data.Interfaces;
using SkySearchWorkerTest.Application.TestDataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorkerTest.Application.Services
{
    public class UpdateDataTest
    {
        private readonly ILogger<UpdateData> _mockLogger;
        private readonly IUnitOfWork _mockUnitOfWork;
        private readonly IAirlineRepository _mockAirlineRepository;
        private readonly IAirportRepository _mockAirportRepository;
        private readonly IFlightRepository _mockFlightRepository;
        private readonly IFlightPriceRepository _mockFlightPriceRepository;
        private readonly UpdateData _updateData;

        public UpdateDataTest()
        {
            _mockLogger = Substitute.For<ILogger<UpdateData>>();
            _mockUnitOfWork = Substitute.For<IUnitOfWork>();
            _mockAirlineRepository = Substitute.For<IAirlineRepository>();
            _mockAirportRepository = Substitute.For<IAirportRepository>();
            _mockFlightRepository = Substitute.For<IFlightRepository>();
            _mockFlightPriceRepository = Substitute.For<IFlightPriceRepository>();
            _updateData = new UpdateData(_mockLogger, _mockUnitOfWork, _mockAirlineRepository, _mockAirportRepository, _mockFlightRepository, _mockFlightPriceRepository);
        }

        [Fact]
        public async Task UpdateAirlines_ShouldReturnTrue_WhenAirlinesAreUpdatedSuccessfully()
        {
            // Arrange
            var carriers = new Dictionary<string, string> { { "AA", "American Airlines" } };
            _mockAirlineRepository.AirlineCodeExistsAsync("AA").Returns(Task.FromResult(false));

            // Act
            var result = await _updateData.UpdateAirlines(carriers);

            // Assert
            Assert.True(result);
            await _mockAirlineRepository.Received(1).AddAsync(Arg.Any<Airline>());
            await _mockUnitOfWork.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task UpdateAirports_ShouldReturnTrue_WhenAirportsAreUpdatedSuccessfully()
        {
            // Arrange
            var locations = new Dictionary<string, DictionaryLocationDto>
            {
                { "JFK", new DictionaryLocationDto { CityCode = "NYC", CountryCode = "US" } }
            };
            _mockAirportRepository.AirportCodeExistsAsync("JFK").Returns(Task.FromResult(false));

            // Act
            var result = await _updateData.UpdateAirports(locations);

            // Assert
            Assert.True(result);
            await _mockAirportRepository.Received(1).AddAsync(Arg.Any<Airport>());
            await _mockUnitOfWork.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task UpdateFlights_ShouldReturnTrue_WhenFlightsAreUpdatedSuccessfully()
        {
            // Arrange
            var dataDtos = DummyTestData.GetDataDto();
            _mockFlightRepository.FlightExistAsync(Arg.Any<DateTime>(), Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<string>()).Returns(Task.FromResult(false));
            _mockAirportRepository.GetAirportAsync("JFK").Returns(Task.FromResult(DummyTestData.GetJFKAirport()));
            _mockAirportRepository.GetAirportAsync("MIA").Returns(Task.FromResult(DummyTestData.GetMIAAirport()));
            _mockAirlineRepository.GetAirlineAsync("AA").Returns(Task.FromResult(DummyTestData.GetAAAirline()));

            // Act
            var result = await _updateData.UpdateFlights(dataDtos);

            // Assert
            Assert.True(result);
            await _mockFlightRepository.Received(1).AddAsync(Arg.Any<Flight>());
            await _mockUnitOfWork.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task UpdateFlightPrices_ShouldReturnTrue_WhenFlightPricesAreUpdatedSuccessfully()
        {
            // Arrange
            var dataDtos = DummyTestData.GetDataDto();
            _mockFlightRepository.GetFlight(Arg.Any<DateTime>(), Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<string>()).Returns(Task.FromResult(new Flight { Id = 1 }));

            // Act
            var result = await _updateData.UpdateFlightPrices(dataDtos);

            // Assert
            Assert.True(result);
            await _mockFlightPriceRepository.Received(1).AddAsync(Arg.Any<FlightPrice>());
            await _mockUnitOfWork.Received(1).SaveChangesAsync();
        }
    }
}
