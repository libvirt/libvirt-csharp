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

    #region delegates
    ///<summary>
    /// Callback for receiving file handle events. The callback will be invoked once for each event which is pending.
    ///</summary>
    ///<param name="watch">watch on which the event occurred</param>
    ///<param name="fd">file handle on which the event occurred</param>
    ///<param name="events">bitset of events from virEventHandleType constants</param>
    ///<param name="opaque">user data registered with handle</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void virEventHandleCallback(int watch, int fd, int events, IntPtr opaque);
    ///<summary>
    /// Free callbacks
    ///</summary>
    ///<param name="opaque">user data registered with handle</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void virFreeCallback(IntPtr opaque);
    ///<summary>
    /// Part of the EventImpl, this callback Adds a file handle callback to listen for specific events. The same file handle can be registered multiple times provided the requested event sets are non-overlapping If the opaque user data requires free'ing when the handle is unregistered, then a 2nd callback can be supplied for this purpose.
    ///</summary>
    ///<param name="fd">file descriptor to listen on</param>
    ///<param name="events">bitset of events on which to fire the callback</param>
    ///<param name="cb">the callback to be called when an event occurrs</param>
    ///<param name="opaque">user data to pass to the callback</param>
    ///<param name="ff">the callback invoked to free opaque data blob</param>
    ///<returns>a handle watch number to be used for updating and unregistering for events</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int virEventAddHandleFunc(int fd, int events, virEventHandleCallback cb, IntPtr opaque, virFreeCallback ff);
    ///<summary>
    /// Part of the EventImpl, this user-provided callback is notified when events to listen on change
    ///</summary>
    ///<param name="watch">file descriptor watch to modify</param>
    ///<param name="events">new events to listen on</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void virEventUpdateHandleFunc(int watch, int events);
    ///<summary>
    /// Part of the EventImpl, this user-provided callback is notified when an fd is no longer being listened on. If a virEventHandleFreeFunc was supplied when the handle was registered, it will be invoked some time during, or after this function call, when it is safe to release the user data.
    ///</summary>
    ///<param name="watch">file descriptor watch to stop listening on</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int virEventRemoveHandleFunc(int watch);
    ///<summary>
    /// callback for receiving timer events
    ///</summary>
    ///<param name="timer">timer id emitting the event</param>
    ///<param name="opaque">user data registered with handle</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void virEventTimeoutCallback(int timer, IntPtr opaque);
    ///<summary>
    /// Part of the EventImpl, this user-defined callback handles adding an event timeout. If the opaque user data requires free'ing when the handle is unregistered, then a 2nd callback can be supplied for this purpose.
    ///</summary>
    ///<param name="timeout">The timeout to monitor</param>
    ///<param name="cb">the callback to call when timeout has expired</param>
    ///<param name="opaque">user data to pass to the callback</param>
    ///<param name="ff">the callback invoked to free opaque data blob</param>
    /// <returns>A timer value</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int virEventAddTimeoutFunc(int timeout, virEventTimeoutCallback cb, IntPtr opaque, virFreeCallback ff);
    ///<summary>
    /// Part of the EventImpl, this user-defined callback updates an event timeout.
    ///</summary>
    ///<param name="timer">the timer to modify</param>
    ///<param name="timeout">the new timeout value</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void virEventUpdateTimeoutFunc(int timer, int timeout);
    ///<summary>
    /// Part of the EventImpl, this user-defined callback removes a timer If a virEventTimeoutFreeFunc was supplied when the handle was registered, it will be invoked some time during, or after this function call, when it is safe to release the user data.
    ///</summary>
    ///<param name="timer">the timer to remove</param>
    /// <returns>0 on success, -1 on failure</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int virEventRemoveTimeoutFunc(int timer);
    ///<summary>
    /// Authentication callback
    ///</summary>
    ///<param name="cred">Pointer to a virConnectCredential array</param>
    ///<param name="ncred">number of virConnectCredential in cred</param>
    ///<param name="cbdata">user data passed to callback</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int virConnectAuthCallbackPtr([In, Out] IntPtr cred, uint ncred, IntPtr cbdata);
    #endregion

    #region structs
    ///<summary>
    /// Blocks domain statistics
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct virDomainBlockStatsStruct
    {
        /// <summary>
        /// Number of read requests.
        /// </summary>
        public long rd_req;
        /// <summary>
        /// Number of read bytes.
        /// </summary>
        public long rd_bytes;
        /// <summary>
        /// Number of write requests.
        /// </summary>
        public long wr_req;
        /// <summary>
        /// Number of written bytes.
        /// </summary>
        public long wr_bytes;
        /// <summary>
        /// In Xen this returns the mysterious 'oo_req'.
        /// </summary>
        public long errs;
    }

    ///<summary>
    /// Domain interface statistics
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct virDomainInterfaceStatsStruct
    {
        ///<summary>
        /// Bytes received
        ///</summary>
        public long rx_bytes;
        /// <summary>
        /// Packets received
        /// </summary>
        public long rx_packets;
        /// <summary>
        /// Errors received
        /// </summary>
        public long rx_errs;
        /// <summary>
        /// Drops received
        /// </summary>
        public long rx_drop;
        /// <summary>
        /// Bytes sended
        /// </summary>
        public long tx_bytes;
        /// <summary>
        /// Packets sended
        /// </summary>
        public long tx_packets;
        /// <summary>
        /// Errors sended
        /// </summary>
        public long tx_errs;
        /// <summary>
        /// Drops sended
        /// </summary>
        public long tx_drop;
    }

    ///<summary>
    /// Structure to handle connection authentication
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct virConnectAuth
    {
        /// <summary>
        /// List of supported virConnectCredentialType values, should be a IntPtr to an int array or to a virConnectCredentialType array
        /// </summary>
        public IntPtr credtypes;
        ///<summary>
        /// Number of virConnectCredentialType in credtypes
        ///</summary>
        [MarshalAs(UnmanagedType.I4)]
        public int ncredtype;
        ///<summary>
        /// Callback used to collect credentials, a virConnectAuthCallback delegate in bindings
        ///</summary>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public virConnectAuthCallbackPtr cb;
        ///<summary>
        /// Data transported with callback, should be a IntPtr on what you want
        ///</summary>
        public IntPtr cbdata;
    }

    ///<summary>
    /// Credential structure
    ///</summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct virConnectCredential
    {
        ///<summary>
        /// One of virConnectCredentialType constants
        ///</summary>
        [MarshalAs(UnmanagedType.I4)]
        public virConnectCredentialType type;
        ///<summary>
        /// Prompt to show to user
        ///</summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string prompt;
        ///<summary>
        /// Additional challenge to show
        ///</summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string challenge;
        ///<summary>
        /// Optional default result
        ///</summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string defresult;
        ///<summary>
        /// Result to be filled with user response (or defresult). An IntPtr to a marshalled allocated string
        ///</summary>
        public IntPtr result;
        ///<summary>
        /// Length of the result
        ///</summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint resultlen;
    }

    ///<summary>
    /// Structure for domain memory statistics
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct virDomainMemoryStat
    {
        /// <summary>
        /// Tag
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public virDomainMemoryStatTags tag;
        /// <summary>
        /// Value
        /// </summary>
        [MarshalAs(UnmanagedType.I8)]
        public ulong val;
    }

    ///<summary>
    /// Structure to handle volume informations
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct virStorageVolInfo
    {
        /// <summary>
        /// virStorageVolType flags.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public virStorageVolType type;
        /// <summary>
        /// Logical size bytes.
        /// </summary>
        [MarshalAs(UnmanagedType.U8)]
        public ulong capacity;
        /// <summary>
        /// Current allocation bytes.
        /// </summary>
        [MarshalAs(UnmanagedType.U8)]
        public ulong allocation;
    }

    /// <summary>
    /// Structure to handle storage pool informations
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct virStoragePoolInfo
    {
        /// <summary>
        /// virStoragePoolState flags
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public virStoragePoolState state;
        /// <summary>
        /// Logical size bytes
        /// </summary>
        [MarshalAs(UnmanagedType.U8)]
        public ulong capacity;
        /// <summary>
        /// Current allocation bytes.
        /// </summary>
        [MarshalAs(UnmanagedType.U8)]
        public ulong allocation;
        /// <summary>
        /// Remaining free space bytes.
        /// </summary>
        [MarshalAs(UnmanagedType.U8)]
        public ulong available;
    }

    /// <summary>
    /// Structure to handle node informations
    /// </summary>
    public struct virNodeInfo
    {
        /// <summary>
        /// String indicating the CPU model.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string model;
        /// <summary>
        /// Memory size in kilobytes
        /// </summary>
        public ulong memory;
        /// <summary>
        /// The number of active CPUs.
        /// </summary>
        public uint cpus;
        /// <summary>
        /// Expected CPU frequency.
        /// </summary>
        public uint mhz;
        /// <summary>
        /// The number of NUMA cell, 1 for uniform mem access.
        /// </summary>
        public uint nodes;
        /// <summary>
        /// Number of CPU socket per node.
        /// </summary>
        public uint sockets;
        /// <summary>
        /// Number of core per socket.
        /// </summary>
        public uint cores;
        /// <summary>
        /// Number of threads per core.
        /// </summary>
        public uint threads;
    }
    /// <summary>
    /// Structure tu handle domain informations
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct virDomainInfo
    {
        /// <summary>
        /// The running state, one of virDomainState.
        /// </summary>
        public virDomainState state;
        /// <summary>
        /// The maximum memory in KBytes allowed.
        /// </summary>
        public ulong maxMem;
        /// <summary>
        /// The memory in KBytes used by the domain.
        /// </summary>
        public ulong memory;
        /// <summary>
        /// The number of virtual CPUs for the domain.
        /// </summary>
        public ushort nrVirtCpu;
        /// <summary>
        /// The CPU time used in nanoseconds.
        /// </summary>
        public ulong cpuTime;
    }
    #endregion

    #region enums
    ///<summary>
    /// Type of handles for callback
    ///</summary>
    [Flags]
    public enum virEventHandleType
    {
        /// <summary>
        /// None
        /// </summary>
        NONE = 0,
        /// <summary>
        /// Readable handle
        /// </summary>
        VIR_EVENT_HANDLE_READABLE = 1,
        /// <summary>
        /// Writable handle
        /// </summary>
        VIR_EVENT_HANDLE_WRITABLE = 2,
        /// <summary>
        /// Error handle
        /// </summary>
        VIR_EVENT_HANDLE_ERROR = 4,
        /// <summary>
        /// Hangup handle
        /// </summary>
        VIR_EVENT_HANDLE_HANGUP = 8
    }

    ///<summary>
    /// Memory statistics tags
    ///</summary>
    [Flags]
    public enum virDomainMemoryStatTags
    {
        /// <summary>
        ///  The total amount of memory written out to swap space (in kB).
        /// </summary>
        VIR_DOMAIN_MEMORY_STAT_SWAP_IN = 0,
        /// <summary>
        /// * Page faults occur when a process makes a valid access to virtual memory * that is not available. When servicing the page fault, if disk IO is * required, it is considered a major fault. If not, it is a minor fault. * These are expressed as the number of faults that have occurred. *
        /// </summary>
        VIR_DOMAIN_MEMORY_STAT_SWAP_OUT = 1,
#pragma warning disable 1591
        VIR_DOMAIN_MEMORY_STAT_MAJOR_FAULT = 2,
#pragma warning restore 1591
        /// <summary>
        /// * The amount of memory left completely unused by the system. Memory that * is available but used for reclaimable caches should NOT be reported as * free. This value is expressed in kB. *
        /// </summary>
        VIR_DOMAIN_MEMORY_STAT_MINOR_FAULT = 3,
        /// <summary>
        /// * The total amount of usable memory as seen by the domain. This value * may be less than the amount of memory assigned to the domain if a * balloon driver is in use or if the guest OS does not initialize all * assigned pages. This value is expressed in kB. *
        /// </summary>
        VIR_DOMAIN_MEMORY_STAT_UNUSED = 4,
        /// <summary>
        /// * The number of statistics supported by this version of the interface. * To add new statistics, add them to the enum and increase this value. *
        /// </summary>
        VIR_DOMAIN_MEMORY_STAT_AVAILABLE = 5,
#pragma warning disable 1591
        VIR_DOMAIN_MEMORY_STAT_NR = 6
#pragma warning restore 1591
    }

    ///<summary>
    /// Types of storage volume
    ///</summary>
    public enum virStorageVolType
    {
        /// <summary>
        /// Regular file based volumes.
        /// </summary>
        VIR_STORAGE_VOL_FILE = 0,
        /// <summary>
        /// Block based volumes.
        /// </summary>
        VIR_STORAGE_VOL_BLOCK = 1
    }

    /// <summary>
    /// States of storage pool
    /// </summary>
    public enum virStoragePoolState
    {
        /// <summary>
        /// Not running.
        /// </summary>
        VIR_STORAGE_POOL_INACTIVE = 0,
        /// <summary>
        /// Initializing pool, not available.
        /// </summary>
        VIR_STORAGE_POOL_BUILDING = 1,
        /// <summary>
        /// Running normally.
        /// </summary>
        VIR_STORAGE_POOL_RUNNING = 2,
        /// <summary>
        /// Running degraded.
        /// </summary>
        VIR_STORAGE_POOL_DEGRADED = 3,
    }
    /// <summary>
    /// Flasg for XML domain rendering
    /// </summary>
    [Flags]
    public enum virDomainXMLFlags
    {
        /// <summary>
        /// Dump security sensitive information too.
        /// </summary>
        VIR_DOMAIN_XML_SECURE = 1,
        /// <summary>
        /// Dump inactive domain information.
        /// </summary>
        VIR_DOMAIN_XML_INACTIVE = 2
    }

    /// <summary>
    /// Details on the caused of the 'defined' lifecycle event
    /// </summary>
    public enum virDomainEventDefinedDetailType
    {
        /// <summary>
        /// Newly created config file
        /// </summary>
        VIR_DOMAIN_EVENT_DEFINED_ADDED = 0,
        /// <summary>
        /// Changed config file
        /// </summary>
        VIR_DOMAIN_EVENT_DEFINED_UPDATED = 1
    }

    /// <summary>
    /// Details on the caused of the 'undefined' lifecycle event
    /// </summary>
    public enum virDomainEventUndefinedDetailType
    {
        /// <summary>
        /// Deleted the config file
        /// </summary>
        VIR_DOMAIN_EVENT_UNDEFINED_REMOVED = 0
    }
    /// <summary>
    /// Details on the caused of the 'started' lifecycle event
    /// </summary>
    public enum virDomainEventStartedDetailType
    {
        /// <summary>
        /// Normal startup from boot
        /// </summary>
        VIR_DOMAIN_EVENT_STARTED_BOOTED = 0,
        /// <summary>
        /// Incoming migration from another host
        /// </summary>
        VIR_DOMAIN_EVENT_STARTED_MIGRATED = 1,
        /// <summary>
        /// Restored from a state file
        /// </summary>
        VIR_DOMAIN_EVENT_STARTED_RESTORED = 2
    }
    /// <summary>
    /// Details on the caused of the 'suspended' lifecycle event
    /// </summary>
    public enum virDomainEventSuspendedDetailType
    {
        /// <summary>
        /// Normal suspend due to admin pause
        /// </summary>
        VIR_DOMAIN_EVENT_SUSPENDED_PAUSED = 0,
        /// <summary>
        /// Suspended for offline migration
        /// </summary>
        VIR_DOMAIN_EVENT_SUSPENDED_MIGRATED = 1
    }
    /// <summary>
    /// Details on the caused of the 'resumed' lifecycle event
    /// </summary>
    public enum virDomainEventResumedDetailType
    {
        /// <summary>
        /// Normal resume due to admin unpause
        /// </summary>
        VIR_DOMAIN_EVENT_RESUMED_UNPAUSED = 0,
        /// <summary>
        /// Resumed for completion of migration
        /// </summary>
        VIR_DOMAIN_EVENT_RESUMED_MIGRATED = 1
    }
    /// <summary>
    /// Details on the caused of the 'stopped' lifecycle event
    /// </summary>
    public enum virDomainEventStoppedDetailType
    {
        /// <summary>
        /// Normal shutdown
        /// </summary>
        VIR_DOMAIN_EVENT_STOPPED_SHUTDOWN = 0,
        /// <summary>
        /// Forced poweroff from host
        /// </summary>
        VIR_DOMAIN_EVENT_STOPPED_DESTROYED = 1,
        /// <summary>
        /// Guest crashed
        /// </summary>
        VIR_DOMAIN_EVENT_STOPPED_CRASHED = 2,
        /// <summary>
        /// Migrated off to another host
        /// </summary>
        VIR_DOMAIN_EVENT_STOPPED_MIGRATED = 3,
        /// <summary>
        /// Saved to a state file
        /// </summary>
        VIR_DOMAIN_EVENT_STOPPED_SAVED = 4,
        /// <summary>
        /// Host emulator/mgmt failed
        /// </summary>
        VIR_DOMAIN_EVENT_STOPPED_FAILED = 5
    }

    /// <summary>
    /// Types of domain events
    /// </summary>
    public enum virDomainEventType
    {
        /// <summary>
        /// Domain defined
        /// </summary>
        VIR_DOMAIN_EVENT_DEFINED = 0,
        /// <summary>
        /// Domain undefined
        /// </summary>
        VIR_DOMAIN_EVENT_UNDEFINED = 1,
        /// <summary>
        /// Domain started
        /// </summary>
        VIR_DOMAIN_EVENT_STARTED = 2,
        /// <summary>
        /// Domain suspended
        /// </summary>
        VIR_DOMAIN_EVENT_SUSPENDED = 3,
        /// <summary>
        /// Domain resumed
        /// </summary>
        VIR_DOMAIN_EVENT_RESUMED = 4,
        /// <summary>
        /// Domain stopped
        /// </summary>
        VIR_DOMAIN_EVENT_STOPPED = 5
    }

    /// <summary>
    /// Flags for storage pool building
    /// </summary>
    public enum virStoragePoolBuildFlags
    {
        /// <summary>
        /// Regular build from scratch.
        /// </summary>
        VIR_STORAGE_POOL_BUILD_NEW = 0,
        /// <summary>
        /// Repair / reinitialize.
        /// </summary>
        VIR_STORAGE_POOL_BUILD_REPAIR = 1,
        /// <summary>
        /// Extend existing pool.
        /// </summary>
        VIR_STORAGE_POOL_BUILD_RESIZE = 2
    }

    ///<summary>
    /// Flags for storage pool deletion
    ///</summary>
    public enum virStoragePoolDeleteFlags
    {
        /// <summary>
        /// Delete metadata only (fast).
        /// </summary>
        VIR_STORAGE_POOL_DELETE_NORMAL = 0,
        /// <summary>
        /// Clear all data to zeros (slow).
        /// </summary>
        VIR_STORAGE_POOL_DELETE_ZEROED = 1
    }
    ///<summary>
    /// Types of credentials
    ///</summary>
    public enum virConnectCredentialType
    {
        ///<summary>
        /// Identity to act as
        ///</summary>
        VIR_CRED_USERNAME = 1,
        ///<summary>
        /// Identify to authorize as
        ///</summary>
        VIR_CRED_AUTHNAME = 2,
        ///<summary>
        /// RFC 1766 languages, comma separated
        ///</summary>
        VIR_CRED_LANGUAGE = 3,
        ///<summary>
        /// client supplies a nonce
        ///</summary>
        VIR_CRED_CNONCE = 4,
        /// <summary>
        /// Passphrase secret
        /// </summary>
        VIR_CRED_PASSPHRASE = 5,
        /// <summary>
        /// Challenge response
        /// </summary>
        VIR_CRED_ECHOPROMPT = 6,
        /// <summary>
        /// Challenge response
        /// </summary>
        VIR_CRED_NOECHOPROMPT = 7,
        /// <summary>
        /// Authentication realm
        /// </summary>
        VIR_CRED_REALM = 8,
        /// <summary>
        /// Externally managed credential More may be added - expect the unexpected
        /// </summary>
        VIR_CRED_EXTERNAL = 9
    }

    /// <summary>
    /// States of a domain
    /// </summary>
    public enum virDomainState
    {
        /// <summary>
        /// No state.
        /// </summary>
        VIR_DOMAIN_NOSTATE = 0,
        /// <summary>
        /// The domain is running.
        /// </summary>
        VIR_DOMAIN_RUNNING = 1,
        /// <summary>
        /// The domain is blocked on resource.
        /// </summary>
        VIR_DOMAIN_BLOCKED = 2,
        /// <summary>
        /// The domain is paused by user.
        /// </summary>
        VIR_DOMAIN_PAUSED = 3,
        /// <summary>
        /// The domain is being shut down.
        /// </summary>
        VIR_DOMAIN_SHUTDOWN = 4,
        /// <summary>
        /// The domain is shut off.
        /// </summary>
        VIR_DOMAIN_SHUTOFF = 5,
        /// <summary>
        /// The domain is crashed.
        /// </summary>
        VIR_DOMAIN_CRASHED = 6
    }
    #endregion
}
