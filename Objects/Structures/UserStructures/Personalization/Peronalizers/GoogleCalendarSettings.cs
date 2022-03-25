namespace Mercury.Snapshot.Objects.Structures.UserStructures.Personalization.Peronalizers
{
    internal class GoogleCalendarSettings
    {
        internal GoogleCalendarSettings(string CalendarId = "primary")
        {
            this.CalendarId = CalendarId;
        }

        internal string CalendarId { get; }
    }
}
