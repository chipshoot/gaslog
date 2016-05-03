using System.Linq;
using Hadrian.Common.Utility.Currency;
using NUnit.Framework;

namespace Hadrian.Common.Utility.Tests.MoneyTests
{
    [TestFixture]
    public class MoneyTest
    {
        [Test]
        public void Comparison()
        {
            // Money objects should be equal if their significant digits are the same
            var money1 = new Money(5.130000000000001m);
            var money2 = new Money(5.13m);
            var money3 = new Money(5.12m);

            Assert.IsTrue(money1 == money2);
            Assert.IsTrue(money1.InternalAmount != money2.InternalAmount);
            Assert.IsTrue(money1 != money3);
            
            // Different Currencies aren't equal
            var money4 = new Money(5.12m, CurrencyCodes.Usd);
            Assert.IsTrue(money3 != money4);
        }

        [Test]
        public void TestCreationOfBasicMoney()
        {
            //Locale specific formatting
            var money1 = new Money(2000.1234567m, CurrencyCodes.Usd);
            Assert.AreEqual("$2,000.12", money1.ToString());

            //Default currency
            var money2 = new Money(3000m);
            Assert.AreEqual("ZAR", money2.CurrencyCode);
            Assert.AreEqual("R", money2.CurrencySymbol);
            Assert.AreEqual("South African Rand", money2.CurrencyName);
            Assert.AreEqual(2, money2.DecimalDigits);

            //Implicit casting of int, decimal and double to Money
            var money3 = new Money(5.0d);
            var money4 = new Money(5.0m);
            var money5 = new Money(5);
            Money money6 = 5.0d;
            Money money7 = 5.0m;
            Money money8 = 5;
            Money money9 = 5.0f;
            Money money10 = 5.0;
            Assert.IsTrue(money3 == money4 && money4 == money5 && money5 == money6 && money6 == money7 && money7 == money8 && money8 == money9 && money9 == money10);

            // African currencies
            var money11 = new Money(123456789, CurrencyCodes.Zwl);
            Assert.AreEqual("Z$123,456,789.00", money11.ToString()); //lol: cents in Zimbabwe

            //Generic 3char currency code formatting instead of locale based with symbols
            Assert.AreEqual("ZWL 123,456,789.00", money11.ToString(true));
            Assert.AreEqual("Usd 2,000.12", money1.ToString(true));
            Assert.AreEqual("ZAR 3,000.00", money2.ToString(true));
        }

        [Test]
        public void TestSignificantDecimalDigits()
        {
            var money1 = new Money(13000123.3349m, CurrencyCodes.Usd);
            Assert.AreEqual("$13,000,123.33", money1.ToString());
            // Can also use CurrencyCode string (catch code not found exception)
            var money2 = new Money(13000123.335m, "Usd");
            Assert.AreEqual("$13,000,123.34", money2.ToString());

            // Test Amount rounding
            Money money3 = 1.001m;
            Assert.AreEqual(0.40m, (money3 * 0.404).Amount);
            Assert.AreEqual(0.41m, (money3 * 0.40501).Amount);
            Assert.AreEqual(0.41m, (money3 * 0.404999999999999).Amount);
            money3 = 1.0;
            Assert.AreEqual(0.40m, (money3 * 0.404999999999999).Amount);

            //Different significant digits
            var money4 = new Money(2.499m, CurrencyCodes.Usd);
            Assert.AreEqual("$2.50", money4.ToString());

            var money5 = new Money(2.499m, CurrencyCodes.Jpy);
            Assert.AreEqual("¥2", money5.ToString());

            //Very large numbers
            //Double is used internally, only 16 digits of accuracy can be guaranteed
            Money money6 = 123456789012.34; //14 digits
            money6 *= 1.14; //will add another 2 digits of detail
            money6 /= 1.14;
            Assert.AreEqual(money6.Amount, 123456789012.34m);
        }

        [Test]
        public void TestOperators()
        {
            var money1 = new Money(20, CurrencyCodes.Eur);
            Assert.AreEqual("6,67 €", (money1 / 3).ToString());
            Assert.AreEqual("6,67 €", (money1 / 3m).ToString());
            Assert.AreEqual("6,67 €", (money1 / 3.0).ToString());
            Assert.AreEqual("0,00 €", (money1 * (1 / 3)).ToString());
            Assert.AreEqual("6,67 €", (money1 * (1m / 3m)).ToString());
            Assert.AreEqual("6,67 €", (money1 * (1d / 3d)).ToString());
            Assert.AreEqual("3,33 €", (money1 / 6).ToString());
            Assert.AreEqual("3,33 €", (money1 * (1.0 / 6.0)).ToString());

            // Operators use internal value
            var money2 = new Money(0.01m);
            Assert.AreEqual("R 0.01", (money2 / 2).ToString());

            var money3 = new Money(3, CurrencyCodes.Eur);
            var money4 = new Money(1d / 3d, CurrencyCodes.Eur);
            var money5 = new Money(6, CurrencyCodes.Eur);
            var money6 = new Money(1d / 6d, CurrencyCodes.Eur);
            Assert.AreEqual("6,67 €", (money1 / money3).ToString());
            Assert.AreEqual("6,67 €", (money1 * money4).ToString());
            Assert.AreEqual("3,33 €", (money1 / money5).ToString());
            Assert.AreEqual("3,33 €", (money1 * money6).ToString());
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
            var money1 = new Money(10);
            var allocatedMoney1 = money1.Allocate(3);
            var total1 = new Money();

            total1 = allocatedMoney1.Aggregate(total1, (current, t) => current + t);

            Assert.AreEqual("R 10.00", total1.ToString());
            Assert.AreEqual("R 3.34", allocatedMoney1[0].ToString());
            Assert.AreEqual("R 3.33", allocatedMoney1[1].ToString());
            Assert.AreEqual("R 3.33", allocatedMoney1[2].ToString());

            var money2 = new Money(0.09m, CurrencyCodes.Usd);
            var allocatedMoney2 = money2.Allocate(5);
            
            var total2 = new Money(CurrencyCodes.Usd);
            total2 = allocatedMoney2.Aggregate(total2, (current, t) => current + t);

            Assert.AreEqual("$0.09", total2.ToString());
        }
    }
}