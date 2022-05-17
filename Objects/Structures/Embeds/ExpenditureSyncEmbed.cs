namespace Mercury.Snapshot.Objects.Structures.Embeds
{
    public class ExpenditureSyncEmbed : EmbedBuilder
    {
        public ExpenditureSyncEmbed(bool Loading = true, bool Success = true)
        {
            this.Color = EmbedDefaults.EmbedColor;
            this.Timestamp = DateTime.UtcNow;
            this.Description = Loading
                ? $"{Strings.EmbedStrings.Expenditures.ExpenditureSyncLoading} {Emotes.AliceCenturion}"
                : Success
                    ? $"{Strings.EmbedStrings.Expenditures.ExpenditureSyncCompleted}"
                    : $"{Strings.EmbedStrings.Expenditures.ExpenditureSyncFailed}";
        }
    }
}
