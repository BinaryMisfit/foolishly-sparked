﻿/***************************************************************************
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

namespace Sims.Toolkit.Core.Tools.Settings
{
    /// <summary>
    /// Holds global settings, currently statically defined
    /// </summary>
    public static class Settings
    {
        static Settings()
        {
            // initialisation code, like read from settings file...
        }

        /// <summary>
        /// When true, run extra checks as part of normal operation.
        /// </summary>
        public static bool Checking => true;

        /// <summary>
        /// When true, assume data is dirty regardless of tracking.
        /// </summary>
        public static bool AsBytesWorkaround => true;

        /// <summary>
        /// Indicate whether the wrapper should use TS4 format to decode files.
        /// </summary>
        public static bool IsTS4 => true;
    }
}