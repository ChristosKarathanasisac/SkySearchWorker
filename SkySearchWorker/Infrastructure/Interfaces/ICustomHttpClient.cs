using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Interfaces
{
    public interface ICustomHttpClient
    {
        Task<T?> GetAsyncWithBearerAuth<T>(string url, string client, string accessToken);
        Task<T?> PostUrlEncodedAsync<T>(string url, string client, Dictionary<string, string> keyValues);
    }
}
