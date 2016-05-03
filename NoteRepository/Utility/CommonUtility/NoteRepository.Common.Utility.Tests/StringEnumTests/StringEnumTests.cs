using Hadrian.Common.Utility.StringEnumeration;
using NUnit.Framework;

namespace Hadrian.Common.Utility.Tests.StringEnumTests
{
    public enum TestEnumType
    {
        [StringValue("NoSet")]
        NoSet,

        [StringValue("Order")]
        Order,

        [StringValue("Quote")]
        Quote,
    }

    [TestFixture]
    public class StringEnumTests
    {
        [Test]
        public void Can_Get_Right_Type_With_Right_Name()
        {
            // Arrange
            var str = "Order";

            // Act
            var type = StringEnum.Parse(typeof(TestEnumType), str);

            // Assert
            Assert.That(type, Is.EqualTo(TestEnumType.Order));
        }

        [Test]
        public void Can_Get_Null_With_No_Exists_Enum_String()
        {
            // Arrange
            var str = "no exits string";

            // Act
            var type = StringEnum.Parse(typeof(TestEnumType), str);

            // Assert
            Assert.That(type, Is.Null);
        }

        [Test]
        public void Can_Get_Right_Type_With_Case_Ignore_Flag_Set()
        {
            // Arrange
            var str = "Order";

            // Act
            var type = StringEnum.Parse(typeof(TestEnumType), str);

            // Assert
            Assert.That(type, Is.EqualTo(TestEnumType.Order));

            // Arrange
            str = "order";

            // Act
            type = StringEnum.Parse(typeof(TestEnumType), str);

            // Assert
            Assert.That(type, Is.Null);

            // Act
            type = StringEnum.Parse(typeof(TestEnumType), str, true);

            // Assert
            Assert.That(type, Is.EqualTo(TestEnumType.Order));
        }

        [Test]
        public void Can_Get_Default_Type_With_DefaultForNoMathc_Flag_Set()
        {
            // Arrange
            var str = "order";

            // Act
            var type = StringEnum.Parse(typeof(TestEnumType), str);

            // Assert
            Assert.That(type, Is.Null);

            // Act
            type = StringEnum.Parse(typeof(TestEnumType), str, false, true);

            // Assert
            Assert.That(type, Is.EqualTo(TestEnumType.NoSet));

            // Arrange
            str = "something else";

            // Act
            type = StringEnum.Parse(typeof(TestEnumType), str, true, true);

            // Assert
            Assert.That(type, Is.EqualTo(TestEnumType.NoSet));
        }
    }
}