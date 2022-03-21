namespace Mercury.Snapshot.Objects.Structures.UserStructures.Personalization.Peronalizers
{
    public class GoogleSheetsSettings
    {
        public GoogleSheetsSettings(string? ExpenditureSpreadsheetId)
        {
            this.ExpenditureSpreadsheetId = ExpenditureSpreadsheetId;
        }

        public string? ExpenditureSpreadsheetId { get; }
    }
}
