using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using SkySearchWorker.Application.DTOs.Amadeus.FlightOffer;
using SkySearchWorker.Application.Interfaces;
using SkySearchWorker.Application.Services;
using SkySearchWorker.Infrastructure.Configuration;
using SkySearchWorkerTest.Application.TestDataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorkerTest.Application.Services
{
    public class SkySearchSyncServiceTest
    {
        private readonly ILogger<SkySearchSyncService> _mockLogger;
        private readonly IAmadeusAuthentication _mockAmadeusAuthenticate;
        private readonly IExampleHelper _mockExampleHelper;
        private readonly IAmadeusFlightProvider _mockAmadeusFlightProvider;
        private readonly IUpdateData _mockUpdateData;
        private readonly IUpdateDataHelper _mockUpdateDataHelper;
        private readonly IOptions<AppSettings> _mockAppSettings;
        private readonly AppSettings _appSettings;

        public SkySearchSyncServiceTest()
        {
            _mockLogger = Substitute.For<ILogger<SkySearchSyncService>>();
            _mockAmadeusAuthenticate = Substitute.For<IAmadeusAuthentication>();
            _mockExampleHelper = Substitute.For<IExampleHelper>();
            _mockAmadeusFlightProvider = Substitute.For<IAmadeusFlightProvider>();
            _mockUpdateData = Substitute.For<IUpdateData>();
            _mockUpdateDataHelper = Substitute.For<IUpdateDataHelper>();
            _mockAppSettings = Substitute.For<IOptions<AppSettings>>();
            _appSettings = DummyTestData.GetAppSettings();
            _mockAppSettings.Value.Returns(_appSettings);
        }

        [Fact]
        public async Task Sync_ShouldReturnFalse_WhenAuthenticationFails()
        {
            // Arrange
            _mockAmadeusAuthenticate.Authenticate().Returns(Task.FromResult(false));
            var service = new SkySearchSyncService(_mockLogger, _mockAmadeusAuthenticate, _mockExampleHelper, _mockAppSettings, _mockAmadeusFlightProvider, _mockUpdateData, _mockUpdateDataHelper);

            // Act
            var result = await service.Sync();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Sync_ShouldReturnFalse_WhenNoFlightOffersFound()
        {
            // Arrange
            _mockAmadeusAuthenticate.Authenticate().Returns(Task.FromResult(true));
            _mockExampleHelper.GetGroupedFlightOfferDictionaries().Returns(new List<List<Dictionary<string, string>>>());
            var service = new SkySearchSyncService(_mockLogger, _mockAmadeusAuthenticate, _mockExampleHelper, _mockAppSettings, _mockAmadeusFlightProvider, _mockUpdateData, _mockUpdateDataHelper);

            // Act
            var result = await service.Sync();

            // Assert
            Assert.False(result);
        }
       
        
    }
}
