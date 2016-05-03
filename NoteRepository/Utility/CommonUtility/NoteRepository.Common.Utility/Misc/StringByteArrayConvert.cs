using System;

namespace NoteRepository.Common.Utility.Misc
{
    /// <summary>
    /// Tool for convert between string to byte array
    /// </summary>
    public static class StringByteArrayConvert
    {
        /// <summary>
        /// Convert string to byte array.
        /// </summary>
        /// <param name="str">The string need to be convert.</param>
        /// <returns>The byte array convert</returns>
        public static byte[] StringToByte(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Convert byte array to string.
        /// </summary>
        /// <param name="bytes">The byte array to be convert.</param>
        /// <returns>The string get from byte array</returns>
        public static string ByteToString(byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}