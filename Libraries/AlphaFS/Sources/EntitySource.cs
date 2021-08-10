/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using System;
using Alphaleonis.Win32.Filesystem;
using Cube.Mixin.Generics;

namespace Cube.FileSystem.AlphaFS
{
    /* --------------------------------------------------------------------- */
    ///
    /// EntitySource
    ///
    /// <summary>
    /// Provides functionality to refresh properties of a EntitySource
    /// object by using the AlphaFS module.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class EntitySource : FileSystem.EntitySource
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// EntitySource
        ///
        /// <summary>
        /// Initializes a new instance of the AlphaFS.EntitySource class
        /// with the specified path.
        /// </summary>
        ///
        /// <param name="src">Path of the file or directory.</param>
        ///
        /* ----------------------------------------------------------------- */
        public EntitySource(string src) : this(src, true) { }

        /* ----------------------------------------------------------------- */
        ///
        /// EntitySource
        ///
        /// <summary>
        /// Initializes a new instance of the AlphaFS.EntitySource class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="src">Path of the file or directory.</param>
        /// <param name="refresh">
        /// Value indicating whether to invoke the Refresh method.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected EntitySource(string src, bool refresh) : base(src, refresh) { }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnRefresh
        ///
        /// <summary>
        /// Refreshes the file or directory information.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnRefresh()
        {
            FileSystemInfo obj = Directory.Exists(RawName) ?
                                 new DirectoryInfo(RawName) :
                                 new FileInfo(RawName);

            Exists = obj.Exists;
            Name           = obj.Name;
            Extension      = obj.Extension;
            FullName       = obj.FullName;
            Attributes     = obj.Attributes;
            CreationTime   = obj.CreationTime;
            LastAccessTime = obj.LastAccessTime;
            LastWriteTime  = obj.LastWriteTime;
            Length         = Exists ? (obj.TryCast<FileInfo>()?.Length ?? 0) : 0;
            IsDirectory    = obj is DirectoryInfo;
            BaseName       = Path.GetFileNameWithoutExtension(RawName);
            DirectoryName  = obj.TryCast<FileInfo>()?.DirectoryName ??
                             Path.GetDirectoryName(RawName);
        }

        #endregion
    }
}
