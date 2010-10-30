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
    /// The Event class expose all the event related methods
    /// </summary>
    public class Event
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
        public static extern void RegisterImpl([MarshalAs(UnmanagedType.FunctionPtr)] EventAddHandleFunc addHandle,
                                               [MarshalAs(UnmanagedType.FunctionPtr)] EventUpdateHandleFunc updateHandle,
                                               [MarshalAs(UnmanagedType.FunctionPtr)] EventRemoveHandleFunc removeHandle,
                                               [MarshalAs(UnmanagedType.FunctionPtr)] EventAddTimeoutFunc addTimeout,
                                               [MarshalAs(UnmanagedType.FunctionPtr)] EventUpdateTimeoutFunc updateTimeout,
                                               [MarshalAs(UnmanagedType.FunctionPtr)] EventRemoveTimeoutFunc removeTimeout);
    }
}
