using System;
using System.Collections.Generic;
using System.Linq;
using hb.DataSets;

namespace hb
{
    static class Cash
    {
        public static decimal GetCashAmount(HaushaltsbuchDS.BuchungssatzDataTable records, DateTime input)
        {            
            var result = from HaushaltsbuchDS.BuchungssatzRow r in records where r.Datum <= input.Date orderby r.Datum select r;
            return CalculateAmount(result);
        }        

        public static decimal GetCategoryAmount(HaushaltsbuchDS.BuchungssatzDataTable records, string category,
            DateTime input)
        {            
            var result = from HaushaltsbuchDS.BuchungssatzRow r in records where r.Datum <= input.Date && r.Kategorie == category orderby r.Datum select r;
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

        public static List<ViewModel> GetAllCategories(HaushaltsbuchDS.BuchungssatzDataTable table, DateTime date)
        {
            var result = new List<ViewModel>();
            var query = (from HaushaltsbuchDS.BuchungssatzRow r in table where r.Typ == (int)Types.BookingTypeEnum.Outbound && r.Datum <= date.Date orderby r.Kategorie select r).ToList();
            
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
                    result.Add(new ViewModel(0, previousAmount * (-1), previousCategory));
                    previousAmount = 0;
                }
                previousCategory = resultRow.Kategorie;
            }
            result.Add(new ViewModel(0, previousAmount * (-1), previousCategory));
            return result;
        }
    }
}
