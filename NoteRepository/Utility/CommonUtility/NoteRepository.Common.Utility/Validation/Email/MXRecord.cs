using System;
using System.Runtime.InteropServices;

namespace NoteRepository.Common.Utility.Validation.Email
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MXRecord
    {
        public IntPtr pNext;

        public string pName;

        public short wType;

        public short wDataLength;

        public int flags;

        public int dwTtl;

        public int dwReserved;

        public IntPtr pNameExchange;

        public short wPreference;

        public short Pad;
    }
}