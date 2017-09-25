using System;

namespace hb.ViewModel
{
    class ViewModel
    {
        private readonly decimal _cashtotal;
        private readonly decimal _categorytotal;
        private readonly string _categorydescription;

        public ViewModel(decimal amount)
        {
            _cashtotal = amount;
        }

        public ViewModel(decimal cashamount, decimal categoryamount, string categorydescription)
        {
            _cashtotal = cashamount;
            _categorytotal = categoryamount;
            _categorydescription = categorydescription;
        }

        public string Cashtotal => string.Concat("Kassenbestand: ", _cashtotal, " EUR");
        public string Categorytotal => string.Concat(_categorydescription + ": ", _categorytotal * (-1), " EUR");

        public void PrintCashTotal()
        {
            Console.WriteLine(Cashtotal);
        }

        public void PrintCategoryTotal()
        {
            Console.WriteLine(Categorytotal);
        }        
    }    

}
