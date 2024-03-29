﻿using izolabella.Discord.Commands.Arguments;
using Mercury.Snapshot.Objects.Structures.Cards;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;

namespace Mercury.Snapshot.Objects.Structures.Embeds
{
    public class SnapshotEmbed : EmbedBuilder
    {
        public SnapshotEmbed(CommandArguments Context, MercuryUser Profile)
        {
            this.Color = EmbedDefaults.EmbedColor;
            this.Footer = new()
            {
                Text = "-☿-"
            };
            if (Profile != null && Context != null)
            {
                List<EmbedFieldBuilder> Fields = new CardsBuilder(new List<ICard> { new WeatherCard(), new CalendarEventsCard(), new ExpendituresCard() }, Profile).Build().ToList();
                this.Fields = Fields.Count > 0 ? Fields : new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder()
                    {
                        Name = "Why is Snapshot empty? <a:breakdown:923399825108635681>",
                        Value = !Profile.GoogleClient.IsAuthenticated ? $"[Google authorization]({Program.CurrentApp.Initializer.GoogleOAuth2.CreateAuthorizationRequest(new(Context.SlashCommand.User.Id.ToString(Profile.Settings.CultureSettings.Culture)))}) is required to fill your Snapshot list." : "Check that you have any events or expenses.",
                    }
                };
            }
            else
            {
                throw new ArgumentNullException(nameof(Profile));
            }
        }
    }
}
