using Mercury.Snapshot.Objects.Util;
using Mercury.Unification.IO.File.Records;

namespace Mercury.Snapshot.Objects.Structures.Personalization
{
    public class MercuryProfile
    {
        public MercuryProfile(ulong DiscordId, MercuryUserSettings Settings)
        {
            this.DiscordId = DiscordId;
            this.Settings = new Record<MercuryUserSettings>(Settings, new List<string>());
            this.GoogleClient = new(this.DiscordId);
        }

        public MercuryProfile(ulong DiscordId)
        {
            this.DiscordId = DiscordId;
            this.GoogleClient = new(this.DiscordId);
        }

        public GoogleClient GoogleClient { get; }

        public IRecord<MercuryUserSettings> Settings
        {
            get => Registers.MercurySettingsRegister.GetRecord(this.DiscordId.ToString()) ?? new Record<MercuryUserSettings>(new(), new List<string>() { "Auto-generated" });
            set => Registers.MercurySettingsRegister.SaveRecord(this.DiscordId.ToString(), value);
        }
        public ulong DiscordId { get; }
    }
}
