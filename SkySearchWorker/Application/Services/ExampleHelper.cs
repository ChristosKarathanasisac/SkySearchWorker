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
        private readonly ILogger<ExampleHelper> _logger;
        private readonly AppSettings _appSettings;

        public ExampleHelper(ILogger<ExampleHelper> logger,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }
        public List<string> GetDateRange(string fromDate, string toDate)
        {
            try
            {
                var dateList = new List<string>();
                var startDate = DateTime.ParseExact(fromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                var endDate = DateTime.ParseExact(toDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                if (startDate > endDate)
                    throw new ArgumentException("The start date cannot be later than the end date.");

                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    dateList.Add(date.ToString("yyyy-MM-dd"));
                }

                _logger.LogInformation("Date range from {FromDate} to {ToDate} generated successfully.", fromDate, toDate);
                return dateList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the date range from {FromDate} to {ToDate}.", fromDate, toDate);
                throw;
            }
        }

        public List<Dictionary<string, string>> GetFlightOfferDictionaries()
        {
            try
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
                                        { "max", $"{_appSettings.TestData.MaxFlights}" }
                                    }).ToList();

                _logger.LogInformation("Flight offer dictionaries generated successfully.");
                return dictionaries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating flight offer dictionaries.");
                throw;
            }
        }

        public List<List<Dictionary<string, string>>> GetGroupedFlightOfferDictionaries()
        {
            try
            {
                var dictionaries = GetFlightOfferDictionaries();
                var batchSize = _appSettings.TestData.MaxConcurrentCalls;

                var groupedDictionaries = dictionaries
                    .Select((task, index) => new { task, index })
                    .GroupBy(x => x.index / batchSize)
                    .Select(g => g.Select(x => x.task).ToList())
                    .ToList();

                _logger.LogInformation("Grouped flight offer dictionaries generated successfully.");
                return groupedDictionaries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while grouping flight offer dictionaries.");
                throw;
            }
        }
    }
}
