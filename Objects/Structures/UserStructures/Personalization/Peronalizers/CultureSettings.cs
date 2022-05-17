using izolabella.OpenWeatherMap.NET;
using System.Globalization;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Personalization.Peronalizers
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CultureSettings
    {
        public CultureSettings()
        {

        }

        public CultureSettings(string CultureISOCode, UnitTypes Units, TimeZoneInfo TimeZone)
        {
            this.Culture = CultureInfo.GetCultureInfo(CultureISOCode);
            this.Units = Units;
            this.TimeZone = TimeZone;
        }

        public CultureSettings(string CultureISOCode, UnitTypes Units, string TimeZoneIdentifier)
        {
            this.Culture = CultureInfo.GetCultureInfo(CultureISOCode);
            this.Units = Units;
            try
            {
                this.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneIdentifier);
            }
            catch (Exception E)
            {
                if (E.GetType() == typeof(InvalidTimeZoneException) || E.GetType() == typeof(TimeZoneNotFoundException))
                {
                    this.TimeZone = TimeZoneInfo.Utc;
                }
                else
                {
                    throw;
                }
            }
        }

        [JsonConstructor]
        public CultureSettings(CultureInfo Culture, UnitTypes Units, TimeZoneInfo TimeZone)
        {
            this.Culture = Culture;
            this.Units = Units;
            this.TimeZone = TimeZone;
        }

        [JsonProperty("Culture")]
        public CultureInfo Culture { get; } = CultureInfo.GetCultureInfo("en-US");

        [JsonProperty("Units")]
        public UnitTypes Units { get; } = UnitTypes.Metric;

        [JsonProperty("TimeZone")]
        public TimeZoneInfo TimeZone { get; } = TimeZoneInfo.Utc;
    }
}
