/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 */

using System;
using System.Runtime.InteropServices;

namespace LibvirtBindings
{
    /// <summary>
    /// Libvirt Binding class
    /// </summary>
    public class libVirt
    {
        private const int MaxStringLength = 1024;
#if WINDOWS
        private const string LibvirtDllImportName = "libvirt-0.dll";
        private const string StrDupDllImportName = "msvcrt.dll";
        private const string StrDupEntryPoint = "_strdup";
#else
        private const string LibvirtDllImportName = "libvirt.so.0";
        private const string StrDupDllImportName = "libc.so.6";
        private const string StrDupEntryPoint = "strdup";
#endif
        [DllImport(StrDupDllImportName, EntryPoint = StrDupEntryPoint, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr StrDup(IntPtr strSource);


        #region Connect
        /// <summary>
        /// This function closes the connection to the Hypervisor. This should not be called if further interaction with the Hypervisor are needed especially if there is running domain which need further monitoring by the application.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>0 in case of success or -1 in case of error.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectClose(IntPtr conn);
        /// <summary>
        /// Provides capabilities of the hypervisor / driver.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>NULL in case of error, or an XML string defining the capabilities. The client must free the returned string after use.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virConnectGetCapabilities(IntPtr conn);
        /// <summary>
        /// This returns the system hostname on which the hypervisor is running (the result of the gethostname system call). If we are connected to a remote system, then this returns the hostname of the remote system.
        /// </summary>
        /// <param name="conn">pointer to a hypervisor connection</param>
        /// <returns>the hostname which must be freed by the caller, or NULL if there was an error.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virConnectGetHostname(IntPtr conn);
        /// <summary>
        /// Provides @libVer, which is the version of libvirt used by the daemon running on the @conn host
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="libVer">returns the libvirt library version used on the connection (OUT)</param>
        /// <returns>-1 in case of failure, 0 otherwise, and values for @libVer have the format major * 1,000,000 + minor * 1,000 + release.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectGetLibVersion(IntPtr conn, ref ulong libVer);
        /// <summary>
        /// Provides the maximum number of virtual CPUs supported for a guest VM of a specific type. The 'type' parameter here corresponds to the 'type' attribute in the domain element of the XML.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="type">value of the 'type' attribute in the domain element</param>
        /// <returns>the maximum of virtual CPU or -1 in case of error.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectGetMaxVcpus(IntPtr conn, string type);
        /// <summary>
        /// Get the name of the Hypervisor software used.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>NULL in case of error, a static zero terminated string otherwise. See also: http://www.redhat.com/archives/libvir-list/2007-February/msg00096.html</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virConnectGetType(IntPtr conn);
        /// <summary>
        /// This returns the Uri (name) of the hypervisor connection. Normally this is the same as or similar to the string passed to the virConnectOpen/virConnectOpenReadOnly call, but the driver may make the Uri canonical. If name == NULL was passed to virConnectOpen, then the driver will return a non-NULL Uri which can be used to connect to the same hypervisor later.
        /// </summary>
        /// <param name="conn">pointer to a hypervisor connection</param>
        /// <returns>the Uri string which must be freed by the caller, or NULL if there was an error.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virConnectGetURI(IntPtr conn);
        /// <summary>
        /// Get the version level of the Hypervisor running. This may work only with hypervisor call, i.e. with privileged access to the hypervisor, not with a Read-Only connection.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="hvVer">return value for the version of the running hypervisor (OUT)</param>
        /// <returns>-1 in case of error, 0 otherwise. if the version can't be extracted by lack of capacities returns 0 and @hvVer is 0, otherwise @hvVer value is major * 1,000,000 + minor * 1,000 + release</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectGetVersion(IntPtr conn, ref ulong hvVer);
        /// <summary>
        /// Determine if the connection to the hypervisor is encrypted
        /// </summary>
        /// <param name="conn">pointer to the connection object</param>
        /// <returns>1 if encrypted, 0 if not encrypted, -1 on error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectIsEncrypted(IntPtr conn);
        /// <summary>
        /// Determine if the connection to the hypervisor is secure A connection will be classed as secure if it is either encrypted, or running over a channel which is not exposed to eavesdropping (eg a UNIX domain socket, or pipe)
        /// </summary>
        /// <param name="conn">pointer to the connection object</param>
        /// <returns>1 if secure, 0 if secure, -1 on error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectIsSecure(IntPtr conn);
        /// <summary>
        /// list the defined but inactive domains, stores the pointers to the names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">pointer to an array to store the names</param>
        /// <param name="maxnames">size of the array</param>
        /// <returns>the number of names provided in the array or -1 in case of error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        private static extern int virConnectListDefinedDomains(IntPtr conn, IntPtr names, int maxnames);
        /// <summary>
        /// list the defined but inactive domains, stores the pointers to the names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">pointer to an array to store the names</param>
        /// <param name="maxnames">size of the array</param>
        /// <returns>the number of names provided in the array or -1 in case of error</returns>
        public static int virConnectListDefinedDomains(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = virConnectListDefinedDomains(conn, namesPtr, maxnames);
            if (count > 0)
                names = ptrToStringArray(namesPtr, count);
            Marshal.FreeHGlobal(namesPtr);
            return count;
        }
        /// <summary>
        /// Collect the list of defined (inactive) physical host interfaces, and store their names in @names.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">array to collect the list of names of interfaces</param>
        /// <param name="maxnames">size of @names</param>
        /// <returns>the number of interfaces found or -1 in case of error </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        private static extern int virConnectListDefinedInterfaces(IntPtr conn, IntPtr names, int maxnames);
        ///<summary>
        /// Collect the list of defined (inactive) physical host interfaces, and store their names in @names.
        ///</summary>
        ///<param name="conn">pointer to the hypervisor connection</param>
        ///<param name="names">array to collect the list of names of interfaces</param>
        ///<param name="maxnames">size of @names</param>
        ///<returns>the number of interfaces found or -1 in case of error </returns>
        public static int virConnectListDefinedInterfaces(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = virConnectListDefinedInterfaces(conn, namesPtr, maxnames);
            if (count > 0)
                names = ptrToStringArray(namesPtr, count);
            Marshal.FreeHGlobal(namesPtr);
            return count;
        }
        /// <summary>
        /// list the inactive networks, stores the pointers to the names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">pointer to an array to store the names</param>
        /// <param name="maxnames">size of the array</param>
        /// <returns>the number of names provided in the array or -1 in case of error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        private static extern int virConnectListDefinedNetworks(IntPtr conn, IntPtr names, int maxnames);
        /// <summary>
        /// list the inactive networks, stores the pointers to the names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">pointer to an array to store the names</param>
        /// <param name="maxnames">size of the array</param>
        /// <returns>the number of names provided in the array or -1 in case of error</returns>
        public static int virConnectListDefinedNetworks(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = virConnectListDefinedNetworks(conn, namesPtr, maxnames);
            if (count > 0)
                names = ptrToStringArray(namesPtr, count);
            Marshal.FreeHGlobal(namesPtr);
            return count;
        }
        /// <summary>
        /// Provides the list of names of inactive storage pools upto maxnames. If there are more than maxnames, the remaining names will be silently ignored.
        /// </summary>
        /// <param name="conn">pointer to hypervisor connection</param>
        /// <param name="names">array of char * to fill with pool names (allocated by caller)</param>
        /// <param name="maxnames">size of the names array</param>
        /// <returns>0 on success, -1 on error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        private static extern int virConnectListDefinedStoragePools(IntPtr conn, IntPtr names, int maxnames);
        /// <summary>
        /// Provides the list of names of inactive storage pools upto maxnames. If there are more than maxnames, the remaining names will be silently ignored.
        /// </summary>
        /// <param name="conn">pointer to hypervisor connection</param>
        /// <param name="names">array of char * to fill with pool names (allocated by caller)</param>
        /// <param name="maxnames">size of the names array</param>
        /// <returns>0 on success, -1 on error</returns>
        public static int virConnectListDefinedStoragePools(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = virConnectListDefinedStoragePools(conn, namesPtr, maxnames);
            if (count > 0)
                names = ptrToStringArray(namesPtr, count);
            Marshal.FreeHGlobal(namesPtr);
            return count;
        }
        /// <summary>
        /// Collect the list of active domains, and store their ID in @maxids
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="ids">array to collect the list of IDs of active domains</param>
        /// <param name="maxids">size of @ids</param>
        /// <returns>the number of domain found or -1 in case of error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectListDomains(IntPtr conn, int[] ids, int maxids);
        /// <summary>
        /// Collect the list of active physical host interfaces, and store their names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">array to collect the list of names of interfaces</param>
        /// <param name="maxnames">size of @names</param>
        /// <returns>the number of interfaces found or -1 in case of error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        private static extern int virConnectListInterfaces(IntPtr conn, IntPtr names, int maxnames);
        /// <summary>
        /// Collect the list of active physical host interfaces, and store their names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">array to collect the list of names of interfaces</param>
        /// <param name="maxnames">size of @names</param>
        /// <returns>the number of interfaces found or -1 in case of error</returns>
        public static int virConnectListInterfaces(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = virConnectListInterfaces(conn, namesPtr, maxnames);
            if (count > 0)
                names = ptrToStringArray(namesPtr, count);
            Marshal.FreeHGlobal(namesPtr);
            return count;
        }
        /// <summary>
        /// Collect the list of active networks, and store their names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">array to collect the list of names of active networks</param>
        /// <param name="maxnames">size of @names</param>
        /// <returns>the number of networks found or -1 in case of error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        private static extern int virConnectListNetworks(IntPtr conn, IntPtr names, int maxnames);
        /// <summary>
        /// Collect the list of active networks, and store their names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">array to collect the list of names of active networks</param>
        /// <param name="maxnames">size of @names</param>
        /// <returns>the number of networks found or -1 in case of error</returns>
        public static int virConnectListNetworks(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = virConnectListNetworks(conn, namesPtr, maxnames);
            if (count > 0)
                names = ptrToStringArray(namesPtr, count);
            Marshal.FreeHGlobal(namesPtr);
            return count;
        }
        /// <summary>
        /// List UUIDs of defined secrets, store pointers to names in uuids.
        /// </summary>
        /// <param name="conn">virConnect connection</param>
        /// <param name="uuids">Pointer to an array to store the UUIDs</param>
        /// <param name="maxuuids">size of the array.</param>
        /// <returns>the number of UUIDs provided in the array, or -1 on failure.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        private static extern int virConnectListSecrets(IntPtr conn, IntPtr uuids, int maxuuids);
        /// <summary>
        /// List UUIDs of defined secrets, store pointers to names in uuids.
        /// </summary>
        /// <param name="conn">virConnect connection</param>
        /// <param name="uuids">Pointer to an array to store the UUIDs</param>
        /// <param name="maxuuids">size of the array.</param>
        /// <returns>the number of UUIDs provided in the array, or -1 on failure.</returns>
        public static int virConnectListSecrets(IntPtr conn, ref string[] uuids, int maxuuids)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = virConnectListSecrets(conn, namesPtr, maxuuids);
            if (count > 0)
                uuids = ptrToStringArray(namesPtr, count);
            Marshal.FreeHGlobal(namesPtr);
            return count;
        }
        /// <summary>
        /// Provides the list of names of active storage pools upto maxnames. If there are more than maxnames, the remaining names will be silently ignored.
        /// </summary>
        /// <param name="conn">pointer to hypervisor connection</param>
        /// <param name="names">array of char * to fill with pool names (allocated by caller)</param>
        /// <param name="maxnames">size of the names array</param>
        /// <returns>0 on success, -1 on error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        private static extern int virConnectListStoragePools(IntPtr conn, IntPtr names, int maxnames);
        /// <summary>
        /// Provides the list of names of active storage pools upto maxnames. If there are more than maxnames, the remaining names will be silently ignored.
        /// </summary>
        /// <param name="conn">pointer to hypervisor connection</param>
        /// <param name="names">array of char * to fill with pool names (allocated by caller)</param>
        /// <param name="maxnames">size of the names array</param>
        /// <returns>0 on success, -1 on error</returns>
        public static int virConnectListStoragePools(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = virConnectListStoragePools(conn, namesPtr, maxnames);
            if (count > 0)
                names = ptrToStringArray(namesPtr, count);
            Marshal.FreeHGlobal(namesPtr);
            return count;
        }
        /// <summary>
        /// Provides the number of defined but inactive domains.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>the number of domain found or -1 in case of error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectNumOfDefinedDomains(IntPtr conn);
        /// <summary>
        /// Provides the number of defined (inactive) interfaces on the physical host.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>the number of defined interface found or -1 in case of error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectNumOfDefinedInterfaces(IntPtr conn);
        /// <summary>
        /// Provides the number of inactive networks.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>the number of networks found or -1 in case of error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectNumOfDefinedNetworks(IntPtr conn);
        /// <summary>
        /// Provides the number of inactive storage pools
        /// </summary>
        /// <param name="conn">pointer to hypervisor connection</param>
        /// <returns>the number of pools found, or -1 on error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectNumOfDefinedStoragePools(IntPtr conn);
        /// <summary>
        /// Provides the number of active domains.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>the number of domain found or -1 in case of error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectNumOfDomains(IntPtr conn);
        /// <summary>
        /// Provides the number of active interfaces on the physical host.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>the number of active interfaces found or -1 in case of error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectNumOfInterfaces(IntPtr conn);
        /// <summary>
        /// Provides the number of active networks.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>the number of network found or -1 in case of error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectNumOfNetworks(IntPtr conn);
        /// <summary>
        /// Fetch number of currently defined secrets.
        /// </summary>
        /// <param name="conn">virConnect connection</param>
        /// <returns>the number currently defined secrets.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectNumOfSecrets(IntPtr conn);
        /// <summary>
        /// Provides the number of active storage pools
        /// </summary>
        /// <param name="conn">pointer to hypervisor connection</param>
        /// <returns>the number of pools found, or -1 on error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectNumOfStoragePools(IntPtr conn);
        /// <summary>
        /// This function should be called first to get a connection to the Hypervisor and xen store
        /// </summary>
        /// <param name="name">Uri of the hypervisor</param>
        /// <returns>pointer to the connection</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virConnectOpen(string name);

        ///<summary>
        /// This function should be called first to get a connection to the Hypervisor. If necessary, authentication will be performed fetching credentials via the callback See virConnectOpen for notes about environment variables which can have an effect on opening drivers
        ///</summary>
        ///<param name="name">URI of the hypervisor</param>
        ///<param name="auth">Authenticate callback parameters</param>
        ///<param name="flags">Open flags</param>
        ///<returns>a pointer to the hypervisor connection or NULL in case of error URIs are documented at http://libvirt.org/uri.html </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virConnectOpenAuth(string name, ref virConnectAuth auth, int flags);

        /// <summary>
        /// This function should be called first to get a restricted connection to the library functionalities. The set of APIs usable are then restricted on the available methods to control the domains. See virConnectOpen for notes about environment variables which can have an effect on opening drivers
        /// </summary>
        /// <param name="name">Uri of the hypervisor</param>
        /// <returns>a pointer to the hypervisor connection or NULL in case of error URIs are documented at http://libvirt.org/uri.html </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virConnectOpenReadOnly(string name);
        /// <summary>
        /// Increment the reference count on the connection. For each additional call to this method, there shall be a corresponding call to virConnectClose to release the reference count, once the caller no longer needs the reference to this object. This method is typically useful for applications where multiple threads are using a connection, and it is required that the connection remain open until all threads have finished using it. ie, each new thread using a connection would increment the reference count.
        /// </summary>
        /// <param name="conn">the connection to hold a reference on</param>
        /// <returns>0 in case of success, -1 in case of failure</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virConnectRef(IntPtr conn);
        #endregion

        #region Domain
        /// <summary>
        /// Create a virtual device attachment to backend. This function, having hotplug semantics, is only allowed on an active domain.
        /// </summary>
        /// <param name="domain">pointer to domain object</param>
        /// <param name="xml">pointer to XML description of one device</param>
        /// <returns>0 in case of success, -1 in case of failure.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainAttachDevice(IntPtr domain, string xml);
        /// <summary>
        /// Attach a virtual device to a domain, using the flags parameter to control how the device is attached. VIR_DOMAIN_DEVICE_MODIFY_CURRENT specifies that the device allocation is made based on current domain state. VIR_DOMAIN_DEVICE_MODIFY_LIVE specifies that the device shall be allocated to the active domain instance only and is not added to the persisted domain configuration. VIR_DOMAIN_DEVICE_MODIFY_CONFIG specifies that the device shall be allocated to the persisted domain configuration only. Note that the target hypervisor must return an error if unable to satisfy flags. E.g. the hypervisor driver will return failure if LIVE is specified but it only supports modifying the persisted device allocation.
        /// </summary>
        /// <param name="domain">pointer to domain object</param>
        /// <param name="xml">pointer to XML description of one device</param>
        /// <param name="flags">an OR'ed set of virDomainDeviceModifyFlags</param>
        /// <returns>0 in case of success, -1 in case of failure.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainAttachDeviceFlags(IntPtr domain, string xml, uint flags);

        // TODO virDomainBlockPeek

        /// <summary>
        /// This function returns block device (disk) stats for block devices attached to the domain. The path parameter is the name of the block device. Get this by calling virDomainGetXMLDesc and finding the target dev='...' attribute within //domain/devices/disk. (For example, "xvda"). Domains may have more than one block device. To get stats for each you should make multiple calls to this function. Individual fields within the stats structure may be returned as -1, which indicates that the hypervisor does not support that particular statistic.
        /// </summary>
        /// <param name="dom">pointer to the domain object</param>
        /// <param name="path">path to the block device</param>
        /// <param name="stats">block device stats (returned)</param>
        /// <param name="size">size of stats structure</param>
        /// <returns>0 in case of success or -1 in case of failure.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainBlockStats(IntPtr dom, string path, virDomainBlockStatsStruct stats, int size);
        /// <summary>
        /// This method will dump the core of a domain on a given file for analysis. Note that for remote Xen Daemon the file path will be interpreted in the remote host.
        /// </summary>
        /// <param name="domain">a domain object</param>
        /// <param name="to">path for the core file</param>
        /// <param name="flags">extra flags, currently unused</param>
        /// <returns>0 in case of success and -1 in case of failure.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainCoreDump(IntPtr domain, string to, int flags);
        /// <summary>
        /// Launch a defined domain. If the call succeed the domain moves from the defined to the running domains pools.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/> pointer to a defined domain.
        /// </param>
        /// <returns>
        /// 0 in case of success, -1 in case of error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainCreate(IntPtr domain);
        /// <summary>
        /// Launch a new guest domain, based on an XML description similar to the one returned by virDomainGetXMLDesc().
        /// This function may requires privileged access to the hypervisor. The domain is not persistent, 
        /// so its definition will disappear when it is destroyed, or if the host is restarted (see virDomainDefineXML() to define persistent domains).
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/> pointer to the hypervisor connection.
        /// </param>
        /// <param name="xmlDesc">
        /// A string containing an XML description of the domain.
        /// </param>
        /// <param name="flags">
        /// Callers should always pass 0.
        /// </param>
        /// <returns>
        /// A new domain object or NULL in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virDomainCreateXML(IntPtr conn, string xmlDesc, uint flags);
        /// <summary>
        /// Define a domain, but does not start it. This definition is persistent, until explicitly undefined with virDomainUndefine(). 
        /// A previous definition for this domain would be overriden if it already exists.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to the hypervisor connection.
        /// </param>
        /// <param name="xml">
        /// The XML description for the domain, preferably in UTF-8.
        /// </param>
        /// <returns>
        /// NULL in case of error, a pointer to the domain otherwise.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virDomainDefineXML(IntPtr conn, string xml);
        /// <summary>
        /// Destroy the domain object. The running instance is shutdown if not down already 
        /// and all resources used by it are given back to the hypervisor. 
        /// This does not free the associated virDomainPtr object. This function may require privileged access
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainDestroy(IntPtr domain);
        /// <summary>
        /// Destroy a virtual device attachment to backend.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>pointer to domain object.
        /// </param>
        /// <param name="xml">
        /// Pointer to XML description of one device.
        /// </param>
        /// <returns>
        /// 0 in case of success, -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainDetachDevice(IntPtr domain, string xml);
        /// <summary>
        /// Detach a virtual device from a domain, using the flags parameter to control how the device is detached. VIR_DOMAIN_DEVICE_MODIFY_CURRENT specifies that the device allocation is removed based on current domain state. VIR_DOMAIN_DEVICE_MODIFY_LIVE specifies that the device shall be deallocated from the active domain instance only and is not from the persisted domain configuration. VIR_DOMAIN_DEVICE_MODIFY_CONFIG specifies that the device shall be deallocated from the persisted domain configuration only. Note that the target hypervisor must return an error if unable to satisfy flags. E.g. the hypervisor driver will return failure if LIVE is specified but it only supports removing the persisted device allocation.
        /// </summary>
        /// <param name="domain">pointer to domain object</param>
        /// <param name="xml">pointer to XML description of one device</param>
        /// <param name="flags">an OR'ed set of virDomainDeviceModifyFlags</param>
        /// <returns>0 in case of success, -1 in case of failure.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainDetachDeviceFlags(IntPtr domain, string xml, uint flags);
        /// <summary>
        /// Free the domain object. The running instance is kept alive. The data structure is freed and should not be used thereafter.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainFree(IntPtr domain);
        /// <summary>
        /// Provides a boolean value indicating whether the domain configured to be automatically started when the host machine boots.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <param name="autostart">
        /// The value returned.
        /// </param>
        /// <returns>
        /// -1 in case of error, 0 in case of success.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainGetAutostart(IntPtr domain, out int autostart);
        /// <summary>
        /// Provides the connection pointer associated with a domain. 
        /// The reference counter on the connection is not increased by this call. 
        /// WARNING: When writing libvirt bindings in other languages, do not use this function. 
        /// Instead, store the connection and the domain object together.
        /// </summary>
        /// <param name="dom">
        /// A <see cref="IntPtr"/>pointer to a domain.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>or NULL in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virDomainGetConnect(IntPtr dom);
        /// <summary>
        /// Get the hypervisor ID number for the domain.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <returns>
        /// The domain ID number or (unsigned int) -1 in case of error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainGetID(IntPtr domain);
        /// <summary>
        /// Extract information about a domain. Note that if the connection used to get the domain is limited only a 
        /// partial set of the information can be extracted.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <param name="info">
        /// Pointer to a virDomainInfo structure allocated by the user.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainGetInfo(IntPtr domain, [Out] virDomainInfo info);
        /// <summary>
        /// Retrieve the maximum amount of physical memory allocated to a domain. 
        /// If domain is NULL, then this get the amount of memory reserved to Domain0 i.e. the domain where the application runs.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object or NULL.
        /// </param>
        /// <returns>
        /// the memory size in kilobytes or 0 in case of error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern ulong virDomainGetMaxMemory(IntPtr domain);
        /// <summary>
        /// Provides the maximum number of virtual CPUs supported for the guest VM. 
        /// If the guest is inactive, this is basically the same as virConnectGetMaxVcpus. 
        /// If the guest is running this will reflect the maximum number of virtual CPUs the guest was booted with.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>pointer to domain object.
        /// </param>
        /// <returns>
        /// The maximum of virtual CPU or -1 in case of error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainGetMaxVcpus(IntPtr domain);
        /// <summary>
        /// Get the public name for that domain.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <returns>
        /// Pointer to the name or NULL, the string need not be deallocated its lifetime will be the same as the domain object.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virDomainGetName(IntPtr domain);
        /// <summary>
        /// Get the type of domain operation system.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <returns>
        /// The new string or NULL in case of error, the string must be freed by the caller.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virDomainGetOSType(IntPtr domain);

        // TODO virDomainGetSchedulerParameters

        // TODO virDomainGetSchedulerType

        // TODO virDomainGetSecurityLabel

        /// <summary>
        /// Get the UUID for a domain.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>a domain object.
        /// </param>
        /// <param name="uuid">
        /// A <see cref="System.Char"/>pointer to a VIR_UUID_BUFLEN bytes array.
        /// </param>
        /// <returns>
        /// A <see cref="System.Int32"/>-1 in case of error, 0 in case of success.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainGetUUID(IntPtr domain, [Out] char[] uuid);
        /// <summary>
        /// Get the UUID for a domain as string. For more information about UUID see RFC4122.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <param name="buf">
        /// Pointer to a VIR_UUID_STRING_BUFLEN bytes array.
        /// </param>
        /// <returns>
        /// -1 in case of error, 0 in case of success.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainGetUUIDString(IntPtr domain, [Out] IntPtr buf);

        // TODO virDomainGetVcpus

        /// <summary>
        /// Provide an XML description of the domain. The description may be reused later to relaunch the domain with virDomainCreateXML().
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>a domain object.
        /// </param>
        /// <param name="flags">
        /// An OR'ed set of virDomainXMLFlags.
        /// </param>
        /// <returns>
        /// A 0 terminated UTF-8 encoded XML instance, or NULL in case of error. the caller must free() the returned value.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virDomainGetXMLDesc(IntPtr domain, int flags);
        /// <summary>
        /// This function returns network interface stats for interfaces attached to the domain. 
        /// The path parameter is the name of the network interface. Domains may have more than one network interface. 
        /// To get stats for each you should make multiple calls to this function. 
        /// Individual fields within the stats structure may be returned as -1, 
        /// which indicates that the hypervisor does not support that particular statistic.
        /// </summary>
        /// <param name="dom">
        /// A <see cref="IntPtr"/>pointer to the domain object.
        /// </param>
        /// <param name="path">
        /// Path to the interface.
        /// </param>
        /// <param name="stats">
        /// A <see cref="virDomainInterfaceStatsStruct"/>network interface stats (returned).
        /// </param>
        /// <param name="size">
        /// Size of stats structure.
        /// </param>
        /// <returns>
        /// 0 in case of success or -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainInterfaceStats(IntPtr dom, string path, virDomainInterfaceStatsStruct stats, int size);
        /// <summary>
        /// Determine if the domain is currently running.
        /// </summary>
        /// <param name="dom">
        /// A <see cref="IntPtr"/>pointer to the domain object.
        /// </param>
        /// <returns>
        /// 1 if running, 0 if inactive, -1 on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainIsActive(IntPtr dom);

        /// <summary>
        /// Determine if the domain has a persistent configuration which means it will still exist after shutting down.
        /// </summary>
        /// <param name="dom">
        /// A <see cref="IntPtr"/>pointer to the domain object.
        /// </param>
        /// <returns>
        /// 1 if persistent, 0 if transient, -1 on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainIsPersistent(IntPtr dom);

        /// <summary>
        /// Try to find a domain based on the hypervisor ID number.
        /// Note that this won't work for inactive domains which have an ID of -1, 
        /// in that case a lookup based on the Name or UUId need to be done instead.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to the hypervisor connection.
        /// </param>
        /// <param name="id">
        /// The domain ID number.
        /// </param>
        /// <returns>
        /// A new domain object or NULL in case of failure. If the domain cannot be found, then VIR_ERR_NO_DOMAIN error is raised.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virDomainLookupByID(IntPtr conn, int id);

        /// <summary>
        /// Try to lookup a domain on the given hypervisor based on its name.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to the hypervisor connection.
        /// </param>
        /// <param name="name">
        /// A <see cref="System.String"/>name for the domain.
        /// </param>
        /// <returns>
        /// A new domain object or NULL in case of failure. If the domain cannot be found, then VIR_ERR_NO_DOMAIN error is raised.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virDomainLookupByName(IntPtr conn, string name);

        /// <summary>
        /// Try to lookup a domain on the given hypervisor based on its UUID.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to the hypervisor connection.
        /// </param>
        /// <param name="uuid">
        /// A <see cref="System.Char"/>the raw UUID for the domain.
        /// </param>
        /// <returns>
        /// A new domain object or NULL in case of failure. If the domain cannot be found, then VIR_ERR_NO_DOMAIN error is raised.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virDomainLookupByUUID(IntPtr conn, char[] uuid);

        /// <summary>
        /// Try to lookup a domain on the given hypervisor based on its UUID.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to the hypervisor connection.
        /// </param>
        /// <param name="uuidstr">
        /// The string UUID for the domain.
        /// </param>
        /// <returns>
        /// A new domain object or NULL in case of failure. If the domain cannot be found, then VIR_ERR_NO_DOMAIN error is raised.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virDomainLookupByUUIDString(IntPtr conn, string uuidstr);

        // TODO virDomainMemoryPeek

        // TODO virDomainMigrate

        // TODO virDomainMigrateToURI

        // TODO virDomainPinVcpu

        // TODO virDomainMemoryStats

        /// <summary>
        /// Reboot a domain, the domain object is still usable there after but the domain OS is being stopped for a restart. 
        /// Note that the guest OS may ignore the request.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <param name="flags">
        /// Extra flags for the reboot operation, not used yet.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainReboot(IntPtr domain, uint flags);

        /// <summary>
        /// Increment the reference count on the domain. For each additional call to this method, 
        /// there shall be a corresponding call to virDomainFree to release the reference count, 
        /// once the caller no longer needs the reference to this object. 
        /// This method is typically useful for applications where multiple threads are using a connection, 
        /// and it is required that the connection remain open until all threads have finished using it. ie, 
        /// each new thread using a domain would increment the reference count.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>the domain to hold a reference on.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainRef(IntPtr domain);

        /// <summary>
        /// This method will restore a domain saved to disk by virDomainSave().
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to the hypervisor connection.
        /// </param>
        /// <param name="from">
        /// Path to the file with saved domain.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainRestore(IntPtr conn, string from);

        /// <summary>
        /// Resume a suspended domain, the process is restarted from the state where it was frozen by calling virSuspendDomain(). 
        /// This function may requires privileged access
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainResume(IntPtr domain);

        /// <summary>
        /// This method will suspend a domain and save its memory contents to a file on disk. After the call, if successful, 
        /// the domain is not listed as running anymore (this may be a problem). Use virDomainRestore() to restore a domain after saving.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <param name="to">
        /// Path for the output file.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainSave(IntPtr domain, string to);

        /// <summary>
        /// Configure the domain to be automatically started when the host machine boots.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <param name="autostart">
        /// Whether the domain should be automatically started 0 or 1.
        /// </param>
        /// <returns>
        /// -1 in case of error, 0 in case of success.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainSetAutostart(IntPtr domain, int autostart);

        /// <summary>
        /// Dynamically change the maximum amount of physical memory allocated to a domain. 
        /// If domain is NULL, then this change the amount of memory reserved to Domain0 i.e. 
        /// the domain where the application runs. This function requires privileged access to the hypervisor.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object or NULL.
        /// </param>
        /// <param name="memory">
        /// The memory size in kilobytes.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainSetMaxMemory(IntPtr domain, ulong memory);

        /// <summary>
        /// Dynamically change the target amount of physical memory allocated to a domain. 
        /// If domain is NULL, then this change the amount of memory reserved to Domain0 i.e. 
        /// the domain where the application runs. This function may requires privileged access to the hypervisor.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object or NULL.
        /// </param>
        /// <param name="memory">
        /// The memory size in kilobytes.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainSetMemory(IntPtr domain, ulong memory);

        // TODO virDomainSetSchedulerParameters

        /// <summary>
        /// Dynamically change the number of virtual CPUs used by the domain. 
        /// Note that this call may fail if the underlying virtualization hypervisor does not support it 
        /// or if growing the number is arbitrary limited. This function requires privileged access to the hypervisor.
        /// </summary>
        /// <param name="domain">
        /// Pointer to domain object, or NULL for Domain0
        /// </param>
        /// <param name="nvcpus">
        /// The new number of virtual CPUs for this domain.
        /// </param>
        /// <returns>
        /// 0 in case of success, -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainSetVcpus(IntPtr domain, uint nvcpus);

        /// <summary>
        /// Shutdown a domain, the domain object is still usable there after but the domain OS is being stopped. 
        /// Note that the guest OS may ignore the request. 
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainShutdown(IntPtr domain);

        /// <summary>
        /// Suspends an active domain, the process is frozen without further access to CPU resources and I/O 
        /// but the memory used by the domain at the hypervisor level will stay allocated. 
        /// Use virDomainResume() to reactivate the domain. This function may requires privileged access.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainSuspend(IntPtr domain);

        /// <summary>
        /// Undefine a domain but does not stop it if it is running.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>pointer to a defined domain.
        /// </param>
        /// <returns>
        /// 0 in case of success, -1 in case of error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virDomainUndefine(IntPtr domain);
        #endregion

        #region Events

        ///<summary>
        /// Function to install callbacks
        ///</summary>
        ///<param name="addHandle">the virEventAddHandleFunc which will be called (a delegate)</param>
        ///<param name="updateHandle">the virEventUpdateHandleFunc which will be called (a delegate)</param>
        ///<param name="removeHandle">the virEventRemoveHandleFunc which will be called (a delegate)</param>
        ///<param name="addTimeout">the virEventAddTimeoutFunc which will be called (a delegate)</param>
        ///<param name="updateTimeout">the virEventUpdateTimeoutFunc which will be called (a delegate)</param>
        ///<param name="removeTimeout">the virEventRemoveTimeoutFunc which will be called (a delegate)</param>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern void virEventRegisterImpl([MarshalAs(UnmanagedType.FunctionPtr)]virEventAddHandleFunc addHandle,
                                                       [MarshalAs(UnmanagedType.FunctionPtr)]virEventUpdateHandleFunc updateHandle,
                                                       [MarshalAs(UnmanagedType.FunctionPtr)]virEventRemoveHandleFunc removeHandle,
                                                       [MarshalAs(UnmanagedType.FunctionPtr)]virEventAddTimeoutFunc addTimeout,
                                                       [MarshalAs(UnmanagedType.FunctionPtr)]virEventUpdateTimeoutFunc updateTimeout,
                                                       [MarshalAs(UnmanagedType.FunctionPtr)]virEventRemoveTimeoutFunc removeTimeout);
        #endregion

        #region Library
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
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virGetVersion([Out] out ulong libVer, [In] string type, [Out] out ulong typeVer);
        /// <summary>
        /// Initialize the library. It's better to call this routine at startup in multithreaded applications to avoid 
        /// potential race when initializing the library.
        /// </summary>
        /// <returns>
        /// 0 in case of success, -1 in case of error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virInitialize();
        #endregion

        #region Interface

        // TODO virInterfaceCreate

        // TODO virInterfaceDefineXML

        // TODO virInterfaceDestroy

        // TODO virInterfaceFree

        // TODO virInterfaceGetConnect

        // TODO virInterfaceGetMACString

        // TODO virInterfaceGetName

        // TODO virInterfaceGetXMLDesc

        // TODO virInterfaceIsActive

        // TODO virInterfaceLookupByMACString

        // TODO virInterfaceLookupByName

        // TODO virInterfaceRef

        // TODO virInterfaceUndefine

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
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virNetworkRef(IntPtr network);

        // TODO virNetworkSetAutostart

        // TODO virNetworkUndefine
        #endregion

        #region Node

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
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virNodeDeviceGetXMLDesc(IntPtr dev, uint flags);

        // TODO virNodeDeviceListCaps

        /// <summary>
        /// Lookup a node device by its name.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="name">unique device name</param>
        /// <returns>a virNodeDevicePtr if found, NULL otherwise.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virNodeDeviceLookupByName(IntPtr conn, string name);
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
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern ulong virNodeGetFreeMemory(IntPtr conn);
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
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virNodeGetInfo(IntPtr h, [Out] virNodeInfo info);

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
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virNodeListDevices(IntPtr conn, string cap, IntPtr names, int maxnames, uint flags);
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
        public static int virNodeListDevices(IntPtr conn, string cap, ref string[] names, int maxnames, uint flags)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = virNodeListDevices(conn, cap, namesPtr, maxnames, 0);
            if (count > 0)
                names = ptrToStringArray(namesPtr, count);
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
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virNodeNumOfDevices(IntPtr conn, string cap, uint flags);
        #endregion

        #region Secret

        // TODO virSecretDefineXML

        // TODO virSecretFree

        // TODO virSecretGetConnect

        // TODO virSecretGetUUID

        // TODO virSecretGetUUIDString

        // TODO virSecretGetUsageID

        // TODO virSecretGetUsageType

        // TODO virSecretGetValue

        // TODO virSecretGetXMLDesc

        // TODO virSecretLookupByUUID

        // TODO virSecretLookupByUUIDString

        // TODO virSecretLookupByUsage

        // TODO virSecretRef

        // TODO virSecretSetValue

        // TODO virSecretUndefine
        #endregion

        #region Storage pool

        /// <summary>
        /// Build the underlying storage pool.
        /// </summary>
        /// <param name="pool">
        /// A pointer to storage pool.
        /// </param>
        /// <param name="flags">
        /// Future flags, use 0 for now.
        /// </param>
        /// <returns>
        /// 0 on success, or -1 upon failure
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolBuild(IntPtr pool, virStoragePoolBuildFlags flags);

        /// <summary>
        /// Starts an inactive storage pool.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <param name="flags">
        /// Future flags, use 0 for now.
        /// </param>
        /// <returns>
        /// 0 on success, or -1 if it could not be started.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolCreate(IntPtr pool, uint flags);

        /// <summary>
        /// Create a new storage based on its XML description. The pool is not persistent, 
        /// so its definition will disappear when it is destroyed, or if the host is restarted
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to hypervisor connection.
        /// </param>
        /// <param name="xmlDesc">
        /// A <see cref="System.String"/>XML description for new pool.
        /// </param>
        /// <param name="flags">
        /// A <see cref="System.UInt32"/>future flags, use 0 for now.
        /// </param>
        /// <returns>
        /// A virStoragePoolPtr object, or NULL if creation failed.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virStoragePoolCreateXML(IntPtr conn, string xmlDesc, uint flags);

        /// <summary>
        /// Define a new inactive storage pool based on its XML description. The pool is persistent, 
        /// until explicitly undefined.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to hypervisor connection.
        /// </param>
        /// <param name="xml">
        /// A <see cref="System.String"/>XML description for new pool.
        /// </param>
        /// <param name="flags">
        /// A <see cref="System.UInt32"/>future flags, use 0 for now.
        /// </param>
        /// <returns>
        /// A virStoragePoolPtr object, or NULL if creation failed.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virStoragePoolDefineXML(IntPtr conn, string xml, uint flags);

        /// <summary>
        /// Delete the underlying pool resources. This is a non-recoverable operation. The virStoragePoolPtr object itself is not free'd.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <param name="flags">
        /// A <see cref="virStoragePoolDeleteFlags"/>flags for obliteration process.
        /// </param>
        /// <returns>
        /// 0 on success, or -1 if it could not be obliterate.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolDelete(IntPtr pool, virStoragePoolDeleteFlags flags);

        /// <summary>
        /// Destroy an active storage pool. This will deactivate the pool on the host, 
        /// but keep any persistent config associated with it. 
        /// If it has a persistent config it can later be restarted with virStoragePoolCreate(). 
        /// This does not free the associated virStoragePoolPtr object.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <returns>
        /// 0 on success, or -1 if it could not be destroyed.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolDestroy(IntPtr pool);

        /// <summary>
        /// Free a storage pool object, releasing all memory associated with it. Does not change the state of the pool on the host.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <returns>
        /// 0 on success, or -1 if it could not be free'd.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolFree(IntPtr pool);

        /// <summary>
        /// Fetches the value of the autostart flag, which determines whether the pool is automatically started at boot time.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <param name="autotart"></param>
        /// <returns>
        /// 0 on success, -1 on failure
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolGetAutostart(IntPtr pool, out int autotart);

        /// <summary>
        /// Provides the connection pointer associated with a storage pool. The reference counter on the connection is not increased by this call. 
        /// WARNING: When writing libvirt bindings in other languages, do not use this function. Instead, store the connection and the pool object together.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to a pool.
        /// </param>
        /// <returns>
        /// A <see cref="IntPtr"/>the virConnectPtr or NULL in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virStoragePoolGetConnect(IntPtr pool);

        /// <summary>
        /// Get volatile information about the storage pool such as free space / usage summary.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <param name="info">
        /// Pointer at which to store info.
        /// </param>
        /// <returns>
        /// 0 on success, or -1 on failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolGetInfo(IntPtr pool, ref virStoragePoolInfo info);

        /// <summary>
        /// Fetch the locally unique name of the storage pool.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <returns>
        /// The name of the pool, or NULL on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virStoragePoolGetName(IntPtr pool);

        /// <summary>
        /// Fetch the globally unique ID of the storage pool.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>Pointer to storage pool.
        /// </param>
        /// <param name="uuid">
        /// Buffer of VIR_UUID_BUFLEN bytes in size.
        /// </param>
        /// <returns>
        /// 0 on success, or -1 on error
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolGetUUID(IntPtr pool, [Out] char[] uuid);

        /// <summary>
        /// Fetch the globally unique ID of the storage pool as a string.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <param name="uuid">
        /// A <see cref="IntPtr"/>buffer of VIR_UUID_STRING_BUFLEN bytes in size.
        /// </param>
        /// <returns>
        /// 0 on success, or -1 on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        private static extern int virStoragePoolGetUUIDString(IntPtr pool, [Out] char[] uuid);

        ///<summary>
        /// Fetch the globally unique ID of the storage pool as a string.
        ///</summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        ///<returns>the storage pool UUID</returns>
        public static string virStoragePoolGetUUIDString(IntPtr pool)
        {
            char[] uuidArray = new char[36];
            virStoragePoolGetUUIDString(pool, uuidArray);
            return new string(uuidArray);
        }
        /// <summary>
        /// Fetch an XML document describing all aspects of the storage pool. 
        /// This is suitable for later feeding back into the virStoragePoolCreateXML method.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <param name="flags">
        /// Flags for XML format options (set of virDomainXMLFlags).
        /// </param>
        /// <returns>
        /// A XML document, or NULL on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virStoragePoolGetXMLDesc(IntPtr pool, virDomainXMLFlags flags);

        /// <summary>
        /// Determine if the storage pool is currently running.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to the storage pool object.
        /// </param>
        /// <returns>
        /// 1 if running, 0 if inactive, -1 on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolIsActive(IntPtr pool);

        /// <summary>
        /// Determine if the storage pool has a persistent configuration which means it will still exist after shutting down.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to the storage pool object.
        /// </param>
        /// <returns>
        /// 1 if persistent, 0 if transient, -1 on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolIsPersistent(IntPtr pool);

        /// <summary>
        /// Fetch list of storage volume names, limiting to at most maxnames.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <param name="names">
        /// A <see cref="IntPtr"/>array in which to storage volume names.
        /// </param>
        /// <param name="maxnames">
        /// A <see cref="System.Int32"/>size of names array.
        /// </param>
        /// <returns>
        /// The number of names fetched, or -1 on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        private static extern int virStoragePoolListVolumes(IntPtr pool, IntPtr names, int maxnames);
        /// <summary>
        /// Fetch list of storage volume names, limiting to at most maxnames.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <param name="names">
        /// A <see cref="IntPtr"/>array in which to storage volume names.
        /// </param>
        /// <param name="maxnames">
        /// A <see cref="System.Int32"/>size of names array.
        /// </param>
        /// <returns>
        /// The number of names fetched, or -1 on error.
        /// </returns>
        public static int virStoragePoolListVolumes(IntPtr pool, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = virStoragePoolListVolumes(pool, namesPtr, maxnames);
            if (count > 0)
                names = ptrToStringArray(namesPtr, count);
            Marshal.FreeHGlobal(namesPtr);
            return count;
        }
        /// <summary>
        /// Fetch a storage pool based on its unique name.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to hypervisor connection.
        /// </param>
        /// <param name="name">
        /// Name of pool to fetch.
        /// </param>
        /// <returns>
        /// A <see cref="IntPtr"/>virStoragePoolPtr object, or NULL if no matching pool is found.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virStoragePoolLookupByName(IntPtr conn, string name);

        /// <summary>
        /// Fetch a storage pool based on its globally unique id.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to hypervisor connection.
        /// </param>
        /// <param name="uuid">
        /// Globally unique id of pool to fetch.
        /// </param>
        /// <returns>
        /// A virStoragePoolPtr object, or NULL if no matching pool is found
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virStoragePoolLookupByUUID(IntPtr conn, char[] uuid);

        /// <summary>
        /// Fetch a storage pool based on its globally unique id.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to hypervisor connection.
        /// </param>
        /// <param name="uuidstr">
        /// A <see cref="System.String"/>globally unique id of pool to fetch.
        /// </param>
        /// <returns>
        /// A <see cref="IntPtr"/>object, or NULL if no matching pool is found.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virStoragePoolLookupByUUIDString(IntPtr conn, string uuidstr);

        /// <summary>
        /// Fetch a storage pool which contains a particular volume.
        /// </summary>
        /// <param name="vol">
        /// A <see cref="IntPtr"/>pointer to storage volume.
        /// </param>
        /// <returns>
        /// A <see cref="IntPtr"/>object, or NULL if no matching pool is found.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virStoragePoolLookupByVolume(IntPtr vol);

        /// <summary>
        /// Fetch the number of storage volumes within a pool.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <returns>
        /// A <see cref="System.Int32"/>the number of storage pools, or -1 on failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolNumOfVolumes(IntPtr pool);

        /// <summary>
        /// Increment the reference count on the pool. For each additional call to this method, 
        /// there shall be a corresponding call to virStoragePoolFree to release the reference count, 
        /// once the caller no longer needs the reference to this object. 
        /// This method is typically useful for applications where multiple threads are using a connection, 
        /// and it is required that the connection remain open until all threads have finished using it. ie, 
        /// each new thread using a pool would increment the reference count.
        /// </summary>
        /// <param name="pool">
        /// The pool to hold a reference on.
        /// </param>
        /// <returns>
        /// 0 in case of success, -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolRef(IntPtr pool);

        /// <summary>
        /// Request that the pool refresh its list of volumes. This may involve communicating with a remote server, 
        /// and/or initializing new devices at the OS layer.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <param name="flags">
        /// A <see cref="System.UInt32"/>flags to control refresh behaviour (currently unused, use 0).
        /// </param>
        /// <returns>
        /// 0 if the volume list was refreshed, -1 on failure.
        /// </returns>
        [DllImport("virtlib-0.dll")]
        public static extern int virStoragePoolRefresh(IntPtr pool, uint flags);

        /// <summary>
        /// Sets the autostart flag.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <param name="autostart">
        /// A <see cref="System.Int32"/>new flag setting.
        /// </param>
        /// <returns>
        /// 0 on success, -1 on failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolSetAutostart(IntPtr pool, int autostart);

        /// <summary>
        /// Undefine an inactive storage pool.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <returns>
        /// 0 on success, -1 on failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStoragePoolUndefine(IntPtr pool);
        #endregion

        #region Volumes

        /// <summary>
        /// Create a storage volume within a pool based on an XML description. Not all pools support creation of volumes.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <param name="xmldesc">
        /// A <see cref="System.String"/>description of volume to create.
        /// </param>
        /// <param name="flags">
        /// A <see cref="System.UInt32"/>flags for creation (unused, pass 0).
        /// </param>
        /// <returns>
        /// The storage volume, or NULL on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virStorageVolCreateXML(IntPtr pool, string xmldesc, uint flags);

        /// <summary>
        /// Create a storage volume in the parent pool, using the 'clonevol' volume as input. 
        /// Information for the new volume (name, perms) are passed via a typical volume XML description.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to parent pool for the new volume.
        /// </param>
        /// <param name="xmldesc">
        /// A <see cref="System.String"/>description of volume to create.
        /// </param>
        /// <param name="clonevol">
        /// A <see cref="IntPtr"/>storage volume to use as input.
        /// </param>
        /// <param name="flags">
        /// A <see cref="System.UInt32"/>flags for creation (unused, pass 0).
        /// </param>
        /// <returns>
        /// A <see cref="IntPtr"/>the storage volume, or NULL on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virStorageVolCreateXMLFrom(IntPtr pool, string xmldesc, IntPtr clonevol, uint flags);

        /// <summary>
        /// Delete the storage volume from the pool.
        /// </summary>
        /// <param name="vol">
        /// A <see cref="IntPtr"/>pointer to storage volume.
        /// </param>
        /// <param name="flags">
        /// A <see cref="System.UInt32"/>future flags, use 0 for now.
        /// </param>
        /// <returns>
        /// 0 on success, or -1 on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStorageVolDelete(IntPtr vol, uint flags);

        /// <summary>
        /// Release the storage volume handle. The underlying storage volume continues to exist.
        /// </summary>
        /// <param name="vol">
        /// A <see cref="IntPtr"/>pointer to storage volume.
        /// </param>
        /// <returns>
        /// 0 on success, or -1 on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStorageVolFree(IntPtr vol);

        /// <summary>
        /// Provides the connection pointer associated with a storage volume. 
        /// The reference counter on the connection is not increased by this call. 
        /// WARNING: When writing libvirt bindings in other languages, do not use this function. 
        /// Instead, store the connection and the volume object together.
        /// </summary>
        /// <param name="vol">
        /// A <see cref="IntPtr"/>
        /// </param>
        /// <returns>
        /// A <see cref="IntPtr"/>
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virStorageVolGetConnect(IntPtr vol);

        /// <summary>
        /// Fetches volatile information about the storage volume such as its current allocation.
        /// </summary>
        /// <param name="vol">
        /// A <see cref="IntPtr"/>pointer to storage volume.
        /// </param>
        /// <param name="info">
        /// A <see cref="virStorageVolInfo"/>pointer at which to store info.
        /// </param>
        /// <returns>
        /// 0 on success, or -1 on failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStorageVolGetInfo(IntPtr vol, ref virStorageVolInfo info);

        /// <summary>
        /// Fetch the storage volume key. 
        /// This is globally unique, so the same volume will have the same key no matter what host it is accessed from
        /// </summary>
        /// <param name="vol">
        /// A <see cref="IntPtr"/>pointer to storage volume.
        /// </param>
        /// <returns>
        /// The volume key, or NULL on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virStorageVolGetKey(IntPtr vol);

        /// <summary>
        /// Fetch the storage volume name. This is unique within the scope of a pool.
        /// </summary>
        /// <param name="vol">
        /// A <see cref="IntPtr"/>pointer to storage volume.
        /// </param>
        /// <returns>
        /// The volume name, or NULL on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virStorageVolGetName(IntPtr vol);

        /// <summary>
        /// Fetch the storage volume path. 
        /// Depending on the pool configuration this is either persistent across hosts, 
        /// or dynamically assigned at pool startup. 
        /// Consult pool documentation for information on getting the persistent naming.
        /// </summary>
        /// <param name="vol">
        /// A <see cref="IntPtr"/>pointer to storage volume.
        /// </param>
        /// <returns>
        /// The storage volume path, or NULL on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virStorageVolGetPath(IntPtr vol);

        /// <summary>
        /// Fetch an XML document describing all aspects of the storage volume.
        /// </summary>
        /// <param name="vol">
        /// A <see cref="IntPtr"/>pointer to storage volume.
        /// </param>
        /// <param name="flags">
        /// A <see cref="System.UInt32"/>flags for XML generation (unused, pass 0).
        /// </param>
        /// <returns>
        /// The XML document, or NULL on error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virStorageVolGetXMLDesc(IntPtr vol, uint flags);

        /// <summary>
        /// Fetch a pointer to a storage volume based on its globally unique key.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to hypervisor connection.
        /// </param>
        /// <param name="key">
        /// A <see cref="System.String"/>globally unique key.
        /// </param>
        /// <returns>
        /// A <see cref="IntPtr"/>storage volume, or NULL if not found / error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virStorageVolLookupByKey(IntPtr conn, string key);

        /// <summary>
        /// Fetch a pointer to a storage volume based on its name within a pool.
        /// </summary>
        /// <param name="pool">
        /// A <see cref="IntPtr"/>pointer to storage pool.
        /// </param>
        /// <param name="name">
        /// A <see cref="System.String"/>name of storage volume.
        /// </param>
        /// <returns>
        /// A <see cref="IntPtr"/>storage volume, or NULL if not found / error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virStorageVolLookupByName(IntPtr pool, string name);

        /// <summary>
        /// Fetch a pointer to a storage volume based on its locally (host) unique path.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to hypervisor connection.
        /// </param>
        /// <param name="path">
        /// A <see cref="System.String"/>locally unique path.
        /// </param>
        /// <returns>
        /// A <see cref="IntPtr"/>storage volume, or NULL if not found / error.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virStorageVolLookupByPath(IntPtr conn, string path);

        /// <summary>
        /// Increment the reference count on the vol. For each additional call to this method, 
        /// there shall be a corresponding call to virStorageVolFree to release the reference count, 
        /// once the caller no longer needs the reference to this object. 
        /// This method is typically useful for applications where multiple threads are using a connection, 
        /// and it is required that the connection remain open until all threads have finished using it. ie, 
        /// each new thread using a vol would increment the reference count.
        /// </summary>
        /// <param name="vol">
        /// A <see cref="IntPtr"/>the vol to hold a reference on.
        /// </param>
        /// <returns>
        /// A <see cref="System.Int32"/>0 in case of success, -1 in case of failure.
        /// </returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virStorageVolRef(IntPtr vol);

        #endregion

        #region Streams

        // TODO virStreamAbort

        // TODO virStreamEventAddCallback

        // TODO virStreamEventCallback

        // TODO virStreamEventRemoveCallback

        // TODO virStreamEventUpdateCallback

        // TODO virStreamFinish

        // TODO virStreamFree

        // TODO virStreamNew

        // TODO virStreamRecv

        // TODO virStreamRecvAll

        // TODO virStreamRef

        // TODO virStreamSend

        // TODO virStreamSendAll

        // TODO virStreamSinkFunc

        // TODO virStreamSourceFunc

        /// <summary>
        /// A callback function to be registered, and called when a domain event occurs
        /// </summary>
        /// <param name="conn">virConnect connection </param>
        /// <param name="dom">The domain on which the event occured</param>
        /// <param name="evt">The specfic virDomainEventType which occured</param>
        /// <param name="detail">event specific detail information</param>
        /// <param name="opaque">opaque user data</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void virConnectDomainEventCallback(IntPtr conn, IntPtr dom, virDomainEventType evt, int detail, IntPtr opaque);
        /// <summary>
        /// Adds a callback to receive notifications of domain lifecycle events occurring on a connection Use of this method is no longer recommended. Instead applications should try virConnectDomainEventRegisterAny which has a more flexible API contract The virDomainPtr object handle passed into the callback upon delivery of an event is only valid for the duration of execution of the callback. If the callback wishes to keep the domain object after the callback
        /// </summary>
        /// <param name="conn">pointer to the connection</param>
        /// <param name="cb">callback to the function handling domain events</param>
        /// <param name="opaque">opaque data to pass on to the callback</param>
        /// <param name="ff">optional function to deallocate opaque when not used anymore</param>
        /// <returns>t shall take a reference to it, by calling virDomainRef. The reference can be released once the object is no longer required by calling virDomainFree. Returns 0 on success, -1 on failure</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int virConnectDomainEventRegister(IntPtr conn, [MarshalAs(UnmanagedType.FunctionPtr)] virConnectDomainEventCallback cb,
                                                                IntPtr opaque, [MarshalAs(UnmanagedType.FunctionPtr)] virFreeCallback ff);

        #endregion

        #region Network

        /// <summary>
        /// Create and start a defined network. If the call succeed the network moves from the defined to the running networks pools.
        /// </summary>
        /// <param name="network">pointer to a defined network</param>
        /// <returns>0 in case of success, -1 in case of error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virNetworkCreate(IntPtr network);
        /// <summary>
        /// Create and start a new virtual network, based on an XML description similar to the one returned by virNetworkGetXMLDesc()
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="xmlDesc">an XML description of the network</param>
        /// <returns>a new network object or NULL in case of failure</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virNetworkCreateXML(IntPtr conn, string xmlDesc);
        /// <summary>
        /// Define a network, but does not create it
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="xmlDesc">the XML description for the network, preferably in UTF-8</param>
        /// <returns>NULL in case of error, a pointer to the network otherwise</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virNetworkDefineXML(IntPtr conn, string xmlDesc);
        /// <summary>
        /// Destroy the network object. The running instance is shutdown if not down already and all resources used by it are given back to the hypervisor. This does not free the associated virNetworkPtr object. This function may require privileged access
        /// </summary>
        /// <param name="network">a network object</param>
        /// <returns>0 in case of success and -1 in case of failure.</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virNetworkDestroy(IntPtr network);
        /// <summary>
        /// Free the network object. The running instance is kept alive. The data structure is freed and should not be used thereafter.
        /// </summary>
        /// <param name="network">a network object</param>
        /// <returns>0 in case of success and -1 in case of failure</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virNetworkFree(IntPtr network);
        /// <summary>
        /// Provides a boolean value indicating whether the network configured to be automatically started when the host machine boots.
        /// </summary>
        /// <param name="network">a network object</param>
        /// <param name="autostart">the value returned</param>
        /// <returns>-1 in case of error, 0 in case of success</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virNetworkGetAutostart(IntPtr network, int autostart);
        /// <summary>
        /// Provides a bridge interface name to which a domain may connect a network interface in order to join the network.
        /// </summary>
        /// <param name="network">a network object</param>
        /// <returns>a 0 terminated interface name, or NULL in case of error. the caller must free() the returned value</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virNetworkGetBridgeName(IntPtr network);
        /// <summary>
        /// Provides the connection pointer associated with a network. The reference counter on the connection is not increased by this call. WARNING: When writing libvirt bindings in other languages, do not use this function. Instead, store the connection and the network object together
        /// </summary>
        /// <param name="network">pointer to a network</param>
        /// <returns>the virConnectPtr or NULL in case of failure</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virNetworkGetConnect(IntPtr network);
        /// <summary>
        /// Get the public name for that network
        /// </summary>
        /// <param name="network">a network object</param>
        /// <returns>a pointer to the name or NULL, the string need not be deallocated its lifetime will be the same as the network object</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virNetworkGetName(IntPtr network);
        /// <summary>
        /// Get the UUID for a network as string
        /// </summary>
        /// <param name="network">a network object</param>
        /// <param name="uuid">string of the uuid</param>
        /// <returns>-1 in case of error, 0 in case of success</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        private static extern int virNetworkGetUUIDString(IntPtr network, [Out] char[] uuid);

        ///<summary>
        /// Get the UUID for a network as string
        ///</summary>
        ///<param name="network">a network object, a netowrk IntPtr</param>
        ///<returns>string of the uuid</returns>
        public static string virNetworkGetUUIDString(IntPtr network)
        {
            char[] uuidArray = new char[36];
            virNetworkGetUUIDString(network, uuidArray);
            return new string(uuidArray);
        }
        /// <summary>
        /// Provide an XML description of the network. The description may be reused later to relaunch the network with virNetworkCreateXML().
        /// </summary>
        /// <param name="network">a network object</param>
        /// <param name="flags">an OR'ed set of extraction flags, not used yet</param>
        /// <returns>a 0 terminated UTF-8 encoded XML instance, or NULL in case of error. the caller must free() the returned value</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string virNetworkGetXMLDesc(IntPtr network, int flags);
        /// <summary>
        /// Determine if the network is currently running
        /// </summary>
        /// <param name="network">pointer to the network object</param>
        /// <returns>1 if running, 0 if inactive, -1 on error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virNetworkIsActive(IntPtr network);
        /// <summary>
        /// Determine if the network has a persistent configuration which means it will still exist after shutting down
        /// </summary>
        /// <param name="network">pointer to the network object</param>
        /// <returns>x1 if persistent, 0 if transient, -1 on error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virNetworkIsPersistent(IntPtr network);
        /// <summary>
        /// Try to lookup a network on the given hypervisor based on its name.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="name">name for the network</param>
        /// <returns>a new network object or NULL in case of failure. If the network cannot be found, then VIR_ERR_NO_NETWORK error is raised</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virNetworkLookupByName(IntPtr conn, string name);
        /// <summary>
        /// Try to lookup a network on the given hypervisor based on its UUID
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="uuidstr">the string UUID for the network</param>
        /// <returns>a new network object or NULL in case of failure. If the network cannot be found, then VIR_ERR_NO_NETWORK error is raised</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr virNetworkLookupByUUIDString(IntPtr conn, string uuidstr);
        /// <summary>
        /// Configure the network to be automatically started when the host machine boots
        /// </summary>
        /// <param name="network">a network object</param>
        /// <param name="autostart">whether the network should be automatically started 0 or 1</param>
        /// <returns>-1 in case of error, 0 in case of success</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virNetworkSetAutostart(IntPtr network, int autostart);
        /// <summary>
        /// Undefine a network but does not stop it if it is running
        /// </summary>
        /// <param name="network">pointer to a defined network</param>
        /// <returns>0 in case of success, -1 in case of error</returns>
        [DllImport(LibvirtDllImportName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int virNetworkUndefine(IntPtr network);

        #endregion

        #region Helpers
        
        private static string[] ptrToStringArray(IntPtr stringPtr, int stringCount)
        {
            string[] members = new string[stringCount];
            for (int i = 0; i < stringCount; ++i)
            {
                IntPtr s = Marshal.ReadIntPtr(stringPtr, i * IntPtr.Size);
                members[i] = Marshal.PtrToStringAnsi(s);
            }
            return members;
        }

// ReSharper disable UnusedMember.Local
        private static string ptrToString(IntPtr stringPtr)
// ReSharper restore UnusedMember.Local
        {
            IntPtr s = Marshal.ReadIntPtr(stringPtr, IntPtr.Size);
            return Marshal.PtrToStringAnsi(s);
        }

        #endregion
    }
}
