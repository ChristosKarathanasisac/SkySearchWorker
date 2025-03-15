using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
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
    public class ExampleHelperTest
    {
        private readonly ILogger<ExampleHelper> _mockLogger;
        private readonly IOptions<AppSettings> _mockAppSettings;
        private readonly AppSettings _appSettings;

        public ExampleHelperTest()
        {
            _mockLogger = Substitute.For<ILogger<ExampleHelper>>();
            _mockAppSettings = Substitute.For<IOptions<AppSettings>>();
            _appSettings = DummyTestData.GetAppSettings();
            _mockAppSettings.Value.Returns(_appSettings);
        }

        [Fact]
        public void GetDateRange_ShouldReturnCorrectDateRange()
        {
            // Arrange
            var exampleHelper = new ExampleHelper(_mockLogger, _mockAppSettings);

            // Act
            var result = exampleHelper.GetDateRange("01-01-2025", "05-01-2025");

            // Assert
            Assert.Equal(5, result.Count);
            Assert.Equal("2025-01-01", result[0]);
            Assert.Equal("2025-01-02", result[1]);
        }

        [Fact]
        public void GetDateRange_ShouldThrowsExeption_WhenFromDateIsBiggerThanToDate()
        {
            // Arrange
            var exampleHelper = new ExampleHelper(_mockLogger, _mockAppSettings);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => exampleHelper.GetDateRange("05-01-2025", "01-01-2025"));
            Assert.Equal("The start date cannot be later than the end date.", exception.Message);
        }
        [Fact]
        public void GetFlightOfferDictionaries_ShouldReturnCorrectDictionaries()
        {
            // Arrange
            var exampleHelper = new ExampleHelper(_mockLogger, _mockAppSettings);

            // Act
            var result = exampleHelper.GetFlightOfferDictionaries();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); 
            Assert.All(result, dict =>
            {
                Assert.Contains("originLocationCode", dict.Keys);
                Assert.Contains("destinationLocationCode", dict.Keys);
                Assert.Contains("departureDate", dict.Keys);
                Assert.Contains("adults", dict.Keys);
                Assert.Contains("nonStop", dict.Keys);
                Assert.Contains("max", dict.Keys);
            });
        }
        [Fact]
        public void GetGroupedFlightOfferDictionaries_ShouldReturnCorrectGroupedDictionaries()
        {
            // Arrange
            var exampleHelper = new ExampleHelper(_mockLogger, _mockAppSettings);

            // Act
            var result = exampleHelper.GetGroupedFlightOfferDictionaries();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count); 
            Assert.All(result, group =>
            {
                Assert.True(group.Count <= 2);
            });
        }
        [Fact]
        public void GetGroupedFlightOfferDictionaries_ShouldReturnMultipleGroups_WhenMoreDictionariesThanBatchSize()
        {
            // Arrange
            _appSettings.TestData.Airports = new List<string> { "JFK", "LAX", "SFO" };
            _appSettings.TestData.FromDate = "01-01-2025";
            _appSettings.TestData.ToDate = "01-01-2025";
            _appSettings.TestData.MaxConcurrentCalls = 2;
            _mockAppSettings.Value.Returns(_appSettings);

            var exampleHelper = new ExampleHelper(_mockLogger, _mockAppSettings);

            // Act
            var result = exampleHelper.GetGroupedFlightOfferDictionaries();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count); 
            Assert.All(result, group =>
            {
                Assert.True(group.Count <= 2);
            });
        }
    }
}
