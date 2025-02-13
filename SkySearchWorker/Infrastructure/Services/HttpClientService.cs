using Azure.Core;
using SkySearchWorker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly ILogger<HttpClientService> _logger;
        private readonly IHttpClientFactory _factory;

        public HttpClientService(ILogger<HttpClientService> logger, IHttpClientFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }
        public async Task<T?> GetAsync<T>(string url, string client)
        {
            try
            {
                using var httpClient = _factory.CreateClient(client);
                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("HTTP Request to {Url} failed with status code {StatusCode}", url, response.StatusCode);
                    return default;
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP Request error while calling {Url}", url);
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "HTTP Request timeout while calling {Url}", url);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON Deserialization error for response from {Url}", url);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Error while fetching data from {url}", url);
                
            }
            return default;
        }
    }
}
