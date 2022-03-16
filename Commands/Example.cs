using Google.Apis.Auth.OAuth2.Responses;
using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Commands
{
    public class Example
    {
        [Command(new string[] { "a" }, "Example command.")]
        public static async void Abc(CommandArguments Args)
        {
            string ToSend = Program.GoogleOAuth2Handler.CreateAuthorizationRequest(new(Args.SlashCommand.User.Id.ToString()));
            await Args.SlashCommand.RespondAsync("a", new Embed[]
            {
                new EmbedBuilder()
                {
                    Description = $"auth!",
                    Fields = new()
                    {
                        {   
                            new()
                            {
                                Name = "<3",
                                Value = $"[auth!]({ToSend})"
                            }
                        }
                    }
                }.Build()
            }, false, true);
            Program.GoogleOAuth2Handler.TokenPOSTed += async (UserCredential, TokResponse, OriginalCall) =>
            {
                if(OriginalCall.ApplicationAppliedTag == Args.SlashCommand.User.Id.ToString())
                {
                    await Args.SlashCommand.FollowupAsync("auth <3", null, false, true);
                }
            };
        }
    }
}
