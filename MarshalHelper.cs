/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace LibvirtBindings
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
    }
}
