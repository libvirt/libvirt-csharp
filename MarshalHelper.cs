/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 */

using System;
using System.Runtime.InteropServices;

namespace Libvirt
{
    public class MarshalHelper
    {
        public static string[] ptrToStringArray(IntPtr stringPtr, int stringCount)
        {
            string[] members = new string[stringCount];
            for (int i = 0; i < stringCount; ++i)
            {
                IntPtr s = Marshal.ReadIntPtr(stringPtr, i * IntPtr.Size);
                members[i] = Marshal.PtrToStringAnsi(s);
            }
            return members;
        }

        public static string ptrToString(IntPtr stringPtr)
        {
            IntPtr s = Marshal.ReadIntPtr(stringPtr, IntPtr.Size);
            return Marshal.PtrToStringAnsi(s);
        }

        public static IntPtr IntPtrOffset(IntPtr src, int offset)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return new IntPtr(src.ToInt32() + offset);
                case 8:
                    return new IntPtr(src.ToInt64() + offset);
                default:
                    throw new NotSupportedException("Surprise!  This is running on a machine where pointers are " + IntPtr.Size + " bytes and arithmetic doesn't work in C# on them.");
            }
        }
    }
}
