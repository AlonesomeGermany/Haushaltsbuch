using System;
using System.Collections.Generic;
using System.Linq;
using hb;
using hb.DataSets;
using NUnit.Framework;

namespace hbTests
{
    [TestFixture]
    public class CashTests
    {
        private HaushaltsbuchDS.BuchungssatzDataTable _testDataTable;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _testDataTable = new HaushaltsbuchDS.BuchungssatzDataTable();
            // Eingang1
            CreateTestRow(string.Empty, 1200, Functions.GetDate("01.01.1982"), string.Empty,
                Types.BookingTypeEnum.Inbound);
            // Ausgang1-1
            CreateTestRow("Miete", 700, Functions.GetDate("02.01.1982"), string.Empty, Types.BookingTypeEnum.Outbound);
            // Ausgang1-2
            CreateTestRow("Lebensmittel", 100, Functions.GetDate("03.01.1982"), string.Empty,
                Types.BookingTypeEnum.Outbound);
            // Ausgang1-3
            CreateTestRow("Getränke", 50, Functions.GetDate("04.01.1982"), string.Empty,
                Types.BookingTypeEnum.Outbound);
            // Ausgang1-4
            CreateTestRow("Kino", 10, Functions.GetDate("05.01.1982"), "Star Wars Episode II",
                Types.BookingTypeEnum.Outbound);
            // Eingang2                                  
            CreateTestRow(string.Empty, 1150, Functions.GetDate("01.02.1982"), string.Empty,
                Types.BookingTypeEnum.Inbound);
            // Ausgang2-1
            CreateTestRow("Miete", 700, Functions.GetDate("02.02.1982"), string.Empty, Types.BookingTypeEnum.Outbound);
            // Ausgang2-2
            CreateTestRow("Lebensmittel", 100, Functions.GetDate("03.02.1982"), string.Empty,
                Types.BookingTypeEnum.Outbound);
        }

        private void CreateTestRow(string category, decimal amount, DateTime date,
            string memo, Types.BookingTypeEnum type)
        {
            var testRow = _testDataTable.NewBuchungssatzRow();
            testRow.Kategorie = category;
            testRow.Betrag = amount;
            testRow.Datum = date;
            testRow.Memo = memo;
            testRow.Typ = (int) type;
            _testDataTable.AddBuchungssatzRow(testRow);
        }

        [Test]
        public void CalculateAmount_TwoValuesAdded_CorrectAmmount()
        {
            var rows = new List<HaushaltsbuchDS.BuchungssatzRow>
            {
                _testDataTable.ElementAt(0),
                _testDataTable.ElementAt(1)
            };
            var result = Cash.CalculateAmount(rows.OrderBy(x => x.Datum));
            Assert.That(result, Is.EqualTo(500));
        }

        [Test]
        public void GetCashAmount_ReturnCorrectValue()
        {
            var result = Cash.GetCashAmount(_testDataTable, Functions.GetDate("03.01.1982"));
            Assert.That(result, Is.EqualTo(400));
        }

        [Test]
        public void GetCategoryAmount_ReturnCorrectValue()
        {
            var result = Cash.GetCategoryAmount(_testDataTable, "Miete", Functions.GetDate("02.02.1982"));
            Assert.That(result, Is.EqualTo(-1400));
        }

        [Test]
        public void GetAllCategories_AllCategoriesReturned()
        {
            var result = Cash.GetAllCategories(_testDataTable, Functions.GetDate("28.02.1982"));
            Assert.That(result.Count, Is.EqualTo(4));
        }
    }
}