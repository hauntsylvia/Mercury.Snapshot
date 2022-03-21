namespace Mercury.Snapshot.Consts
{
    internal static class Strings
    {
        internal static string MercuryStartupItemsKey => "Startup Items";
        internal static string MercuryBaseUrl => "https://mercury-bot.ml:443/";
        internal static string MercuryGoogleRedirectUrl => "https://mercury-bot.ml:443/google-oauth2/GoogleAuthReceiver/";
        internal static string GoogleFileDatastoreLocation => "Tokens";
        internal static string GoogleCredentialsFileLocation => "Google Credentials.json";

        internal static class EmbedStrings
        {
            internal static class Expenditures
            {
                internal static string ExpenditureSuccessfullyLogged => "Expenditure successfully saved.";
                internal static string ExpenditureLogFailed => "Something went wrong saving your expenditure.";
            }
            internal static class Calendars
            {
                internal static string CalendarEventSuccessfullyLogged => "Event has been saved to your Mercury calendar.";
                internal static string CalendarEventFailureToLog => "Event couldn't be saved to your Mercury calendar.";
            }
        }
    }
}
