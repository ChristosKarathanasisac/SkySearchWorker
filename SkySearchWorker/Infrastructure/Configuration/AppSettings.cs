using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Configuration
{
    public class AppSettings
    {
        public required Credentials credentials { get; set; }
        public required Urls urls { get; set; }
        public required string amadeusClient { get; set; }
    }
    public class Credentials
    {
        public required string clientΙd { get; set; }
        public required string clientSecret { get; set; }
        public required string grantType { get; set; }
        public required string accessToken { get; set; }
    }
    public class Urls
    {
        public required string baseUrl { get; set; }
        public required string authenticate { get; set; }
    }
}
