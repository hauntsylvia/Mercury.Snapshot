namespace Mercury.Snapshot.Objects.Structures.Personalization.Peronalizers
{
    public class GoogleCalendarSettings
    {
        public GoogleCalendarSettings(string CalendarId = "primary")
        {
            this.CalendarId = CalendarId;
        }

        public string CalendarId { get; }
    }
}
