using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Haushaltsbuch.DataSets;

namespace Haushaltsbuch
{
    static class Cash
    {
        public static decimal GetCashAmount(HaushaltsbuchDS.BuchungssatzDataTable records, DateTime input)
        {
            decimal retValue = 0;
            var result = from HaushaltsbuchDS.BuchungssatzRow r in records where r.Datum <= input orderby r.Datum select r;
            foreach (HaushaltsbuchDS.BuchungssatzRow row in result)
            {
                if (row.Typ == (int) Types.BookingTypeEnum.Inbound)
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
    }
}
