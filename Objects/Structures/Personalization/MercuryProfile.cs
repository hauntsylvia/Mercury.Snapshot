using Mercury.Snapshot.Objects.Structures.Cards;
using Mercury.Unification.IO.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Personalization
{
    public class MercuryProfile
    {
        public MercuryProfile(ulong DiscordId, MercuryUserSettings Settings)
        {
            this.discordId = DiscordId;
            this.Settings = new(Settings, new List<string>());
        }

        public MercuryProfile(ulong DiscordId)
        {
            this.discordId = DiscordId;
        }


        private readonly Register settingsSaveRegister = new("Mercury User Settings");
        public Register SettingsSaveRegister => this.settingsSaveRegister;


        public Record<MercuryUserSettings> Settings 
        { 
            get => this.SettingsSaveRegister.GetRecord<MercuryUserSettings>(this.DiscordId.ToString()) ?? new Record<MercuryUserSettings>(new(), new List<string>() { "Auto-generated" });
            set => this.SettingsSaveRegister.SaveRecord(this.DiscordId.ToString(), value);
        }



        private readonly ulong discordId;
        public ulong DiscordId => this.discordId;
    }
}
