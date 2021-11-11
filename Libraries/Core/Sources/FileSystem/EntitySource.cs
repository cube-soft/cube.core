﻿/* ------------------------------------------------------------------------- */
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
using System.IO;
using Cube.Mixin.Generic;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// EntitySource
    ///
    /// <summary>
    /// Represents the file or directory information that is editable.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class EntitySource : DisposableBase
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
        public EntitySource(string src) : this(src, true) { }

        /* ----------------------------------------------------------------- */
        ///
        /// EntitySource
        ///
        /// <summary>
        /// Initializes a new instance of the EntitySource class with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="src">Path of the file or directory.</param>
        /// <param name="refresh">
        /// Value indicating whether to invoke the Refresh method.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected EntitySource(string src, bool refresh)
        {
            RawName = src;
            if (refresh) Refresh();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// RawName
        ///
        /// <summary>
        /// Gets the original path.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string RawName { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Exists
        ///
        /// <summary>
        /// Gets or sets the value indicating whether the Source exists.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Exists { get; protected set; }

        /* ----------------------------------------------------------------- */
        ///
        /// IsDirectory
        ///
        /// <summary>
        /// Gets or sets the value indicating whether the Source is
        /// directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsDirectory { get; protected set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name { get; protected set; }

        /* ----------------------------------------------------------------- */
        ///
        /// BaseName
        ///
        /// <summary>
        /// Gets or sets the filename without extension.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string BaseName { get; protected set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Extension
        ///
        /// <summary>
        /// Gets or sets the extension part of the filename.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Extension { get; protected set; }

        /* ----------------------------------------------------------------- */
        ///
        /// FullName
        ///
        /// <summary>
        /// Gets or sets the full path.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string FullName { get; protected set; }

        /* ----------------------------------------------------------------- */
        ///
        /// DirectoryName
        ///
        /// <summary>
        /// Gets or sets the directory name.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string DirectoryName { get; protected set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Length
        ///
        /// <summary>
        /// Gets or sets the file-size.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public long Length { get; protected set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Attributes
        ///
        /// <summary>
        /// Gets or sets the attributes of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FileAttributes Attributes { get; protected set; }

        /* ----------------------------------------------------------------- */
        ///
        /// CreationTime
        ///
        /// <summary>
        /// Gets or sets the creation time of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime CreationTime { get; protected set; }

        /* ----------------------------------------------------------------- */
        ///
        /// LastWriteTime
        ///
        /// <summary>
        /// Gets or sets the last written time of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastWriteTime { get; protected set; }

        /* ----------------------------------------------------------------- */
        ///
        /// LastAccessTime
        ///
        /// <summary>
        /// Gets or sets the last accessed time of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastAccessTime { get; protected set; }

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
        public void Refresh() => OnRefresh();

        /* ----------------------------------------------------------------- */
        ///
        /// OnRefresh
        ///
        /// <summary>
        /// Refreshes the file or directory information.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnRefresh()
        {
            FileSystemInfo obj = Directory.Exists(RawName) ?
                                 new DirectoryInfo(RawName) :
                                 new FileInfo(RawName);

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

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the object and
        /// optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing) { }

        #endregion
    }
}
