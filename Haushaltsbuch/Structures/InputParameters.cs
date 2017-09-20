using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hb
{
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
