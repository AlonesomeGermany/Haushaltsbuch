using System;
using System.Collections.Generic;
using System.Linq;
using hb.DataSets;

namespace hb.Static_Objects
{
    static class Cash
    {
        public static decimal GetCashAmount(HaushaltsbuchDS.BuchungssatzDataTable records, DateTime input)
        {            
            var result = from HaushaltsbuchDS.BuchungssatzRow r in records where r.Datum.Date <= input.Date orderby r.Datum select r;
            return CalculateAmount(result);
        }        

        public static decimal GetCategoryAmount(HaushaltsbuchDS.BuchungssatzDataTable records, string category,
            DateTime input)
        {            
            var result = from HaushaltsbuchDS.BuchungssatzRow r in records where r.Datum.Date <= input.Date && r.Kategorie == category orderby r.Datum select r;
            return CalculateAmount(result);
        }

        internal static decimal CalculateAmount(IOrderedEnumerable<HaushaltsbuchDS.BuchungssatzRow> result)
        {
            decimal retValue = 0;
            foreach (HaushaltsbuchDS.BuchungssatzRow row in result)
            {
                if (row.Typ == (int)Types.BookingTypeEnum.Inbound)
                {
                    retValue += row.Betrag;
                }
                else
                {
                    retValue -= row.Betrag;
                }
            }
            return retValue;
        }

        public static List<ViewModel.ViewModel> GetAllCategories(HaushaltsbuchDS.BuchungssatzDataTable table, DateTime date)
        {
            var result = new List<ViewModel.ViewModel>();
            var query = (from HaushaltsbuchDS.BuchungssatzRow r in table where r.Typ == (int)Types.BookingTypeEnum.Outbound && r.Datum.Date <= date.Date orderby r.Kategorie select r).ToList();
            
            var previousCategory = query.FirstOrDefault()?.Kategorie ?? string.Empty;            
            var previousAmount = query.FirstOrDefault()?.Betrag ?? 0;
            foreach (var resultRow in query.Skip(1))
            {
                if (previousCategory == resultRow.Kategorie)
                {
                    previousAmount += resultRow.Betrag;
                }
                else
                {
                    result.Add(new ViewModel.ViewModel(0, previousAmount * (-1), previousCategory));
                    previousAmount = resultRow.Betrag;
                }
                previousCategory = resultRow.Kategorie;
            }
            if (previousCategory != string.Empty)
            {
                result.Add(new ViewModel.ViewModel(0, previousAmount * (-1), previousCategory));
            }
            return result;
        }
    }
}
