using izolabella.OpenWeatherMap.NET.Classes;

namespace Mercury.Snapshot.Objects.Util.Managers
{
    public static class WeatherManager
    {
        public static async Task<WeatherResponse?> GetWeatherForToday(string Zip, string CountryCode = "US")
        {
            WeatherResponse? Weather = await Program.OpenWeatherMapClient.Processors.CurrentWeatherDataProcessor.GetWeatherByZipCodeAsync(Zip, CountryCode);
            return Weather;
        }
    }
}
