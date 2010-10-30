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
    /// The class expose some useful native functions
    /// </summary>
    public class NativeFunctions
    {
        // TODO : this is a temporary workaround for virConnectOpenAuth callback, this should be removed
        /// <summary>
        /// duplicate a string. The strdup function shall return a pointer to a new string, which is a duplicate of the string pointed to by s1.
        /// </summary>
        /// <param name="strSource">Pointer to the string that should be duplicated</param>
        /// <returns>a pointer to a new string on success. Otherwise, it shall return a null pointer</returns>
        [DllImport("msvcrt.dll", EntryPoint = "_strdup", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr StrDup(IntPtr strSource);

        // TODO : this is a temporary workaround for virConnectOpenAuth callback, this should be removed
        [DllImport("msvcrt.dll", EntryPoint = "free", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Free(IntPtr ptr);
    }
}
