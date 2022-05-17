namespace Mercury.Snapshot.Objects.Structures.Embeds
{
    public class CalendarSyncEmbed : EmbedBuilder
    {
        public CalendarSyncEmbed(bool Loading = true, bool Success = true)
        {
            this.Color = EmbedDefaults.EmbedColor;
            this.Timestamp = DateTime.UtcNow;
            this.Description = Loading
                ? $"{Strings.EmbedStrings.Calendars.CalendarSyncLoading} {Emotes.AliceCenturion}"
                : Success ? $"{Strings.EmbedStrings.Calendars.CalendarSyncSuccess}" : $"{Strings.EmbedStrings.Calendars.CalendarSyncFailure}";
        }
    }
}
