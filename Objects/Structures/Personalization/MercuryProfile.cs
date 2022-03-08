using Mercury.Snapshot.Objects.Structures.Cards;
using Mercury.Unification.IO.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Personalization
{
    internal class MercuryProfile
    {
        internal MercuryProfile(ulong DiscordId, MercuryUserSettings? Settings)
        {
            this.discordId = DiscordId;
            this.SettingsSaveRegister = new("Mercury User Settings");
            if (Settings != null)
                this.SettingsSaveRegister.SaveRecord("Settings", new Record<MercuryUserSettings>(Settings, new List<string>()));
        }


        private Register SettingsSaveRegister { get; }


        internal Record<MercuryUserSettings> Settings => this.SettingsSaveRegister.GetRecord<MercuryUserSettings>("Settings") ?? new Record<MercuryUserSettings>(new(null, null), new List<string>() { "Auto-generated" });



        private readonly ulong discordId;
        internal ulong DiscordId => this.discordId;
    }
}
