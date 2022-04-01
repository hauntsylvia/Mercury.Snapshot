using izolabella.OpenWeatherMap.NET;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Personalization.Peronalizers
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CultureSettings
    {
        public CultureSettings()
        {

        }

        public CultureSettings(string CultureISOCode, UnitTypes Units)
        {
            this.Culture = CultureInfo.GetCultureInfo(CultureISOCode);
            this.Units = Units;
        }

        [JsonConstructor]
        public CultureSettings(CultureInfo Culture, UnitTypes Units)
        {
            this.Culture = Culture;
            this.Units = Units;
        }

        [JsonProperty("Culture")]
        public CultureInfo Culture { get; } = CultureInfo.GetCultureInfo("en-US");

        [JsonProperty("Units")]
        public UnitTypes Units { get; } = UnitTypes.Metric;
    }
}
