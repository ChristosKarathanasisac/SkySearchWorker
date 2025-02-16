using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Interfaces
{
    public interface ICustomHttpClient
    {
        Task<T?> GetAsync<T>(string url, string client);
        Task<T?> PostUrlEncodedAsync<T>(string url, string client, Dictionary<string, string> keyValues);
    }
}
