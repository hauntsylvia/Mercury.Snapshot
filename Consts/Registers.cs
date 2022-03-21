
using Google.Apis.Auth.OAuth2.Responses;
using Mercury.Snapshot.Objects.Structures.MercurySnapshot;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Consts
{
    internal static class Registers
    {
        internal static Register<TokenResponse> GoogleCredentialsRegister => new("Google User Credentials");
        internal static Register<MercuryUserSettings> MercurySettingsRegister => new("Mercury User Settings");
        internal static Register<StartupItems> MercuryStartupItemsRegister => new("Mercury Startup Items");

        internal static Register<MercuryCalendar> CalendarsRegister => new("Calendars");
        internal static Register<MercuryExpenditureLog> ExpenditureLogsRegister => new("Expenditure Logs");
    }
}
