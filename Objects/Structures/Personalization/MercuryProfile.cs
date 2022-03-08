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


        internal Record<MercuryUserSettings> Settings 
        { 
            get => this.SettingsSaveRegister.GetRecord<MercuryUserSettings>(this.DiscordId.ToString()) ?? new Record<MercuryUserSettings>(new(), new List<string>() { "Auto-generated" });
            set => this.SettingsSaveRegister.SaveRecord(this.DiscordId.ToString(), value);
        }



        private readonly ulong discordId;
        internal ulong DiscordId => this.discordId;
    }
}
