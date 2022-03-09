using OpenWeatherMap.NET.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Util.Weather
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
