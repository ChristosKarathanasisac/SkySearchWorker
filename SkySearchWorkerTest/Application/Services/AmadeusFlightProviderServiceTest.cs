using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using SkySearchWorker.Application.Services;
using SkySearchWorker.Infrastructure.Configuration;
using SkySearchWorker.Infrastructure.Interfaces;
using SkySearchWorkerTest.Application.TestDataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorkerTest.Application.Services
{
    public class AmadeusFlightProviderServiceTest
    {
        private readonly ICustomHttpClient _mockHttpClient;
        private readonly IOptions<AppSettings> _mockAppSettings;
        private readonly ILogger<AmadeusFlightProviderService> _mockLogger;
        private readonly AppSettings _appSettings;

        public AmadeusFlightProviderServiceTest()
        {
            _mockHttpClient = Substitute.For<ICustomHttpClient>();
            _mockAppSettings = Substitute.For<IOptions<AppSettings>>();
            _mockLogger = Substitute.For<ILogger<AmadeusFlightProviderService>>();
            _appSettings = DummyTestData.GetAppSettings();
            _mockAppSettings.Value.Returns(_appSettings);
        }

        [Fact]
        public async Task GetFlightOffers_ShouldReturnFlightOffers_WhenRequestIsSuccessful()
        {
            // Arrange
            var expectedResponse = new List<string> { "Flight1", "Flight2" };
            _mockHttpClient.GetAsyncWithBearerAuth<List<string>>(
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(expectedResponse);

            var service = new AmadeusFlightProviderService(_mockLogger, _mockHttpClient, _mockAppSettings);
            var keyValueParams = new Dictionary<string, string>
            {
                { "originLocationCode", "JFK" },
                { "destinationLocationCode", "LAX" },
                { "departureDate", "2025-03-10" }
            };

            // Act
            var result = await service.GetFlightOffers<List<string>>(keyValueParams);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task GetFlightOffers_ShouldReturnNull_WhenRequestFails()
        {
            // Arrange
            _mockHttpClient.GetAsyncWithBearerAuth<List<string>>(
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns((List<string>)null);

            var service = new AmadeusFlightProviderService(_mockLogger, _mockHttpClient, _mockAppSettings);
            var keyValueParams = new Dictionary<string, string>
            {
                { "originLocationCode", "JFK" },
                { "destinationLocationCode", "LAX" },
                { "departureDate", "2025-03-10" }
            };

            // Act
            var result = await service.GetFlightOffers<List<string>>(keyValueParams);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task GetFlightOffers_ShouldReturnEmptyList_WhenThereIsN()
        {
            // Arrange
            _mockHttpClient.GetAsyncWithBearerAuth<List<string>>(
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(new List<string>());

            var service = new AmadeusFlightProviderService(_mockLogger, _mockHttpClient, _mockAppSettings);
            var keyValueParams = new Dictionary<string, string>
            {
                { "originLocationCode", "JFK" },
                { "destinationLocationCode", "LAX" },
                { "departureDate", "2025-03-10" }
            };

            // Act
            var result = await service.GetFlightOffers<List<string>>(keyValueParams);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
