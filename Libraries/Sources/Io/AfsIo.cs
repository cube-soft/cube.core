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
using System.Collections.Generic;
using Alphaleonis.Win32.Filesystem;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// AfsIO
    ///
    /// <summary>
    /// Provides functionality to do something to a file or directory by
    /// using the AlphaFS module.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class AfsIO : IO
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetControllerCore
        ///
        /// <summary>
        /// Gets the Controller object.
        /// </summary>
        ///
        /// <returns>Controller object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected override EntityController GetControllerCore() => _shared ??= new AfsController()
;

        /* ----------------------------------------------------------------- */
        ///
        /// GetFilesCore
        ///
        /// <summary>
        /// Returns the names of files (including their paths) that
        /// match the specified search pattern in the specified directory,
        /// using a value to determine whether to search subdirectories.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override IEnumerable<string> GetFilesCore(string path, string pattern, System.IO.SearchOption option) =>
            Directory.Exists(path) ? Directory.GetFiles(path, pattern, option) : null;

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectoriesCore
        ///
        /// <summary>
        /// Returns the names of the subdirectories (including their paths)
        /// that match the specified search pattern in the specified
        /// directory, and optionally searches subdirectories.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override IEnumerable<string> GetDirectoriesCore(string path, string pattern, System.IO.SearchOption option) =>
            Directory.Exists(path) ? Directory.GetDirectories(path, pattern, option) : null;

        /* ----------------------------------------------------------------- */
        ///
        /// CombineCore
        ///
        /// <summary>
        /// Combiles the specified paths.
        /// </summary>
        ///
        /// <param name="paths">Collection of paths.</param>
        ///
        /// <returns>Combined path.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected override string CombineCore(params string[] paths) =>
            Path.Combine(paths);

        /* ----------------------------------------------------------------- */
        ///
        /// SetAttributesCore
        ///
        /// <summary>
        /// Sets the specified attributes to the specified file or directory.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        /// <param name="attr">Attributes to set.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void SetAttributesCore(string path, System.IO.FileAttributes attr)
        {
            if (Directory.Exists(path)) new DirectoryInfo(path) { Attributes = attr };
            else if (File.Exists(path)) File.SetAttributes(path, attr);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetCreationTimeCore
        ///
        /// <summary>
        /// Sets the specified creation time to the specified file or
        /// directory.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        /// <param name="time">Creation time.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void SetCreationTimeCore(string path, DateTime time)
        {
            if (Directory.Exists(path)) Directory.SetCreationTime(path, time);
            else if (File.Exists(path)) File.SetCreationTime(path, time);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetLastWriteTimeCore
        ///
        /// <summary>
        /// Sets the specified last updated time to the specified file or
        /// directory.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        /// <param name="time">Last updated time.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void SetLastWriteTimeCore(string path, DateTime time)
        {
            if (Directory.Exists(path)) Directory.SetLastWriteTime(path, time);
            else if (File.Exists(path)) File.SetLastWriteTime(path, time);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetLastAccessTimeCore
        ///
        /// <summary>
        /// Sets the specified last accessed time to the specified file or
        /// directory.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        /// <param name="time">Last accessed time.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void SetLastAccessTimeCore(string path, DateTime time)
        {
            if (Directory.Exists(path)) Directory.SetLastAccessTime(path, time);
            else if (File.Exists(path)) File.SetLastAccessTime(path, time);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DeleteCore
        ///
        /// <summary>
        /// Deletes the specified file or directory.
        /// </summary>
        ///
        /// <param name="path">Path to delete.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void DeleteCore(string path)
        {
            if (Directory.Exists(path))
            {
                foreach (var f in Directory.GetFiles(path)) DeleteCore(f);
                foreach (var d in Directory.GetDirectories(path)) DeleteCore(d);
                SetAttributes(path, System.IO.FileAttributes.Normal);
                Directory.Delete(path, false);
            }
            else if (File.Exists(path))
            {
                SetAttributes(path, System.IO.FileAttributes.Normal);
                File.Delete(path);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateCore
        ///
        /// <summary>
        /// Creates or opens the specified file and gets the stream.
        /// </summary>
        ///
        /// <param name="path">Path to create or open file.</param>
        ///
        /// <returns>FileStream object to write.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected override System.IO.FileStream CreateCore(string path) =>
            File.Create(path);

        /* ----------------------------------------------------------------- */
        ///
        /// OpenReadCore
        ///
        /// <summary>
        /// Opens the specified file as read-only.
        /// </summary>
        ///
        /// <param name="path">File path.</param>
        ///
        /// <returns>Read-only stream.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected override System.IO.FileStream OpenReadCore(string path) =>
            File.OpenRead(path);

        /* ----------------------------------------------------------------- */
        ///
        /// OpenWriteCore
        ///
        /// <summary>
        /// Creates or opens the specified file and gets the stream.
        /// </summary>
        ///
        /// <param name="path">Path to create or open file.</param>
        ///
        /// <returns>FileStream object to write.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected override System.IO.FileStream OpenWriteCore(string path) =>
            File.OpenWrite(path);

        /* ----------------------------------------------------------------- */
        ///
        /// CreateDirectoryCore
        ///
        /// <summary>
        /// Creates a directory.
        /// </summary>
        ///
        /// <param name="path">Path to create.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void CreateDirectoryCore(string path) =>
            Directory.CreateDirectory(path);

        /* ----------------------------------------------------------------- */
        ///
        /// MoveCore
        ///
        /// <summary>
        /// Moves the specified file.
        /// </summary>
        ///
        /// <param name="src">Source path.</param>
        /// <param name="dest">Destination path.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void MoveCore(string src, string dest) =>
            File.Move(src, dest);

        /* ----------------------------------------------------------------- */
        ///
        /// CopyCore
        ///
        /// <summary>
        /// Copies the specified file.
        /// </summary>
        ///
        /// <param name="src">Source path.</param>
        /// <param name="dest">Destination path.</param>
        /// <param name="overwrite">Overwrite or not.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void CopyCore(string src, string dest, bool overwrite) =>
            File.Copy(src, dest, overwrite);

        #endregion

        #region Fields
        private static EntityController _shared;
        #endregion
    }
}
