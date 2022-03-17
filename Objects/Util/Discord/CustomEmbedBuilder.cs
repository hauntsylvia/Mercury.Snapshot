namespace Mercury.Snapshot.Objects.Util.Discord
{
    public static class CustomEmbedBuilder
    {
        public static EmbedBuilder GetConformingEmbed(EmbedBuilder Original)
        {
            Original.Color = new(0x00000);
            Original.Footer = new()
            {
                Text = "-☿-"
            };
            Original.Timestamp = DateTime.UtcNow;
            return Original;
        }
    }
}
