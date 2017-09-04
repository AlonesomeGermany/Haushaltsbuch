using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haushaltsbuch
{
    class ViewModel
    {
        private readonly decimal _cashtotal;

        public ViewModel(decimal amount)
        {
            _cashtotal = amount;
        }

        public string Cashtotal => string.Concat("Kassenbestand: ", _cashtotal, " EUR");

        public void PrintCashTotal()
        {
            Console.WriteLine(Cashtotal);
        }
    }    

}
