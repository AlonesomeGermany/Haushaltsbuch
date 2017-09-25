using hb.Static_Objects;

namespace hb
{
    internal static class Hb
    {       
        static void Main(string[] args)
        {
            if (args.Length > 0) {
                var readValues = Functions.GetInputValues(args);                                             
                switch (readValues.Type)
                {
                    case Types.TypeOutpayment:
                    {
                        Payment.CreateNewRecord(Types.BookingTypeEnum.Outbound, readValues.Date, readValues.Amount,
                            readValues.Category, readValues.Memo);
                        Payment.SaveRecord();
                        var table = Payment.ReadAllRecords();
                        var totalCash = Cash.GetCashAmount(table, readValues.Date);
                        var categoryCash = Cash.GetCategoryAmount(table, readValues.Category, readValues.Date);
                        var vm = new ViewModel.ViewModel(totalCash, categoryCash, readValues.Category);
                        vm.PrintCashTotal();
                        vm.PrintCategoryTotal();
                        break;
                    }
                    case Types.TypePayment:
                    {
                        Payment.CreateNewRecord(Types.BookingTypeEnum.Inbound, readValues.Date, readValues.Amount);
                        Payment.SaveRecord();
                        var  table = Payment.ReadAllRecords();
                        var totalCash = Cash.GetCashAmount(table, readValues.Date);
                        var vm = new ViewModel.ViewModel(totalCash);
                        vm.PrintCashTotal();
                        break;                            
                    }
                    case Types.TypeOverview:
                    {
                        var table = Payment.ReadAllRecords();
                        var totalCash = Cash.GetCashAmount(table, readValues.Date);
                        var vm = new ViewModel.ViewModel(totalCash);
                        vm.PrintCashTotal();
                        var categories = Cash.GetAllCategories(table, readValues.Date);
                        foreach (var model in categories)
                        {
                            model.PrintCategoryTotal();
                        }
                        break;
                    }
                }               
            }
        }
       
    }    

}
