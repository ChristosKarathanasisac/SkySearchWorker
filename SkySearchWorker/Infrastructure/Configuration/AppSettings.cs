using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Configuration
{
    public class AppSettings
    {
        public required Credentials Credentials { get; set; }
        public required Urls Urls { get; set; }
        public required string AmadeusClient = "amadeus";
        public required TestData TestData { get; set; }
    }
    public class Credentials
    {
        public required string ClientΙd { get; set; }
        public required string ClientSecret { get; set; }
        public required string GrantType { get; set; }
        public required string AccessToken { get; set; }
    }
    public class Urls
    {
        public required string AuthBase { get; set; }
        public required string ShoppingBase { get; set; }
        public required string Authenticate { get; set; }
        public required string FlightOffers { get; set; }
    }
    public class TestData
    {
        public required List<string> Airports { get; set; }
        public required string FromDate { get; set; }
        public required string ToDate { get; set; }
        public required int MaxConcurrentCalls { get; set; }
        public required int DelayBetweenCalls { get; set; }
    }
}
