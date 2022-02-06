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

using System.Collections.Generic;
using System.IO;
using Sims.Toolkit.Core.Tools.Interfaces;

namespace Sims.Toolkit.Core.Tools.Packages
{
    /// <summary>
    /// Internal -- used by Package to manage the package index
    /// </summary>
    internal class PackageIndex : List<IResourceIndexEntry>
    {
        private const int numFields = 9;

        public uint Indextype { get; }

        int Hdrsize
        {
            get
            {
                int hc = 1;
                for (int i = 0; i < sizeof(uint); i++)
                    if ((Indextype & (1 << i)) != 0)
                        hc++;
                return hc;
            }
        }

        public PackageIndex()
        {
        }

        public PackageIndex(uint type)
        {
            Indextype = type;
        }

        public PackageIndex(Stream s, int indexposition, int indexcount)
        {
            if (s == null) return;
            if (indexposition == 0) return;

            BinaryReader r = new BinaryReader(s);
            s.Position = indexposition;
            Indextype = r.ReadUInt32();

            int[] hdr = new int[Hdrsize];
            int[] entry = new int[numFields - Hdrsize];

            hdr[0] = (int) Indextype;
            for (int i = 1; i < hdr.Length; i++)
                hdr[i] = r.ReadInt32();

            for (int i = 0; i < indexcount; i++)
            {
                for (int j = 0; j < entry.Length; j++)
                    entry[j] = r.ReadInt32();
                base.Add(new ResourceIndexEntry(hdr, entry));
            }
        }

        public IResourceIndexEntry Add(IResourceKey rk)
        {
            ResourceIndexEntry rc = new ResourceIndexEntry(new int[Hdrsize], new int[numFields - Hdrsize]);

            rc.ResourceType = rk.ResourceType;
            rc.ResourceGroup = rk.ResourceGroup;
            rc.Instance = rk.Instance;
            rc.Chunkoffset = 0xffffffff;
            rc.Unknown2 = 1;
            rc.ResourceStream = null;

            base.Add(rc);
            return rc;
        }

        public int Size
        {
            get { return (Count * (numFields - Hdrsize) + Hdrsize) * 4; }
        }

        public void Save(BinaryWriter w)
        {
            BinaryReader r;
            if (Count == 0)
            {
                r = new BinaryReader(new MemoryStream(new byte[numFields * 4]));
            }
            else
            {
                r = new BinaryReader(this[0].Stream);
            }

            r.BaseStream.Position = 4;
            w.Write(Indextype);
            if ((Indextype & 0x01) != 0) w.Write(r.ReadUInt32());
            else r.BaseStream.Position += 4;
            if ((Indextype & 0x02) != 0) w.Write(r.ReadUInt32());
            else r.BaseStream.Position += 4;
            if ((Indextype & 0x04) != 0) w.Write(r.ReadUInt32());
            else r.BaseStream.Position += 4;

            foreach (var ie in this)
            {
                r = new BinaryReader(ie.Stream);
                r.BaseStream.Position = 4;
                if ((Indextype & 0x01) == 0) w.Write(r.ReadUInt32());
                else r.BaseStream.Position += 4;
                if ((Indextype & 0x02) == 0) w.Write(r.ReadUInt32());
                else r.BaseStream.Position += 4;
                if ((Indextype & 0x04) == 0) w.Write(r.ReadUInt32());
                else r.BaseStream.Position += 4;
                w.Write(r.ReadBytes((int) (ie.Stream.Length - ie.Stream.Position)));
            }
        }

        /// <summary>
        /// Sort the index by the given field
        /// </summary>
        /// <param name="index">Field to sort by</param>
        public void Sort(string index)
        {
            base.Sort(new AApiVersionedFields.Comparer<IResourceIndexEntry>(index));
        }

        /// <summary>
        /// Return the index entry with the matching TGI
        /// </summary>
        /// <param name="type">Entry type</param>
        /// <param name="group">Entry group</param>
        /// <param name="instance">Entry instance</param>
        /// <returns>Matching entry</returns>
        public IResourceIndexEntry this[uint type, uint group, ulong instance]
        {
            get
            {
                foreach (IResourceIndexEntry rie in this)
                {
                    if (rie.ResourceType != type) continue;
                    if (rie.ResourceGroup != group) continue;
                    if (rie.Instance == instance) return rie;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns requested resource, ignoring EPFlags
        /// </summary>
        /// <param name="rk">Resource key to find</param>
        /// <returns>Matching entry</returns>
        public IResourceIndexEntry this[IResourceKey rk]
        {
            get { return this[rk.ResourceType, rk.ResourceGroup, rk.Instance]; }
        }
    }
}
