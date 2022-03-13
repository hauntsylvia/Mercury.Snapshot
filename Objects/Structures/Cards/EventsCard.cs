using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Mercury.Snapshot.Objects.Structures.Personalization;
using Mercury.Snapshot.Objects.Util;
using Mercury.Snapshot.Objects.Util.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using izolabella.OpenWeatherMap.NET.Classes;
using Mercury.Snapshot.Objects.Structures.Generics;

namespace Mercury.Snapshot.Objects.Structures.Cards
{
    public class EventsCard : ICard
    {
        public EventsCard()
        {
        }

        public IReadOnlyList<EmbedFieldBuilder> Render(MercuryProfile Profile)
        {
            List<EmbedFieldBuilder> EmbedFieldBuilders = new();

            IReadOnlyCollection<IEvent> EventsToday = Profile.GoogleClient.CalendarManager.GetEvents(DateTime.Today.Date, DateTime.Today.Date.Add(new TimeSpan(23, 59, 59))).Result;
            IReadOnlyCollection<IEvent> EventsWeek = Profile.GoogleClient.CalendarManager.GetEvents(DateTime.Today.AddDays(1), DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).AddDays(7).Date.Add(new TimeSpan(23, 59, 59))).Result;
            IReadOnlyCollection<IEvent> EventsMonth = Profile.GoogleClient.CalendarManager.GetEvents(DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).AddDays(7).Date.Add(new TimeSpan(23, 59, 59)), new(DateTime.Today.Date.Year, DateTime.Today.Month + 1, 1)).Result;

            WeatherResponse? WeatherToday = WeatherManager.GetWeatherForToday(Profile.Settings.ObjectToStore.WeatherSettings.Zip);

            if (EventsToday.Count > 0 || WeatherToday != null)
            {
                EmbedFieldBuilder Today = new()
                {
                    Name = $"TODAY • {DateTime.Today.ToLongDateString()}",
                };
                if(WeatherToday != null)
                {
                    decimal Temperature = WeatherToday.Main.Temp;
                    decimal TemperatureMax = WeatherToday.Main.TempMaximum;
                    decimal TemperatureMin = WeatherToday.Main.TempMinimum;
                    Today.Value = $"{Temperature}°C - {WeatherToday.Name}\nH: {TemperatureMax}°C L: {TemperatureMin}°C\n";
                }
                foreach(Event Event in EventsToday)
                {
                    if(Event.Start.DateTime.HasValue)
                        Today.Value += $"\n{Event.Start.DateTime.Value.ToShortTimeString()}\n```\n{Event.Summary}{Event.Description}\n```";
                }
                Today.Value += "\u200b\n";
                EmbedFieldBuilders.Add(Today);
            }

            if(EventsWeek.Count > 0)
            {
                EmbedFieldBuilder Week = new()
                {
                    Name = $"THIS WEEK",
                };
                foreach(Event Event in EventsWeek)
                {
                    if(Event.Start.DateTime.HasValue)
                        Week.Value += $"\n{Event.Start.DateTime.Value.ToShortDateString()} {Event.Start.DateTime.Value.ToShortTimeString()}\n```\n{Event.Summary}\n```";
                }
                Week.Value += "\u200b\n";
                EmbedFieldBuilders.Add(Week);
            }

            if(EventsMonth.Count > 0)
            {
                EmbedFieldBuilder Month = new()
                {
                    Name = $"THIS MONTH",
                    Value = "\u200b"
                };
                foreach(IEvent Event in EventsMonth)
                {
                    string ToAppend = $"\n{Event.Start.ToShortDateString()} {Event.Start.ToShortTimeString()}\n```\n{Event.Summary}\n```";
                    string? MonthValue = Month.Value.ToString();
                    if (MonthValue != null)
                    {
                        if (MonthValue.Length + ToAppend.Length <= 1024)
                            Month.Value += ToAppend;
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
            return EmbedFieldBuilders;
        }
    }
}
