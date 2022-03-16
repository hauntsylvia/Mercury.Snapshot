using Mercury.Snapshot.Consts;
using Mercury.Snapshot.Objects.Structures.Cards;
using Mercury.Snapshot.Objects.Util.Google.General;
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
            this.googleClient = new(this.DiscordId);
        }

        public MercuryProfile(ulong DiscordId)
        {
            this.discordId = DiscordId;
            this.googleClient = new(this.DiscordId);
        }

        private readonly GoogleApp googleClient;
        public GoogleApp GoogleClient => this.googleClient;

        public Record<MercuryUserSettings> Settings 
        { 
            get => Registers.MercurySettingsRegister.GetRecord<MercuryUserSettings>(this.DiscordId.ToString()) ?? new Record<MercuryUserSettings>(new(), new List<string>() { "Auto-generated" });
            set => Registers.MercurySettingsRegister.SaveRecord(this.DiscordId.ToString(), value);
        }



        private readonly ulong discordId;
        public ulong DiscordId => this.discordId;
    }
}
