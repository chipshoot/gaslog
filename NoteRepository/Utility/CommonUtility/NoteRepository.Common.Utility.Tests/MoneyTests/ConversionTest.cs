﻿using System;
using Hadrian.Common.Utility.Currency;
using NUnit.Framework;

namespace Hadrian.Common.Utility.Tests.MoneyTests
{
    [TestFixture]
    public class ConversionTest
    {
        [Test]
        public void TestConversion()
        {
            var money1 = new Money(12.34, CurrencyCodes.Usd);
            var money2 = new Money(12.34, CurrencyCodes.Zar);

            Money money3 = money1.Convert(CurrencyCodes.Zar, 7.8);

            Assert.AreEqual(money3.CurrencyCode, money2.CurrencyCode);
            Assert.AreNotEqual(money3, money2);
            Assert.IsTrue(money3 > money2);
            // No way to check if Rands are equal to dollars
            Assert.IsTrue(money1 != money3);
        }

        [Test]
        public void TestConverter()
        {
            var money1 = new Money(12.34, CurrencyCodes.Usd);
            var money2 = new Money(12.34, CurrencyCodes.Zar);
            Money.Converter = new SampleConverter();

            var money3 = money1.Convert(CurrencyCodes.Zar);

            Assert.AreEqual(money3.CurrencyCode, money2.CurrencyCode);
            Assert.AreNotEqual(money3, money2);
            Assert.IsTrue(money3 > money2);
            Assert.IsTrue(money1 != money3);
            Assert.AreNotEqual(money3, money1);

            // comparing apples to oranges possible with Converter!
            // Will only return a match if the Converter has the same rate for from -> to and (inverted) to -> from
            Money.AllowImplicitConversion = true;
            var m1To3 = Money.Converter.GetRate(money1.CurrencyCode, money3.CurrencyCode, DateTime.Now);
            var m3To1 = Money.Converter.GetRate(money3.CurrencyCode, money1.CurrencyCode, DateTime.Now);
            if (m1To3 == 1d / m3To1)
            {
                Assert.IsTrue(money3 == money1);
                Assert.IsTrue(money1 == money3);
                Assert.AreEqual(money3, money1);
            }
            else
            {
                Assert.IsFalse(money3 == money1);
                Assert.IsFalse(money1 == money3);
                Assert.AreNotEqual(money3, money1);
            }
        }

        [Test]
        public void TestOperations()
        {
            var money1 = new Money(12.34, CurrencyCodes.Usd);
            var money2 = new Money(12.34, CurrencyCodes.Zar);
            Money.Converter = new SampleConverter();
            Money.AllowImplicitConversion = true;

            // adding oranges to apples gives you apples
            var money3 = money1 + money2;
            Assert.AreEqual("USD", money3.CurrencyCode);

            // left side is ZAR and right side is USD, money3 gets converted back to ZAR
            // the same converter should return the same inverted rates
            var m1To3 = Money.Converter.GetRate(money1.CurrencyCode, money3.CurrencyCode, DateTime.Now);
            var m3To1 = Money.Converter.GetRate(money3.CurrencyCode, money1.CurrencyCode, DateTime.Now);
            if (m1To3 == 1d / m3To1)
                Assert.AreEqual(money2, money3 - money1);
            else
                Assert.AreNotEqual(money2, money3 - money1);
            // Mix up ZAR and USD. moneys converted only one way
            Assert.AreEqual(money1, money3 - money2);

            // Should fail if allowImplicitconversion is false (default)
            Money.AllowImplicitConversion = false;
            try
            {
                money3 = money1 + money2;
                Assert.Fail("Money type exception was not thrown");
            }
            catch (InvalidOperationException e)
            {
                Assert.AreEqual("Money type mismatch", e.Message);
            }
        }
    }
}