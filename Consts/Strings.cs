using Google.Apis.Calendar.v3;
using Google.Apis.Sheets.v4;

namespace Mercury.Snapshot.Consts
{
    public static class Strings
    {
        internal static class MercuryStrings
        {
            public static string InputOWMAppId => "Input OpenWeatherMap application id.";
            public static string InputDiscordToken => "Input Discord token.";
            public static string MercuryStartupItemsKey => "Startup Items";
            public static Uri MercuryBaseUrl => new("https://mercury-bot.ml:443/");
        }
        internal static class GoogleStrings
        {
            public static string GoogleFileDatastoreLocation => "Tokens";
            public static string GoogleCredentialsFileLocation => "Google Credentials.json";
            public static Uri MercuryGoogleRedirectUrl => new("https://mercury-bot.ml:443/google-oauth2/GoogleAuthReceiver/");
            public static string[] Scopes => new string[] { CalendarService.Scope.CalendarReadonly };

        }
        internal static class SettingsStrings
        {
            public static string SettingsSaved => "Your settings have been saved.";
        }
        internal static class EmbedStrings
        {
            public static class Expenditures
            {
                public static string ExpenditureSuccessfullyLogged => "Expenditure successfully saved.";
                public static string ExpenditureLogFailed => "Something went wrong saving your expenditure.";
                public static string ExpenditureSyncFailed => "Expenditure log sync couldn't be completed.";
                public static string ExpenditureSyncCompleted => "Expenditure log sync completed! <3";
                public static string ExpenditureSyncLoading => "Your Expenditure logs are syncing..";
            }
            public static class Calendars
            {
                public static string CalendarEventSuccessfullyLogged => "Event has been saved to your Mercury calendar.";
                public static string CalendarEventFailureToLog => "Event couldn't be saved to your Mercury calendar.";
                public static string CalendarEventInvalidDateSupplied => "Event couldn't be saved to your Mercury calendar because the date you supplied is written in an invalid format.";
                public static string CalendarSyncFailure => "Calendar sync couldn't be completed.";
                public static string CalendarSyncSuccess => "Calendar sync completed! <3";
                public static string CalendarSyncLoading => "Your calendars are syncing..";
            }
        }
    }
}
