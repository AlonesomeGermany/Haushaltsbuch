namespace hb
{
    static class Types
    {
        public const string TypeOutpayment = "auszahlung";
        public const string TypePayment = "einzahlung";
        public const string TypeOverview = "übersicht";

        public enum BookingTypeEnum
        {
            Inbound,
            Outbound
        }        
    }   
}
