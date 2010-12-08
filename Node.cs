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
    /// The Node class expose all libvirt node related functions
    /// </summary>
    public class Node
    {
		private const int MaxStringLength = 1024;

        // TODO virNodeDeviceCreateXML

        // TODO virNodeDeviceDestroy

        // TODO virNodeDeviceDettach

        // TODO virNodeDeviceFree

        // TODO virNodeDeviceGetName

        // TODO virNodeDeviceGetParent

        /// <summary>
        /// Fetch an XML document describing all aspects of the device.
        /// </summary>
        /// <param name="dev">pointer to the node device</param>
        /// <param name="flags">flags for XML generation (unused, pass 0)</param>
        /// <returns>the XML document, or NULL on error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNodeDeviceGetXMLDesc")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string DeviceGetXMLDesc(IntPtr dev, uint flags);

        // TODO virNodeDeviceListCaps

        /// <summary>
        /// Lookup a node device by its name.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="name">unique device name</param>
        /// <returns>a virNodeDevicePtr if found, NULL otherwise.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNodeDeviceLookupByName")]
        public static extern IntPtr DeviceLookupByName(IntPtr conn, string name);
        // TODO virNodeDeviceNumOfCaps

        // TODO virNodeDeviceReAttach

        // TODO virNodeDeviceRef

        // TODO virNodeDeviceReset

        // TODO virNodeGetCellsFreeMemory

        /// <summary>
        /// Provides the free memory available on the Node Note: most libvirt APIs provide memory sizes in kilobytes,
        /// but in this function the returned value is in bytes. Divide by 1024 as necessary.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to the hypervisor connection.
        /// </param>
        /// <returns>
        /// The available free memory in bytes or 0 in case of error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNodeGetFreeMemory")]
        public static extern ulong GetFreeMemory(IntPtr conn);
        /// <summary>
        /// Extract hardware information about the node.
        /// </summary>
        /// <param name="h">
        /// A <see cref="IntPtr"/>pointer to the hypervisor connection.
        /// </param>
        /// <param name="info">
        /// Pointer to a virNodeInfo structure allocated by the user.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNodeGetInfo")]
        public static extern int GetInfo(IntPtr h, [Out] NodeInfo info);

        // TODO virNodeGetSecurityModel

        /// <summary>
        /// Collect the list of node devices, and store their names in @names.
        /// If the optional 'cap' argument is non-NULL, then the count will be restricted to devices with the specified capability.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to the hypervisor connection.
        /// </param>
        /// <param name="cap">
        /// Capability name.
        /// </param>
        /// <param name="names">
        /// Array to collect the list of node device names.
        /// </param>
        /// <param name="maxnames">
        /// Size of @names.
        /// </param>
        /// <param name="flags">
        /// Flags (unused, pass 0).
        /// </param>
        /// <returns>
        /// The number of node devices found or -1 in case of error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNodeListDevices")]
        private static extern int ListDevices(IntPtr conn, string cap, IntPtr names, int maxnames, uint flags);
        /// <summary>
        /// Collect the list of node devices, and store their names in @names.
        /// If the optional 'cap' argument is non-NULL, then the count will be restricted to devices with the specified capability.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to the hypervisor connection.
        /// </param>
        /// <param name="cap">
        /// Capability name.
        /// </param>
        /// <param name="names">
        /// Array to collect the list of node device names.
        /// </param>
        /// <param name="maxnames">
        /// Size of @names.
        /// </param>
        /// <param name="flags">
        /// Flags (unused, pass 0).
        /// </param>
        /// <returns>
        /// The number of node devices found or -1 in case of error.
        /// </returns>
        public static int ListDevices(IntPtr conn, string cap, ref string[] names, int maxnames, uint flags)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = ListDevices(conn, cap, namesPtr, maxnames, 0);
            if (count > 0)
                names = MarshalHelper.ptrToStringArray(namesPtr, count);
            Marshal.FreeHGlobal(namesPtr);
            return count;
        }
        /// <summary>
        /// Provides the number of node devices. If the optional 'cap' argument is non-NULL,
        /// then the count will be restricted to devices with the specified capability.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to the hypervisor connection.
        /// </param>
        /// <param name="cap">
        /// A <see cref="System.String"/>capability name.
        /// </param>
        /// <param name="flags">
        /// A <see cref="System.UInt32"/>flags (unused, pass 0).
        /// </param>
        /// <returns>
        /// The number of node devices or -1 in case of error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNodeNumOfDevices")]
        public static extern int NumOfDevices(IntPtr conn, string cap, uint flags);
    }
}
