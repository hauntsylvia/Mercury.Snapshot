using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Snapshot.Objects.Util.Managers;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;
using System.Reflection;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Calendars
{
    public class MercuryCalendar : ICalendar, IMercuryCalendar, ISyncable
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

        private static Task<List<CalendarEvent>> GetAllNonMatchingAsync(CalendarEvent[] A, CalendarEvent[] B)
        {
            List<CalendarEvent> Events = new();
            foreach(CalendarEvent EventA in A)
            {
                CalendarEvent? MatchingEvent = B.FirstOrDefault(Event => ObjectEqualityManager.PropertiesAreEqual(Event, EventA));
                if(MatchingEvent == null)
                {
                    Events.Add(EventA);
                }
            }
            return Task.FromResult(Events);
        }

        private async Task RecursivePull(DateTime Min, DateTime Max, List<ICalendar?> CalendarsToSync, CalendarEvent[] Buffer)
        {
            IReadOnlyCollection<CalendarEvent> MercuryEvents = await this.GetEvents(Min, Max, Buffer.Length);
            foreach(ICalendar? Calendar in CalendarsToSync.ToList())
            {
                if (Calendar != null)
                {
                    Buffer = (await Calendar.GetEvents(Min, Max, Buffer.Length)).ToArray();
                    List<CalendarEvent> NonMatching = await GetAllNonMatchingAsync(Buffer, MercuryEvents.ToArray());
                    if (Buffer.All(X => X != null) && NonMatching.Count > 0)
                    {
                        await this.SaveEvents(NonMatching.ToArray());
                    }
                    else
                    {
                        CalendarsToSync.Remove(Calendar);
                    }
                    await this.RecursivePull(NonMatching.Count > 0 ? NonMatching.Last().Start : Min, Max, CalendarsToSync, new CalendarEvent[Buffer.Length]);
                }
            }
        }

        /// <summary>
        /// Pulls using a maximum value to download at a time from each calendar in <see cref="MercuryUser.Calendars"/> to prevent heavy memory use. This calls every
        /// calendar's <see cref="ICalendar.GetEvents(DateTime, DateTime, int)"/> method recursively until all events have been 
        /// retrieved. This method will also download every event by calling <see cref="ICalendar.SaveEvents(CalendarEvent[])"/> for each
        /// iteration.
        /// </summary>
        /// <returns></returns>
        public async Task Pull()
        {
            await this.RecursivePull(DateTime.MinValue, DateTime.MaxValue, this.User.Calendars.ToList(), new CalendarEvent[512]);
        }

        public async Task Push()
        {
            IReadOnlyCollection<CalendarEvent> MercuryEvents = await this.GetEvents(DateTime.MinValue, DateTime.MaxValue, int.MaxValue);
            foreach(ICalendar? Calendar in this.User.Calendars)
            {
                if(Calendar != null)
                {
                    foreach(CalendarEvent Event in MercuryEvents)
                    {
                        if(Event.Origin == Origins.Mercury)
                        {
                            await Calendar.SaveEvents(Event);
                        }
                    }
                }
            }
        }
    }
}
