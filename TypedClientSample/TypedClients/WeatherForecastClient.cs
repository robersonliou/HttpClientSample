using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TypedClientSample.TypedClients
{
    public interface IWeatherForecastClient
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherForecasts();
    }

    public class WeatherForecastClient : IWeatherForecastClient
    {
        private readonly HttpClient _httpClient;
        public WeatherForecastClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
        {
            var url = $"{_httpClient.BaseAddress}weatherforecast";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var result = JsonSerializer.Deserialize<List<WeatherForecast>>(content, options);

            return result;
        }

    }
}