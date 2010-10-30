/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 */

using System.Runtime.InteropServices;

namespace Libvirt
{
    /// <summary>
    /// The Library class expose all libvirt library related methods
    /// </summary>
    public class Library
    {
        /// <summary>
        /// Provides two information back, @libVer is the version of the library while @typeVer will be the version of the hypervisor
        /// type @type against which the library was compiled. If @type is NULL, "Xen" is assumed,
        /// if @type is unknown or not available, an error code will be returned and @typeVer will be 0.
        /// </summary>
        /// <param name="libVer">
        /// A <see cref="System.UInt64"/>return value for the library version (OUT).
        /// </param>
        /// <param name="type">
        /// A <see cref="System.String"/>the type of connection/driver looked at.
        /// </param>
        /// <param name="typeVer">
        /// A <see cref="System.UInt64"/>return value for the version of the hypervisor (OUT).
        /// </param>
        /// <returns>
        /// -1 in case of failure, 0 otherwise, and values for @libVer and @typeVer have the format major * 1,000,000 + minor * 1,000 + release.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virGetVersion")]
        public static extern int GetVersion([Out] out ulong libVer, [In] string type, [Out] out ulong typeVer);
        /// <summary>
        /// Initialize the library. It's better to call this routine at startup in multithreaded applications to avoid
        /// potential race when initializing the library.
        /// </summary>
        /// <returns>
        /// 0 in case of success, -1 in case of error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virInitialize")]
        public static extern int Initialize();
    }
}
