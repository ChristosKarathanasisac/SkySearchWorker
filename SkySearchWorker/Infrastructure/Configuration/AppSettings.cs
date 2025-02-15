using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Configuration
{
    public class AppSettings
    {
        public required string BaseUri { get; set; }
        public required string AccessToken { get; set; }
    }
}
