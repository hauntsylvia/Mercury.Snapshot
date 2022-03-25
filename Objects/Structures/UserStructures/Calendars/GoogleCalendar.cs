
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Identification;
using Mercury.Snapshot.Objects.Util;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Calendars
{
    internal class GoogleCalendar : ICalendar
    {
        internal GoogleCalendar(UserCredential Credential)
        {
            this.Service = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = Credential,
                ApplicationName = GoogleClient.ApplicationName,
            });
        }

        internal CalendarService Service { get; set; }

        internal Task<IReadOnlyCollection<CalendarEvent>> GetEvents(DateTime TimeMin, DateTime TimeMax, int MaxResults = 2500)
        {
            EventsResource.ListRequest Request = this.Service.Events.List("primary");
            Request.TimeMin = TimeMin;
            Request.TimeMax = TimeMax;
            Request.ShowDeleted = false;
            Request.SingleEvents = true;
            Request.MaxResults = MaxResults;
            Request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            Google.Apis.Calendar.v3.Data.Events Events = Request.Execute();
            List<CalendarEvent> EventItems = new();
            foreach (Event Event in Events.Items)
            {
                string RFC3339Z = "yyyy-MM-dd'T'HH:mm:ss.fffZ";
                string RFC3339 = "yyyy-MM-dd'T'HH:mm:sszzz"; //2022-03-14T12:00:00-04:00'
                string GeneralDate = "yyyy-MM-dd";
                string GeneralDateTime = "yyyy-MM-dd HH:mm:ss";
                DateTime Created = DateTime.ParseExact(Event.CreatedRaw, RFC3339Z, null);
                DateTime Updated = DateTime.ParseExact(Event.UpdatedRaw, RFC3339Z, null);
                if (!DateTime.TryParseExact(Event.Start.DateTimeRaw, RFC3339, null, System.Globalization.DateTimeStyles.None, out DateTime Start))
                {
                    DateTime.TryParseExact(Event.Start.Date, new string[] { GeneralDate, GeneralDateTime }, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out Start);
                }

                if (!DateTime.TryParseExact(Event.End.DateTimeRaw, RFC3339, null, System.Globalization.DateTimeStyles.None, out DateTime End))
                {
                    DateTime.TryParseExact(Event.End.Date, new string[] { GeneralDate, GeneralDateTime }, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out End);
                }

                EventItems.Add(new MercuryCalendarEvent(Event.Summary, Event.Description, Updated, Created, Start, End, Origins.Google, Identifier.GetIdentifier()));
            }
            return Task.FromResult<IReadOnlyCollection<CalendarEvent>>(EventItems);
        }

        internal Task SaveEvents(params CalendarEvent[] Events)
        {
            try
            {
                foreach (CalendarEvent Event in Events)
                {
                    this.Service.Events.Insert(new Event()
                    {
                        Summary = Event.Summary,
                        Description = Event.Description,
                        Source = new Event.SourceData()
                        {
                            Title = "Mercury.Snapshot",
                        },
                        Start = new()
                        {
                            DateTime = Event.Start.ToUniversalTime(),
                        },
                        End = new()
                        {
                            DateTime = Event.End.ToUniversalTime(),
                        },
                    }, "primary");
                }
                return Task.CompletedTask;
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex);
                throw;
            }
        }
    }
}
