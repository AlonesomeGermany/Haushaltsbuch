using System;
using System.Linq;
using hb.DataSets;

namespace hb
{
    internal static class Hb
    {
        private const string TypeOutpayment = "auszahlung";
        private const string TypePayment = "einzahlung";
        private const string TypeOverview = "übersicht";

        static void Main(string[] args)
        {
            if (args.Length > 0) {
                InputParameters readValues = GetInputValues(args);
                HaushaltsbuchDS.BuchungssatzDataTable table;
                ViewModel vm;
                decimal totalCash;
                switch (readValues.Type.ToLower())
                {
                    case TypeOutpayment:                       
                        Payment.CreateNewRecord(Types.BookingTypeEnum.Outbound, readValues.Date, readValues.Amount, readValues.Category, readValues.Memo);
                        Payment.SaveRecord();
                        table = Payment.ReadAllRecords();
                        totalCash = Cash.GetCashAmount(table, readValues.Date);
                        var categoryCash = Cash.GetCategoryAmount(table, readValues.Category, readValues.Date);
                        vm = new ViewModel(totalCash, categoryCash, readValues.Category);
                        vm.PrintCashTotal();
                        vm.PrintCategoryTotal();
                        break;
                    case TypePayment:                      
                        Payment.CreateNewRecord(Types.BookingTypeEnum.Inbound, readValues.Date, readValues.Amount);
                        Payment.SaveRecord();
                        table = Payment.ReadAllRecords();
                        totalCash = Cash.GetCashAmount(table, readValues.Date);
                        vm = new ViewModel(totalCash);
                        vm.PrintCashTotal();                        
                        break;
                    case TypeOverview:
                        table = Payment.ReadAllRecords();
                        totalCash = Cash.GetCashAmount(table, readValues.Date);
                        vm = new ViewModel(totalCash);
                        vm.PrintCashTotal();
                        var categories = Cash.GetAllCategories(table, readValues.Date);
                        foreach (ViewModel model in categories)
                        {
                            model.PrintCategoryTotal();
                        }

                        break;
                }
                Console.ReadLine();
            }
        }

        private static InputParameters GetInputValues(string[] args)
        {
            var typeValue = args[0].ToLower();
            switch (typeValue)
            {
                case TypeOutpayment:                   
                    if (Functions.IsDate(args[1]))
                    {
                        var dateValue = Functions.GetDate(args[1]);
                        var amountValue = Functions.Amount(args[2]);
                        var categoryValue = args[3];
                        var memoValue = args.ElementAtOrDefault(4);
                        return new InputParameters(typeValue, dateValue, amountValue, categoryValue, memoValue);
                    }
                    else
                    {
                        var amountValue = Functions.Amount(args[1]);
                        var categoryValue = args[2];
                        var memoValue = args.ElementAtOrDefault(3);
                        var dateValue = Functions.GetDate(string.Empty);
                        return new InputParameters(typeValue, dateValue, amountValue, categoryValue, memoValue);
                    }                                     
                case TypePayment:
                    if (args.Length == 3)
                    {
                        var dateValue = Functions.GetDate(args[1]);
                        var amountValue = Functions.Amount(args[2]);
                        return new InputParameters(typeValue, dateValue, amountValue);
                    } else
                    {
                        var dateValue = Functions.GetDate((DateTime?) null);
                        var amountValue = Functions.Amount(args[1]);
                        return new InputParameters(typeValue, dateValue, amountValue);
                    }
                   
                case TypeOverview:
                    if (args.Length == 3)
                    {
                        var month = short.Parse(args[1]);
                        var year = short.Parse(args[2]);
                        var dateValue = Functions.GetDate(month, year);
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

    class InputParameters
    {
        public readonly string Type;
        public readonly DateTime Date;
        public readonly decimal Amount;
        public readonly string Category;
        public readonly string Memo;

        public InputParameters()
        {
            Type = null;
            Date = DateTime.Now;
            Amount = 0;
            Category = null;
            Memo = null;
        }

        public InputParameters(string typeValue)
        {
            Type = typeValue;
            Date = DateTime.Now;
            Amount = 0;
            Category = null;
            Memo = null;
        }

        public InputParameters(string typeValue, DateTime dateValue)
        {
            Type = typeValue;
            Date = dateValue;
            Amount = 0;
            Category = null;
            Memo = null;
        }

        public InputParameters(string typeValue, DateTime dateValue, decimal amountValue)
        {
            Type = typeValue;
            Date = dateValue;
            Amount = amountValue;
            Category = string.Empty;
            Memo = string.Empty;
        }

        public InputParameters(string typeValue, DateTime dateValue, decimal amountValue, string categoryValue)
        {
            Type = typeValue;
            Date = dateValue;
            Amount = amountValue;
            Category = categoryValue;
            Memo = string.Empty;
        }

        public InputParameters(string typeValue, DateTime dateValue, decimal amountValue, string categoryValue, string memoValue)
        {
            Type = typeValue;
            Date = dateValue;
            Amount = amountValue;
            Category = categoryValue;
            Memo = memoValue;
        }

    }

}
