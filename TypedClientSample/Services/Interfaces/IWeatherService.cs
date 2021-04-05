using System.Threading.Tasks;

namespace TypedClientSample.Services.Interfaces
{
    public interface IWeatherService
    {
        Task LogOverTemperatureDates();
    }
}