using System;
using hb;
using NUnit.Framework;

namespace hbTests
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
    }
}