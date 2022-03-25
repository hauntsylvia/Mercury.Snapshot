using izolabella.OpenWeatherMap.NET.Classes;
using izolabella.OpenWeatherMap.NET.Classes.Responses.CurrentWeatherData;

namespace Mercury.Snapshot.Objects.Util.Managers
{
    internal static class WeatherManager
    {
        internal static async Task<WeatherResponse?> GetWeatherForToday(string Zip, string CountryCode = "US")
        {
            WeatherResponse? Weather = await Program.CurrentApp.Initializer.OpenWeatherMapClient.Processors.CurrentWeatherDataProcessor.GetWeatherByZipCodeAsync(Zip, CountryCode).ConfigureAwait(false);
            return Weather;
        }
    }
}
