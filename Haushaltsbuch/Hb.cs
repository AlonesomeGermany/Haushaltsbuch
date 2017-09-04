using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Haushaltsbuch
{
    class Hb
    {
        private const string TypeOutpayment = "auszahlung";
        private const string TypePayment = "einzahlung";
        private const string TypeOverview = "übersicht";

        static void Main(string[] args)
        {
            if (args.Length > 0) {
                var type = args[0].ToLower();
                string category;
                decimal amount;
                DateTime date;
                string memo;
                switch (type)
                {
                    case TypeOutpayment:
                        date = GetInputValues(args).Item1;
                        break;
                    case TypePayment:
                        date = GetInputValues(args).Item1;
                        amount = GetInputValues(args).Item2;
                        Payment.CreateNewRecord(date, amount);
                        Payment.SaveRecord();
                        var table = Payment.ReadAllRecords();
                        var totalCash = Cash.GetCashAmount(table, date);
                        var vm = new ViewModel(totalCash);
                        vm.PrintCashTotal();                        
                        break;
                    case TypeOverview:
                        if (short.TryParse(args[1], out var month) && short.TryParse(args[2], out var year))
                        {
                            //date = Functions.GetDate(month, year);
                            //if (date != null)
                            //{

                            //}
                        }
                        break;
                }
                Console.ReadLine();
            }
        }

        private static Tuple<DateTime, decimal> GetInputValues(string type, string[] args)
        {
            switch (type)
            {
                case TypeOutpayment:
                    if (args.Length == 3)
                    {
                        
                    }
                    break;
                case TypePayment:
                    if (args.Length == 3)
                    {
                       return Tuple.Create(Functions.GetDate(args[1]), Functions.Amount(args[2]));                
                    }
                    if (args.Length == 2)
                    {
                        return Tuple.Create(Functions.GetDate((DateTime?) null), Functions.Amount(args[1]));                
                    }
                    break;
            }
            return null;
        }       
    }
}
