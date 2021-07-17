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
    /// EntityController
    ///
    /// <summary>
    /// Provides functionality to refresh properties of a EntityControllable
    /// object by using the AlphaFS module.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public class EntitySource : FileSystem.EntitySource
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// EntitySource
        ///
        /// <summary>
        /// Initializes a new instance of the EntitySource class with the
        /// specified path.
        /// </summary>
        ///
        /// <param name="src">Path of the file or directory.</param>
        ///
        /* ----------------------------------------------------------------- */
        public EntitySource(string src) : base(src) { }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        ///
        /// <summary>
        /// Refreshes the file or directory information.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override void Refresh()
        {
            var obj = Create();

            Exists         = obj.Exists;
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

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Creates a new instance of the FileSystemInfo inherited class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private FileSystemInfo Create() =>
            Directory.Exists(RawName) ? new DirectoryInfo(RawName) : new FileInfo(RawName);

        #endregion
    }
}
