﻿using System.Linq.Expressions;
using hb;
using NUnit.Framework;

namespace hbTests
{
    [TestFixture]
    public class ViewModelTest
    {
        [Test]
        public void VieModelCreated_PropertiesCorrect()
        {
           var model = new ViewModel(100, 150, "Test Category");
           Assert.That(model.Cashtotal, Is.EqualTo("Kassenbestand: 100 EUR"));
           Assert.That(model.Categorytotal, Is.EqualTo("Test Category: -150 EUR"));           
        }
    }
}