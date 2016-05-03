/*
 * This SimpleMoney class gives you the ability to work with money of a single currency.
 * It is a simpler version of the Money class in this Assembly - Supports all features except multiple currencies.
 * Money is not Derived from SimpleMoney, because of limited re-use due to the absence of virtual operators.
 * Polymorphism is not applicable as one should only use the one or the other in a project.
 * Performance was also a critical measure, so storage and functional redundancy had to be minimized.
 *
 * Important!
 * This Money class uses double to store the Money value internally.
 * Only 15 decimal digits of accuracy are guaranteed! (16 if the first digit is smaller than 9)
 * It should be fairly simple to replace the internal double with a decimal if this is not sufficient and performance is not an issue.
 */

using System;
using System.Globalization;
using Hadrian.Common.Utility.Currency;

namespace Hadrian.Common.Utility.Tests.MoneyTests
{
    public class SimpleMoney : IComparable<SimpleMoney>, IEquatable<SimpleMoney>, IComparable
    {
        #region private fields

        private static readonly CurrencyCodes currencyCode = (CurrencyCodes)Enum.Parse(typeof(CurrencyCodes), new RegionInfo(CultureInfo.CurrentCulture.LCID).ISOCurrencySymbol);

        private double _amount;

        #endregion private fields

        #region Constructors

        public SimpleMoney()
            : this(0d)
        {
        }

        public SimpleMoney(long amount)
            : this((double)amount)
        {
        }

        public SimpleMoney(decimal amount)
            : this((double)amount)
        {
        }

        public SimpleMoney(double amount)
        {
            this._amount = amount;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the CurrentCulture from the CultureInfo object and creates a CurrencyCodes enum object.
        /// </summary>
        /// <returns>The CurrencyCodes enum of the current locale.</returns>
        public static CurrencyCodes LocalCurrencyCode
        {
            get
            {
                return currencyCode;
            }
        }

        /// <summary>
        /// Rounds the _amount to the number of significant decimal digits
        /// of the associated currency using MidpointRounding.AwayFromZero.
        /// </summary>
        /// <returns>A decimal with the _amount rounded to the significant number of decimal digits.</returns>
        public decimal Amount
        {
            get
            {
                return Decimal.Round((Decimal)_amount, this.DecimalDigits, MidpointRounding.AwayFromZero);
            }
        }

        public string CurrencyCode
        {
            get { return Currency.Currency.Get(currencyCode).Code; }
        }

        public string CurrencyName
        {
            get { return Currency.Currency.Get(currencyCode).EnglishName; }
        }

        public string CurrencySymbol
        {
            get { return Currency.Currency.Get(currencyCode).Symbol; }
        }

        /// <summary>
        /// Gets the number of decimal digits for the associated currency.
        /// </summary>
        /// <returns>An int containing the number of decimal digits.</returns>
        public int DecimalDigits
        {
            get { return Currency.Currency.Get(currencyCode).NumberFormat.CurrencyDecimalDigits; }
        }

        /// <summary>
        /// Accesses the internal representation of the value of the Money
        /// </summary>
        /// <returns>A decimal with the internal _amount stored for this Money.</returns>
        public double InternalAmount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        /// <summary>
        /// Truncates the _amount to the number of significant decimal digits
        /// of the associated currency.
        /// </summary>
        /// <returns>A decimal with the _amount truncated to the significant number of decimal digits.</returns>
        public decimal TruncatedAmount
        {
            get
            {
                return (decimal)((long)Math.Truncate(_amount * DecimalDigits)) / DecimalDigits;
            }
        }

        #endregion Public Properties

        #region Money Operators

        public static SimpleMoney operator -(SimpleMoney first, SimpleMoney second)
        {
            return new SimpleMoney(first._amount - second._amount);
        }

        public static bool operator !=(SimpleMoney first, SimpleMoney second)
        {
            return !first.Equals(second);
        }

        public static SimpleMoney operator *(SimpleMoney first, SimpleMoney second)
        {
            return new SimpleMoney(first._amount * second._amount);
        }

        public static SimpleMoney operator /(SimpleMoney first, SimpleMoney second)
        {
            return new SimpleMoney(first._amount / second._amount);
        }

        public static SimpleMoney operator +(SimpleMoney first, SimpleMoney second)
        {
            return new SimpleMoney(first._amount + second._amount);
        }

        public static bool operator <(SimpleMoney first, SimpleMoney second)
        {
            return first.Amount < second.Amount;
        }

        public static bool operator <=(SimpleMoney first, SimpleMoney second)
        {
            return first.Amount <= second.Amount;
        }

        public static bool operator ==(SimpleMoney first, SimpleMoney second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return first.Amount == second.Amount;
        }

        public static bool operator >(SimpleMoney first, SimpleMoney second)
        {
            return first.Amount > second.Amount;
        }

        public static bool operator >=(SimpleMoney first, SimpleMoney second)
        {
            return first.Amount >= second.Amount;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is SimpleMoney))
            {
                throw new ArgumentException("Argument must be SimpleMoney");
            }

            return CompareTo((SimpleMoney)obj);
        }

        public int CompareTo(SimpleMoney other)
        {
            if (this < other)
            {
                return -1;
            }

            if (this > other)
            {
                return 1;
            }

            return 0;
        }

        public override bool Equals(object obj)
        {
            return (obj is SimpleMoney) && Equals((SimpleMoney)obj);
        }

        public bool Equals(SimpleMoney other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return Amount == other.Amount;
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }

        #endregion Money Operators

        #region Cast Operators

        public static implicit operator SimpleMoney(long amount)
        {
            return new SimpleMoney(amount);
        }

        public static implicit operator SimpleMoney(decimal amount)
        {
            return new SimpleMoney(amount);
        }

        public static implicit operator SimpleMoney(double amount)
        {
            return new SimpleMoney(amount);
        }

        public static SimpleMoney operator -(SimpleMoney money, long value)
        {
            return money - (double)value;
        }

        public static SimpleMoney operator -(SimpleMoney money, decimal value)
        {
            return money - (double)value;
        }

        public static SimpleMoney operator -(SimpleMoney money, double value)
        {
            if (money == null)
            {
                throw new ArgumentNullException("money");
            }

            return new SimpleMoney(money._amount - value);
        }

        public static bool operator !=(SimpleMoney money, long value)
        {
            return !(money == value);
        }

        public static bool operator !=(SimpleMoney money, decimal value)
        {
            return !(money == value);
        }

        public static bool operator !=(SimpleMoney money, double value)
        {
            return !(money == value);
        }

        public static SimpleMoney operator *(SimpleMoney money, long value)
        {
            return money * (double)value;
        }

        public static SimpleMoney operator *(SimpleMoney money, decimal value)
        {
            return money * (double)value;
        }

        public static SimpleMoney operator *(SimpleMoney money, double value)
        {
            if (money == null)
            {
                throw new ArgumentNullException("money");
            }

            return new SimpleMoney(money._amount * value);
        }

        public static SimpleMoney operator /(SimpleMoney money, long value)
        {
            return money / (double)value;
        }

        public static SimpleMoney operator /(SimpleMoney money, decimal value)
        {
            return money / (double)value;
        }

        public static SimpleMoney operator /(SimpleMoney money, double value)
        {
            if (money == null)
            {
                throw new ArgumentNullException("money");
            }

            return new SimpleMoney(money._amount / value);
        }

        public static SimpleMoney operator +(SimpleMoney money, long value)
        {
            return money + (double)value;
        }

        public static SimpleMoney operator +(SimpleMoney money, decimal value)
        {
            return money + (double)value;
        }

        public static SimpleMoney operator +(SimpleMoney money, double value)
        {
            if (money == null)
            {
                throw new ArgumentNullException("money");
            }

            return new SimpleMoney(money._amount + value);
        }

        public static bool operator ==(SimpleMoney money, long value)
        {
            if (ReferenceEquals(money, null) || ReferenceEquals(value, null))
            {
                return false;
            }

            return (money.Amount == value);
        }

        public static bool operator ==(SimpleMoney money, decimal value)
        {
            if (ReferenceEquals(money, null) || ReferenceEquals(value, null))
            {
                return false;
            }

            return (money.Amount == value);
        }

        public static bool operator ==(SimpleMoney money, double value)
        {
            if (ReferenceEquals(money, null) || ReferenceEquals(value, null))
            {
                return false;
            }

            return (money.Amount == (decimal)value);
        }

        #endregion Cast Operators

        #region Functions

        /// <summary>
        /// Evenly distributes the _amount over n parts, resolving remainders that occur due to rounding
        /// errors, thereby guaranteeing the post-condition: result->sum(r|r._amount) = this._amount and
        /// x elements in result are greater than at least one of the other elements, where x = _amount mod n.
        /// </summary>
        /// <param name="n">Number of parts over which the _amount is to be distributed.</param>
        /// <returns>Array with distributed Money amounts.</returns>
        public SimpleMoney[] Allocate(int n)
        {
            double cents = Math.Pow(10, DecimalDigits);
            double lowResult = ((long)Math.Truncate(_amount / n * cents)) / cents;
            double highResult = lowResult + 1.0d / cents;

            SimpleMoney[] results = new SimpleMoney[n];

            var remainder = (int)((_amount * cents) % n);
            for (var i = 0; i < remainder; i++)
            {
                results[i] = new SimpleMoney((Decimal)highResult);
            }

            for (var i = remainder; i < n; i++)
            {
                results[i] = new SimpleMoney((Decimal)lowResult);
            }

            return results;
        }

        public SimpleMoney Clone()
        {
            return new SimpleMoney();
        }

        public SimpleMoney Copy()
        {
            return new SimpleMoney(Amount);
        }

        public string ToString(string format = "C")
        {
            return Amount.ToString(format, Currency.Currency.Get(currencyCode).NumberFormat);
        }

        #endregion Functions
    }
}