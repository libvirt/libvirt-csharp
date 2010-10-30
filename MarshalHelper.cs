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
    /// <summary>
    /// The MarshalHelper class provide some methods that simplify marshaling
    /// </summary>
    public class MarshalHelper
    {
        /// <summary>
        /// Convert an IntPtr to a string array
        /// </summary>
        /// <param name="stringPtr">The pointer to the first element of the array</param>
        /// <param name="stringCount">The number of elements in the array</param>
        /// <returns>The string array</returns>
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
        /// <summary>
        /// Convert an IntPtr to a string
        /// </summary>
        /// <param name="stringPtr">The pointer</param>
        /// <returns>The string</returns>
        public static string ptrToString(IntPtr stringPtr)
        {
            IntPtr s = Marshal.ReadIntPtr(stringPtr, IntPtr.Size);
            return Marshal.PtrToStringAnsi(s);
        }
        /// <summary>
        /// Shift a pointer by offset (32 or 64 bits pointer)
        /// </summary>
        /// <param name="src">The pointer</param>
        /// <param name="offset">The offset in bytes</param>
        /// <returns>the shifted pointer</returns>
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
