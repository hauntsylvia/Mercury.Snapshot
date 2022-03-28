using izolabella.OpenWeatherMap.NET.Classes;
using izolabella.OpenWeatherMap.NET.Classes.Responses.CurrentWeatherData;

namespace Mercury.Snapshot.Objects.Util.Managers
{
    public static class WeatherManager
    {
        public static async Task<WeatherResponse?> GetWeatherForToday(string? Zip, string CountryCode = "US")
        {
            if(Zip != null)
            {
                WeatherResponse? Weather = await Program.CurrentApp.Initializer.OpenWeatherMapClient.Processors.CurrentWeatherDataProcessor.GetWeatherByZipCodeAsync(Zip, CountryCode).ConfigureAwait(false);
                return Weather;
            }
            else
            {
                return null;
            }
        }
    }
}
