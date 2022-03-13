﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using Mercury.Snapshot.Objects.Util.Google.General;
using Mercury.Snapshot.Objects.Util.Generics;
using Mercury.Snapshot.Objects.Structures.Mercury.Calendar;

namespace Mercury.Snapshot.Objects.Util.Google.Calendar
{
    public class GoogleCalendarManager
    {
        public GoogleCalendarManager()
        {
            this.Service = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleApp.GetUserCredential(),
                ApplicationName = GoogleApp.ApplicationName,
            });
        }

        public CalendarService Service { get; set; }

        public IReadOnlyList<IEvent> GetIzolabellasEvents(EventsResource.ListRequest? Request)
        {
            if (Request == null)
            {
                Request = this.Service.Events.List("primary");
                Request.TimeMin = DateTime.Now;
                Request.ShowDeleted = false;
                Request.SingleEvents = true;
                Request.MaxResults = 2500;
                Request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            }
            Events Events = Request.Execute();
            List<IEvent> EventItems = new();
            foreach(Event Event in Events.Items)
            {
                string RFC3339Z = "yyyy-MM-dd'T'HH:mm:ss.fffZ";
                string RFC3339 = "yyyy-MM-dd'T'HH:mm:sszzz"; //2022-03-14T12:00:00-04:00'
                string GeneralDate = "yyyy-MM-dd";
                string GeneralDateTime = "yyyy-MM-dd HH:mm:ss";
                DateTime Created = DateTime.ParseExact(Event.CreatedRaw, RFC3339Z, null);
                DateTime Updated = DateTime.ParseExact(Event.UpdatedRaw, RFC3339Z, null);
                if(!DateTime.TryParseExact(Event.Start.DateTimeRaw, RFC3339, null, System.Globalization.DateTimeStyles.None, out DateTime Start))
                {
                    DateTime.TryParseExact(Event.Start.Date, new string[] { GeneralDate, GeneralDateTime }, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out Start);
                }
                if(!DateTime.TryParseExact(Event.End.DateTimeRaw, RFC3339, null, System.Globalization.DateTimeStyles.None, out DateTime End))
                {
                    DateTime.TryParseExact(Event.End.Date, new string[] { GeneralDate, GeneralDateTime }, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out End);
                }
                EventItems.Add(new MercuryEvent(Event.Summary, Event.Description, Updated, Created, Start, End, "Google"));
            }
            return EventItems;
        }
    }
}
