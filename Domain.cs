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
    /// The Domain class expose the domains related methods
    /// </summary>
    public class Domain
    {
        /// <summary>
        /// /// Create a virtual device attachment to backend. This function, having hotplug semantics, is only allowed on an active domain.
        /// </summary>
        /// <param name="domain">pointer to domain object</param>
        /// <param name="xml">pointer to XML description of one device</param>
        /// <returns>0 in case of success, -1 in case of failure.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainAttachDevice")]
        public static extern int AttachDevice(IntPtr domain, string xml);
        /// <summary>
        /// Attach a virtual device to a domain, using the flags parameter to control how the device is attached. VIR_DOMAIN_DEVICE_MODIFY_CURRENT specifies that the device allocation is made based on current domain state. VIR_DOMAIN_DEVICE_MODIFY_LIVE specifies that the device shall be allocated to the active domain instance only and is not added to the persisted domain configuration. VIR_DOMAIN_DEVICE_MODIFY_CONFIG specifies that the device shall be allocated to the persisted domain configuration only. Note that the target hypervisor must return an error if unable to satisfy flags. E.g. the hypervisor driver will return failure if LIVE is specified but it only supports modifying the persisted device allocation.
        /// </summary>
        /// <param name="domain">pointer to domain object</param>
        /// <param name="xml">pointer to XML description of one device</param>
        /// <param name="flags">an OR'ed set of virDomainDeviceModifyFlags</param>
        /// <returns>0 in case of success, -1 in case of failure.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainAttachDeviceFlags")]
        public static extern int AttachDeviceFlags(IntPtr domain, string xml, uint flags);

        // TODO virDomainBlockPeek

        /// <summary>
        /// This function returns block device (disk) stats for block devices attached to the domain. The path parameter is the name of the block device. Get this by calling virDomainGetXMLDesc and finding the target dev='...' attribute within //domain/devices/disk. (For example, "xvda"). Domains may have more than one block device. To get stats for each you should make multiple calls to this function. Individual fields within the stats structure may be returned as -1, which indicates that the hypervisor does not support that particular statistic.
        /// </summary>
        /// <param name="dom">pointer to the domain object</param>
        /// <param name="path">path to the block device</param>
        /// <param name="stats">block device stats (returned)</param>
        /// <param name="size">size of stats structure</param>
        /// <returns>0 in case of success or -1 in case of failure.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainBlockStats")]
        private static extern int BlockStats(IntPtr dom, string path, IntPtr stats, int size);
        /// <summary>
        /// This function returns block device (disk) stats for block devices attached to the domain. The path parameter is the name of the block device. Get this by calling virDomainGetXMLDesc and finding the target dev='...' attribute within //domain/devices/disk. (For example, "xvda"). Domains may have more than one block device. To get stats for each you should make multiple calls to this function. Individual fields within the stats structure may be returned as -1, which indicates that the hypervisor does not support that particular statistic.
        /// </summary>
        /// <param name="dom">pointer to the domain object</param>
        /// <param name="path">path to the block device</param>
        /// <param name="stats">block device stats (returned)</param>
        /// <returns>0 in case of success or -1 in case of failure.</returns>
        public static int BlockStats(IntPtr dom, string path, out DomainBlockStatsStruct stats)
        {
            DomainBlockStatsStruct statStruct = new DomainBlockStatsStruct();
            IntPtr statStructPtr = Marshal.AllocHGlobal(Marshal.SizeOf(statStruct));
            Marshal.StructureToPtr(statStruct, statStructPtr, true);
            int result = BlockStats(dom, path, statStructPtr, Marshal.SizeOf(statStruct));
            Marshal.PtrToStructure(statStructPtr, statStruct);
            stats = statStruct;
            Marshal.FreeHGlobal(statStructPtr);
            return result;
        }
        /// <summary>
        /// This method will dump the core of a domain on a given file for analysis. Note that for remote Xen Daemon the file path will be interpreted in the remote host.
        /// </summary>
        /// <param name="domain">a domain object</param>
        /// <param name="to">path for the core file</param>
        /// <param name="flags">extra flags, currently unused</param>
        /// <returns>0 in case of success and -1 in case of failure.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainCoreDump")]
        public static extern int CoreDump(IntPtr domain, string to, int flags);
        /// <summary>
        /// Launch a defined domain. If the call succeed the domain moves from the defined to the running domains pools.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/> pointer to a defined domain.
        /// </param>
        /// <returns>
        /// 0 in case of success, -1 in case of error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainCreate")]
        public static extern int Create(IntPtr domain);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainCreateXML")]
        public static extern IntPtr CreateXML(IntPtr conn, string xmlDesc, uint flags);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainDefineXML")]
        public static extern IntPtr DefineXML(IntPtr conn, string xml);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainDestroy")]
        public static extern int Destroy(IntPtr domain);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainDetachDevice")]
        public static extern int DetachDevice(IntPtr domain, string xml);
        /// <summary>
        /// Detach a virtual device from a domain, using the flags parameter to control how the device is detached. VIR_DOMAIN_DEVICE_MODIFY_CURRENT specifies that the device allocation is removed based on current domain state. VIR_DOMAIN_DEVICE_MODIFY_LIVE specifies that the device shall be deallocated from the active domain instance only and is not from the persisted domain configuration. VIR_DOMAIN_DEVICE_MODIFY_CONFIG specifies that the device shall be deallocated from the persisted domain configuration only. Note that the target hypervisor must return an error if unable to satisfy flags. E.g. the hypervisor driver will return failure if LIVE is specified but it only supports removing the persisted device allocation.
        /// </summary>
        /// <param name="domain">pointer to domain object</param>
        /// <param name="xml">pointer to XML description of one device</param>
        /// <param name="flags">an OR'ed set of virDomainDeviceModifyFlags</param>
        /// <returns>0 in case of success, -1 in case of failure.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainDetachDeviceFlags")]
        public static extern int DetachDeviceFlags(IntPtr domain, string xml, uint flags);
        /// <summary>
        /// Free the domain object. The running instance is kept alive. The data structure is freed and should not be used thereafter.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <returns>
        /// 0 in case of success and -1 in case of failure.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainFree")]
        public static extern int Free(IntPtr domain);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetAutostart")]
        public static extern int GetAutostart(IntPtr domain, out int autostart);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetConnect")]
        public static extern IntPtr GetConnect(IntPtr dom);
        /// <summary>
        /// Get the hypervisor ID number for the domain.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <returns>
        /// The domain ID number or (unsigned int) -1 in case of error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetID")]
        public static extern int GetID(IntPtr domain);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetInfo")]
        public static extern int GetInfo(IntPtr domain, [Out] DomainInfo info);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetMaxMemory")]
        public static extern ulong GetMaxMemory(IntPtr domain);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetMaxVcpus")]
        public static extern int GetMaxVcpus(IntPtr domain);
        /// <summary>
        /// Get the public name for that domain.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <returns>
        /// Pointer to the name or NULL, the string need not be deallocated its lifetime will be the same as the domain object.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetName")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetName(IntPtr domain);
        /// <summary>
        /// Get the type of domain operation system.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>domain object.
        /// </param>
        /// <returns>
        /// The new string or NULL in case of error, the string must be freed by the caller.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetOSType")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetOSType(IntPtr domain);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetUUID")]
        public static extern int GetUUID(IntPtr domain, [Out] char[] uuid);
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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetUUIDString")]
        public static extern int GetUUIDString(IntPtr domain, [Out] IntPtr buf);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainGetXMLDesc")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetXMLDesc(IntPtr domain, int flags);
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
        /// A <see cref="DomainInterfaceStatsStruct"/>network interface stats (returned).
        /// </param>
        /// <param name="size">
        /// Size of stats structure.
        /// </param>
        /// <returns>
        /// 0 in case of success or -1 in case of failure.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainInterfaceStats")]
        private static extern int InterfaceStats(IntPtr dom, string path, IntPtr stats, int size);
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
        /// A <see cref="DomainInterfaceStatsStruct"/>network interface stats (returned).
        /// </param>
        /// <returns>0 in case of success or -1 in case of failure.</returns>
        public static int InterfaceStats(IntPtr dom, string path, out DomainInterfaceStatsStruct stats)
        {
            DomainInterfaceStatsStruct statStruct = new DomainInterfaceStatsStruct();
            IntPtr statStructPtr = Marshal.AllocHGlobal(Marshal.SizeOf(statStruct));
            Marshal.StructureToPtr(statStruct, statStructPtr, true);
            int result = InterfaceStats(dom, path, statStructPtr, Marshal.SizeOf(statStruct));
            Marshal.PtrToStructure(statStructPtr, statStruct);
            stats = statStruct;
            Marshal.FreeHGlobal(statStructPtr);
            return result;
        }
        /// <summary>
        /// Determine if the domain is currently running.
        /// </summary>
        /// <param name="dom">
        /// A <see cref="IntPtr"/>pointer to the domain object.
        /// </param>
        /// <returns>
        /// 1 if running, 0 if inactive, -1 on error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainIsActive")]
        public static extern int IsActive(IntPtr dom);

        /// <summary>
        /// Determine if the domain has a persistent configuration which means it will still exist after shutting down.
        /// </summary>
        /// <param name="dom">
        /// A <see cref="IntPtr"/>pointer to the domain object.
        /// </param>
        /// <returns>
        /// 1 if persistent, 0 if transient, -1 on error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainIsPersistent")]
        public static extern int IsPersistent(IntPtr dom);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainLookupByID")]
        public static extern IntPtr LookupByID(IntPtr conn, int id);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainLookupByName")]
        public static extern IntPtr LookupByName(IntPtr conn, string name);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainLookupByUUID")]
        public static extern IntPtr LookupByUUID(IntPtr conn, char[] uuid);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainLookupByUUIDString")]
        public static extern IntPtr LookupByUUIDString(IntPtr conn, string uuidstr);

        // TODO virDomainMemoryPeek

        // TODO virDomainMigrate

        // TODO virDomainMigrateToURI

        // TODO virDomainPinVcpu

        // TODO virDomainMemoryStats : Currently disabled in QEMU

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainReboot")]
        public static extern int Reboot(IntPtr domain, uint flags);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainRef")]
        public static extern int Ref(IntPtr domain);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainRestore")]
        public static extern int Restore(IntPtr conn, string from);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainResume")]
        public static extern int Resume(IntPtr domain);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainSave")]
        public static extern int Save(IntPtr domain, string to);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainSetAutostart")]
        public static extern int SetAutostart(IntPtr domain, int autostart);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainSetMaxMemory")]
        public static extern int SetMaxMemory(IntPtr domain, ulong memory);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainSetMemory")]
        public static extern int SetMemory(IntPtr domain, ulong memory);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainSetVcpus")]
        public static extern int SetVcpus(IntPtr domain, uint nvcpus);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainShutdown")]
        public static extern int Shutdown(IntPtr domain);

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
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainSuspend")]
        public static extern int Suspend(IntPtr domain);

        /// <summary>
        /// Undefine a domain but does not stop it if it is running.
        /// </summary>
        /// <param name="domain">
        /// A <see cref="IntPtr"/>pointer to a defined domain.
        /// </param>
        /// <returns>
        /// 0 in case of success, -1 in case of error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDomainUndefine")]
        public static extern int Undefine(IntPtr domain);

    }
}
