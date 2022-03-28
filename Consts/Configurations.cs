namespace Mercury.Snapshot.Consts
{
    public static class Configurations
    {
        public static DiscordSocketConfig DiscordConfig => new()
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
