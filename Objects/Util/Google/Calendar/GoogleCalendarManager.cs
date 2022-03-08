using System;
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

namespace Mercury.Snapshot.Objects.Util.Google.Calendar
{
    internal class GoogleCalendarManager
    {
        internal GoogleCalendarManager(GoogleApp CurrentApp)
        {
            this.CurrentApp = CurrentApp;
            this.Service = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleApp.GetUserCredential(),
                ApplicationName = GoogleApp.ApplicationName,
            });
        }

        private GoogleApp CurrentApp { get; }
        internal CalendarService Service { get; set; }

        internal IReadOnlyList<Event> GetIzolabellasEvents(EventsResource.ListRequest? Request)
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
            return (IReadOnlyList<Event>)Events.Items;
        }
    }
}
