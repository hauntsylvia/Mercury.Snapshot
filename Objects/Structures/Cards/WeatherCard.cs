using izolabella.OpenWeatherMap.NET.Classes.Responses;
using izolabella.OpenWeatherMap.NET.Classes.Responses.CurrentWeatherData;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Snapshot.Objects.Util.Managers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Cards
{
    public class WeatherCard : ICard
    {
        public async Task<IReadOnlyCollection<EmbedFieldBuilder>> RenderAsync(MercuryUser Profile)
        {
            List<EmbedFieldBuilder> Builders = new();
            CultureInfo UserCulture = Profile.Settings.CultureSettings.Culture;
            WeatherResponse? WeatherToday = await WeatherManager.GetWeatherForToday(Profile.Settings.WeatherSettings.Zip);
            OneCallWeatherResponse? Weather = await WeatherManager.GetWeather(Profile.Settings.WeatherSettings.Zip);
            if(WeatherToday != null && Weather != null)
            {
                double Temperature = WeatherToday.Main.Temp;
                double TemperatureMax = WeatherToday.Main.TempMaximum;
                double TemperatureMin = WeatherToday.Main.TempMinimum;
                EmbedFieldBuilder Today = new()
                {
                    Name = $"TODAY'S WEATHER • {DateTime.Today.ToString(UserCulture.DateTimeFormat.LongDatePattern, UserCulture)}",
                };
                Today.Value = $"" +
                    $"{Temperature.ToString(UserCulture)}°C - {WeatherToday.CityName}\n" +
                    $"H: {TemperatureMax.ToString(UserCulture)}°C L: {TemperatureMin.ToString(UserCulture)}°C\n";
                foreach(izolabella.OpenWeatherMap.NET.Classes.Responses.OneCall.HourlyWeatherData? Wt in Weather.Hourly)
                {
                    Today.Value += $"{Wt.Temperature}°C {Wt.WeatherTime.ToShortTimeString()}";
                }
                Builders.Add(Today);
            }
            return Builders;
        }
    }
}
