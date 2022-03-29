using izolabella.OpenWeatherMap.NET.Classes.Responses;
using izolabella.OpenWeatherMap.NET.Classes.Responses.CurrentWeatherData;
using izolabella.OpenWeatherMap.NET.Classes.Responses.OneCall;
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
            OneCallWeatherResponse? Weather = await WeatherManager.GetWeather(Profile.Settings.WeatherSettings.Zip);
            if(Weather != null)
            {
                double Temperature = Weather.Current.Temperature;
                double TemperatureMax = 0;
                double TemperatureMin = 0;
                foreach(DailyWeatherData Data in Weather.Daily)
                {
                    if(TemperatureMax < Data.Temperature.Max)
                    {
                        TemperatureMax = Data.Temperature.Max;
                    }
                    if(TemperatureMin > Data.Temperature.Min)
                    {
                        TemperatureMin = Data.Temperature.Min;
                    }
                }
                EmbedFieldBuilder Today = new()
                {
                    Name = $"TODAY'S WEATHER • {DateTime.Today.ToString(UserCulture.DateTimeFormat.LongDatePattern, UserCulture)}",
                };
                Today.Value = $"" +
                    $"{Temperature.ToString(UserCulture)}°C - {Weather.Current.CityName}\n" +
                    $"H: {TemperatureMax.ToString(UserCulture)}°C L: {TemperatureMin.ToString(UserCulture)}°C\n\n";
                foreach(HourlyWeatherData? Wt in Weather.Hourly.Where(Hr => Hr.WeatherTime.ToLocalTime().Date == DateTime.Today))
                {
                    Today.Value += $"{Wt.WeatherTime.ToLocalTime().ToShortTimeString()}: {Wt.Temperature}°C\n";
                }
                Builders.Add(Today);
            }
            return Builders;
        }
    }
}
