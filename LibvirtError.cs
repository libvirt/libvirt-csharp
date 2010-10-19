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
    ///<summary>
    /// class for libvirt errors binding
    ///</summary>
    public class libvirtError
    {
        ///<summary>
        /// Signature of a function to use when there is an error raised by the library.
        ///</summary>
        ///<param name="userData">user provided data for the error callback</param>
        ///<param name="error">the error being raised.</param>
        public delegate void virErrorFunc(IntPtr userData, virError error);

        ///<summary>
        /// Enumerate errors
        ///</summary>
        public enum virErrorNumber
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
        public enum virErrorDomain
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
            VIR_FROM_XENAPI	 =	32,
            /// <summary>
            /// Error from network filter driver
            /// </summary>
            VIR_FROM_NWFILTER	 =	33,
            /// <summary>
            /// Error from Synchronous hooks
            /// </summary>
            VIR_FROM_HOOK	 =	34,
            /// <summary>
            /// Error from domain snapshot
            /// </summary>
            VIR_FROM_DOMAIN_SNAPSHOT =	35
        }

        /// <summary>
        /// Enumerate the error levels
        /// </summary>
        public enum virErrorLevel
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

        /// <summary>
        /// the virError object
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class virError
        {
            /// <summary>
            /// The error code, a virErrorNumber.
            /// </summary>
            [MarshalAs(UnmanagedType.I4)]
            public virErrorNumber code;
            /// <summary>
            /// What part of the library raised this error.
            /// </summary>
            [MarshalAs(UnmanagedType.I4)]
            public int domain;
            /// <summary>
            /// Human-readable informative error message.
            /// </summary>
            [MarshalAs(UnmanagedType.SysInt)]
#pragma warning disable 649
            private IntPtr message;
#pragma warning restore 649
            /// <summary>
            /// Human-readable informative error message.
            /// </summary>
// ReSharper disable InconsistentNaming
            public string Message
// ReSharper restore InconsistentNaming
            {
                get { return Marshal.PtrToStringAnsi(message); }
            }

            /// <summary>
            /// How consequent is the error.
            /// </summary>
            [MarshalAs(UnmanagedType.I4)]
            public virErrorLevel level;
            /// <summary>
            /// Connection if available, deprecated see note above.
            /// </summary>
            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr conn;
            /// <summary>
            /// Domain if available, deprecated see note above.
            /// </summary>
            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr dom;
            /// <summary>
            /// Extra string information.
            /// </summary>
            [MarshalAs(UnmanagedType.SysInt)]
#pragma warning disable 649
            private IntPtr str1;
#pragma warning restore 649
            /// <summary>
            /// Extra string information.
            /// </summary>
// ReSharper disable InconsistentNaming
            public string Str1 { get { return Marshal.PtrToStringAnsi(str1); } }
// ReSharper restore InconsistentNaming
            /// <summary>
            /// Extra string information.
            /// </summary>
            [MarshalAs(UnmanagedType.SysInt)]
#pragma warning disable 649
            private IntPtr str2;
#pragma warning restore 649
            /// <summary>
            /// Extra string information.
            /// </summary>
// ReSharper disable InconsistentNaming
            public string Str2 { get { return Marshal.PtrToStringAnsi(str2); } }
// ReSharper restore InconsistentNaming
            /// <summary>
            /// Extra string information.
            /// </summary>
            [MarshalAs(UnmanagedType.SysInt)]
#pragma warning disable 649
            private IntPtr str3;
#pragma warning restore 649
            /// <summary>
            /// Extra string information.
            /// </summary>
// ReSharper disable InconsistentNaming
            public string Str3 { get { return Marshal.PtrToStringAnsi(str3); } }
// ReSharper restore InconsistentNaming
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
            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr net;
        }


        // virConnCopyLastError - Deprecated
        // virConnGetLastError - Deprecated

        /// <summary>
        /// The error object is kept in thread local storage, so separate threads can safely access this concurrently. 
        /// Reset the last error caught on that connection.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/> pointer to the hypervisor connection.
        /// </param>
        [DllImport("libvirt-0.dll")]
        public static extern void virConnResetLastError(IntPtr conn);

        /// <summary>
        /// Set a connection error handling function, if @handler is NULL it will reset to default 
        /// which is to pass error back to the global library handler.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to the hypervisor connection.
        /// </param>
        /// <param name="userData">
        /// A <see cref="IntPtr"/>pointer to the user data provided in the handler callback.
        /// </param>
        /// <param name="handler">
        /// A <see cref="virErrorFunc"/>function to get called in case of error or NULL
        /// </param>
        [DllImport("libvirt-0.dll")]
        public static extern void virConnSetErrorFunc(IntPtr conn, IntPtr userData, [MarshalAs(UnmanagedType.FunctionPtr)]virErrorFunc handler);

        /// <summary>
        /// Copy the content of the last error caught at the library level. 
        /// The error object is kept in thread local storage, so separate threads can safely access this concurrently. 
        /// One will need to free the result with virResetError().
        /// </summary>
        /// <param name="to">
        /// A <see cref="virError"/> target to receive the copy.
        /// </param>
        /// <returns>
        /// 0 if no error was found and the error code otherwise and -1 in case of parameter error.
        /// </returns>
        [DllImport("libvirt-0.dll")]
        public static extern int virCopyLastError([Out] virError to);

        /// <summary>
        /// Default routine reporting an error to stderr.
        /// </summary>
        /// <param name="err">
        /// A <see cref="virError"/> pointer to the error.
        /// </param>
        [DllImport("libvirt-0.dll")]
        public static extern void virDefaultErrorFunc([In] virError err);

        /// <summary>
        /// Resets and frees the given error.
        /// </summary>
        /// <param name="err">
        /// A <see cref="virError"/> error to free.
        /// </param>
        [DllImport("libvirt-0.dll")]
        public static extern void virFreeError(virError err); // Does not work, anybody know why?

        /// <summary>
        /// Provide a pointer to the last error caught at the library level. 
        /// The error object is kept in thread local storage, so separate threads can safely access this concurrently.
        /// </summary>
        /// <returns>
        /// A pointer to the last error or NULL if none occurred.
        /// </returns>
        [DllImport("libvirt-0.dll")]
        public static extern int virGetLastError();

        /// <summary>
        /// Reset the error being pointed to.
        /// </summary>
        /// <param name="err">
        /// A <see cref="virError"/> pointer to the to clean up.
        /// </param>
        [DllImport("libvirt-0.dll")]
        public static extern void virResetError(virError err);

        /// <summary>
        /// Reset the last error caught at the library level. The error object is kept in thread local storage, 
        /// so separate threads can safely access this concurrently, only resetting their own error object.
        /// </summary>
        [DllImport("libvirt-0.dll")]
        public static extern void virResetLastError();

        /// <summary>
        /// Save the last error into a new error object.
        /// </summary>
        /// <returns>
        /// A <see cref="virError"/> pointer to the copied error or NULL if allocation failed. 
        /// It is the caller's responsibility to free the error with virFreeError().
        /// </returns>
        [DllImport("libvirt-0.dll")]
        public static extern virError virSaveLastError();

        /// <summary>
        /// Set a library global error handling function, if @handler is NULL, it will reset to default printing on stderr. 
        /// The error raised there are those for which no handler at the connection level could caught.
        /// </summary>
        /// <param name="userData">
        /// A <see cref="IntPtr"/>pointer to the user data provided in the handler callback.
        /// </param>
        /// <param name="handler">
        /// A <see cref="virErrorFunc"/>function to get called in case of error or NULL.
        /// </param>
        [DllImport("libvirt-0.dll")]
        public static extern void virSetErrorFunc(IntPtr userData, [MarshalAs(UnmanagedType.FunctionPtr)]virErrorFunc handler);
    }
}
