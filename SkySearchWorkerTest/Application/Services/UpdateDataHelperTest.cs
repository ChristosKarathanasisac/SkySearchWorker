using SkySearchWorker.Application.DTOs.Amadeus.FlightOffer;
using SkySearchWorker.Application.Services;
using SkySearchWorkerTest.Application.TestDataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorkerTest.Application.Services
{
    public class UpdateDataHelperTest
    {
        private readonly UpdateDataHelper _updateDataHelper;

        public UpdateDataHelperTest()
        {
            _updateDataHelper = new UpdateDataHelper();
        }

        [Fact]
        public void GetUniqueAirports_ShouldReturnUniqueAirports()
        {
            // Arrange
            var flightOffers = DummyTestData.GetFlightOfferDtos();

            // Act
            var result = _updateDataHelper.GetUniqueAirports(flightOffers);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("JFK", result.Keys);
            Assert.Contains("LAX", result.Keys);
        }

        [Fact]
        public void GetUniqueCarriers_ShouldReturnUniqueCarriers()
        {
            // Arrange
            var flightOffers = DummyTestData.GetFlightOfferDtos();

            // Act
            var result = _updateDataHelper.GetUniqueCarriers(flightOffers);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("AA", result.Keys);
            Assert.Contains("DL", result.Keys);
        }

        [Fact]
        public void GetUniqueFlights_ShouldReturnUniqueFlights()
        {
            // Arrange
            var flightOffers = DummyTestData.GetFlightOfferDtos();

            // Act
            var result = _updateDataHelper.GetUniqueFlights(flightOffers);

            // Assert
            Assert.Single(result);
        }
    }
}
