using Azure.Core;
using SkySearchWorker.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorkerTest.Application.TestDataHelper
{
    public static class DummyTestData
    {
        public static AppSettings GetAppSettings() =>
            new AppSettings()
            {
                IsDevelopment = false,
                Credentials = new Credentials
                {
                    ClientΙd = "ClientΙd",
                    ClientSecret = "ClientSecret",
                    GrantType = "GrantType",
                    AccessToken = "AccessToken"
                },
                Urls = new Urls
                {
                    AuthBase = "https://test.api.amadeus.com/v1/",
                    ShoppingBase = "https://test.api.amadeus.com/v2/",
                    Authenticate = "security/oauth2/token",
                    FlightOffers = "shopping/flight-offers"
                },
                AmadeusClient = "AmadeusClient",
                TestData = new TestData
                {
                    Airports = new List<string>()
                 {
                     "Airport1",
                     "Airport2"
                 },
                    FromDate = "01-01-2025",
                    ToDate = "01-01-2025",
                    MaxConcurrentCalls = 2,
                    DelayBetweenCalls = 1,
                    MaxFlights = 1,
                }
            };
    }
}
