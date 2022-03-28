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

        [JsonConstructor]
        public CultureSettings(CultureInfo Culture)
        {
            this.Culture = Culture;
        }

        [JsonProperty("Culture")]
        public CultureInfo Culture { get; } = CultureInfo.GetCultureInfo("en-US");
    }
}
