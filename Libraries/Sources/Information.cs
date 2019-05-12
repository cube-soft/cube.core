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
    /// Information
    ///
    /// <summary>
    /// Represents the file or directory information.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public class Information
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Information
        ///
        /// <summary>
        /// Creates a new instance of the Information class with the
        /// specified path.
        /// </summary>
        ///
        /// <param name="src">Path of the source file.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Information(string src) : this(src, new Controller()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Information
        ///
        /// <summary>
        /// Creates a new instance of the Information class with the
        /// specified object.
        /// </summary>
        ///
        /// <param name="src">Copied information.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Information(Information src) : this(src.Source, src.Controller) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Information
        ///
        /// <summary>
        /// Creates a new instance of the Information class with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="src">Path of the source file.</param>
        /// <param name="controller">Refresher object.</param>
        /// <param name="options">Optional parameters.</param>
        ///
        /// <remarks>
        /// options は Controller の継承クラス等の拡張用に定義しています。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public Information(string src, Controller controller, params object[] options)
        {
            Controller   = controller;
            Controllable = controller.Create(src, options);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Controllable
        ///
        /// <summary>
        /// Gets the inner object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected Controllable Controllable { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Controller
        ///
        /// <summary>
        /// Gets the controller object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected Controller Controller { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Source
        ///
        /// <summary>
        /// Gets the original path.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Source => Controllable.Source;

        /* ----------------------------------------------------------------- */
        ///
        /// Exists
        ///
        /// <summary>
        /// Gets the value indicating whether the Source exists.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Exists => Controllable.Exists;

        /* ----------------------------------------------------------------- */
        ///
        /// IsDirectory
        ///
        /// <summary>
        /// ディレクトリかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsDirectory => Controllable.IsDirectory;

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// Gets the filename.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name => Controllable.Name;

        /* ----------------------------------------------------------------- */
        ///
        /// BaseName
        ///
        /// <summary>
        /// Gets the filename without extension.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string BaseName => Controllable.BaseName;

        /* ----------------------------------------------------------------- */
        ///
        /// Extension
        ///
        /// <summary>
        /// Gets the extension part of the filename.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Extension => Controllable.Extension;

        /* ----------------------------------------------------------------- */
        ///
        /// FullName
        ///
        /// <summary>
        /// Gets the full path.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string FullName => Controllable.FullName;

        /* ----------------------------------------------------------------- */
        ///
        /// DirectoryName
        ///
        /// <summary>
        /// Gets the directory name.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string DirectoryName => Controllable.DirectoryName;

        /* ----------------------------------------------------------------- */
        ///
        /// Length
        ///
        /// <summary>
        /// Gets the filesize.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public long Length => Controllable.Length;

        /* ----------------------------------------------------------------- */
        ///
        /// Attributes
        ///
        /// <summary>
        /// Gets the attributes of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FileAttributes Attributes => Controllable.Attributes;

        /* ----------------------------------------------------------------- */
        ///
        /// CreationTime
        ///
        /// <summary>
        /// Gets the creation time of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime CreationTime => Controllable.CreationTime;

        /* ----------------------------------------------------------------- */
        ///
        /// LastWriteTime
        ///
        /// <summary>
        /// Gets the last written time of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastWriteTime => Controllable.LastWriteTime;

        /* ----------------------------------------------------------------- */
        ///
        /// LastAccessTime
        ///
        /// <summary>
        /// Gets the last accessed time of the file or directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastAccessTime => Controllable.LastAccessTime;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        ///
        /// <summary>
        /// Refreshes file or directory information.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Refresh() => Controller.Refresh(Controllable);

        #endregion
    }
}
