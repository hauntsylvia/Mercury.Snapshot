
using Google.Apis.Auth.OAuth2.Responses;
using Mercury.Snapshot.Objects.Structures.MercurySnapshot;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Consts
{
    public static class CommonRegisters
    {
        public static Register<TokenResponse> GoogleCredentialsRegister => new("Google User Credentials");
        public static Register<MercuryUserSettings> MercurySettingsRegister => new("Mercury User Settings");
        public static Register<StartupItems> MercuryStartupItemsRegister => new("Mercury Startup Items");

        public static Register<MercuryCalendar> CalendarsRegister => new("Calendars");
        public static Register<MercuryExpenditureLog> ExpenditureLogsRegister => new("Expenditure Logs");
    }
}
