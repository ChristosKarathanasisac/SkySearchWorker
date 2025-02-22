using Microsoft.Extensions.Options;
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
    public class ExampleHelper : IExampleHelper
    {
        private readonly IAmadeusFlightProvider _amadeusFlightProvider;
        private readonly AppSettings _appSettings;

        public ExampleHelper(IAmadeusFlightProvider amadeusFlightProvider,
            IOptions<AppSettings> appSettings)
        {
            _amadeusFlightProvider = amadeusFlightProvider;
            _appSettings = appSettings.Value;
        }
        public List<string> GetDateRange(string fromDate, string toDate)
        {
            var dateList = new List<string>();
            var startDate = DateTime.ParseExact(fromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(toDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                dateList.Add(date.ToString("yyyy-MM-dd"));
            }
            return dateList; 
        }

        public List<Dictionary<string,string>> GetFlightOfferDictionaries()
        {
            var airports = _appSettings.TestData.Airports;
            var dates = GetDateRange(_appSettings.TestData.FromDate, _appSettings.TestData.ToDate);

            var dictionaries = (from date in dates
                                from origin in airports
                                from destination in airports
                                where origin != destination
                                select new Dictionary<string, string>
                                {
                                    { "originLocationCode", origin },
                                    { "destinationLocationCode", destination },
                                    { "departureDate", date },
                                    { "adults", "1" },
                                    { "nonStop", "true" },
                                    { "max", "1" }
                                }).ToList();

            return dictionaries;
        }

        public List<List<Dictionary<string, string>>> GetGroupedFlightOfferDictionaries()
        {
            var dictionaries = GetFlightOfferDictionaries();
            var batchSize = _appSettings.TestData.MaxConcurrentCalls;

            var groupedDictionaries = dictionaries
                .Select((task, index) => new { task, index })
                .GroupBy(x => x.index / batchSize)
                .Select(g => g.Select(x => x.task).ToList())
                .ToList();

            return groupedDictionaries;
        }
    }
}
