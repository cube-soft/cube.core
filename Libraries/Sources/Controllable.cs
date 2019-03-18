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
    /// Controllable
    ///
    /// <summary>
    /// Represents the controllable file or directory information.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public class Controllable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Controllable
        ///
        /// <summary>
        /// Initializes a new instance of the Controllable class with
        /// the specified path.
        /// </summary>
        ///
        /// <param name="src">Path of the file or directory.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected internal Controllable(string src)
        {
            Source = src;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Source
        ///
        /// <summary>
        /// Gets the original path.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Source { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Exists
        ///
        /// <summary>
        /// Gets or sets the value indicating whether the Source exists.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Exists { get; set; }

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
        public bool IsDirectory { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// NameWithoutExtension
        ///
        /// <summary>
        /// Gets or sets the filename without extension.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string NameWithoutExtension { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Extension
        ///
        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Extension { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// FullName
        ///
        /// <summary>
        /// Gets or sets the full path.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string FullName { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// DirectoryName
        ///
        /// <summary>
        /// Gets or sets the directory name.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string DirectoryName { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Length
        ///
        /// <summary>
        /// Gets or sets the filesize.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public long Length { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Attributes
        ///
        /// <summary>
        /// Gets or sets the attributes of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FileAttributes Attributes { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// CreationTime
        ///
        /// <summary>
        /// Gets or sets the creation time of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime CreationTime { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// LastWriteTime
        ///
        /// <summary>
        /// Gets or sets the last written time of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastWriteTime { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// LastAccessTime
        ///
        /// <summary>
        /// Gets or sets the last accessed time of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastAccessTime { get; set; }

        #endregion
    }
}
