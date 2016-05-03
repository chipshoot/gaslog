using NoteRepository.Common.Utility.MeasureUnit;
using NUnit.Framework;

namespace NoteRepository.Common.Utility.Tests.MeasureUnitTests
{
    [TestFixture]
    public class WeightTests
    {
        [Test]
        public void Can_Get_Rigth_Pound_FromGrams_Value()
        {
            // Act
            var weight = Weight.FromGrams(1);

            // Assert
            Assert.AreEqual(weight.TotalGrams, 1);
            Assert.AreEqual(weight.TotalKilograms, 0.001);
            Assert.AreEqual(weight.TotalPounds, 0.002);
        }

        [Test]
        public void Can_Get_Rigth_Pound_FromKilograms_Value()
        {
            // Act
            var weight = Weight.FromKilograms(1);

            // Assert
            Assert.AreEqual(weight.TotalGrams, 1000);
            Assert.AreEqual(weight.TotalKilograms, 1);
            Assert.AreEqual(weight.TotalPounds, 2.205);
        }

        [Test]
        public void Can_Get_Rigth_Pound_FromPounds_Value()
        {
            // Act
            var weight = Weight.FromPounds(1);

            // Assert
            Assert.AreEqual(weight.TotalGrams, 454);
            Assert.AreEqual(weight.TotalKilograms, 0.454);
            Assert.AreEqual(weight.TotalPounds, 1.001);
        }

        [Test]
        public void Can_Get_Right_ToString_Result()
        {
            // Act
            var weight = Weight.FromPounds(1);

            // Assert
            Assert.AreEqual(weight.ToString(), "454");
            Assert.AreEqual(weight.ToString("g"), "454 g");
            Assert.AreEqual(weight.ToString("kg"), "0.454 kg");
            Assert.AreEqual(weight.ToString("lb"), "1.001 lb");
            Assert.AreEqual(weight.ToString("all"), "1.001 lbs / 0.454 kg");
            Assert.AreEqual(weight.ToString("something else"), "454");
        }

        [Test]
        public void Fractional_Test()
        {
            // default fraction is 3
            var weight = Weight.FromKilograms(1);
            Assert.AreEqual(weight.TotalPounds, 2.205);

            // Change fractional digital
            weight.Fractional = 2;
            Assert.AreEqual(weight.TotalPounds, 2.2);

            // fractional has to be larger then 0
            weight.Fractional = -1;
            Assert.AreEqual(weight.TotalPounds, 2.2);

            weight.Fractional = 0;
            Assert.AreEqual(weight.TotalPounds, 2.2);
        }
    }
}