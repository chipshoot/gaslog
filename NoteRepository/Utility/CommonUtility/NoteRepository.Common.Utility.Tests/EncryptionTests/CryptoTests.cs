using NUnit.Framework;

namespace Hadrian.Common.Utility.Tests.EncryptionTests
{
    [TestFixture]
    public class CryptoTests
    {
        //private const string Key = "Xf8$g24";
        //private const string Iv = "I3&d$O2";
        //private ICrypto _crypto;

        //[SetUp]
        //public void EnvironmentSetUp()
        //{
        //    _crypto = new Crypto(Key, Iv);
        //}

        //[Test]
        //public void Crypto_Encrypt()
        //{
        //    const string pwd = "123456";
        //    var encryptedPwd = _crypto.Encrypt(pwd.ToByte());
        //    var encryptedPwdString = Convert.ToBase64String(encryptedPwd, 0, encryptedPwd.Length);
        //    Assert.AreEqual(encryptedPwdString, "LIxMEogdmQ4=");
        //}

        //[Test]
        //public void Crypto_Decrypt()
        //{
        //    const string orgPwd = "LIxMEogdmQ4=";
        //    var chars = orgPwd.ToCharArray();
        //    var orgBytes = Convert.FromBase64CharArray(chars, 0, chars.Length);
        //    var clearPwd = _crypto.Decrypt(orgBytes);
        //    var clearPwdString = clearPwd.ByteToString();
        //    Assert.AreEqual(clearPwdString, "123456");
        //}
    }
}