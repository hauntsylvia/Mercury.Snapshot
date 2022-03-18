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
        public MercurySnapshotProgram()
        {
            this.Initializer = new(new StartupItems());
        }

        public MercurySnapshotInitializer Initializer { get; }

        public async void RunProgram()
        {
            if (this.Initializer != null && !this.Initializer.StartupItems.IsCold)
            {
                this.Initializer.DiscordSocketClient.Client.Ready += this.Client_Ready;
                await this.Initializer.DiscordSocketClient.Client.LoginAsync(TokenType.Bot, this.Initializer.StartupItems.DiscordToken, true);
                await this.Initializer.DiscordSocketClient.Client.StartAsync();
            }
            else
            {
                throw new NullReferenceException("No initializer is present.");
            }
        }

        private async Task Client_Ready()
        {
            await this.Initializer.DiscordSocketClient.CommandHandler.StartReceiving();
        }
    }
}
