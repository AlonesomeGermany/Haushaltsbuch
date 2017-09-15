using System;
using System.IO;
using hb.DataSets;

namespace hb
{
    internal static class Payment
    {
        private static readonly HaushaltsbuchDS Dataset = new HaushaltsbuchDS();

        static Payment()
        {
            if (File.Exists("haushaltsbuch.xml")) Dataset.ReadXml("haushaltsbuch.xml");
            else
                using (File.Create("haushaltsbuch.xml"))
                {
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
            Dataset.ReadXml("haushaltsbuch.xml");
            return Dataset.Buchungssatz;
        }
    }
}