namespace Mercury.Snapshot.Objects.Structures.Embeds
{
    public class GoogleAuthPrompt : EmbedBuilder
    {
        public GoogleAuthPrompt(Uri AuthorizationUrl)
        {
            this.Color = new(0x0F9D58);
            this.Footer = new()
            {
                Text = "-☿-"
            };
            this.Description = $"By [connecting your Google acccount]({AuthorizationUrl.OriginalString}), you are letting me see events placed on your calendar, as well as enabling me to help you budget. :)";
            this.Timestamp = DateTime.UtcNow;
        }
    }
}
