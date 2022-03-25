namespace Mercury.Snapshot.Objects.Structures.UserStructures.Personalization.Peronalizers
{
    internal class GoogleSheetsSettings
    {
        internal GoogleSheetsSettings(string? ExpenditureSpreadsheetId)
        {
            this.ExpenditureSpreadsheetId = ExpenditureSpreadsheetId;
        }

        internal string? ExpenditureSpreadsheetId { get; }
    }
}
