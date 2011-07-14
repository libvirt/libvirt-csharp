/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 */

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Libvirt
{

    #region delegates
    ///<summary>
    /// Signature of a function to use when there is an error raised by the library.
    ///</summary>
    ///<param name="userData">user provided data for the error callback</param>
    ///<param name="error">the error being raised.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ErrorFunc(IntPtr userData, Error error);
    /// <summary>
    /// A callback function to be registered, and called when a domain event occurs
    /// </summary>
    /// <param name="conn">virConnect connection </param>
    /// <param name="dom">The domain on which the event occured</param>
    /// <param name="evt">The specfic virDomainEventType which occured</param>
    /// <param name="detail">event specific detail information</param>
    /// <param name="opaque">opaque user data</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ConnectDomainEventCallback(IntPtr conn, IntPtr dom, [MarshalAs(UnmanagedType.I4)] DomainEventType evt, int detail, IntPtr opaque);
    ///<summary>
    /// Callback for receiving file handle events. The callback will be invoked once for each event which is pending.
    ///</summary>
    ///<param name="watch">watch on which the event occurred</param>
    ///<param name="fd">file handle on which the event occurred</param>
    ///<param name="events">bitset of events from virEventHandleType constants</param>
    ///<param name="opaque">user data registered with handle</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void EventHandleCallback(int watch, int fd, int events, IntPtr opaque);
    ///<summary>
    /// Free callbacks
    ///</summary>
    ///<param name="opaque">user data registered with handle</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FreeCallback(IntPtr opaque);
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
    public delegate int EventAddHandleFunc(int fd, int events, [MarshalAs(UnmanagedType.FunctionPtr)] EventHandleCallback cb, IntPtr opaque, [MarshalAs(UnmanagedType.FunctionPtr)] FreeCallback ff);
    ///<summary>
    /// Part of the EventImpl, this user-provided callback is notified when events to listen on change
    ///</summary>
    ///<param name="watch">file descriptor watch to modify</param>
    ///<param name="events">new events to listen on</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void EventUpdateHandleFunc(int watch, int events);
    ///<summary>
    /// Part of the EventImpl, this user-provided callback is notified when an fd is no longer being listened on. If a virEventHandleFreeFunc was supplied when the handle was registered, it will be invoked some time during, or after this function call, when it is safe to release the user data.
    ///</summary>
    ///<param name="watch">file descriptor watch to stop listening on</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int EventRemoveHandleFunc(int watch);
    ///<summary>
    /// callback for receiving timer events
    ///</summary>
    ///<param name="timer">timer id emitting the event</param>
    ///<param name="opaque">user data registered with handle</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void EventTimeoutCallback(int timer, IntPtr opaque);
    ///<summary>
    /// Part of the EventImpl, this user-defined callback handles adding an event timeout. If the opaque user data requires free'ing when the handle is unregistered, then a 2nd callback can be supplied for this purpose.
    ///</summary>
    ///<param name="timeout">The timeout to monitor</param>
    ///<param name="cb">the callback to call when timeout has expired</param>
    ///<param name="opaque">user data to pass to the callback</param>
    ///<param name="ff">the callback invoked to free opaque data blob</param>
    /// <returns>A timer value</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int EventAddTimeoutFunc(int timeout, [MarshalAs(UnmanagedType.FunctionPtr)] EventTimeoutCallback cb, IntPtr opaque, [MarshalAs(UnmanagedType.FunctionPtr)] FreeCallback ff);
    ///<summary>
    /// Part of the EventImpl, this user-defined callback updates an event timeout.
    ///</summary>
    ///<param name="timer">the timer to modify</param>
    ///<param name="timeout">the new timeout value</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void EventUpdateTimeoutFunc(int timer, int timeout);
    ///<summary>
    /// Part of the EventImpl, this user-defined callback removes a timer If a virEventTimeoutFreeFunc was supplied when the handle was registered, it will be invoked some time during, or after this function call, when it is safe to release the user data.
    ///</summary>
    ///<param name="timer">the timer to remove</param>
    /// <returns>0 on success, -1 on failure</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int EventRemoveTimeoutFunc(int timer);
    ///<summary>
    /// Authentication callback
    ///</summary>
    ///<param name="creds">virConnectCredential array</param>
    ///<param name="ncred">number of virConnectCredential in cred</param>
    ///<param name="cbdata">user data passed to callback</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int ConnectAuthCallbackUnmanaged(IntPtr creds, uint ncred, IntPtr cbdata);
    /// <summary>
    /// Authentication callback
    /// </summary>
    /// <param name="creds">ConnectCredential array</param>
    /// <param name="cbdata">user data passed to callback</param>
    /// <returns></returns>
    public delegate int ConnectAuthCallback(ref ConnectCredential[] creds, IntPtr cbdata);
	/// <summary>
	/// Callback for receiving stream events. The callback will be invoked once for each event which is pending.
	/// </summary>
	/// <param name="stream">stream on which the event occurred</param>
	/// <param name="events">bitset of events from virEventHandleType constants</param>
	/// <param name="opaque">user data registered with handle</param>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void StreamEventCallback(IntPtr stream, int events, IntPtr opaque);
	/// <summary>
	/// The virStreamSinkFunc callback is used together with the virStreamRecvAll function for libvirt
	/// to provide the data that has been received. The callback will be invoked multiple times, providing
	/// data in small chunks. The application should consume up 'nbytes' from the 'data' array of data and
	/// then return the number actual number of bytes consumed. The callback will continue to be invoked until
	/// it indicates the end of the stream has been reached. A return value of -1 at any time will abort the
	/// receive operation
	/// </summary>
	/// <param name="st">the stream object</param>
	/// <param name="data">preallocated array to be filled with data</param>
	/// <param name="nbytes">size of the data array</param>
	/// <param name="opaque">optional application provided data</param>
	/// <returns>the number of bytes filled, 0 upon end of file, or -1 upon error</returns>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int StreamSinkFunc(IntPtr st, IntPtr data, int nbytes, IntPtr opaque);
    #endregion

    #region structs
    /// <summary>
    /// This is a struct used to simply the C# bindings, for C# bindings internal use only.
    /// </summary>
    public struct OpenAuthManagedCB
    {
        /// <summary>
        /// Pointer to user data of the ConnectOpenAuth
        /// </summary>
        public IntPtr cbdata;
        /// <summary>
        /// The C# delegate which must be called
        /// </summary>
        public ConnectAuthCallback cbManaged;
    }

    /// <summary>
    /// the virError object
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class Error
    {
        /// <summary>
        /// The error code, a virErrorNumber.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public ErrorNumber code;
        /// <summary>
        /// What part of the library raised this error.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int domain;
        /// <summary>
        /// Human-readable informative error message.
        /// </summary>
        private IntPtr message;
        /// <summary>
        /// Human-readable informative error message.
        /// </summary>
        public string Message
        {
            get { return Marshal.PtrToStringAnsi(message); }
        }

        /// <summary>
        /// How consequent is the error.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public ErrorLevel level;
        /// <summary>
        /// Connection if available, deprecated see note above.
        /// </summary>
        public IntPtr conn;
        /// <summary>
        /// Domain if available, deprecated see note above.
        /// </summary>
        public IntPtr dom;
        /// <summary>
        /// Extra string information.
        /// </summary>
        private IntPtr str1;
        /// <summary>
        /// Extra string information.
        /// </summary>
        public string Str1 { get { return Marshal.PtrToStringAnsi(str1); } }
        /// <summary>
        /// Extra string information.
        /// </summary>
        [MarshalAs(UnmanagedType.SysInt)]
        private IntPtr str2;
        /// <summary>
        /// Extra string information.
        /// </summary>
        public string Str2 { get { return Marshal.PtrToStringAnsi(str2); } }
        /// <summary>
        /// Extra string information.
        /// </summary>
        private IntPtr str3;
        /// <summary>
        /// Extra string information.
        /// </summary>
        public string Str3 { get { return Marshal.PtrToStringAnsi(str3); } }
        /// <summary>
        /// Extra number information.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int int1;
        /// <summary>
        /// Extra number information.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int int2;
        /// <summary>
        /// Network if available, deprecated see note above.
        /// </summary>
        public IntPtr net;
    }

    ///<summary>
    /// Blocks domain statistics
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public class DomainBlockStatsStruct
    {
        /// <summary>
        /// Number of read requests.
        /// </summary>
        [MarshalAs(UnmanagedType.I8)]
        public long rd_req;
        /// <summary>
        /// Number of read bytes.
        /// </summary>
        [MarshalAs(UnmanagedType.I8)]
        public long rd_bytes;
        /// <summary>
        /// Number of write requests.
        /// </summary>
        [MarshalAs(UnmanagedType.I8)]
        public long wr_req;
        /// <summary>
        /// Number of written bytes.
        /// </summary>
        [MarshalAs(UnmanagedType.I8)]
        public long wr_bytes;
        /// <summary>
        /// In Xen this returns the mysterious 'oo_req'.
        /// </summary>
        [MarshalAs(UnmanagedType.I8)]
        public long errs;
    }

    ///<summary>
    /// Domain interface statistics
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public class DomainInterfaceStatsStruct
    {
        ///<summary>
        /// Bytes received
        ///</summary>
        [MarshalAs(UnmanagedType.I8)]
        public long rx_bytes;
        /// <summary>
        /// Packets received
        /// </summary>
        [MarshalAs(UnmanagedType.I8)]
        public long rx_packets;
        /// <summary>
        /// Errors received
        /// </summary>
        [MarshalAs(UnmanagedType.I8)]
        public long rx_errs;
        /// <summary>
        /// Drops received
        /// </summary>
        [MarshalAs(UnmanagedType.I8)]
        public long rx_drop;
        /// <summary>
        /// Bytes sended
        /// </summary>
        [MarshalAs(UnmanagedType.I8)]
        public long tx_bytes;
        /// <summary>
        /// Packets sended
        /// </summary>
        [MarshalAs(UnmanagedType.I8)]
        public long tx_packets;
        /// <summary>
        /// Errors sended
        /// </summary>
        [MarshalAs(UnmanagedType.I8)]
        public long tx_errs;
        /// <summary>
        /// Drops sended
        /// </summary>
        [MarshalAs(UnmanagedType.I8)]
        public long tx_drop;
    }

    ///<summary>
    /// Structure to handle connection authentication
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ConnectAuthUnmanaged
    {
        /// <summary>
        /// List of supported virConnectCredentialType values, should be a IntPtr to an int array or to a virConnectCredentialType array
        /// </summary>
        private IntPtr credtypes;
        ///<summary>
        /// Number of virConnectCredentialType in credtypes
        ///</summary>
        [MarshalAs(UnmanagedType.U4)]
        private uint ncredtype;
        ///<summary>
        /// Callback used to collect credentials, a virConnectAuthCallback delegate in bindings
        ///</summary>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public ConnectAuthCallbackUnmanaged cb;
        ///<summary>
        /// Data transported with callback, should be a IntPtr on what you want
        ///</summary>
        public IntPtr cbdata;
        /// <summary>
        /// List of supported virConnectCredentialType values
        /// </summary>
        public ConnectCredentialType[] CredTypes
        {
            get
            {
                int[] intCredTypes = new int[ncredtype];
                Marshal.Copy(credtypes, intCredTypes, 0, (int)ncredtype);
                ConnectCredentialType[] result = new ConnectCredentialType[ncredtype];
                for (int i = 0; i < intCredTypes.Length; i++)
                {
                    result[i] = (ConnectCredentialType)intCredTypes[i];
                }
                return result;
            }
            set
            {
                ncredtype = (uint)value.Length;
                credtypes = Marshal.AllocHGlobal(value.Length * sizeof(int));
                int[] vals = new int[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    vals[i] = (int)value[i];
                }
                Marshal.Copy(vals, 0, credtypes, (int)ncredtype);
            }
        }
    }

    ///<summary>
    /// Structure to handle connection authentication
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ConnectAuth
    {
        /// <summary>
        /// List of supported virConnectCredentialType values, should be a IntPtr to an int array or to a virConnectCredentialType array
        /// </summary>
        private IntPtr credtypes;
        ///<summary>
        /// Number of virConnectCredentialType in credtypes
        ///</summary>
        [MarshalAs(UnmanagedType.U4)]
        private uint ncredtype;
        ///<summary>
        /// Callback used to collect credentials, a virConnectAuthCallback delegate in bindings
        ///</summary>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public ConnectAuthCallback cb;
        ///<summary>
        /// Data transported with callback, should be a IntPtr on what you want
        ///</summary>
        public IntPtr cbdata;
        /// <summary>
        /// List of supported virConnectCredentialType values
        /// </summary>
        public ConnectCredentialType[] CredTypes
        {
            get
            {
                int[] intCredTypes = new int[ncredtype];
                Marshal.Copy(credtypes, intCredTypes, 0, (int)ncredtype);
                ConnectCredentialType[] result = new ConnectCredentialType[ncredtype];
				for (int i=0; i< intCredTypes.Length; i++)
				{
                    result[i] = (ConnectCredentialType)intCredTypes[i];
				}
                return result;
            }
            set
            {
                ncredtype = (uint)value.Length;
                credtypes = Marshal.AllocHGlobal(value.Length * sizeof(int));
				int[] vals = new int[value.Length];
				for (int i=0; i<value.Length; i++)
				{
					vals[i] = (int)value[i];
				}
                Marshal.Copy(vals, 0, credtypes, (int)ncredtype);
            }
        }
    }

    ///<summary>
    /// Credential structure
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public class ConnectCredential
    {
        public ConnectCredential ()
        {
            result = IntPtr.Zero;
        }
        ///<summary>
        /// One of virConnectCredentialType constants
        ///</summary>
        [MarshalAs(UnmanagedType.I4)]
        public ConnectCredentialType type;
        ///<summary>
        /// Prompt to show to user
        ///</summary>
        private IntPtr prompt;
        ///<summary>
        /// Additional challenge to show
        ///</summary>
        private IntPtr challenge;
        ///<summary>
        /// Optional default result
        ///</summary>
        private IntPtr defresult;
        ///<summary>
        /// Result to be filled with user response (or defresult). An IntPtr to a marshalled allocated string
        ///</summary>
        private IntPtr result;
        ///<summary>
        /// Length of the result
        ///</summary>
        [MarshalAs(UnmanagedType.U4)]
        private uint resultlen;
        ///<summary>
        /// Prompt to show to user
        ///</summary>
        public string Prompt
        {
            get
            {
                return Marshal.PtrToStringAnsi(prompt);
            }
        }
        ///<summary>
        /// Additional challenge to show
        ///</summary>
        public string Challenge
        {
            get
            {
                return Marshal.PtrToStringAnsi(challenge);
            }
        }
        ///<summary>
        /// Optional default result
        ///</summary>
        public string Defresult
        {
            get
            {
                return Marshal.PtrToStringAnsi(defresult);
            }
        }
        ///<summary>
        /// Result to be filled with user response (or defresult).
        ///</summary>
        public string Result
        {
            get
            {
                return Marshal.PtrToStringAnsi(result);
            }
            set
            {
                IntPtr tmp = Marshal.StringToHGlobalAnsi(value);

                NativeFunctions.Free(result);
                result = NativeFunctions.StrDup(tmp);
                resultlen = (uint)value.Length;

                Marshal.FreeHGlobal(tmp);
            }
        }
    }

    ///<summary>
    /// Structure for domain memory statistics
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public class DomainMemoryStat
    {
        /// <summary>
        /// Tag
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public DomainMemoryStatTags tag;
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
    public struct StorageVolInfo
    {
        /// <summary>
        /// virStorageVolType flags.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public StorageVolType type;
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
    public struct StoragePoolInfo
    {
        /// <summary>
        /// virStoragePoolState flags
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public StoragePoolState state;
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
    [StructLayout(LayoutKind.Sequential)]
    public class NodeInfo
    {
        /// <summary>
        /// String indicating the CPU model.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string model;
        /// <summary>
        /// Memory size in kilobytes
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)]
        public UIntPtr memory;
        /// <summary>
        /// The number of active CPUs.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)]
        public UIntPtr cpus;
        /// <summary>
        /// Expected CPU frequency.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)]
        public UIntPtr mhz;
        /// <summary>
        /// The number of NUMA cell, 1 for uniform mem access.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)]
        public UIntPtr nodes;
        /// <summary>
        /// Number of CPU socket per node.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)]
        public UIntPtr sockets;
        /// <summary>
        /// Number of core per socket.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)]
        public UIntPtr cores;
        /// <summary>
        /// Number of threads per core.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)]
        public UIntPtr threads;
    }
    /// <summary>
    /// Structure to handle domain informations
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class DomainInfo
    {
        /// <summary>
        /// The running state, one of virDomainState.
        /// </summary>
        private byte state;
        /// <summary>
        /// The maximum memory in KBytes allowed.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)]
        public UIntPtr maxMem;
        /// <summary>
        /// The memory in KBytes used by the domain.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)]
        public UIntPtr memory;
        /// <summary>
        /// The number of virtual CPUs for the domain.
        /// </summary>
        public ushort nrVirtCpu;
        /// <summary>
        /// The CPU time used in nanoseconds.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)]
        public UIntPtr cpuTime;
        /// <summary>
        /// The running state, one of virDomainState.
        /// </summary>
        public DomainState State { get { return (DomainState)state; } }
    }
    #endregion

    #region enums
    ///<summary>
    /// Enumerate errors
    ///</summary>
    public enum ErrorNumber
    {
        /// <summary>
        /// No error
        /// </summary>
        VIR_ERR_OK = 0,
        /// <summary>
        /// Internal error
        /// </summary>
        VIR_ERR_INTERNAL_ERROR = 1,
        /// <summary>
        /// Memory allocation failure
        /// </summary>
        VIR_ERR_NO_MEMORY = 2,
        /// <summary>
        /// No support for this function
        /// </summary>
        VIR_ERR_NO_SUPPORT = 3,
        /// <summary>
        /// Could not resolve hostname
        /// </summary>
        VIR_ERR_UNKNOWN_HOST = 4,
        /// <summary>
        /// Can't connect to hypervisor
        /// </summary>
        VIR_ERR_NO_CONNECT = 5,
        /// <summary>
        /// Invalid connection object
        /// </summary>
        VIR_ERR_INVALID_CONN = 6,
        /// <summary>
        /// Invalid domain object
        /// </summary>
        VIR_ERR_INVALID_DOMAIN = 7,
        /// <summary>
        /// Invalid function argument
        /// </summary>
        VIR_ERR_INVALID_ARG = 8,
        /// <summary>
        ///  A command to hypervisor failed
        /// </summary>
        VIR_ERR_OPERATION_FAILED = 9,
        /// <summary>
        /// A HTTP GET command to failed
        /// </summary>
        VIR_ERR_GET_FAILED = 10,
        /// <summary>
        /// A HTTP POST command to failed
        /// </summary>
        VIR_ERR_POST_FAILED = 11,
        /// <summary>
        /// Unexpected HTTP error code
        /// </summary>
        VIR_ERR_HTTP_ERROR = 12,
        /// <summary>
        /// Failure to serialize an S-Expr
        /// </summary>
        VIR_ERR_SEXPR_SERIAL = 13,
        /// <summary>
        /// Could not open Xen hypervisor control
        /// </summary>
        VIR_ERR_NO_XEN = 14,
        /// <summary>
        /// Failure doing an hypervisor call
        /// </summary>
        VIR_ERR_XEN_CALL = 15,
        /// <summary>
        /// Unknown OS type
        /// </summary>
        VIR_ERR_OS_TYPE = 16,
        /// <summary>
        /// Missing kernel information
        /// </summary>
        VIR_ERR_NO_KERNEL = 17,
        /// <summary>
        /// Missing root device information
        /// </summary>
        VIR_ERR_NO_ROOT = 18,
        /// <summary>
        /// Missing source device information
        /// </summary>
        VIR_ERR_NO_SOURCE = 19,
        /// <summary>
        /// Missing target device information
        /// </summary>
        VIR_ERR_NO_TARGET = 20,
        /// <summary>
        /// Missing domain name information
        /// </summary>
        VIR_ERR_NO_NAME = 21,
        /// <summary>
        /// Missing domain OS information
        /// </summary>
        VIR_ERR_NO_OS = 22,
        /// <summary>
        /// Missing domain devices information
        /// </summary>
        VIR_ERR_NO_DEVICE = 23,
        /// <summary>
        /// Could not open Xen Store control
        /// </summary>
        VIR_ERR_NO_XENSTORE = 24,
        /// <summary>
        /// Too many drivers registered
        /// </summary>
        VIR_ERR_DRIVER_FULL = 25,
        /// <summary>
        /// Not supported by the drivers (DEPRECATED)
        /// </summary>
        VIR_ERR_CALL_FAILED = 26,
        /// <summary>
        /// An XML description is not well formed or broken
        /// </summary>
        VIR_ERR_XML_ERROR = 27,
        /// <summary>
        /// The domain already exist
        /// </summary>
        VIR_ERR_DOM_EXIST = 28,
        /// <summary>
        /// Operation forbidden on read-only connections
        /// </summary>
        VIR_ERR_OPERATION_DENIED = 29,
        /// <summary>
        /// Failed to open a conf file
        /// </summary>
        VIR_ERR_OPEN_FAILED = 30,
        /// <summary>
        /// Failed to read a conf file
        /// </summary>
        VIR_ERR_READ_FAILED = 31,
        /// <summary>
        /// Failed to parse a conf file
        /// </summary>
        VIR_ERR_PARSE_FAILED = 32,
        /// <summary>
        /// Failed to parse the syntax of a conf file
        /// </summary>
        VIR_ERR_CONF_SYNTAX = 33,
        /// <summary>
        /// Failed to write a conf file
        /// </summary>
        VIR_ERR_WRITE_FAILED = 34,
        /// <summary>
        /// Detail of an XML error
        /// </summary>
        VIR_ERR_XML_DETAIL = 35,
        /// <summary>
        /// Invalid network object
        /// </summary>
        VIR_ERR_INVALID_NETWORK = 36,
        /// <summary>
        /// The network already exist
        /// </summary>
        VIR_ERR_NETWORK_EXIST = 37,
        /// <summary>
        /// General system call failure
        /// </summary>
        VIR_ERR_SYSTEM_ERROR = 38,
        /// <summary>
        /// Some sort of RPC error
        /// </summary>
        VIR_ERR_RPC = 39,
        /// <summary>
        /// Error from a GNUTLS call
        /// </summary>
        VIR_ERR_GNUTLS_ERROR = 40,
        /// <summary>
        /// Failed to start network
        /// </summary>
        VIR_WAR_NO_NETWORK = 41,
        /// <summary>
        /// Domain not found or unexpectedly disappeared
        /// </summary>
        VIR_ERR_NO_DOMAIN = 42,
        /// <summary>
        /// Network not found
        /// </summary>
        VIR_ERR_NO_NETWORK = 43,
        /// <summary>
        /// Invalid MAC address
        /// </summary>
        VIR_ERR_INVALID_MAC = 44,
        /// <summary>
        /// Authentication failed
        /// </summary>
        VIR_ERR_AUTH_FAILED = 45,
        /// <summary>
        /// Invalid storage pool object
        /// </summary>
        VIR_ERR_INVALID_STORAGE_POOL = 46,
        /// <summary>
        /// Invalid storage vol object
        /// </summary>
        VIR_ERR_INVALID_STORAGE_VOL = 47,
        /// <summary>
        /// Failed to start storage
        /// </summary>
        VIR_WAR_NO_STORAGE = 48,
        /// <summary>
        /// Storage pool not found
        /// </summary>
        VIR_ERR_NO_STORAGE_POOL = 49,
        /// <summary>
        /// Storage pool not found
        /// </summary>
        VIR_ERR_NO_STORAGE_VOL = 50,
        /// <summary>
        /// Failed to start node driver
        /// </summary>
        VIR_WAR_NO_NODE = 51,
        /// <summary>
        /// Invalid node device object
        /// </summary>
        VIR_ERR_INVALID_NODE_DEVICE = 52,
        /// <summary>
        /// Node device not found
        /// </summary>
        VIR_ERR_NO_NODE_DEVICE = 53,
        /// <summary>
        /// Security model not found
        /// </summary>
        VIR_ERR_NO_SECURITY_MODEL = 54,
        /// <summary>
        /// Operation is not applicable at this time
        /// </summary>
        VIR_ERR_OPERATION_INVALID = 55,
        /// <summary>
        /// Failed to start interface driver
        /// </summary>
        VIR_WAR_NO_INTERFACE = 56,
        /// <summary>
        /// Interface driver not running
        /// </summary>
        VIR_ERR_NO_INTERFACE = 57,
        /// <summary>
        /// Invalid interface object
        /// </summary>
        VIR_ERR_INVALID_INTERFACE = 58,
        /// <summary>
        /// More than one matching interface found
        /// </summary>
        VIR_ERR_MULTIPLE_INTERFACES = 59,
        /// <summary>
        /// Failed to start secret storage
        /// </summary>
        VIR_WAR_NO_SECRET = 60,
        /// <summary>
        /// Invalid secret
        /// </summary>
        VIR_ERR_INVALID_SECRET = 61,
        /// <summary>
        /// Secret not found
        /// </summary>
        VIR_ERR_NO_SECRET = 62,
        /// <summary>
        /// Unsupported configuration construct
        /// </summary>
        VIR_ERR_CONFIG_UNSUPPORTED = 63,
        /// <summary>
        /// Timeout occurred during operation
        /// </summary>
        VIR_ERR_OPERATION_TIMEOUT = 64,
        /// <summary>
        /// A migration worked, but making the VM persist on the dest host failed
        /// </summary>
        VIR_ERR_MIGRATE_PERSIST_FAILED = 65,
    }

    /// <summary>
    /// Enumrate types of domain errors
    /// </summary>
    public enum ErrorDomain
    {
        /// <summary>
        /// None
        /// </summary>
        VIR_FROM_NONE = 0,
        /// <summary>
        /// Error at Xen hypervisor layer
        /// </summary>
        VIR_FROM_XEN = 1,
        /// <summary>
        /// Error at connection with xend daemon
        /// </summary>
        VIR_FROM_XEND = 2,
        /// <summary>
        /// Error at connection with xen store
        /// </summary>
        VIR_FROM_XENSTORE = 3,
        /// <summary>
        /// Error in the S-Expression code
        /// </summary>
        VIR_FROM_SEXPR = 4,
        /// <summary>
        /// Error in the XML code
        /// </summary>
        VIR_FROM_XML = 5,
        /// <summary>
        /// Error when operating on a domain
        /// </summary>
        VIR_FROM_DOM = 6,
        /// <summary>
        /// Error in the XML-RPC code
        /// </summary>
        VIR_FROM_RPC = 7,
        /// <summary>
        /// Error in the proxy code
        /// </summary>
        VIR_FROM_PROXY = 8,
        /// <summary>
        /// Error in the configuration file handling
        /// </summary>
        VIR_FROM_CONF = 9,
        /// <summary>
        /// Error at the QEMU daemon
        /// </summary>
        VIR_FROM_QEMU = 10,
        /// <summary>
        /// Error when operating on a network
        /// </summary>
        VIR_FROM_NET = 11,
        /// <summary>
        /// Error from test driver
        /// </summary>
        VIR_FROM_TEST = 12,
        /// <summary>
        /// Error from remote driver
        /// </summary>
        VIR_FROM_REMOTE = 13,
        /// <summary>
        /// Error from OpenVZ driver
        /// </summary>
        VIR_FROM_OPENVZ = 14,
        /// <summary>
        /// Error at Xen XM layer
        /// </summary>
        VIR_FROM_XENXM = 15,
        /// <summary>
        /// Error in the Linux Stats code
        /// </summary>
        VIR_FROM_STATS_LINUX = 16,
        /// <summary>
        /// Error from Linux Container driver
        /// </summary>
        VIR_FROM_LXC = 17,
        /// <summary>
        /// Error from storage driver
        /// </summary>
        VIR_FROM_STORAGE = 18,
        /// <summary>
        /// Error from network config
        /// </summary>
        VIR_FROM_NETWORK = 19,
        /// <summary>
        /// Error from domain config
        /// </summary>
        VIR_FROM_DOMAIN = 20,
        /// <summary>
        /// Error at the UML driver
        /// </summary>
        VIR_FROM_UML = 21,
        /// <summary>
        /// Error from node device monitor
        /// </summary>
        VIR_FROM_NODEDEV = 22,
        /// <summary>
        /// Error from xen inotify layer
        /// </summary>
        VIR_FROM_XEN_INOTIFY = 23,
        /// <summary>
        /// Error from security framework
        /// </summary>
        VIR_FROM_SECURITY = 24,
        /// <summary>
        /// Error from VirtualBox driver
        /// </summary>
        VIR_FROM_VBOX = 25,
        /// <summary>
        /// Error when operating on an interface
        /// </summary>
        VIR_FROM_INTERFACE = 26,
        /// <summary>
        /// Error from OpenNebula driver
        /// </summary>
        VIR_FROM_ONE = 27,
        /// <summary>
        /// Error from ESX driver
        /// </summary>
        VIR_FROM_ESX = 28,
        /// <summary>
        /// Error from IBM power hypervisor
        /// </summary>
        VIR_FROM_PHYP = 29,
        /// <summary>
        /// Error from secret storage
        /// </summary>
        VIR_FROM_SECRET = 30,
        /// <summary>
        /// Error from CPU driver
        /// </summary>
        VIR_FROM_CPU = 31,
        /// <summary>
        /// Error from XenAPI
        /// </summary>
        VIR_FROM_XENAPI = 32,
        /// <summary>
        /// Error from network filter driver
        /// </summary>
        VIR_FROM_NWFILTER = 33,
        /// <summary>
        /// Error from Synchronous hooks
        /// </summary>
        VIR_FROM_HOOK = 34,
        /// <summary>
        /// Error from domain snapshot
        /// </summary>
        VIR_FROM_DOMAIN_SNAPSHOT = 35
    }

    /// <summary>
    /// Enumerate the error levels
    /// </summary>
    public enum ErrorLevel
    {
        /// <summary>
        /// No error
        /// </summary>
        VIR_ERR_NONE = 0,
        /// <summary>
        /// A simple warning.
        /// </summary>
        VIR_ERR_WARNING = 1,
        /// <summary>
        /// An error.
        /// </summary>
        VIR_ERR_ERROR = 2,
    }
    ///<summary>
    /// Type of handles for callback
    ///</summary>
    [Flags]
    public enum EventHandleType
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
    public enum DomainMemoryStatTags
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
    public enum StorageVolType
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
    public enum StoragePoolState
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
    public enum DomainXMLFlags
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
    public enum DomainEventDefinedDetailType
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
    public enum DomainEventUndefinedDetailType
    {
        /// <summary>
        /// Deleted the config file
        /// </summary>
        VIR_DOMAIN_EVENT_UNDEFINED_REMOVED = 0
    }
    /// <summary>
    /// Details on the caused of the 'started' lifecycle event
    /// </summary>
    public enum DomainEventStartedDetailType
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
    public enum DomainEventSuspendedDetailType
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
    public enum DomainEventResumedDetailType
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
    public enum DomainEventStoppedDetailType
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
    public enum DomainEventType
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
    public enum StoragePoolBuildFlags
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
    public enum StoragePoolDeleteFlags
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
    public enum ConnectCredentialType
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
    public enum DomainState
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
