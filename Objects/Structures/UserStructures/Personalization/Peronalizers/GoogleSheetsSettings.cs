namespace Mercury.Snapshot.Objects.Structures.UserStructures.Personalization.Peronalizers
{
    public class GoogleSheetsSettings
    {
        public GoogleSheetsSettings(string? ExpenditureSpreadsheetId, string? ExpenditureSpreadsheetName)
        {
            this.ExpenditureSpreadsheetId = ExpenditureSpreadsheetId;
            this.ExpenditureSpreadsheetName = ExpenditureSpreadsheetName;
        }

        public string? ExpenditureSpreadsheetId { get; }

        public string? ExpenditureSpreadsheetName { get; }
    }
}
