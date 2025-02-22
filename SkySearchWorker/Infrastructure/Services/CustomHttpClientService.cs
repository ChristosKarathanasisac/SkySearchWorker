using Azure.Core;
using SkySearchWorker.Infrastructure.Configuration;
using SkySearchWorker.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkySearchWorker.Infrastructure.Services
{
    public class CustomHttpClientService : ICustomHttpClient
    {
        private readonly ILogger<CustomHttpClientService> _logger;
        private readonly IHttpClientFactory _factory;

        public CustomHttpClientService(ILogger<CustomHttpClientService> logger,
            IHttpClientFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }
        public async Task<T?> GetAsyncWithBearerAuth<T>(string url, string client, string accessToken)
        {
            try
            {
                using var httpClient = _factory.CreateClient(client);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(json);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP Request error on {Url}", url);
                throw;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "HTTP Request timeout on {Url}",url);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON Deserialization error for URL: {Url}", url);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General Exception occurred for URL: {Url}", url);
                throw;
            }
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
                _logger.LogError(ex, "HTTP Request error on {Url}", url);
                throw;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "HTTP Request timeout on {Url}",url);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON Deserialization error for URL: {Url}", url);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General Exception occurred for URL: {Url}", url);
                throw;
            }
        }
    }
}
