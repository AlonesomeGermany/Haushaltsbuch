using System;
using System.IO;
using hb.DataSets;

namespace hb
{
    internal static class Payment
    {
        internal static readonly HaushaltsbuchDS Dataset = new HaushaltsbuchDS();
        private static readonly bool HasSavedRecords;

        static Payment()
        {
            if (File.Exists("haushaltsbuch.xml") && File.Exists("haushaltsbuch.xsd") && File.ReadAllText("haushaltsbuch.xml") != string.Empty)
            {
                Dataset.ReadXml("haushaltsbuch.xml");
                Dataset.ReadXmlSchema("haushaltsbuch.xsd");
                HasSavedRecords = true;
            }
            else {               
                Dataset.WriteXmlSchema("haushaltsbuch.xsd");
            }
        }

        public static void CreateNewRecord(Types.BookingTypeEnum type, DateTime date, decimal amount)
        {
            var newRow = Dataset.Buchungssatz.NewBuchungssatzRow();
            newRow.Kategorie = string.Empty;
            newRow.Betrag = amount;
            newRow.Datum = date;
            newRow.Memo = string.Empty;
            newRow.Typ = (int) type;

            Dataset.Buchungssatz.Rows.Add(newRow);
        }

        public static void CreateNewRecord(Types.BookingTypeEnum type, DateTime date, decimal amount, string category,
            string memo)
        {
            var newRow = Dataset.Buchungssatz.NewBuchungssatzRow();
            newRow.Kategorie = category;
            newRow.Betrag = amount;
            newRow.Datum = date;
            newRow.Memo = memo;
            newRow.Typ = (int) type;

            Dataset.Buchungssatz.Rows.Add(newRow);
        }

        public static void SaveRecord()
        {
            Dataset.WriteXml("haushaltsbuch.xml");
        }

        public static HaushaltsbuchDS.BuchungssatzDataTable ReadAllRecords()
        {
            Dataset.Buchungssatz.Clear();
            if (HasSavedRecords)
            {
                Dataset.ReadXml("haushaltsbuch.xml");
            }
            return Dataset.Buchungssatz;
        }
    }
}