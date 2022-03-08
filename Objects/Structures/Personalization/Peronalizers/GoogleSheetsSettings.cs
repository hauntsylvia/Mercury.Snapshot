using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Personalization.Peronalizers
{
    internal class GoogleSheetsSettings
    {
        internal GoogleSheetsSettings(string? ExpenditureSpreadsheetId)
        {
            this.ExpenditureSpreadsheetId = ExpenditureSpreadsheetId;
        }

        public string? ExpenditureSpreadsheetId { get; }
    }
}
