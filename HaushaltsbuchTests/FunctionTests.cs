using System;
using hb;
using hb.Static_Objects;
using NUnit.Framework;

namespace HaushaltsbuchTests
{
    [TestFixture]
    public class FunctionTests
    {
        [Test]
        public void GetDate_ValidInput_DateReturned()
        {
            var result = Functions.GetDate("28.02.1982");
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetDate_NoInvalidInput_DateNowReturned()
        {
            var result = Functions.GetDate("31.02.1982");
            Assert.That(result.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void GetDate_InputMonthYear_DateReturned()
        {
            var result = Functions.GetDate(4, 1982);
            Assert.That(result.Date, Is.EqualTo(Functions.GetDate("30.04.1982")));
        }

        [Test]
        public void GetInputValues_ValuesReturnedOutpayment()
        {
            var input = Functions.GetInputValues(new[]{Types.TypeOutpayment, DateTime.Now.ToString("dd.MM.yyyy"), "110", "Test Category"});
            Assert.That(input.Type, Is.EqualTo(Types.TypeOutpayment));
            Assert.That(input.Date.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(input.Amount, Is.EqualTo(110));
            Assert.That(input.Category, Is.EqualTo("Test Category"));
        }

        [Test]
        public void GetInputValues_ValuesReturnedPayment()
        {
            var input = Functions.GetInputValues(new[] { Types.TypePayment, DateTime.Now.ToString("dd.MM.yyyy"), "150"});
            Assert.That(input.Type, Is.EqualTo(Types.TypePayment));
            Assert.That(input.Date.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(input.Amount, Is.EqualTo(150));            
        }
    }
}