﻿using System.Linq;
using NUnit.Framework;

namespace Hadrian.Common.Utility.Tests.MoneyTests
{
    [TestFixture]
    public class SimpleMoneyTest
    {
        [Test]
        public void Comparision()
        {
            // SimpleMoney objects should be equal if their significant digits are the same
            var money1 = new SimpleMoney(5.130000000000001m);
            var money2 = new SimpleMoney(5.13m);
            var money3 = new SimpleMoney(5.12m);

            Assert.IsTrue(money1 == money2);
            Assert.IsTrue(money1.InternalAmount != money2.InternalAmount);
            Assert.IsTrue(money1 != money3);
        }

        [Test]
        public void TestCreationOfBasicSimpleMoney()
        {
            //Default currency
            var money2 = new SimpleMoney(3000m);
            Assert.AreEqual("ZAR", money2.CurrencyCode);
            Assert.AreEqual("R", money2.CurrencySymbol);
            Assert.AreEqual("South African Rand", money2.CurrencyName);
            Assert.AreEqual(2, money2.DecimalDigits);

            //Implicit casting of int, decimal and double to SimpleMoney
            var money3 = new SimpleMoney(5.0d);
            var money4 = new SimpleMoney(5.0m);
            var money5 = new SimpleMoney(5);
            SimpleMoney money6 = 5.0d;
            SimpleMoney money7 = 5.0m;
            SimpleMoney money8 = 5;
            SimpleMoney money9 = 5.0f;
            SimpleMoney money10 = 5.0;
            Assert.IsTrue(money3 == money4 && money4 == money5 && money5 == money6 && money6 == money7 && money7 == money8 && money8 == money9 && money9 == money10);
        }

        [Test]
        public void TestSignificantDecimalDigits()
        {
            var money1 = new SimpleMoney(13000123.3349m);
            Assert.AreEqual("R 13,000,123.33", money1.ToString());
            // Can also use CurrencyCode string (catch code not found exception)
            var money2 = new SimpleMoney(13000123.335m);
            Assert.AreEqual("R 13,000,123.34", money2.ToString());

            // Test Amount rounding
            SimpleMoney money3 = 1.001m;
            Assert.AreEqual(0.40m, (money3 * 0.404).Amount);
            Assert.AreEqual(0.41m, (money3 * 0.40501).Amount);
            Assert.AreEqual(0.41m, (money3 * 0.404999999999999).Amount);
            money3 = 1.0;
            Assert.AreEqual(0.40m, (money3 * 0.404999999999999).Amount);

            //Very large numbers
            //Double is used internally, only 16 digits of accuracy can be guaranteed
            SimpleMoney money6 = 123456789012.34; //14 digits
            money6 *= 1.14; //will add another 2 digits of detail
            money6 /= 1.14;
            Assert.AreEqual(money6.Amount, 123456789012.34m);
        }

        [Test]
        public void TestOperators()
        {
            var money1 = new SimpleMoney(20);
            Assert.AreEqual("R 6.67", (money1 / 3).ToString());
            Assert.AreEqual("R 6.67", (money1 / 3m).ToString());
            Assert.AreEqual("R 6.67", (money1 / 3.0).ToString());
            Assert.AreEqual("R 0.00", (money1 * (1 / 3)).ToString());
            Assert.AreEqual("R 6.67", (money1 * (1m / 3m)).ToString());
            Assert.AreEqual("R 6.67", (money1 * (1d / 3d)).ToString());
            Assert.AreEqual("R 3.33", (money1 / 6).ToString());
            Assert.AreEqual("R 3.33", (money1 * (1.0 / 6.0)).ToString());

            // Operators use internal value
            var money2 = new SimpleMoney(0.01m);
            Assert.AreEqual("R 0.01", (money2 / 2).ToString());

            var money3 = new SimpleMoney(3);
            var money4 = new SimpleMoney(1d / 3d);
            var money5 = new SimpleMoney(6);
            var money6 = new SimpleMoney(1d / 6d);
            Assert.AreEqual("R 6.67", (money1 / money3).ToString());
            Assert.AreEqual("R 6.67", (money1 * money4).ToString());
            Assert.AreEqual("R 3.33", (money1 / money5).ToString());
            Assert.AreEqual("R 3.33", (money1 * money6).ToString());
            Assert.IsTrue((money3 + money5).Amount == 9);
            Assert.IsTrue((money5 - money3).Amount == 3);

            //Using implicit casting
            Assert.IsTrue(money3 == 3);
            Assert.IsTrue(money5 - money3 == 3);
            Assert.IsTrue(money3 + 3d == money5);
            Assert.IsTrue(money5 - 3 == money3);
            Assert.IsTrue(money5 + 2 == 8);
            Assert.IsTrue(money5 - 2 == 4d);
            Assert.IsTrue(2m + money3.Amount == 5);
        }

        [Test]
        public void TestAllocation()
        {
            var money1 = new SimpleMoney(10);
            var allocatedMoney1 = money1.Allocate(3);
            var total1 = new SimpleMoney();
            total1 = allocatedMoney1.Aggregate(total1, (current, t) => current + t);

            Assert.AreEqual("R 10.00", total1.ToString());
            Assert.AreEqual("R 3.34", allocatedMoney1[0].ToString());
            Assert.AreEqual("R 3.33", allocatedMoney1[1].ToString());
            Assert.AreEqual("R 3.33", allocatedMoney1[2].ToString());

            var money2 = new SimpleMoney(0.09m);
            var allocatedMoney2 = money2.Allocate(5);
            var total2 = new SimpleMoney();
            total2 = allocatedMoney2.Aggregate(total2, (current, t) => current + t);
            
            Assert.AreEqual("R 0.09", total2.ToString());
        }
    }
}