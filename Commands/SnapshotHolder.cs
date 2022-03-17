using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using Mercury.Snapshot.Objects.Structures.Cards;
using Mercury.Snapshot.Objects.Structures.Personalization;

namespace Mercury.Snapshot.Commands
{
    public static class SnapshotHolder
    {
        [Command(new string[] { "snapshot" }, "Receive a general rundown.")]
        public static async Task Snap(CommandArguments Args)
        {
            try
            {
                if (Args != null)
                {
                    MercuryProfile Profile = new(Args.SlashCommand.User.Id);
                    Console.WriteLine($"{Args.SlashCommand.User.Username} {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");

                    await Args.SlashCommand.RespondAsync("here u go . .", new[] { CustomEmbedBuilder.GetConformingEmbed(new EmbedBuilder()
                    {
                        Color = new(0x00000),
                        Footer = new()
                        {
                            Text = "-☿-"
                        },
                        Description = "<a:loadinghearts:950503533910835241>"
                    }).Build() }, false, true);
                    List<EmbedFieldBuilder> Fields = new CardsBuilder(new List<ICard> { new EventsCard(), new ExpendituresCard() }, new(Args.SlashCommand.User.Id)).Build();
                    await Args.SlashCommand.ModifyOriginalResponseAsync(MessageInfo =>
                    {
                        MessageInfo.Embed = new EmbedBuilder()
                        {
                            Fields = Fields.Count > 0 ? Fields : new List<EmbedFieldBuilder>()
                            {
                                {
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Why is Snapshot empty? <a:breakdown:923399825108635681>",
                                        Value = !Profile.GoogleClient.IsAuthenticated ? $"[Google Authorization]({Program.CurrentApp.Initializer.GoogleOAuth2.CreateAuthorizationRequest(new(Args.SlashCommand.User.Id.ToString()))}) is required to fill your Snapshot list." : "Check that you have any events or expenses.",
                                    }
                                }
                            },
                            Color = new(0x00000),
                            Footer = new()
                            {
                                Text = "-☿-"
                            }
                        }.Build();
                    });
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E);
            }
        }
    }
}
