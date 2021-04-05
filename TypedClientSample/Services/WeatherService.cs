using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TypedClientSample.Services.Interfaces;
using TypedClientSample.TypedClients;

namespace TypedClientSample.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherForecastClient _weatherForecastClient;
        private readonly ILogger<WeatherService> _logger;
        
        public WeatherService(IWeatherForecastClient weatherForecastClient, 
            ILogger<WeatherService> logger)
        {
            _weatherForecastClient = weatherForecastClient;
            _logger = logger;
        }
        
        public async Task LogOverTemperatureDates() 
        {
            var result = await _weatherForecastClient.GetWeatherForecasts();

            var forecastsOverThan30Degree = result.Where(x => x.TemperatureC > 30);
            foreach (var weatherForecast in forecastsOverThan30Degree)
            {
                _logger.LogWarning($"日期: {weatherForecast.Date} 氣溫過高，禁止室外操課!");
            }
        }
    }
}