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
    /// The Network clas expose all libvirt network related functions
    /// </summary>
    public class Network
    {
        /// <summary>
        /// Increment the reference count on the network. For each additional call to this method,
        /// there shall be a corresponding call to virNetworkFree to release the reference count,
        /// once the caller no longer needs the reference to this object.
        /// This method is typically useful for applications where multiple threads are using a connection,
        /// and it is required that the connection remain open until all threads have finished using it. ie,
        /// each new thread using a network would increment the reference count.
        /// </summary>
        /// <param name="network">
        /// A <see cref="IntPtr"/>the network to hold a reference on.
        /// </param>
        /// <returns>
        /// 0 in case of success, -1 in case of failure.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkRef")]
        public static extern int Ref(IntPtr network);

        // TODO virNetworkSetAutostart

        // TODO virNetworkUndefine
        /// <summary>
        /// Create and start a defined network. If the call succeed the network moves from the defined to the running networks pools.
        /// </summary>
        /// <param name="network">pointer to a defined network</param>
        /// <returns>0 in case of success, -1 in case of error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkCreate")]
        public static extern int Create(IntPtr network);
        /// <summary>
        /// Create and start a new virtual network, based on an XML description similar to the one returned by virNetworkGetXMLDesc()
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="xmlDesc">an XML description of the network</param>
        /// <returns>a new network object or NULL in case of failure</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkCreateXML")]
        public static extern IntPtr CreateXML(IntPtr conn, string xmlDesc);
        /// <summary>
        /// Define a network, but does not create it
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="xmlDesc">the XML description for the network, preferably in UTF-8</param>
        /// <returns>NULL in case of error, a pointer to the network otherwise</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkDefineXML")]
        public static extern IntPtr DefineXML(IntPtr conn, string xmlDesc);
        /// <summary>
        /// Destroy the network object. The running instance is shutdown if not down already and all resources used by it are given back to the hypervisor. This does not free the associated virNetworkPtr object. This function may require privileged access
        /// </summary>
        /// <param name="network">a network object</param>
        /// <returns>0 in case of success and -1 in case of failure.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkDestroy")]
        public static extern int Destroy(IntPtr network);
        /// <summary>
        /// Free the network object. The running instance is kept alive. The data structure is freed and should not be used thereafter.
        /// </summary>
        /// <param name="network">a network object</param>
        /// <returns>0 in case of success and -1 in case of failure</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkFree")]
        public static extern int Free(IntPtr network);
        /// <summary>
        /// Provides a boolean value indicating whether the network configured to be automatically started when the host machine boots.
        /// </summary>
        /// <param name="network">a network object</param>
        /// <param name="autostart">the value returned</param>
        /// <returns>-1 in case of error, 0 in case of success</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkGetAutostart")]
        public static extern int GetAutostart(IntPtr network, ref int autostart);
        /// <summary>
        /// Provides a bridge interface name to which a domain may connect a network interface in order to join the network.
        /// </summary>
        /// <param name="network">a network object</param>
        /// <returns>a 0 terminated interface name, or NULL in case of error. the caller must free() the returned value</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkGetBridgeName")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetBridgeName(IntPtr network);
        /// <summary>
        /// Provides the connection pointer associated with a network. The reference counter on the connection is not increased by this call. WARNING: When writing libvirt bindings in other languages, do not use this function. Instead, store the connection and the network object together
        /// </summary>
        /// <param name="network">pointer to a network</param>
        /// <returns>the virConnectPtr or NULL in case of failure</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkGetConnect")]
        public static extern IntPtr GetConnect(IntPtr network);
        /// <summary>
        /// Get the public name for that network
        /// </summary>
        /// <param name="network">a network object</param>
        /// <returns>a pointer to the name or NULL, the string need not be deallocated its lifetime will be the same as the network object</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkGetName")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetName(IntPtr network);
        /// <summary>
        /// Get the UUID for a network as string
        /// </summary>
        /// <param name="network">a network object</param>
        /// <param name="uuid">string of the uuid</param>
        /// <returns>-1 in case of error, 0 in case of success</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkGetUUIDString")]
        private static extern int GetUUIDString(IntPtr network, [Out] byte[] uuid);

        ///<summary>
        /// Get the UUID for a network as string
        ///</summary>
        ///<param name="network">a network object, a netowrk IntPtr</param>
        ///<returns>string of the uuid</returns>
        public static string GetUUIDString(IntPtr network)
        {
            byte[] uuidArray = new byte[36];
            if (GetUUIDString(network, uuidArray) == 0)
                return System.Text.Encoding.UTF8.GetString(uuidArray);
            throw new Exception("Error at native GetUUIDString call");
        }
        /// <summary>
        /// Provide an XML description of the network. The description may be reused later to relaunch the network with virNetworkCreateXML().
        /// </summary>
        /// <param name="network">a network object</param>
        /// <param name="flags">an OR'ed set of extraction flags, not used yet</param>
        /// <returns>a 0 terminated UTF-8 encoded XML instance, or NULL in case of error. the caller must free() the returned value</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkGetXMLDesc")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetXMLDesc(IntPtr network, int flags);
        /// <summary>
        /// Determine if the network is currently running
        /// </summary>
        /// <param name="network">pointer to the network object</param>
        /// <returns>1 if running, 0 if inactive, -1 on error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkIsActive")]
        public static extern int IsActive(IntPtr network);
        /// <summary>
        /// Determine if the network has a persistent configuration which means it will still exist after shutting down
        /// </summary>
        /// <param name="network">pointer to the network object</param>
        /// <returns>x1 if persistent, 0 if transient, -1 on error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkIsPersistent")]
        public static extern int IsPersistent(IntPtr network);
        /// <summary>
        /// Try to lookup a network on the given hypervisor based on its name.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="name">name for the network</param>
        /// <returns>a new network object or NULL in case of failure. If the network cannot be found, then VIR_ERR_NO_NETWORK error is raised</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkLookupByName")]
        public static extern IntPtr LookupByName(IntPtr conn, string name);
        /// <summary>
        /// Try to lookup a network on the given hypervisor based on its UUID
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="uuidstr">the string UUID for the network</param>
        /// <returns>a new network object or NULL in case of failure. If the network cannot be found, then VIR_ERR_NO_NETWORK error is raised</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkLookupByUUIDString")]
        public static extern IntPtr LookupByUUIDString(IntPtr conn, string uuidstr);
        /// <summary>
        /// Configure the network to be automatically started when the host machine boots
        /// </summary>
        /// <param name="network">a network object</param>
        /// <param name="autostart">whether the network should be automatically started 0 or 1</param>
        /// <returns>-1 in case of error, 0 in case of success</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkSetAutostart")]
        public static extern int SetAutostart(IntPtr network, int autostart);
        /// <summary>
        /// Undefine a network but does not stop it if it is running
        /// </summary>
        /// <param name="network">pointer to a defined network</param>
        /// <returns>0 in case of success, -1 in case of error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virNetworkUndefine")]
        public static extern int Undefine(IntPtr network);
    }
}
