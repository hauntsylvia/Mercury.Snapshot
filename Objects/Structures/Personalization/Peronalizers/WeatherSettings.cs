using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Personalization.Peronalizers
{
    internal class WeatherSettings
    {
        internal WeatherSettings(string Zip)
        {
            this.Zip = Zip;
        }

        internal string Zip { get; }
    }
}
