using izolabella.OpenWeatherMap.NET.Classes;

namespace Mercury.Snapshot.Objects.Util
{
    public class WeatherManager
    {
        public static WeatherResponse? GetWeatherForToday(string Zip)
        {
            WeatherResponse? Weather = Program.OpenWeatherMapClient.GetWeatherByZipCode(Zip).Result;
            return Weather;
        }
    }
}
