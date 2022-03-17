namespace Mercury.Snapshot.Consts
{
    internal class Configurations
    {
        internal static DiscordSocketConfig DiscordConfig => new()
        {
            UseSystemClock = false,
            MessageCacheSize = 20,
            AlwaysDownloadUsers = true,
            AlwaysDownloadDefaultStickers = true,
            AlwaysResolveStickers = true,
            UseInteractionSnowflakeDate = false,
        };
    }
}
