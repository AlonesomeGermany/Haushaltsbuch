using System;
using hb.DataSets;

namespace hb
{
    internal static class Hb
    {
       
        static void Main(string[] args)
        {
            if (args.Length > 0) {
                InputParameters readValues = Functions.GetInputValues(args);
                HaushaltsbuchDS.BuchungssatzDataTable table;
                ViewModel vm;
                decimal totalCash;
                switch (readValues.Type.ToLower())
                {
                    case Types.TypeOutpayment:                       
                        Payment.CreateNewRecord(Types.BookingTypeEnum.Outbound, readValues.Date, readValues.Amount, readValues.Category, readValues.Memo);
                        Payment.SaveRecord();
                        table = Payment.ReadAllRecords();
                        totalCash = Cash.GetCashAmount(table, readValues.Date);
                        var categoryCash = Cash.GetCategoryAmount(table, readValues.Category, readValues.Date);
                        vm = new ViewModel(totalCash, categoryCash, readValues.Category);
                        vm.PrintCashTotal();
                        vm.PrintCategoryTotal();
                        break;
                    case Types.TypePayment:                      
                        Payment.CreateNewRecord(Types.BookingTypeEnum.Inbound, readValues.Date, readValues.Amount);
                        Payment.SaveRecord();
                        table = Payment.ReadAllRecords();
                        totalCash = Cash.GetCashAmount(table, readValues.Date);
                        vm = new ViewModel(totalCash);
                        vm.PrintCashTotal();                        
                        break;
                    case Types.TypeOverview:
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
       
    }    

}
