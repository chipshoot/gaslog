using System;
using Hadrian.Common.Utility.MeasureUnit;
using NUnit.Framework;

namespace Hadrian.Common.Utility.Tests.MeasureUnitTests
{
    [TestFixture]
    public class DimensionTests
    {
        private const double Tolerant = 0.001;

        [Test]
        public void DimensionInitial_Should_Get_ZeroDimension()
        {
            var zeroLength = new Dimension();

            Assert.NotNull(zeroLength);
            Assert.That(zeroLength == 0);
        }

        [Test]
        public void FromMillimetre_Should_Get_Millimetre_Value()
        {
            var dim = Dimension.FromMillimetre(1.0);

            Assert.AreEqual(DimensionUnit.Millimetre, dim.Unit);
            Assert.That(Math.Abs(dim.Value - 1.0) < Tolerant);
            Assert.That(Math.Abs(dim.TotalMillimetre - 1.0) < Tolerant);
        }

        [Test]
        public void FromCentimetre_Should_Get_Centimetre_Value()
        {
            var dim = Dimension.FromCentimetre(1.0);

            Assert.AreEqual(DimensionUnit.Centimetre, dim.Unit);
            Assert.That(Math.Abs(dim.Value - 1.0) < Tolerant);
            Assert.That(Math.Abs(dim.TotalCentimetre - 1.0) < Tolerant);
            Assert.That(Math.Abs(dim.TotalMillimetre - 10.0) < Tolerant);
        }

        [Test]
        public void FromMetre_Should_Get_Metre_Value()
        {
            var dim = Dimension.FromMetre(1.0);

            Assert.AreEqual(DimensionUnit.Metre, dim.Unit);
            Assert.That(Math.Abs(dim.Value - 1.0) < Tolerant);
            Assert.That(Math.Abs(dim.TotalMetre - 1.0) < Tolerant);
            Assert.That(Math.Abs(dim.TotalMillimetre - 100.0) < Tolerant);
        }

        [Test]
        public void FromInch_Should_Get_Inch_Value()
        {
            var dim = Dimension.FromInch(1.0);

            Assert.AreEqual(DimensionUnit.Inch, dim.Unit);
            Assert.That(Math.Abs(dim.Value - 1.0) < Tolerant);
            Assert.That(Math.Abs(dim.TotalInch - 1.0) < Tolerant);
            Assert.That(Math.Abs(dim.TotalMillimetre - 25.4) < Tolerant);
        }

        [Test]
        public void FromFeet_Should_Get_Feet_Value()
        {
            var dim = Dimension.FromFeet(1.0);

            Assert.AreEqual(DimensionUnit.Feet, dim.Unit);
            Assert.That(Math.Abs(dim.Value - 1.0) < Tolerant);
            Assert.That(Math.Abs(dim.TotalFeet - 1.0) < Tolerant);
            Assert.That(Math.Abs(dim.TotalInch - 12.0) < Tolerant);
            Assert.That(Math.Abs(dim.TotalMillimetre - 304.8) < Tolerant);
        }

        [Test]
        public void Convert_Test_From_Millimetre()
        {
            // a default mm dimension
            var dim = new Dimension(1, DimensionUnit.Millimetre, 5);

            Assert.AreEqual(1.0, dim.Value);
            Assert.AreEqual(1.0, dim.TotalMillimetre);
            Assert.AreEqual(0.1, dim.TotalCentimetre);
            Assert.AreEqual(0.01, dim.TotalMetre);
            Assert.That(Math.Abs(dim.TotalInch - 0.0393701) < Tolerant);
            Assert.That(Math.Abs(dim.TotalFeet - 0.00328084) < Tolerant);
        }

        [Test]
        public void Convert_Test_From_Centimetre()
        {
            var dim = new Dimension(1, DimensionUnit.Centimetre, 5);
            Assert.AreEqual(1.0, dim.Value);
            Assert.AreEqual(10.0, dim.TotalMillimetre);
            Assert.AreEqual(1.0, dim.TotalCentimetre);
            Assert.AreEqual(0.1, dim.TotalMetre);
            Assert.That(Math.Abs(dim.TotalInch - 0.393701) < Tolerant);
            Assert.That(Math.Abs(dim.TotalFeet - 0.0328084) < Tolerant);
        }

        [Test]
        public void Convert_Test_From_Metre()
        {
            var dim = new Dimension(1, DimensionUnit.Metre, 5);
            Assert.AreEqual(1.0, dim.Value);
            Assert.AreEqual(100.0, dim.TotalMillimetre);
            Assert.AreEqual(10.0, dim.TotalCentimetre);
            Assert.AreEqual(1.0, dim.TotalMetre);
            Assert.That(Math.Abs(dim.TotalInch - 3.93701) < Tolerant);
            Assert.That(Math.Abs(dim.TotalFeet - 0.328084) < Tolerant);
        }

        [Test]
        public void Convert_Test_From_Inch()
        {
            var dim = new Dimension(1, DimensionUnit.Inch, 5);
            Assert.AreEqual(1.0, dim.Value);
            Assert.AreEqual(25.4, dim.TotalMillimetre);
            Assert.AreEqual(2.54, dim.TotalCentimetre);
            Assert.AreEqual(0.254, dim.TotalMetre);
            Assert.That(Math.Abs(dim.TotalInch - 1.0) < Tolerant);
            Assert.That(Math.Abs(dim.TotalFeet - 0.0833333) < Tolerant);

            dim = new Dimension(0.125, DimensionUnit.Inch, 5);
            Assert.AreEqual(0.125, dim.Value);
            Assert.AreEqual(3.175, dim.TotalMillimetre);
            Assert.AreEqual(0.3175, dim.TotalCentimetre);
            Assert.AreEqual(0.03175, dim.TotalMetre);
            Assert.That(Math.Abs(dim.TotalInch - 0.125) < Tolerant);
            Assert.That(Math.Abs(dim.TotalFeet - 0.010417) < Tolerant);
        }

        [Test]
        public void Min_Should_Always_Get_Minimum_Dimension()
        {
            var dim1 = new Dimension(10);
            var dim2 = new Dimension(10.3);
            var dim3 = new Dimension(20.5);
            var dim4 = new Dimension(30);
            var dim5 = new Dimension(-2);

            var min = Dimension.Min(dim3, dim2, dim1, dim4);
            Assert.AreEqual(dim1.Value, min.Value);

            min = Dimension.Min(dim3, dim2, dim1, dim4, dim1);
            Assert.AreEqual(dim1.Value, min.Value);

            min = Dimension.Min(dim3, dim2, dim1, dim4, dim4);
            Assert.AreEqual(dim1.Value, min.Value);

            min = Dimension.Min(dim3, dim2, dim1, dim4, dim4, dim5);
            Assert.AreEqual(dim5.Value, min.Value);
        }

        [Test]
        public void Max_Should_Always_Get_Maximum_Dimension()
        {
            var dim1 = new Dimension(10);
            var dim2 = new Dimension(10.3);
            var dim3 = new Dimension(20.5);
            var dim4 = new Dimension(30);
            var dim5 = new Dimension(-2);

            var min = Dimension.Max(dim3, dim2, dim1, dim4);
            Assert.AreEqual(dim4.Value, min.Value);

            min = Dimension.Max(dim3, dim2, dim1, dim4, dim1);
            Assert.AreEqual(dim4.Value, min.Value);

            min = Dimension.Max(dim3, dim2, dim1, dim4, dim4);
            Assert.AreEqual(dim4.Value, min.Value);

            min = Dimension.Max(dim3, dim2, dim1, dim4, dim4, dim5);
            Assert.AreEqual(dim4.Value, min.Value);
        }

        [Test]
        public void Abs_Sould_Always_Get_Positive_Dimension()
        {
            var dim = new Dimension(-1);

            Assert.True(dim < 0);
            Assert.True(Dimension.Abs(dim) > 0);
        }

        [Test]
        public void ToString_Test()
        {
            var dim = Dimension.FromInch(0.125);

            Assert.AreEqual("0.125 in", dim.ToString(DimensionUnitFormatType.Inch));

            dim = Dimension.FromInch(0.75);
            Assert.AreEqual("0.75 in", dim.ToString(DimensionUnitFormatType.Inch));
            Assert.AreEqual("0.062 ft", dim.ToString(DimensionUnitFormatType.Feet));
            Assert.AreEqual("0.19 m", dim.ToString(DimensionUnitFormatType.Metre));
            Assert.AreEqual("19.05 mm", dim.ToString());
            Assert.AreEqual("1.905 cm", dim.ToString(DimensionUnitFormatType.Centimetre));
            Assert.AreEqual("19.05 mm / 0.75 in", dim.ToString(DimensionUnitFormatType.MillimetreAndInch));
        }

        #region operator tests

        [Test]
        public void Plus_Test()
        {
            var dim1 = new Dimension(1);
            var dim2 = new Dimension(2);

            var result = dim1 + dim2;
            Assert.AreEqual(3, result.Value);

            dim1 = new Dimension(-3);
            dim2 = new Dimension(2);
            result = dim1 + dim2;
            Assert.True(result < 0);
            Assert.AreEqual(-1, result.Value);

            dim1 = Dimension.FromInch(1);
            dim2 = Dimension.FromMillimetre(1);
            result = dim1 + dim2;
            Assert.AreEqual(DimensionUnit.Inch, result.Unit);
            Assert.AreEqual(1.039, result.Value);
        }

        #endregion operator tests

        #region Hadrian dimension align tests

        [Test]
        public void Adjusted_Dimension_Can_Always_Divided_By_Step_Without_Remainder()
        {
            var dim = Dimension.HadrianDimensionAlign(Dimension.FromInch(-1));

            Assert.AreEqual(-1, dim.Value);
            Assert.That(dim==-1);

            dim = Dimension.HadrianDimensionAlign(Dimension.FromInch(0.125));
            Assert.AreEqual(0.125, dim.Value);

            dim = Dimension.HadrianDimensionAlign(Dimension.FromInch(0.127));
            Assert.AreEqual(0.125, dim.Value);

            dim = Dimension.HadrianDimensionAlign(Dimension.FromInch(0.13));
            Assert.AreEqual(0.125, dim.Value);

            dim = Dimension.HadrianDimensionAlign(Dimension.FromInch(1287));
            Assert.AreEqual(0, dim.Value%0.125);

            dim = Dimension.HadrianDimensionAlign(Dimension.FromInch(1287.53));
            Assert.AreEqual(0, dim.Value % 0.125);

            dim = Dimension.HadrianDimensionAlign(Dimension.FromInch(1287.5467));
            Assert.AreEqual(0, dim.Value % 0.125);

            dim = Dimension.HadrianDimensionAlign(Dimension.FromInch(1287.7567));
            Assert.AreEqual(0, dim.Value % 0.125);

            dim = Dimension.HadrianDimensionAlign(Dimension.FromInch(1287.8467));
            Assert.AreEqual(0, dim.Value % 0.125);
        }
        #endregion Hadrian dimension align tests
    }
}