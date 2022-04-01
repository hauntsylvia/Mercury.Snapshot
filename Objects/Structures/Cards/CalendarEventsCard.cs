using izolabella.OpenWeatherMap.NET.Classes;
using izolabella.OpenWeatherMap.NET.Classes.Responses.CurrentWeatherData;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Snapshot.Objects.Util.Managers;
using System.Globalization;

namespace Mercury.Snapshot.Objects.Structures.Cards
{
    public class CalendarEventsCard : ICard
    {
        public async Task<IReadOnlyCollection<EmbedFieldBuilder>> RenderAsync(MercuryUser Profile)
        {
            CultureInfo UserCulture = Profile.Settings.CultureSettings.Culture;
            List<EmbedFieldBuilder> EmbedFieldBuilders = new();
            if (Profile.GoogleClient.IsAuthenticated && Profile.GoogleClient.CalendarManager != null)
            {
                IReadOnlyCollection<CalendarEvent> EventsToday = await Profile.Calendar.GetEvents(DateTime.Today.Date, DateTime.Today.Date.Add(new TimeSpan(23, 59, 59)), int.MaxValue).ConfigureAwait(false);
                IReadOnlyCollection<CalendarEvent> EventsWeek = await Profile.Calendar.GetEvents(DateTime.Today.AddDays(1), DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).AddDays(7).Date.Add(new TimeSpan(23, 59, 59)), int.MaxValue).ConfigureAwait(false);
                IReadOnlyCollection<CalendarEvent> EventsMonth = await Profile.Calendar.GetEvents(DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).AddDays(7).Date.Add(new TimeSpan(23, 59, 59)), new(DateTime.Today.Date.Year, DateTime.Today.Month + 1, 1), int.MaxValue).ConfigureAwait(false);

                if (EventsToday.Count > 0)
                {
                    EmbedFieldBuilder Today = new()
                    {
                        Name = $"TODAY • {DateTime.Today.ToString(UserCulture.DateTimeFormat.LongDatePattern, UserCulture)}",
                    };
                    foreach (CalendarEvent Event in EventsToday)
                    {
                        Today.Value += $"\n{Event.Start.ToString(UserCulture.DateTimeFormat.ShortTimePattern, UserCulture)}\n```\n{Event.Summary}\n{Event.Description}\n```";
                    }
                    Today.Value += "\u200b\n";
                    EmbedFieldBuilders.Add(Today);
                }

                if (EventsWeek.Count > 0)
                {
                    EmbedFieldBuilder Week = new()
                    {
                        Name = $"THIS WEEK",
                    };
                    foreach (CalendarEvent Event in EventsWeek)
                    {
                        Week.Value += $"\n{Event.Start.ToString(UserCulture.DateTimeFormat.ShortDatePattern, UserCulture)} {Event.Start.ToString(UserCulture.DateTimeFormat.ShortTimePattern, UserCulture)}\n```\n{Event.Summary}\n```";
                    }
                    Week.Value += "\u200b\n";
                    EmbedFieldBuilders.Add(Week);
                }

                if (EventsMonth.Count > 0)
                {
                    EmbedFieldBuilder Month = new()
                    {
                        Name = $"THIS MONTH",
                        Value = "\u200b"
                    };
                    foreach (CalendarEvent Event in EventsMonth)
                    {
                        string ToAppend = $"\n{Event.Start.ToString(UserCulture.DateTimeFormat.ShortDatePattern, UserCulture)} {Event.Start.ToString(UserCulture.DateTimeFormat.ShortTimePattern, UserCulture)}\n```\n{Event.Summary}\n```";
                        string? MonthValue = Month.Value.ToString();
                        if (MonthValue != null)
                        {
                            if (MonthValue.Length + ToAppend.Length <= 1024)
                            {
                                Month.Value += ToAppend;
                            }
                            else
                            {
                                Month.Value += $"\n_. . and {EventsMonth.Count.ToString(UserCulture)} more events this month_.";
                                break;
                            }
                        }
                    }
                    Month.Value += "\u200b\n";
                    EmbedFieldBuilders.Add(Month);
                }
            }
            return EmbedFieldBuilders;
        }
    }
}
