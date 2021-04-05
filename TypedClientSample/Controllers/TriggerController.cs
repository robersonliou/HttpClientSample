using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TypedClientSample.Services.Interfaces;

namespace TypedClientSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TriggerController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        public TriggerController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            await _weatherService.LogOverTemperatureDates();
            return "Trigger succeed";
        }
    }
}