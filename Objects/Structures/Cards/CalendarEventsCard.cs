using izolabella.OpenWeatherMap.NET.Classes;
using izolabella.OpenWeatherMap.NET.Classes.Responses.CurrentWeatherData;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Snapshot.Objects.Util.Managers;

namespace Mercury.Snapshot.Objects.Structures.Cards
{
    public class CalendarEventsCard : ICard
    {
        public CalendarEventsCard()
        {
        }

        public async Task<IReadOnlyList<EmbedFieldBuilder>> RenderAsync(MercuryUser Profile)
        {
            List<EmbedFieldBuilder> EmbedFieldBuilders = new();
            if (Profile.GoogleClient.IsAuthenticated && Profile.GoogleClient.CalendarManager != null)
            {
                IReadOnlyCollection<CalendarEvent> EventsToday = await Profile.GetAllCalendarEventsAsync(DateTime.Today.Date, DateTime.Today.Date.Add(new TimeSpan(23, 59, 59)), int.MaxValue);
                IReadOnlyCollection<CalendarEvent> EventsWeek = await Profile.GetAllCalendarEventsAsync(DateTime.Today.AddDays(1), DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).AddDays(7).Date.Add(new TimeSpan(23, 59, 59)), int.MaxValue);
                IReadOnlyCollection<CalendarEvent> EventsMonth = await Profile.GetAllCalendarEventsAsync(DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).AddDays(7).Date.Add(new TimeSpan(23, 59, 59)), new(DateTime.Today.Date.Year, DateTime.Today.Month + 1, 1), int.MaxValue);

                WeatherResponse? WeatherToday = WeatherManager.GetWeatherForToday(Profile.Settings.ObjectToStore.WeatherSettings.Zip).Result;

                if (EventsToday.Count > 0 || WeatherToday != null)
                {
                    EmbedFieldBuilder Today = new()
                    {
                        Name = $"TODAY • {DateTime.Today.ToLongDateString()}",
                    };
                    if (WeatherToday != null)
                    {
                        decimal Temperature = WeatherToday.Main.Temp;
                        decimal TemperatureMax = WeatherToday.Main.TempMaximum;
                        decimal TemperatureMin = WeatherToday.Main.TempMinimum;
                        Today.Value = $"{Temperature}°C - {WeatherToday.CityName}\nH: {TemperatureMax}°C L: {TemperatureMin}°C\n";
                    }
                    foreach (CalendarEvent Event in EventsToday)
                    {
                        Today.Value += $"\n{Event.Start.ToShortTimeString()}\n```\n{Event.Summary}\n{Event.Description}\n```";
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
                        Week.Value += $"\n{Event.Start.ToShortDateString()} {Event.Start.ToShortTimeString()}\n```\n{Event.Summary}\n```";
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
                        string ToAppend = $"\n{Event.Start.ToShortDateString()} {Event.Start.ToShortTimeString()}\n```\n{Event.Summary}\n```";
                        string? MonthValue = Month.Value.ToString();
                        if (MonthValue != null)
                        {
                            if (MonthValue.Length + ToAppend.Length <= 1024)
                            {
                                Month.Value += ToAppend;
                            }
                            else
                            {
                                Month.Value += $"\n_. . and {EventsMonth.Count} more events this month_.";
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
