using Mercury.Snapshot.Consts.Enums;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.Util.ObjectComparisons;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Calendars
{
    public class MercuryCalendar : ICalendar, ISyncable
    {
        public MercuryCalendar(MercuryUser User)
        {
            this.User = User;
        }

        public MercuryUser User { get; }
        public Task<IReadOnlyCollection<CalendarEvent>> GetEvents(DateTime TimeMin, DateTime TimeMax, int MaxResults)
        {
            List<CalendarEvent> Events = new();
            if (this.User.CalendarEventsRegister != null)
            {
                Events.AddRange(this.User.CalendarEventsRegister.GetAllRecords().Select(X => X.ObjectToStore).Where(Z => Z.Start >= TimeMin && Z.End <= TimeMax));
            }
            return Task.FromResult<IReadOnlyCollection<CalendarEvent>>(Events.SkipLast(Events.Count - MaxResults).ToList());
        }

        public Task SaveEvents(params CalendarEvent[] Events)
        {
            if (this.User.CalendarEventsRegister != null)
            {
                foreach (CalendarEvent Event in Events)
                {
                    this.User.CalendarEventsRegister.SaveRecord(Event.Id, new Record<CalendarEvent>(Event));
                }
            }
            return Task.CompletedTask;
        }

        public async Task Pull()
        {
            IReadOnlyCollection<CalendarEvent> MercuryEvents = await this.GetEvents(DateTime.MinValue, DateTime.Today.AddYears(1), int.MaxValue).ConfigureAwait(false);
            await this.DeleteEvents(MercuryEvents.ToArray()).ConfigureAwait(false);
            foreach (ICalendar? Calendar in this.User.Calendars)
            {
                if (Calendar != null && Calendar.GetType() != typeof(MercuryCalendar))
                {
                    IReadOnlyCollection<CalendarEvent> ThisCalendarsEvents = await Calendar.GetEvents(DateTime.MinValue, DateTime.Today.AddYears(1), int.MaxValue).ConfigureAwait(false);
                    foreach (CalendarEvent CEvent in ThisCalendarsEvents)
                    {
                        await this.SaveEvents(CEvent).ConfigureAwait(false);
                    }
                }
            }
        }

        public async Task Push()
        {
            IReadOnlyCollection<CalendarEvent> MercuryEvents = await this.GetEvents(DateTime.MinValue, DateTime.MaxValue, int.MaxValue).ConfigureAwait(false);
            foreach (ICalendar? Calendar in this.User.Calendars)
            {
                if (Calendar != null)
                {
                    foreach (CalendarEvent Event in MercuryEvents)
                    {
                        if (Event.Origin == Origins.Mercury)
                        {
                            await Calendar.SaveEvents(Event).ConfigureAwait(false);
                        }
                    }
                }
            }
        }

        public async Task DeleteEvents(params CalendarEvent[] Events)
        {
            IReadOnlyCollection<CalendarEvent> MercuryEvents = await this.GetEvents(DateTime.MinValue, DateTime.Today.AddYears(1), int.MaxValue).ConfigureAwait(false);
            foreach (CalendarEvent CEvent in Events)
            {
                CalendarEvent? MatchingEvent = MercuryEvents.FirstOrDefault(MEvent => ObjectEqualityManager.PropertiesAreEqual(MEvent, CEvent));
                if (MatchingEvent != null)
                {
                    if (this.User.CalendarEventsRegister != null)
                    {
                        this.User.CalendarEventsRegister.DeleteRecord(MatchingEvent.Id);
                    }
                }
            }
        }
    }
}
