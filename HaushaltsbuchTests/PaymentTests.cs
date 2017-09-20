using System;
using System.Security.Policy;
using hb;
using hb.DataSets;
using NUnit.Framework;

namespace hbTests
{
    [TestFixture]
    public class PaymentTests
    {

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Payment.CreateNewRecord(Types.BookingTypeEnum.Inbound, DateTime.Now, 100);
            Payment.CreateNewRecord(Types.BookingTypeEnum.Outbound, DateTime.Now, 100, "Test", "Only for testing");
        }


        [Test]
        public void CreateNewRecordTest_RecordsCreated()
        {           
            Assert.That(Payment.Dataset.Buchungssatz.Rows.Count, Is.EqualTo(2));
        }

        [Test]
        public void CreateNewRecordTest_DataOK()
        {            
            var category = ((HaushaltsbuchDS.BuchungssatzRow) Payment.Dataset.Buchungssatz.Rows[1]).Kategorie;
            var amount = ((HaushaltsbuchDS.BuchungssatzRow) Payment.Dataset.Buchungssatz.Rows[1]).Betrag;
            var date = ((HaushaltsbuchDS.BuchungssatzRow) Payment.Dataset.Buchungssatz.Rows[1]).Datum;
            var memo = ((HaushaltsbuchDS.BuchungssatzRow)Payment.Dataset.Buchungssatz.Rows[1]).Memo;
            var type = ((HaushaltsbuchDS.BuchungssatzRow)Payment.Dataset.Buchungssatz.Rows[1]).Typ;
            Assert.That(category, Is.EqualTo("Test"));
            Assert.That(amount, Is.EqualTo(100));
            Assert.That(date.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(memo, Is.EqualTo("Only for testing"));
            Assert.That(type, Is.EqualTo((int)Types.BookingTypeEnum.Outbound));
        }

    }
}