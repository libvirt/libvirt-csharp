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
    /// The Connect class expose all connection related methods
    /// </summary>
    public class Connect
    {
        private const int MaxStringLength = 1024;

        /// <summary>
        /// This function closes the connection to the Hypervisor. This should not be called if further interaction with the Hypervisor are needed especially if there is running domain which need further monitoring by the application.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>0 in case of success or -1 in case of error.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint="virConnectClose")]
        public static extern int Close(IntPtr conn);
        /// <summary>
        /// Provides capabilities of the hypervisor / driver.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>NULL in case of error, or an XML string defining the capabilities. The client must free the returned string after use.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectGetCapabilities")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetCapabilities(IntPtr conn);
        /// <summary>
        /// This returns the system hostname on which the hypervisor is running (the result of the gethostname system call). If we are connected to a remote system, then this returns the hostname of the remote system.
        /// </summary>
        /// <param name="conn">pointer to a hypervisor connection</param>
        /// <returns>the hostname which must be freed by the caller, or NULL if there was an error.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectGetHostname")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetHostname(IntPtr conn);
        /// <summary>
        /// Provides @libVer, which is the version of libvirt used by the daemon running on the @conn host
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="libVer">returns the libvirt library version used on the connection (OUT)</param>
        /// <returns>-1 in case of failure, 0 otherwise, and values for @libVer have the format major * 1,000,000 + minor * 1,000 + release.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectGetLibVersion")]
        public static extern int GetLibVersion(IntPtr conn, ref ulong libVer);
        /// <summary>
        /// Provides the maximum number of virtual CPUs supported for a guest VM of a specific type. The 'type' parameter here corresponds to the 'type' attribute in the domain element of the XML.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="type">value of the 'type' attribute in the domain element</param>
        /// <returns>the maximum of virtual CPU or -1 in case of error.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectGetMaxVcpus")]
        public static extern int GetMaxVcpus(IntPtr conn, string type);
        /// <summary>
        /// Get the name of the Hypervisor software used.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>NULL in case of error, a static zero terminated string otherwise. See also: http://www.redhat.com/archives/libvir-list/2007-February/msg00096.html</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectGetType")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetType(IntPtr conn);
        /// <summary>
        /// This returns the Uri (name) of the hypervisor connection. Normally this is the same as or similar to the string passed to the virConnectOpen/virConnectOpenReadOnly call, but the driver may make the Uri canonical. If name == NULL was passed to virConnectOpen, then the driver will return a non-NULL Uri which can be used to connect to the same hypervisor later.
        /// </summary>
        /// <param name="conn">pointer to a hypervisor connection</param>
        /// <returns>the Uri string which must be freed by the caller, or NULL if there was an error.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectGetURI")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetURI(IntPtr conn);
        /// <summary>
        /// Get the version level of the Hypervisor running. This may work only with hypervisor call, i.e. with privileged access to the hypervisor, not with a Read-Only connection.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="hvVer">return value for the version of the running hypervisor (OUT)</param>
        /// <returns>-1 in case of error, 0 otherwise. if the version can't be extracted by lack of capacities returns 0 and @hvVer is 0, otherwise @hvVer value is major * 1,000,000 + minor * 1,000 + release</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectGetVersion")]
        public static extern int GetVersion(IntPtr conn, ref ulong hvVer);
        /// <summary>
        /// Determine if the connection to the hypervisor is encrypted
        /// </summary>
        /// <param name="conn">pointer to the connection object</param>
        /// <returns>1 if encrypted, 0 if not encrypted, -1 on error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectIsEncrypted")]
        public static extern int IsEncrypted(IntPtr conn);
        /// <summary>
        /// Determine if the connection to the hypervisor is secure A connection will be classed as secure if it is either encrypted, or running over a channel which is not exposed to eavesdropping (eg a UNIX domain socket, or pipe)
        /// </summary>
        /// <param name="conn">pointer to the connection object</param>
        /// <returns>1 if secure, 0 if secure, -1 on error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectIsSecure")]
        public static extern int IsSecure(IntPtr conn);
        /// <summary>
        /// list the defined but inactive domains, stores the pointers to the names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">pointer to an array to store the names</param>
        /// <param name="maxnames">size of the array</param>
        /// <returns>the number of names provided in the array or -1 in case of error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectListDefinedDomains")]
        private static extern int ListDefinedDomains(IntPtr conn, IntPtr names, int maxnames);
        /// <summary>
        /// list the defined but inactive domains, stores the pointers to the names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">pointer to an array to store the names</param>
        /// <param name="maxnames">size of the array</param>
        /// <returns>the number of names provided in the array or -1 in case of error</returns>
        public static int ListDefinedDomains(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = ListDefinedDomains(conn, namesPtr, maxnames);
            if (count > 0)
                names = MarshalHelper.ptrToStringArray(namesPtr, count);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectListDefinedInterfaces")]
        private static extern int ListDefinedInterfaces(IntPtr conn, IntPtr names, int maxnames);
        ///<summary>
        /// Collect the list of defined (inactive) physical host interfaces, and store their names in @names.
        ///</summary>
        ///<param name="conn">pointer to the hypervisor connection</param>
        ///<param name="names">array to collect the list of names of interfaces</param>
        ///<param name="maxnames">size of @names</param>
        ///<returns>the number of interfaces found or -1 in case of error </returns>
        public static int ListDefinedInterfaces(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = ListDefinedInterfaces(conn, namesPtr, maxnames);
            if (count > 0)
                names = MarshalHelper.ptrToStringArray(namesPtr, count);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectListDefinedNetworks")]
        private static extern int ListDefinedNetworks(IntPtr conn, IntPtr names, int maxnames);
        /// <summary>
        /// list the inactive networks, stores the pointers to the names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">pointer to an array to store the names</param>
        /// <param name="maxnames">size of the array</param>
        /// <returns>the number of names provided in the array or -1 in case of error</returns>
        public static int ListDefinedNetworks(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = ListDefinedNetworks(conn, namesPtr, maxnames);
            if (count > 0)
                names = MarshalHelper.ptrToStringArray(namesPtr, count);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectListDefinedStoragePools")]
        private static extern int ListDefinedStoragePools(IntPtr conn, IntPtr names, int maxnames);
        /// <summary>
        /// Provides the list of names of inactive storage pools upto maxnames. If there are more than maxnames, the remaining names will be silently ignored.
        /// </summary>
        /// <param name="conn">pointer to hypervisor connection</param>
        /// <param name="names">array of char * to fill with pool names (allocated by caller)</param>
        /// <param name="maxnames">size of the names array</param>
        /// <returns>0 on success, -1 on error</returns>
        public static int ListDefinedStoragePools(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = ListDefinedStoragePools(conn, namesPtr, maxnames);
            if (count > 0)
                names = MarshalHelper.ptrToStringArray(namesPtr, count);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectListDomains")]
        public static extern int ListDomains(IntPtr conn, int[] ids, int maxids);
        /// <summary>
        /// Collect the list of active physical host interfaces, and store their names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">array to collect the list of names of interfaces</param>
        /// <param name="maxnames">size of @names</param>
        /// <returns>the number of interfaces found or -1 in case of error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectListInterfaces")]
        private static extern int ListInterfaces(IntPtr conn, IntPtr names, int maxnames);
        /// <summary>
        /// Collect the list of active physical host interfaces, and store their names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">array to collect the list of names of interfaces</param>
        /// <param name="maxnames">size of @names</param>
        /// <returns>the number of interfaces found or -1 in case of error</returns>
        public static int ListInterfaces(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = ListInterfaces(conn, namesPtr, maxnames);
            if (count > 0)
                names = MarshalHelper.ptrToStringArray(namesPtr, count);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectListNetworks")]
        private static extern int ListNetworks(IntPtr conn, IntPtr names, int maxnames);
        /// <summary>
        /// Collect the list of active networks, and store their names in @names
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <param name="names">array to collect the list of names of active networks</param>
        /// <param name="maxnames">size of @names</param>
        /// <returns>the number of networks found or -1 in case of error</returns>
        public static int ListNetworks(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = ListNetworks(conn, namesPtr, maxnames);
            if (count > 0)
                names = MarshalHelper.ptrToStringArray(namesPtr, count);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectListSecrets")]
        private static extern int ListSecrets(IntPtr conn, IntPtr uuids, int maxuuids);
        /// <summary>
        /// List UUIDs of defined secrets, store pointers to names in uuids.
        /// </summary>
        /// <param name="conn">virConnect connection</param>
        /// <param name="uuids">Pointer to an array to store the UUIDs</param>
        /// <param name="maxuuids">size of the array.</param>
        /// <returns>the number of UUIDs provided in the array, or -1 on failure.</returns>
        public static int ListSecrets(IntPtr conn, ref string[] uuids, int maxuuids)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = ListSecrets(conn, namesPtr, maxuuids);
            if (count > 0)
                uuids = MarshalHelper.ptrToStringArray(namesPtr, count);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectListStoragePools")]
        private static extern int ListStoragePools(IntPtr conn, IntPtr names, int maxnames);
        /// <summary>
        /// Provides the list of names of active storage pools upto maxnames. If there are more than maxnames, the remaining names will be silently ignored.
        /// </summary>
        /// <param name="conn">pointer to hypervisor connection</param>
        /// <param name="names">array of char * to fill with pool names (allocated by caller)</param>
        /// <param name="maxnames">size of the names array</param>
        /// <returns>0 on success, -1 on error</returns>
        public static int ListStoragePools(IntPtr conn, ref string[] names, int maxnames)
        {
            IntPtr namesPtr = Marshal.AllocHGlobal(MaxStringLength);
            int count = ListStoragePools(conn, namesPtr, maxnames);
            if (count > 0)
                names = MarshalHelper.ptrToStringArray(namesPtr, count);
            Marshal.FreeHGlobal(namesPtr);
            return count;
        }
        /// <summary>
        /// Provides the number of defined but inactive domains.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>the number of domain found or -1 in case of error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectNumOfDefinedDomains")]
        public static extern int NumOfDefinedDomains(IntPtr conn);
        /// <summary>
        /// Provides the number of defined (inactive) interfaces on the physical host.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>the number of defined interface found or -1 in case of error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectNumOfDefinedInterfaces")]
        public static extern int NumOfDefinedInterfaces(IntPtr conn);
        /// <summary>
        /// Provides the number of inactive networks.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>the number of networks found or -1 in case of error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectNumOfDefinedNetworks")]
        public static extern int NumOfDefinedNetworks(IntPtr conn);
        /// <summary>
        /// Provides the number of inactive storage pools
        /// </summary>
        /// <param name="conn">pointer to hypervisor connection</param>
        /// <returns>the number of pools found, or -1 on error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectNumOfDefinedStoragePools")]
        public static extern int NumOfDefinedStoragePools(IntPtr conn);
        /// <summary>
        /// Provides the number of active domains.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>the number of domain found or -1 in case of error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectNumOfDomains")]
        public static extern int NumOfDomains(IntPtr conn);
        /// <summary>
        /// Provides the number of active interfaces on the physical host.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>the number of active interfaces found or -1 in case of error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectNumOfInterfaces")]
        public static extern int NumOfInterfaces(IntPtr conn);
        /// <summary>
        /// Provides the number of active networks.
        /// </summary>
        /// <param name="conn">pointer to the hypervisor connection</param>
        /// <returns>the number of network found or -1 in case of error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectNumOfNetworks")]
        public static extern int NumOfNetworks(IntPtr conn);
        /// <summary>
        /// Fetch number of currently defined secrets.
        /// </summary>
        /// <param name="conn">virConnect connection</param>
        /// <returns>the number currently defined secrets.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectNumOfSecrets")]
        public static extern int NumOfSecrets(IntPtr conn);
        /// <summary>
        /// Provides the number of active storage pools
        /// </summary>
        /// <param name="conn">pointer to hypervisor connection</param>
        /// <returns>the number of pools found, or -1 on error</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectNumOfStoragePools")]
        public static extern int NumOfStoragePools(IntPtr conn);
        /// <summary>
        /// This function should be called first to get a connection to the Hypervisor and xen store
        /// </summary>
        /// <param name="name">Uri of the hypervisor</param>
        /// <returns>pointer to the connection</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectOpen")]
        public static extern IntPtr Open(string name);

        ///<summary>
        /// This function should be called first to get a connection to the Hypervisor. If necessary, authentication will be performed fetching credentials via the callback See virConnectOpen for notes about environment variables which can have an effect on opening drivers
        ///</summary>
        ///<param name="name">URI of the hypervisor</param>
        ///<param name="auth">Authenticate callback parameters</param>
        ///<param name="flags">Open flags</param>
        ///<returns>a pointer to the hypervisor connection or NULL in case of error URIs are documented at http://libvirt.org/uri.html </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectOpenAuth")]
        private static extern IntPtr OpenAuth(string name, ref ConnectAuthUnmanaged auth, int flags);
        /// <summary>
        /// This function should be called first to get a connection to the Hypervisor. If necessary, authentication will be performed fetching credentials via the callback See virConnectOpen for notes about environment variables which can have an effect on opening drivers
        /// </summary>
        /// <param name="name">URI of the hypervisor</param>
        /// <param name="auth">Authenticate callback parameters</param>
        /// <param name="flags">Open flags</param>
        /// <returns>a pointer to the hypervisor connection or NULL in case of error URIs are documented at http://libvirt.org/uri.html </returns>
        public static IntPtr OpenAuth(string name, ref ConnectAuth auth, int flags)
        {
            // Create a structure that hold cbdata and the callback target
            OpenAuthManagedCB cbAndUserData = new OpenAuthManagedCB();
            cbAndUserData.cbdata = auth.cbdata;
            cbAndUserData.cbManaged = auth.cb;
            // Pass the structure as cbdata
            IntPtr cbAndUserDataPtr = Marshal.AllocHGlobal(Marshal.SizeOf(cbAndUserData));
            Marshal.StructureToPtr(cbAndUserData, cbAndUserDataPtr, true);

            // Create the real ConnectAuth structure, it will call OpenAuthCallbackFromUnmanaged via callback
            ConnectAuthUnmanaged connectAuth = new ConnectAuthUnmanaged();
            connectAuth.cbdata = cbAndUserDataPtr;
            connectAuth.cb = OpenAuthCallbackFromUnmanaged;
            connectAuth.CredTypes = auth.CredTypes;

            return OpenAuth(name, ref connectAuth, flags);
        }

        private static int OpenAuthCallbackFromUnmanaged(IntPtr creds, uint ncreds, IntPtr cbdata)
        {
            // Give back the structure that hold cbdata and the callback target
            OpenAuthManagedCB cbAndUserData = (OpenAuthManagedCB)Marshal.PtrToStructure(cbdata, typeof(OpenAuthManagedCB));
            int offset = 0;
            int credIndex = 0;

            ConnectCredential[] cc = new ConnectCredential[ncreds];
            // Loop thru credentials and initialize the ConnectCredential array
            while (credIndex < ncreds)
            {
                IntPtr currentCred = MarshalHelper.IntPtrOffset(creds, offset);
                ConnectCredential cred = (ConnectCredential)Marshal.PtrToStructure(currentCred, typeof(ConnectCredential));
                offset += Marshal.SizeOf(cred);
                cc[credIndex] = cred;
                credIndex++;
            }
            // Call the delegate with the ConnectCredential array, this allow the user to answer the result
            cbAndUserData.cbManaged(ref cc, cbAndUserData.cbdata);

            offset = 0;
            credIndex = 0;
            // Loop thru ConnectCredential array and copy back to unmanaged memory
            while (credIndex < ncreds)
            {
                IntPtr currentCred = MarshalHelper.IntPtrOffset(creds, offset);
                Marshal.StructureToPtr(cc[credIndex], currentCred, true);
                offset += Marshal.SizeOf(cc[credIndex]);
                credIndex++;
            }

            return 0;
        }
        /// <summary>
        /// This function should be called first to get a restricted connection to the library functionalities. The set of APIs usable are then restricted on the available methods to control the domains. See virConnectOpen for notes about environment variables which can have an effect on opening drivers
        /// </summary>
        /// <param name="name">Uri of the hypervisor</param>
        /// <returns>a pointer to the hypervisor connection or NULL in case of error URIs are documented at http://libvirt.org/uri.html </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectOpenReadOnly")]
        public static extern IntPtr OpenReadOnly(string name);
        /// <summary>
        /// Increment the reference count on the connection. For each additional call to this method, there shall be a corresponding call to virConnectClose to release the reference count, once the caller no longer needs the reference to this object. This method is typically useful for applications where multiple threads are using a connection, and it is required that the connection remain open until all threads have finished using it. ie, each new thread using a connection would increment the reference count.
        /// </summary>
        /// <param name="conn">the connection to hold a reference on</param>
        /// <returns>0 in case of success, -1 in case of failure</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectRef")]
        public static extern int Ref(IntPtr conn);
        /// <summary>
        /// Adds a callback to receive notifications of domain lifecycle events occurring on a connection Use of this method is no longer recommended. Instead applications should try virConnectDomainEventRegisterAny which has a more flexible API contract The virDomainPtr object handle passed into the callback upon delivery of an event is only valid for the duration of execution of the callback. If the callback wishes to keep the domain object after the callback
        /// </summary>
        /// <param name="conn">pointer to the connection</param>
        /// <param name="cb">callback to the function handling domain events</param>
        /// <param name="opaque">opaque data to pass on to the callback</param>
        /// <param name="ff">optional function to deallocate opaque when not used anymore</param>
        /// <returns>t shall take a reference to it, by calling virDomainRef. The reference can be released once the object is no longer required by calling virDomainFree. Returns 0 on success, -1 on failure</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnectDomainEventRegister")]
        public static extern int DomainEventRegister(IntPtr conn, [MarshalAs(UnmanagedType.FunctionPtr)] ConnectDomainEventCallback cb,
                                                                IntPtr opaque, [MarshalAs(UnmanagedType.FunctionPtr)] FreeCallback ff);
    }
}
