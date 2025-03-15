using Azure.Core;
using SkySearchWorker.Application.DTOs.Amadeus.FlightOffer;
using SkySearchWorker.Infrastructure.Configuration;
using SkySearchWorker.Infrastructure.Data.Entities;
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
        public static List<DataDto> GetDataDto() =>
            new List<DataDto>
            {
                new DataDto
                {
                    Itineraries = new List<ItineraryDto>
                    {
                        new ItineraryDto
                        {
                            Segments = new List<SegmentDto>
                            {
                                new SegmentDto
                                {
                                    Departure =new LocationDto
                                    {
                                        IataCode = "JFK",
                                        Terminal = "1",
                                        At = DateTime.Now,
                                    },
                                    Arrival =new LocationDto
                                    {
                                        IataCode = "MIA",
                                        Terminal = "2",
                                        At = DateTime.Now,
                                    },
                                    Operating = new OperatingDto { CarrierCode = "AA" }
                                }
                            }
                        }
                    },
                    Price = new PriceDto
                    {
                        Currency = "EUR",
                        Total = 100.ToString()
                    },
                    TravelerPricings = new List<TravelerPricingDto>
                    {
                        new TravelerPricingDto
                        {
                            FareDetailsBySegment = new List<FareDetailsBySegmentDto>
                            {
                                new FareDetailsBySegmentDto
                                {
                                    Cabin = "economy"
                                }
                            }
                        }
                    },
                    NumberOfBookableSeats = 100
                }
            };
        public static Airport GetJFKAirport() =>
            new Airport { 
                Id = 1,
                CityCode = "NY",
                Code = "JFK",
                CountryCode = "USA" 
            };
        public static Airport GetMIAAirport() =>
            new Airport
            {
                Id = 1,
                CityCode = "MIA",
                Code = "MIA",
                CountryCode = "USA"
            };
        public static Airline GetAAAirline() =>
            new Airline
            {
                Id = 1,
                Name = "American Airlines",
                Code = "AA"
            };
        public static List<FlightOfferDto> GetFlightOfferDtos() =>
            new List<FlightOfferDto>
            {
                new FlightOfferDto
                {
                    Dictionary = new DictionaryDto
                    {
                        Locations = new Dictionary<string, DictionaryLocationDto>
                        {
                            { "JFK", new DictionaryLocationDto { CityCode = "NYC", CountryCode = "US" } },
                            { "LAX", new DictionaryLocationDto { CityCode = "LA", CountryCode = "US" } }
                        },
                        Carriers = new Dictionary<string, string>
                        {
                            { "AA", "American Airlines" },
                            { "DL", "Delta Airlines" }
                        },
                        Aircraft = new Dictionary<string, string>
                        {
                            { "737", "Boeing 737" },
                            { "320", "Airbus A320" }
                        },
                        Currencies = new Dictionary<string, string>
                        {
                            { "USD", "United States Dollar" },
                            { "EUR", "Euro" }
                        }
                    },
                    Data = new List<DataDto>
                    {
                        new DataDto
                        {
                            Id = "1",
                            Type = "flight-offer",
                            Source = "GDS",
                            InstantTicketingRequired = false,
                            NonHomogeneous = false,
                            OneWay = false,
                            IsUpsellOffer = false,
                            LastTicketingDate = "2025-03-10",
                            LastTicketingDateTime = "2025-03-10T23:59:59",
                            NumberOfBookableSeats = 5,
                            Itineraries = new List<ItineraryDto>
                            {
                                new ItineraryDto
                                {
                                    Duration = "PT5H",
                                    Segments = new List<SegmentDto>
                                    {
                                        new SegmentDto
                                        {
                                            Departure = new LocationDto
                                            {
                                                At = DateTime.Now,
                                                IataCode = "JFK"
                                            },
                                            Arrival = new LocationDto
                                            {
                                                At = DateTime.Now.AddHours(5),
                                                IataCode = "LAX"
                                            },
                                            Operating = new OperatingDto
                                            {
                                                CarrierCode = "AA"
                                            }
                                        }
                                    }
                                }
                            },
                            Price = new PriceDto
                            {
                                Currency = "USD",
                                Total = "500.00",
                                Base = "450.00",
                                Fees = new List<FeeDto>
                                {
                                    new FeeDto
                                    {
                                        Amount = "50.00",
                                        Type = "Service Fee"
                                    }
                                },
                                GrandTotal = "500.00"
                            },
                            PricingOptions = new PricingOptionsDto
                            {
                                FareType = new List<string> { "PUBLISHED" },
                                IncludedCheckedBagsOnly = true
                            },
                            ValidatingAirlineCodes = new List<string> { "AA" },
                            TravelerPricings = new List<TravelerPricingDto>
                            {
                                new TravelerPricingDto
                                {
                                    TravelerId = "1",
                                    FareOption = "STANDARD",
                                    TravelerType = "ADULT",
                                    Price = new PriceDto
                                    {
                                        Currency = "USD",
                                        Total = "500.00",
                                        Base = "450.00"
                                    },
                                    FareDetailsBySegment = new List<FareDetailsBySegmentDto>
                                    {
                                        new FareDetailsBySegmentDto
                                        {
                                            Cabin = "ECONOMY"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
    }
}
