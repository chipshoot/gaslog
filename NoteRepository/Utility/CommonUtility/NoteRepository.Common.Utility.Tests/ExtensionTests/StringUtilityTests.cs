using NUnit.Framework;

namespace Hadrian.Common.Utility.Tests.ExtensionTests
{
    [TestFixture]
    public class StringUtilityTests
    {
        //[Test]
        //public void StringToByte_WithValidString_ByteArrayGot()
        //{
        //    string testStr = "jack fang";

        //    var bytes = testStr.ToByte();
        //    Assert.AreEqual(bytes.Length, 10);
        //    Assert.AreEqual(bytes[1], 'j');
        //    Assert.AreEqual(bytes[2], 'a');
        //    Assert.AreEqual(bytes[3], 'c');
        //    Assert.AreEqual(bytes[4], 'k');
        //    Assert.AreEqual(bytes[5], ' ');
        //    Assert.AreEqual(bytes[6], 'f');
        //    Assert.AreEqual(bytes[7], 'a');
        //    Assert.AreEqual(bytes[8], 'n');
        //    Assert.AreEqual(bytes[9], 'g');

        //    testStr = "Hadrian";

        //    bytes = testStr.ToByte();
        //    Assert.AreEqual(bytes.Length, 8);
        //}

        //[Test]
        //public void StringToByte_WithEmptyString_NullGot()
        //{
        //    const string testStr = "";

        //    var bytes = testStr.ToByte();
        //    Assert.Null(bytes);
        //}

        //[Test]
        //public void BytesToString_WithValidByteArray_StringGot()
        //{
        //    var bytes = new byte[] { 9, 106, 97, 99, 107, 32, 102, 97, 110, 103 };

        //    var str = bytes.ByteToString();
        //    Assert.AreEqual(str, "jack fang");
        //}

        //[Test]
        //public void BytesToString_WithInvalidByteArray_NullGot()
        //{
        //    // empty byte array
        //    var bytes = new byte[] { };
        //    var str = bytes.ByteToString();
        //    Assert.IsEmpty(str);

        //    // invalid byte array with only one header element
        //    bytes = new byte[] { 8 };
        //    str = bytes.ByteToString();
        //    Assert.IsEmpty(str);

        //    // invalid byte array with incorrect header element
        //    bytes = new byte[] { 8, 106, 97, 99, 107, 32, 102, 97, 110, 103 };
        //    str = bytes.ByteToString();
        //    Assert.Null(str);
        //}
    }
}