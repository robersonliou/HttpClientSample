using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TypedClientSample.Services.Interfaces;

namespace TypedClientSample.Services
{
    public class WeatherLegacyService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private ILogger<WeatherLegacyService> _logger;
        public WeatherLegacyService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task LogOverTemperatureDates()
        {
            // API 介接邏輯
            
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer your_token");
            
            var response = await httpClient.GetAsync("http://localhost:5000");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<WeatherForecast>>(content);

            // API 處理邏輯
            var forecastGreaterThan30Degree = result.Where(x => x.TemperatureC > 30);
            foreach (var weatherForecast in forecastGreaterThan30Degree)
            {
                _logger.LogInformation($"日期: {weatherForecast.Date} 氣溫過高，禁止室外操課!");
            }
        }
    }
}