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
    /// The StorageVol class expose all libvirt storage volume related functions
    /// </summary>
    public class StorageVol
    {

        /// <summary>
        /// Create a storage volume within a pool based on an XML description. Not all pools support creation of volumes.
        /// </summary>
        /// <param name="pool">A <see cref="IntPtr"/>pointer to storage pool.</param>
        /// <param name="xmldesc">A <see cref="System.String"/>description of volume to create.</param>
        /// <param name="flags">A <see cref="System.UInt32"/>flags for creation (unused, pass 0).</param>
        /// <returns>The storage volume, or NULL on error.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolCreateXML")]
        public static extern IntPtr CreateXML(IntPtr pool, string xmldesc, uint flags);

        /// <summary>
        /// Create a storage volume in the parent pool, using the 'clonevol' volume as input.
        /// Information for the new volume (name, perms) are passed via a typical volume XML description.
        /// </summary>
        /// <param name="pool">A <see cref="IntPtr"/>pointer to parent pool for the new volume.</param>
        /// <param name="xmldesc">A <see cref="System.String"/>description of volume to create.</param>
        /// <param name="clonevol">A <see cref="IntPtr"/>storage volume to use as input.</param>
        /// <param name="flags">A <see cref="System.UInt32"/>flags for creation (unused, pass 0).</param>
        /// <returns>
        /// A <see cref="IntPtr"/>the storage volume, or NULL on error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolCreateXMLFrom")]
        public static extern IntPtr CreateXMLFrom(IntPtr pool, string xmldesc, IntPtr clonevol, uint flags);

        /// <summary>
        /// Delete the storage volume from the pool.
        /// </summary>
        /// <param name="vol">A <see cref="IntPtr"/>pointer to storage volume.</param>
        /// <param name="flags">A <see cref="System.UInt32"/>future flags, use 0 for now.</param>
        /// <returns>0 on success, or -1 on error.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolDelete")]
        public static extern int Delete(IntPtr vol, uint flags);

        /// <summary>
        /// Release the storage volume handle. The underlying storage volume continues to exist.
        /// </summary>
        /// <param name="vol">A <see cref="IntPtr"/>pointer to storage volume.</param>
        /// <returns>0 on success, or -1 on error.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolFree")]
        public static extern int Free(IntPtr vol);

        /// <summary>
        /// Provides the connection pointer associated with a storage volume.
        /// The reference counter on the connection is not increased by this call.
        /// WARNING: When writing libvirt bindings in other languages, do not use this function.
        /// Instead, store the connection and the volume object together.
        /// </summary>
        /// <param name="vol">A <see cref="IntPtr"/> pointer to the storage volume</param>
        /// <returns>A <see cref="IntPtr"/>A Pointer to the connect that hold the storage volume</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolGetConnect")]
        public static extern IntPtr GetConnect(IntPtr vol);

        /// <summary>
        /// Fetches volatile information about the storage volume such as its current allocation.
        /// </summary>
        /// <param name="vol">A <see cref="IntPtr"/>pointer to storage volume.</param>
        /// <param name="info">A <see cref="StorageVolInfo"/>pointer at which to store info.</param>
        /// <returns>0 on success, or -1 on failure.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolGetInfo")]
        public static extern int GetInfo(IntPtr vol, ref StorageVolInfo info);

        /// <summary>
        /// Fetch the storage volume key.
        /// This is globally unique, so the same volume will have the same key no matter what host it is accessed from
        /// </summary>
        /// <param name="vol">A <see cref="IntPtr"/>pointer to storage volume.</param>
        /// <returns>The volume key, or NULL on error.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolGetKey")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetKey(IntPtr vol);

        /// <summary>
        /// Fetch the storage volume name. This is unique within the scope of a pool.
        /// </summary>
        /// <param name="vol">A <see cref="IntPtr"/>pointer to storage volume.</param>
        /// <returns>The volume name, or NULL on error.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolGetName")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetName(IntPtr vol);

        /// <summary>
        /// Fetch the storage volume path.
        /// Depending on the pool configuration this is either persistent across hosts,
        /// or dynamically assigned at pool startup.
        /// Consult pool documentation for information on getting the persistent naming.
        /// </summary>
        /// <param name="vol">A <see cref="IntPtr"/>pointer to storage volume.</param>
        /// <returns>
        /// The storage volume path, or NULL on error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolGetPath")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetPath(IntPtr vol);

        /// <summary>
        /// Fetch an XML document describing all aspects of the storage volume.
        /// </summary>
        /// <param name="vol">A <see cref="IntPtr"/>pointer to storage volume.</param>
        /// <param name="flags">A <see cref="System.UInt32"/>flags for XML generation (unused, pass 0).</param>
        /// <returns>The XML document, or NULL on error.</returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolGetXMLDesc")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringWithoutNativeCleanUpMarshaler))]
        public static extern string GetXMLDesc(IntPtr vol, uint flags);

        /// <summary>
        /// Fetch a pointer to a storage volume based on its globally unique key.
        /// </summary>
        /// <param name="conn">A <see cref="IntPtr"/>pointer to hypervisor connection.</param>
        /// <param name="key">A <see cref="System.String"/>globally unique key.</param>
        /// <returns>
        /// A <see cref="IntPtr"/>storage volume, or NULL if not found / error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolLookupByKey")]
        public static extern IntPtr LookupByKey(IntPtr conn, string key);

        /// <summary>
        /// Fetch a pointer to a storage volume based on its name within a pool.
        /// </summary>
        /// <param name="pool">A <see cref="IntPtr"/>pointer to storage pool.</param>
        /// <param name="name">A <see cref="System.String"/>name of storage volume.</param>
        /// <returns>
        /// A <see cref="IntPtr"/>storage volume, or NULL if not found / error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolLookupByName")]
        public static extern IntPtr LookupByName(IntPtr pool, string name);

        /// <summary>
        /// Fetch a pointer to a storage volume based on its locally (host) unique path.
        /// </summary>
        /// <param name="conn">A <see cref="IntPtr"/>pointer to hypervisor connection.</param>
        /// <param name="path">A <see cref="System.String"/>locally unique path.</param>
        /// <returns>
        /// A <see cref="IntPtr"/>storage volume, or NULL if not found / error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolLookupByPath")]
        public static extern IntPtr LookupByPath(IntPtr conn, string path);

        /// <summary>
        /// Increment the reference count on the vol. For each additional call to this method,
        /// there shall be a corresponding call to virStorageVolFree to release the reference count,
        /// once the caller no longer needs the reference to this object.
        /// This method is typically useful for applications where multiple threads are using a connection,
        /// and it is required that the connection remain open until all threads have finished using it. ie,
        /// each new thread using a vol would increment the reference count.
        /// </summary>
        /// <param name="vol">A <see cref="IntPtr"/>the vol to hold a reference on.</param>
        /// <returns>
        /// A <see cref="System.Int32"/>0 in case of success, -1 in case of failure.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virStorageVolRef")]
        public static extern int Ref(IntPtr vol);
    }
}
