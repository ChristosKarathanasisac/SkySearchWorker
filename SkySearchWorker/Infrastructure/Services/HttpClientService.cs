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
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(json);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP Request error");
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "HTTP Request timeout");
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON Deserialization error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General Exception");
            }
            return default;
        }

        public async Task<T?> PostUrlEncodedAsync<T>(string url,string client, Dictionary<string, string> keyValues)
        {
            try 
            {
                using var httpClient = _factory.CreateClient(client);

                var content = new FormUrlEncodedContent(keyValues);
                var response = await httpClient.PostAsync(url,content);
                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(json);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP Request error");
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "HTTP Request timeout");
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON Deserialization error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General Exception");
            }
            return default;
        }
    }
}
