using System;
using System.Data;
using System.IO;
using Haushaltsbuch.DataSets;

namespace Haushaltsbuch
{
    internal static class Payment
    {
        
        private static readonly HaushaltsbuchDS Dataset = new HaushaltsbuchDS();

        static Payment()
        {
            if (File.Exists("haushaltsbuch.xml")) {
                Dataset.ReadXml("haushaltsbuch.xml");
            }
            else
            {
                using (FileStream fs = File.Create("haushaltsbuch.xml"))
                {                   
                }                
            }
        }

        public static HaushaltsbuchDS.BuchungssatzDataTable CreateNewRecord(DateTime date, decimal amount)
        {
            var newRow = Dataset.Buchungssatz.NewBuchungssatzRow();
            newRow.Kategorie = string.Empty;
            newRow.Betrag = amount;
            newRow.Datum = date;
            newRow.Memo = string.Empty;
            newRow.Typ = (int)Types.BookingTypeEnum.Inbound;

            Dataset.Buchungssatz.Rows.Add(newRow);
            return Dataset.Buchungssatz;
        }

        public static void SaveRecord()
        {            
            Dataset.WriteXml("haushaltsbuch.xml");
        }

        public static HaushaltsbuchDS.BuchungssatzDataTable ReadAllRecords()
        {
            Dataset.Buchungssatz.Clear();
            Dataset.ReadXml("haushaltsbuch.xml");
            return Dataset.Buchungssatz;
        }

    }
}