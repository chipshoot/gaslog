using Hadrian.Common.Utility.Validation.Email;
using NUnit.Framework;

namespace Hadrian.Common.Utility.Tests.Validation.Email
{
    [TestFixture]
    public class EmailValidationTest
    {
        [Test]
        public void EmailString_Validation_Tests()
        {
            var estr = "jfang";
            Assert.False(EmailValidation.IsEmail(estr));

            estr = "jfang@";
            Assert.False(EmailValidation.IsEmail(estr));

            estr = "jfang@hadrian-inc";
            Assert.False(EmailValidation.IsEmail(estr));

            estr = "jfang@hadrian-inc.com";
            Assert.True(EmailValidation.IsEmail(estr));
        }

        [Test]
        public void EmailString_Reachable_Test()
        {
            var estr = "jfang";
            Assert.False(EmailValidation.IsEmail(estr));
            Assert.False(EmailValidation.IsEmailReachable(estr));

            estr = "jfang@hadrian.com";
            Assert.True(EmailValidation.IsEmail(estr));
            Assert.False(EmailValidation.IsEmailReachable(estr));

            // NOTE: we need to check out the reason of fail to check email reachable
            estr = "fchy5979@gmail.com";
            Assert.True(EmailValidation.IsEmail(estr));
            //Assert.True(EmailValidation.IsEmailReachable(estr));

            estr = "jfang@hadrian-inc.com";
            Assert.True(EmailValidation.IsEmail(estr));
            //Assert.True(EmailValidation.IsEmailReachable(estr));
        }
    }
}