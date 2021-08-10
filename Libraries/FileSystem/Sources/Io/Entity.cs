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
using System.IO;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// Entity
    ///
    /// <summary>
    /// Represents the file or directory information.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public class Entity
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Entity
        ///
        /// <summary>
        /// Creates a new instance of the Information class with the
        /// specified source object.
        /// </summary>
        ///
        /// <param name="src">EntitySource object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Entity(EntitySource src) { Source = src; }

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
        public string RawName => Source.RawName;

        /* ----------------------------------------------------------------- */
        ///
        /// Exists
        ///
        /// <summary>
        /// Gets the value indicating whether the Source exists.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Exists => Source.Exists;

        /* ----------------------------------------------------------------- */
        ///
        /// IsDirectory
        ///
        /// <summary>
        /// Gets a value indicating whether the provided path is a directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsDirectory => Source.IsDirectory;

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// Gets the filename.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name => Source.Name;

        /* ----------------------------------------------------------------- */
        ///
        /// BaseName
        ///
        /// <summary>
        /// Gets the filename without extension.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string BaseName => Source.BaseName;

        /* ----------------------------------------------------------------- */
        ///
        /// Extension
        ///
        /// <summary>
        /// Gets the extension part of the filename.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Extension => Source.Extension;

        /* ----------------------------------------------------------------- */
        ///
        /// FullName
        ///
        /// <summary>
        /// Gets the full path.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string FullName => Source.FullName;

        /* ----------------------------------------------------------------- */
        ///
        /// DirectoryName
        ///
        /// <summary>
        /// Gets the directory name.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string DirectoryName => Source.DirectoryName;

        /* ----------------------------------------------------------------- */
        ///
        /// Length
        ///
        /// <summary>
        /// Gets the file-size.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public long Length => Source.Length;

        /* ----------------------------------------------------------------- */
        ///
        /// Attributes
        ///
        /// <summary>
        /// Gets the attributes of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FileAttributes Attributes => Source.Attributes;

        /* ----------------------------------------------------------------- */
        ///
        /// CreationTime
        ///
        /// <summary>
        /// Gets the creation time of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime CreationTime => Source.CreationTime;

        /* ----------------------------------------------------------------- */
        ///
        /// LastWriteTime
        ///
        /// <summary>
        /// Gets the last written time of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastWriteTime => Source.LastWriteTime;

        /* ----------------------------------------------------------------- */
        ///
        /// LastAccessTime
        ///
        /// <summary>
        /// Gets the last accessed time of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastAccessTime => Source.LastAccessTime;

        /* ----------------------------------------------------------------- */
        ///
        /// Source
        ///
        /// <summary>
        /// Gets the inner object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected EntitySource Source { get; }

        #endregion
    }
}
