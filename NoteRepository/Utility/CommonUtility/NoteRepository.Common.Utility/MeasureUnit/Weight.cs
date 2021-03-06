﻿using System;
using System.ComponentModel;
using System.Globalization;

namespace NoteRepository.Common.Utility.MeasureUnit
{
    /// <summary>
    /// Help class to help convert between lb and kg
    /// <remarks>
    /// The value of weight internally saved as grams and can be convert to kg and lb
    ///
    /// The only way to get <see cref="Weight"/> object if from four static method, e.g.
    ///  <code>
    ///     var weight1 = Weight.FromGrams(35.0);
    ///     var weight2 = Weight.FromKilograms(0.035);
    ///     var weight3 = Weight.FromPonds(34.0);
    ///  </code>
    /// </remarks>
    /// </summary>
    [ImmutableObject(true)]
    public struct Weight : IEquatable<Weight>, IComparable<Weight>
    {
        #region private fields

        private const double GramsPerPound = 453.59237038037829803270366517422;

        private readonly long _valueInG;

        private int _fractional;

        private WeightUnit _unit;

        #endregion private fields

        #region constructor

        private Weight(long valueInG, WeightUnit unit = WeightUnit.Kilogram, int fractional = 3)
        {
            _valueInG = valueInG;
            _unit = unit;
            _fractional = fractional;
        }

        #endregion constructor

        #region public properties

        public double TotalGrams
        {
            get { return Math.Round((double)_valueInG, Fractional); }
        }

        public double TotalKilograms
        {
            get { return Math.Round(_valueInG / 1000.0, Fractional); }
        }

        public double TotalPounds
        {
            get { return Math.Round(_valueInG / GramsPerPound, Fractional); }
        }

        public double Value
        {
            get
            {
                switch (_unit)
                {
                    case WeightUnit.Gram:
                        return TotalGrams;

                    case WeightUnit.Kilogram:
                        return TotalKilograms;

                    case WeightUnit.Pound:
                        return TotalPounds;

                    default:
                        return TotalKilograms;
                }
            }
        }

        public WeightUnit Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        public int Fractional
        {
            get { return _fractional; }

            set
            {
                if (value > 0)
                {
                    _fractional = value;
                }
            }
        }

        #endregion public properties

        #region public methods

        public static Weight FromGrams(double value)
        {
            var g = (long)value;

            return new Weight(g, WeightUnit.Gram);
        }

        public static Weight FromKilograms(int value)
        {
            var g = (long)Math.Round(value * 1000.0, 0);
            return new Weight(g);
        }

        public static Weight FromKilograms(long value)
        {
            var g = (long)Math.Round(value * 1000.0, 0);
            return new Weight(g);
        }

        public static Weight FromKilograms(double value)
        {
            var g = (long)Math.Round(value * 1000.0, 0);
            return new Weight(g);
        }

        public static Weight FromPounds(double value)
        {
            var grams = value * GramsPerPound;

            var g = (long)Math.Round(grams, 0);

            return new Weight(g, WeightUnit.Pound);
        }

        #endregion public methods

        #region override operators

        public static Weight operator -(Weight x, Weight y)
        {
            var newValue = x._valueInG - y._valueInG;
            return new Weight(newValue);
        }

        public static bool operator !=(Weight x, Weight y)
        {
            return !x.Equals(y);
        }

        public static Weight operator *(Weight x, int y)
        {
            var newValue = x._valueInG * y;
            return new Weight(newValue);
        }

        public static Weight operator *(Weight x, double y)
        {
            var newValue = x._valueInG * y;
            var newValueAsLong = (long)Math.Round(newValue, 0);

            return new Weight(newValueAsLong);
        }

        public static Weight operator /(Weight x, int y)
        {
            var newValue = (double)x._valueInG / y;
            var newValueAsLong = (long)Math.Round(newValue, 0);

            return new Weight(newValueAsLong);
        }

        public static Weight operator /(Weight x, double y)
        {
            var newValue = x._valueInG / y;
            var newValueAsLong = (long)Math.Round(newValue, 0);

            return new Weight(newValueAsLong);
        }

        public static Weight operator +(Weight x, Weight y)
        {
            var newValue = x._valueInG + y._valueInG;
            return new Weight(newValue);
        }

        public static bool operator ==(Weight x, Weight y)
        {
            return x.Equals(y);
        }

        public static bool operator >(Weight x, Weight y)
        {
            return x._valueInG > y._valueInG;
        }

        public static bool operator >(Weight x, int y)
        {
            return x > new Weight(y, x.Unit);
        }

        public static bool operator <(Weight x, Weight y)
        {
            return x._valueInG < y._valueInG;
        }

        public static bool operator <(Weight x, int y)
        {
            return x < new Weight(y, x.Unit);
        }

        public string ToString(string format = null)
        {
            if (string.IsNullOrEmpty(format))
            {
                return _valueInG.ToString(CultureInfo.InvariantCulture);
            }

            var fmt = format.Trim().ToLower(CultureInfo.InvariantCulture);
            string result;
            switch (fmt)
            {
                case "lb":
                    result = string.Format(CultureInfo.InvariantCulture, "{0} lb", TotalPounds);
                    break;

                case "g":
                    result = string.Format(CultureInfo.InvariantCulture, "{0} g", TotalGrams);
                    break;

                case "kg":
                    result = string.Format(CultureInfo.InvariantCulture, "{0} kg", TotalKilograms);
                    break;

                case "all":
                    result = string.Format(CultureInfo.InvariantCulture, "{0} lbs / {1} kg", TotalPounds, TotalKilograms);
                    break;

                default:
                    result = _valueInG.ToString(CultureInfo.InvariantCulture);
                    break;
            }

            return result;
        }

        #endregion override operators

        #region implementation of interface IComparable

        public int CompareTo(Weight other)
        {
            return _valueInG.CompareTo(other._valueInG);
        }

        #endregion implementation of interface IComparable

        #region implementation of interface IEquatable

        public bool Equals(Weight other)
        {
            return _valueInG == other._valueInG;
        }

        #endregion implementation of interface IEquatable

        #region override public methods of System.ValueType

        public override bool Equals(object obj)
        {
            return obj is Weight && Equals((Weight)obj);
        }

        public override int GetHashCode()
        {
            return _valueInG.GetHashCode();
        }

        #endregion override public methods of System.ValueType
    }
}