/***************************************************************************
 *  Copyright (C) 2009, 2010 by Peter L Jones                              *
 *  pljones@users.sf.net                                                   *
 *                                                                         *
 *  This file is part of the Sims 3 Package Interface (s3pi)               *
 *                                                                         *
 *  s3pi is free software: you can redistribute it and/or modify           *
 *  it under the terms of the GNU General Public License as published by   *
 *  the Free Software Foundation, either version 3 of the License, or      *
 *  (at your option) any later version.                                    *
 *                                                                         *
 *  s3pi is distributed in the hope that it will be useful,                *
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of         *
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the          *
 *  GNU General Public License for more details.                           *
 *                                                                         *
 *  You should have received a copy of the GNU General Public License      *
 *  along with s3pi.  If not, see <http://www.gnu.org/licenses/>.          *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using Sims.Toolkit.Core.Tools.Interfaces;

namespace Sims.Toolkit.Core.Tools.Packages
{
    /// <summary>
    /// Implementation of an index entry
    /// </summary>
    public class ResourceIndexEntry : AResourceIndexEntry
    {
        private const int recommendedApiVersion = 2;

        /// <summary>
        /// The version of the API in use
        /// </summary>
        public override int RecommendedApiVersion => recommendedApiVersion;

        //No ContentFields override as we don't want to make anything more public than AResourceIndexEntry provides

        /// <summary>
        /// The "type" of the resource
        /// </summary>
        [MinimumVersion(1)]
        [MaximumVersion(recommendedApiVersion)]
        public override uint ResourceType
        {
            get => BitConverter.ToUInt32(indexEntry, 4);
            set { var src = BitConverter.GetBytes(value); Array.Copy(src, 0, indexEntry, 4, src.Length); OnElementChanged(); }
        }

        /// <summary>
        /// The "group" the resource is part of
        /// </summary>
        [MinimumVersion(1)]
        [MaximumVersion(recommendedApiVersion)]
        public override uint ResourceGroup
        {
            get { return BitConverter.ToUInt32(indexEntry, 8); }
            set { var src = BitConverter.GetBytes(value); Array.Copy(src, 0, indexEntry, 8, src.Length); OnElementChanged(); }
        }
        /// <summary>
        /// The "instance" number of the resource
        /// </summary>
        [MinimumVersion(1)]
        [MaximumVersion(recommendedApiVersion)]
        public override ulong Instance
        {
            get { return ((ulong)BitConverter.ToUInt32(indexEntry, 12) << 32) | BitConverter.ToUInt32(indexEntry, 16); }
            set
            {
                var src = BitConverter.GetBytes((uint)(value >> 32)); Array.Copy(src, 0, indexEntry, 12, src.Length);
                src = BitConverter.GetBytes((uint)(value & 0xffffffff)); Array.Copy(src, 0, indexEntry, 16, src.Length);
                OnElementChanged();
            }
        }
        /// <summary>
        /// If the resource was read from a package, the location in the package the resource was read from
        /// </summary>
        [MinimumVersion(1)]
        [MaximumVersion(recommendedApiVersion)]
        public override uint Chunkoffset
        {
            get { return BitConverter.ToUInt32(indexEntry, 20); }
            set { var src = BitConverter.GetBytes(value); Array.Copy(src, 0, indexEntry, 20, src.Length); OnElementChanged(); }
        }
        /// <summary>
        /// The number of bytes the resource uses within the package
        /// </summary>
        [MinimumVersion(1)]
        [MaximumVersion(recommendedApiVersion)]
        public override uint Filesize
        {
            get { return BitConverter.ToUInt32(indexEntry, 24) & 0x7fffffff; }
            set { var src = BitConverter.GetBytes(value | 0x80000000); Array.Copy(src, 0, indexEntry, 24, src.Length); OnElementChanged(); OnElementChanged(); }
        }
        /// <summary>
        /// The number of bytes the resource uses in memory
        /// </summary>
        [MinimumVersion(1)]
        [MaximumVersion(recommendedApiVersion)]
        public override uint Memsize
        {
            get { return BitConverter.ToUInt32(indexEntry, 28); }
            set { var src = BitConverter.GetBytes(value); Array.Copy(src, 0, indexEntry, 28, src.Length); OnElementChanged(); }
        }
        /// <summary>
        /// 0x0000 if not compressed, should be 0x5A42 if compressed
        /// </summary>
        [MinimumVersion(1)]
        [MaximumVersion(recommendedApiVersion)]
        public override ushort Compressed
        {
            get { return BitConverter.ToUInt16(indexEntry, 32); }
            set
            {
                if (value != BitConverter.ToUInt16(indexEntry, 32)) compressionChanged = !compressionChanged;
                var src = BitConverter.GetBytes(value); Array.Copy(src, 0, indexEntry, 32, src.Length); OnElementChanged();
            }
        }
        /// <summary>
        /// Returns the compression the resource is currently using if it has been changed since the last save
        /// </summary>
        public ushort OriginalCompression
        {
            get
            {
                if (compressionChanged) return (ushort)(Compressed == 0x0000 ? 0x5A42 : 0x0000);
                return Compressed;
            }
        }
        /// <summary>
        /// Always 0x0001
        /// </summary>
        [MinimumVersion(1)]
        [MaximumVersion(recommendedApiVersion)]
        public override ushort Unknown2
        {
            get { return BitConverter.ToUInt16(indexEntry, 34); }
            set { var src = BitConverter.GetBytes(value); Array.Copy(src, 0, indexEntry, 34, src.Length); OnElementChanged(); }
        }

        /// <summary>
        /// A MemoryStream covering the index entry bytes
        /// </summary>
        [MinimumVersion(1)]
        [MaximumVersion(recommendedApiVersion)]
        public override Stream Stream { get { return new MemoryStream(indexEntry); } }

        /// <summary>
        /// True if the index entry has been deleted from the package index
        /// </summary>
        [MinimumVersion(1)]
        [MaximumVersion(recommendedApiVersion)]
        public override bool IsDeleted { get { return isDeleted; } set { if (isDeleted != value) { isDeleted = value; OnElementChanged(); } } }

        /// <summary>
        /// Get a copy of this element but with a new change event handler
        /// </summary>
        /// <param name="handler">Element change event handler</param>
        /// <returns>Return a copy of this element but with a new change event handler</returns>
        [MinimumVersion(1)]
        [MaximumVersion(recommendedApiVersion)]
        public override AHandlerElement Clone(EventHandler handler) { return new ResourceIndexEntry(indexEntry); }

        /// <summary>
        /// Indicates whether the current <see cref="ResourceIndexEntry"/> instance is equal to another <see cref="IResourceIndexEntry"/> instance.
        /// </summary>
        /// <param name="other">An <see cref="IResourceIndexEntry"/> instance to compare with this instance.</param>
        /// <returns>true if the current instance is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        public override bool Equals(IResourceIndexEntry other)
        {
            return (other is ResourceIndexEntry entry) && indexEntry.Equals<byte>(entry.indexEntry);
        }
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() => indexEntry.GetHashCode();

        /// <summary>
        /// The index entry data
        /// </summary>
        private readonly byte[] indexEntry;

        /// <summary>
        /// True if the index entry should be treated as deleted
        /// </summary>
        private bool isDeleted;

        /// <summary>
        /// True if the user has requested a change in compression
        /// </summary>
        private bool compressionChanged;

        /// <summary>
        /// The uncompressed resource data associated with this index entry
        /// (used to save having to uncompress the same entry again if it's requested more than once)
        /// </summary>
        private Stream resourceStream;

        /// <summary>
        /// Create a new index entry as a byte-for-byte copy of <paramref name="indexEntry"/>
        /// </summary>
        /// <param name="indexEntry">The source index entry</param>
        private ResourceIndexEntry(byte[] indexEntry) { this.indexEntry = (byte[])indexEntry.Clone(); }

        /// <summary>
        /// Create a new expanded index entry from the header and entry data passed
        /// </summary>
        /// <param name="header">header ints (same for each index entry); [0] is the index type</param>
        /// <param name="entry">entry ints (specific to this entry)</param>
        internal ResourceIndexEntry(IReadOnlyList<int> header, IReadOnlyList<int> entry)
        {
            indexEntry = new byte[(header.Count + entry.Count) * 4];
            var ms = new MemoryStream(indexEntry);
            var w = new BinaryWriter(ms);

            w.Write(header[0]);

            var hc = 1;// header[0] is indexType, already written!
            var ec = 0;
            var IhGT = (uint)header[0];
            w.Write((IhGT & 0x01) != 0 ? header[hc++] : entry[ec++]);
            w.Write((IhGT & 0x02) != 0 ? header[hc++] : entry[ec++]);
            w.Write((IhGT & 0x04) != 0 ? header[hc++] : entry[ec++]);

            for (; hc < header.Count - 1; hc++)
                w.Write(header[hc]);

            for (; ec < entry.Count; ec++)
                w.Write(entry[ec]);
        }

        /// <summary>
        /// Return a new index entry as a copy of this one
        /// </summary>
        /// <returns>A copy of this index entry</returns>
        internal ResourceIndexEntry Clone() { return (ResourceIndexEntry)this.Clone(null); }

        /// <summary>
        /// Flag this index entry as deleted
        /// </summary>
        /// <remarks>Use APackage.RemoveResource() from user code</remarks>
        internal void Delete()
        {
            if (Settings.Settings.Checking && isDeleted) throw new InvalidOperationException("Index entry already deleted!");

            isDeleted = true;
            OnElementChanged();
        }

        /// <summary>
        /// The uncompressed resource data associated with this index entry
        /// (used to save having to uncompress the same entry again if it's requested more than once)
        /// Setting the stream updates the Memsize
        /// </summary>
        /// <remarks>Use Package.ReplaceResource() from user code</remarks>
        internal Stream ResourceStream
        {
            get => resourceStream;
            set
            {
                if (resourceStream == value) return;
                resourceStream = value; if (Memsize != (uint)resourceStream.Length) Memsize = (uint)resourceStream.Length; else OnElementChanged();
            }
        }

        /// <summary>
        /// True if the index entry should be treated as dirty - e.g. the ResourceStream has been replaced
        /// </summary>
        internal bool IsDirty => dirty;
    }
}
