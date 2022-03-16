

using Google.Apis.Auth.OAuth2.Responses;
using Mercury.Snapshot.Objects.Structures.Calendars;
using Mercury.Snapshot.Objects.Structures.Financial;
using Mercury.Snapshot.Objects.Structures.Personalization;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Consts
{
    internal class Registers
    {
        internal static readonly Register<TokenResponse> GoogleCredentialsRegister = new("Google User Credentials");
        internal static readonly Register<MercuryUserSettings> MercurySettingsRegister = new("Mercury User Settings");
        internal static readonly Register<MercuryCalendar> CalendarsRegister = new("Calendars");
        internal static readonly Register<MercuryExpenditureLog> ExpenditureLogsRegister = new("Expenditure Logs");
    }
}
