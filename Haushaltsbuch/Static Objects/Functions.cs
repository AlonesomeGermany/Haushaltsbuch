using System;
using System.Globalization;
using System.Linq;

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

        internal static InputParameters GetInputValues(string[] args)
        {
            var typeValue = args[0].ToLower();
            switch (typeValue)
            {
                case Types.TypeOutpayment:
                    if (IsDate(args[1]))
                    {
                        var dateValue = GetDate(args[1]);
                        var amountValue = Amount(args[2]);
                        var categoryValue = args[3];
                        var memoValue = args.ElementAtOrDefault(4);
                        return new InputParameters(typeValue, dateValue, amountValue, categoryValue, memoValue);
                    }
                    else
                    {
                        var amountValue = Amount(args[1]);
                        var categoryValue = args[2];
                        var memoValue = args.ElementAtOrDefault(3);
                        var dateValue = GetDate(string.Empty);
                        return new InputParameters(typeValue, dateValue, amountValue, categoryValue, memoValue);
                    }
                case Types.TypePayment:
                    if (args.Length == 3)
                    {
                        var dateValue = GetDate(args[1]);
                        var amountValue = Amount(args[2]);
                        return new InputParameters(typeValue, dateValue, amountValue);
                    }
                    else
                    {
                        var dateValue = GetDate((DateTime?)null);
                        var amountValue = Amount(args[1]);
                        return new InputParameters(typeValue, dateValue, amountValue);
                    }

                case Types.TypeOverview:
                    if (args.Length == 3)
                    {
                        var month = short.Parse(args[1]);
                        var year = short.Parse(args[2]);
                        var dateValue = GetDate(month, year);
                        return new InputParameters(typeValue, dateValue);
                    }
                    else
                    {
                        return new InputParameters(typeValue);
                    }
            }
            return new InputParameters();
        }
    }
}
