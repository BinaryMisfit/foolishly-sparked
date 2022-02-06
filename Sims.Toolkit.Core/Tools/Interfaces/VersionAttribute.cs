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

namespace Sims.Toolkit.Core.Tools.Interfaces
{
    /// <summary>
    /// Base class for versioning support.  Not directly used by the API.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class VersionAttribute : Attribute
    {
        /// <summary>
        /// Version number attribute (base)
        /// </summary>
        /// <param name="Version">Version number</param>
        protected VersionAttribute(int Version)
        {
            this.Version = Version;
        }

        /// <summary>
        /// Version number
        /// </summary>
        public int Version { get; set; }
    }

    /// <summary>
    /// Specify the Minimum version from which a field or method is supported
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class MinimumVersionAttribute : VersionAttribute
    {
        /// <summary>
        /// Specify the Minimum version from which a field or method is supported
        /// </summary>
        /// <param name="Version">Version number</param>
        public MinimumVersionAttribute(int Version) : base(Version)
        {
        }
    }

    /// <summary>
    /// Specify the Maximum version up to which a field or method is supported
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class MaximumVersionAttribute : VersionAttribute
    {
        /// <summary>
        /// Specify the Maximum version up to which a field or method is supported
        /// </summary>
        /// <param name="Version">Version number</param>
        public MaximumVersionAttribute(int Version) : base(Version)
        {
        }
    }
}
