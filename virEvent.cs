/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 */

using System.Runtime.InteropServices;

namespace LibvirtBindings
{
    public class virEvent
    {
        ///<summary>
        /// Function to install callbacks
        ///</summary>
        ///<param name="addHandle">the virEventAddHandleFunc which will be called (a delegate)</param>
        ///<param name="updateHandle">the virEventUpdateHandleFunc which will be called (a delegate)</param>
        ///<param name="removeHandle">the virEventRemoveHandleFunc which will be called (a delegate)</param>
        ///<param name="addTimeout">the virEventAddTimeoutFunc which will be called (a delegate)</param>
        ///<param name="updateTimeout">the virEventUpdateTimeoutFunc which will be called (a delegate)</param>
        ///<param name="removeTimeout">the virEventRemoveTimeoutFunc which will be called (a delegate)</param>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virEventRegisterImpl")]
        public static extern void RegisterImpl([MarshalAs(UnmanagedType.FunctionPtr)]virEventAddHandleFunc addHandle,
                                               [MarshalAs(UnmanagedType.FunctionPtr)]virEventUpdateHandleFunc updateHandle,
                                               [MarshalAs(UnmanagedType.FunctionPtr)]virEventRemoveHandleFunc removeHandle,
                                               [MarshalAs(UnmanagedType.FunctionPtr)]virEventAddTimeoutFunc addTimeout,
                                               [MarshalAs(UnmanagedType.FunctionPtr)]virEventUpdateTimeoutFunc updateTimeout,
                                               [MarshalAs(UnmanagedType.FunctionPtr)]virEventRemoveTimeoutFunc removeTimeout);
    }
}
