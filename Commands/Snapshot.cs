using Discord.Rest;
using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using Mercury.Snapshot.Objects.Structures.Cards;
using Mercury.Snapshot.Objects.Util.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Commands
{
    public class Snapshot
    {
        [Command(new string[] { "snapshot" }, "Receive a general rundown.")]
        public static async Task Abc(CommandArguments Args)
        {
            try
            {
                Console.WriteLine($"{Args.SlashCommand.User.Username} {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
                if (Program.DiscordClient.GetChannel(942608553028501544) is SocketTextChannel Channel)
                {
                    await Args.SlashCommand.RespondAsync("here u go . .", new[] { new EmbedBuilder()
                    {
                        Color = new(0x00000),
                        Footer = new()
                        {
                            Text = "-☿-"
                        },
                        Description = "<a:loadinghearts:950503533910835241>"
                    }.Build() }, false, true);
                    List<EmbedFieldBuilder> Fields = new CardHelper(new List<ICard> { new EventsCard(), new ExpendituresCard(new(Args.SlashCommand.User.Id)) }, new(Args.SlashCommand.User.Id)).CorrectWhitespacing();
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
                                        Value = $"[Google Authorization]({Program.GoogleOAuth2Handler.CreateAuthorizationRequest(new(Args.SlashCommand.User.Id.ToString()))}) is required to fill your Snapshot list."
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
            catch(Exception E)
            {
                Console.WriteLine(E);
            }
        }
    }
}
