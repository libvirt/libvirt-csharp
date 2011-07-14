/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *   Anton Podavalov <a.podavalov@gmail.com>
 *
 * See COPYING.LIB for the License of this software
 */

using System;
using System.Runtime.InteropServices;

namespace Libvirt
{
    /// <summary>
    /// The Stream class expose all libvirt stream related functions
    /// </summary>
    public class Stream
    {
		/// <summary>
		/// Request that the in progress data transfer be cancelled abnormally before the end of
		/// the stream has been reached. For output streams this can be used to inform the driver
		/// that the stream is being terminated early. For input streams this can be used to inform
		/// the driver that it should stop sending data
		/// </summary>
		/// <param name="stream">pointer to the stream object</param>
		/// <returns>0 on success, -1 upon error</returns>
		[DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStreamAbort")]
		public static extern int Abort(IntPtr stream);
		/// <summary>
		/// Register a callback to be notified when a stream becomes writable, or readable.
		/// This is most commonly used in conjunction with non-blocking data streams to integrate
		/// into an event loop
		/// </summary>
		/// <param name="stream">pointer to the stream object</param>
		/// <param name="events">set of events to monitor</param>
		/// <param name="cb">callback to invoke when an event occurs</param>
		/// <param name="opaque">application defined data</param>
		/// <param name="ff">callback to free @opaque data</param>
		/// <returns>0 on success, -1 upon error</returns>
		[DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStreamEventAddCallback")]
		public static extern int EventAddCallback(IntPtr stream, int events, [MarshalAs(UnmanagedType.FunctionPtr)] StreamEventCallback cb, IntPtr opaque, [MarshalAs(UnmanagedType.FunctionPtr)] FreeCallback ff);

        // TODO virStreamEventCallback

		/// <summary>
		/// Remove an event callback from the stream
		/// </summary>
		/// <param name="stream">pointer to the stream object</param>
		/// <returns>0 on success, -1 on error</returns>
		[DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStreamEventRemoveCallback")]
		public static extern int EventRemoveCallback(IntPtr stream);
		/// <summary>
		/// Remove an event callback from the stream
		/// </summary>
		/// <param name="stream">pointer to the stream object</param>
		/// <param name="events">set of events to monitor</param>
		/// <returns>0 on success, -1 if no callback is registered</returns>
		[DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStreamEventUpdateCallback")]
		public static extern int EventUpdateCallback(IntPtr stream, int events);
		/// <summary>
		/// Indicate that there is no further data is to be transmitted on the stream.
		/// For output streams this should be called once all data has been written.
		/// For input streams this should be called once <see cref="Recv" /> returns end-of-file.
		/// This method is a synchronization point for all asynchronous errors, so if this
		/// returns a success code the application can be sure that all data has been
		/// successfully processed.
		/// </summary>
		/// <param name="stream">pointer to the stream object</param>
		/// <returns>0 on success, -1 upon error</returns>
		[DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStreamFinish")]
		public static extern int Finish(IntPtr stream);
		/// <summary>
		/// Decrement the reference count on a stream, releasing the stream object
		/// if the reference count has hit zero. There must not be an active data transfer
		/// in progress when releasing the stream. If a stream needs to be disposed of prior
		/// to end of stream being reached, then the virStreamAbort function should be called
		/// first
		/// </summary>
		/// <param name="stream">pointer to the stream object</param>
		/// <returns>0 upon success, -1 on error</returns>
		[DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStreamFree")]
		public static extern int Free(IntPtr stream);
		/// <summary>
		/// Creates a new stream object which can be used to perform streamed I/O with other
		/// public API function. When no longer needed, a stream object must be released with
		/// virStreamFree. If a data stream has been used, then the application must call
		/// virStreamFinish or @Abort before free'ing to, in order to notify the driver
		/// of termination. If a non-blocking data stream is required passed VIR_STREAM_NONBLOCK
		/// for flags, otherwise pass 0.
		/// </summary>
		/// <param name="conn">pointer to the connection</param>
		/// <param name="flags">control features of the stream</param>
		/// <returns>the new stream, or NULL upon error</returns>
		[DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStreamNew")]
		public static extern int New(IntPtr conn, uint flags);
		/// <summary>
		/// Reads a series of bytes from the stream. This method may block the calling application
		/// for an arbitrary amount of time. Errors are not guaranteed to be reported synchronously
		/// with the call, but may instead be delayed until a subsequent call.
		/// </summary>
		/// <param name="stream">
		/// pointer to the stream object
		/// </param>
		/// <param name="data">
		/// buffer to read into from stream
		/// </param>
		/// <param name="size">
		/// size of @data buffer
		/// </param>
		/// <returns>
		/// the number of bytes read, which may be less than requested.Returns 0 when the end of the stream
		/// is reached, at which time the caller should invoke virStreamFinish() to get confirmation of stream
		/// completion. Returns -1 upon error, at which time the stream will be marked as aborted,
		/// and the caller should now release the stream with virStreamFree. Returns -2 if there is no
		/// data pending to be read & the stream is marked as non-blocking.
		/// </returns>
		[DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStreamRecv")]
		public static extern int Recv(IntPtr stream, IntPtr data, int size);
		/// <summary>
		/// Receive the entire data stream, sending the data to the requested data sink. This is simply a
		/// convenient alternative to virStreamRecv, for apps that do blocking-I/O.
		/// </summary>
		/// <param name="stream">
		/// pointer to the stream object
		/// </param>
		/// <param name="handler">
		/// sink callback for writing data to application
		/// </param>
		/// <param name="opaque">
		/// application defined data
		/// </param>
		/// <returns>
		/// 0 if all the data was successfully received. The caller should invoke virStreamFinish(st) to flush
		/// the stream upon success and then virStreamFree Returns -1 upon any error, with virStreamAbort()
		/// already having been called, so the caller need only call virStreamFree()
		/// </returns>
		[DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStreamRecvAll")]
		public static extern int RecvAll(IntPtr stream, [MarshalAs(UnmanagedType.FunctionPtr)] StreamSinkFunc handler, IntPtr opaque);
		/// <summary>
		/// Increment the reference count on the stream. For each additional call to this method, there shall be a
		/// corresponding call to virStreamFree to release the reference count, once the caller no longer needs
		/// the reference to this object.
		/// </summary>
		/// <param name="stream">
		/// pointer to the stream
		/// </param>
		/// <returns>
		/// 0 in case of success, -1 in case of failure
		/// </returns>
		[DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStreamRef")]
		public static extern int Ref(IntPtr stream);

        // TODO virStreamSend

        // TODO virStreamSendAll

        // TODO virStreamSinkFunc

        // TODO virStreamSourceFunc
    }
}
