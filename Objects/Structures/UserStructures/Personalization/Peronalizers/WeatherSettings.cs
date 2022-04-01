using System.Globalization;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Personalization.Peronalizers
{
    public class WeatherSettings
    {
        public WeatherSettings(int? Zip)
        {
            this.zip = Zip?.ToString(new CultureInfo("en-US"));
        }

        private readonly string? zip;
        public string? Zip => this.zip?.PadLeft(5, '0');
    }
}
