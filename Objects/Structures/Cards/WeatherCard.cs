using izolabella.OpenWeatherMap.NET;
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
            UnitTypes Units = Profile.Settings.CultureSettings.Units;
            string DegreeType = Units == UnitTypes.Imperial ? "F" :
                                (Units == UnitTypes.Metric ? "C" : "K");
            List<EmbedFieldBuilder> Builders = new();
            CultureInfo UserCulture = Profile.Settings.CultureSettings.Culture;
            DateTime UserTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, Profile.Settings.CultureSettings.TimeZone);
            OneCallWeatherResponse? Weather = await WeatherManager.GetWeather(Profile.Settings.CultureSettings.Units, Profile.Settings.WeatherSettings.Zip).ConfigureAwait(false);
            if(Weather != null)
            {
                double Temperature = Weather.Current.Temperature;
                double TemperatureMax = double.MinValue;
                double TemperatureMin = double.MaxValue;
                foreach(HourlyWeatherData Data in Weather.Hourly.Where(Hr => Hr.WeatherTime.ToLocalTime().Date == DateTime.Today))
                {
                    if(TemperatureMax < Data.Temperature)
                    {
                        TemperatureMax = Data.Temperature;
                    }
                    if(TemperatureMin > Data.Temperature)
                    {
                        TemperatureMin = Data.Temperature;
                    }
                }
                EmbedFieldBuilder Today = new()
                {
                    Name = $"TODAY'S WEATHER • {UserTime.ToString(UserCulture.DateTimeFormat.LongDatePattern, UserCulture)}",
                };
                Today.Value = $"" +
                    $"{Temperature.ToString(UserCulture)}°{DegreeType} - {(Weather.Current.CityName != null ? $"{Weather.Current.CityName}" : "Currently")}\n" +
                    $"H: {TemperatureMax.ToString(UserCulture)}°{DegreeType} L: {TemperatureMin.ToString(UserCulture)}°{DegreeType}\n\n";
                foreach(HourlyWeatherData? Wt in Weather.Hourly.Where(Hr => Hr.WeatherTime.ToLocalTime().Date == DateTime.Today))
                {
                    DateTime WeatherTimeUserLocal = TimeZoneInfo.ConvertTime(Wt.WeatherTime, Profile.Settings.CultureSettings.TimeZone);
                    Today.Value += $"{ WeatherTimeUserLocal.ToString(UserCulture.DateTimeFormat.ShortTimePattern, UserCulture)}: {Wt.Temperature.ToString(UserCulture)}°{DegreeType}\n";
                }
                Builders.Add(Today);
            }
            return Builders;
        }
    }
}
