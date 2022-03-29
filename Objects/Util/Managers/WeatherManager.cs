using izolabella.OpenWeatherMap.NET.Classes;
using izolabella.OpenWeatherMap.NET.Classes.Responses;
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

        public static async Task<OneCallWeatherResponse?> GetWeather(double Lat, double Lon)
        {
            OneCallWeatherResponse? Weather = await Program.CurrentApp.Initializer.OpenWeatherMapClient.Processors.OneCallProcessor.CallAsync(Lat, Lon);
            return Weather;
        }
        public static async Task<OneCallWeatherResponse?> GetWeather(string? Zip)
        {
            if (Zip != null)
            {
                double[] Coords = ZipCoords.GetCoordinates(Zip);
                return await GetWeather(Coords[0], Coords[1]);
            }
            else
            {
                return null;
            }
        }
    }
}
