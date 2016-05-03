using System;
using System.Globalization;
using Hadrian.Common.Utility.Currency;

namespace Hadrian.Common.Utility.Tests.MoneyTests
{
    public class SampleConverter : ICurrencyConverter
    {
        public double GetRate(CurrencyCodes fromCode, CurrencyCodes toCode, DateTime asOn)
        {
            // Don't use reflection if you want performance!
            return GetRate(Enum.GetName(typeof(CurrencyCodes), fromCode), Enum.GetName(typeof(CurrencyCodes), toCode), asOn);
        }

        public double GetRate(string fromCode, string toCode, DateTime asOn)
        {
            return toCode.Equals(new RegionInfo(CultureInfo.CurrentCulture.LCID).ISOCurrencySymbol) ? 7.9 : 0.125;
        }
    }
}