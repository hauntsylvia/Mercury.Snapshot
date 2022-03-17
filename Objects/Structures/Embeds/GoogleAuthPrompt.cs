﻿namespace Mercury.Snapshot.Objects.Structures.Embeds
{
    public class GoogleAuthPrompt : EmbedBuilder
    {
        public GoogleAuthPrompt(string AuthorizationUrl)
        {
            this.Color = new(0x00000);
            this.Footer = new()
            {
                Text = "-☿-"
            };
            this.Description = $"By [connecting your Google acccount]({AuthorizationUrl}), you are letting me see events placed on your calendar, as well as enabling me to help you budget. :)";
            this.Timestamp = DateTime.UtcNow;
        }
    }
}