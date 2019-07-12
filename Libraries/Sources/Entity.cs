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
        /// Creates a new instance of the Entity class with the
        /// specified path of file or directory.
        /// </summary>
        ///
        /// <param name="src">Path of file or directory.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Entity(string src) : this(src, new Controller()) { }

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
        public Entity(Entity src) : this(src.Source, src.Controller) { }

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
        /// options for the Controller inherited classes.
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public Entity(string src, Controller controller, params object[] options)
        {
            Controller   = controller;
            Controllable = controller.Create(src, options);
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
        /// Gets a value indicating whether the provided path is a directory.
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
        /// Gets the file-size.
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

        #region Core

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

        #endregion

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
