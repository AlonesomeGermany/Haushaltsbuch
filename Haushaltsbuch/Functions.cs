using System;
using System.Globalization;

namespace hb
{
    static class Functions
    {
        public static bool IsDate(string input)
        {
            return DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var _);
        }

        public static DateTime GetDate(string input)
        {
            if (DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }
            return GetDate((DateTime?)null);
        }

        public static DateTime GetDate(DateTime? input)
        {
            if (input == null)
            {
                return DateTime.Now;
            }
            return (DateTime)input;
        }

        public static DateTime GetDate(short month, short year)
        {
            if (month > 0 && month < 13)
            {
                if (year > 0)
                {
                    if (year < 100)
                    {
                        year += 2000;
                    }
                    var datestring = year + "-" + month.ToString("D2") + "-" +
                                     DateTime.DaysInMonth(year, month);
                    return DateTime.ParseExact(datestring, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
            }
            return GetDate((DateTime?)null);
        }

        public static decimal Amount(string input)
        {
            decimal.TryParse(input, out var amount);
            return amount;
        }       
    }
}
