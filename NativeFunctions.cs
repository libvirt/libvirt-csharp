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
    public class NativeFunctions
    {
        // TODO : this is atemporary workaround for virConnectOpenAuth callback, this should be removed
        [DllImport("msvcrt.dll", EntryPoint = "_strdup", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr StrDup(IntPtr strSource);
    }
}
