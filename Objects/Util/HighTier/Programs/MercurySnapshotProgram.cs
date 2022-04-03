using izolabella.ConsoleHelper;
using Mercury.Snapshot.Objects.Structures.MercurySnapshot;
using Mercury.Snapshot.Objects.Util.HighTier.Initializers;

namespace Mercury.Snapshot.Objects.Util.HighTier.Programs
{
    /// <summary>
    /// Handles the initialized clients.
    /// </summary>
    public class MercurySnapshotProgram
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MercurySnapshotProgram"/> class.
        /// </summary>
        /// <param name="Initializer">A group of objects that should be initialized by startup.</param>
        public MercurySnapshotProgram(MercurySnapshotInitializer Initializer)
        {
            this.Initializer = Initializer;
        }

        public MercurySnapshotInitializer Initializer { get; }

        public async void RunProgram()
        {
            this.Initializer.DiscordSocketClient.Client.Ready += this.Client_Ready;
            await this.Initializer.DiscordSocketClient.Client.LoginAsync(TokenType.Bot, this.Initializer.StartupItems.DiscordToken, true).ConfigureAwait(false);
            await this.Initializer.DiscordSocketClient.Client.StartAsync().ConfigureAwait(false);
        }

        private async Task Client_Ready()
        {
            await this.Initializer.DiscordSocketClient.CommandHandler.StartReceiving().ConfigureAwait(false);
        }
    }
}
