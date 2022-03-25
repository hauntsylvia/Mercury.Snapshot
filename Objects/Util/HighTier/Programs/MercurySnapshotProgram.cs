using izolabella.ConsoleHelper;
using Mercury.Snapshot.Objects.Structures.MercurySnapshot;
using Mercury.Snapshot.Objects.Util.HighTier.Initializers;

namespace Mercury.Snapshot.Objects.Util.HighTier.Programs
{
    /// <summary>
    /// Handles the initialized clients.
    /// </summary>
    internal class MercurySnapshotProgram
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MercurySnapshotProgram"/> class.
        /// </summary>
        /// <param name="Initializer">A group of objects that should be initialized by startup.</param>
        internal MercurySnapshotProgram(MercurySnapshotInitializer Initializer)
        {
            this.Initializer = Initializer;
        }
        internal MercurySnapshotProgram()
        {
            this.Initializer = new(new StartupItems());
        }

        internal MercurySnapshotInitializer Initializer { get; }

        internal async void RunProgram()
        {
            if (this.Initializer != null && !this.Initializer.StartupItems.IsCold)
            {
                this.Initializer.DiscordSocketClient.Client.Ready += this.Client_Ready;
                await this.Initializer.DiscordSocketClient.Client.LoginAsync(TokenType.Bot, this.Initializer.StartupItems.DiscordToken, true).ConfigureAwait(false);
                await this.Initializer.DiscordSocketClient.Client.StartAsync().ConfigureAwait(false);
            }
            else
            {
                throw new NullReferenceException("No initializer is present.");
            }
        }

        private async Task Client_Ready()
        {
            await this.Initializer.DiscordSocketClient.CommandHandler.StartReceiving().ConfigureAwait(false);
        }
    }
}
