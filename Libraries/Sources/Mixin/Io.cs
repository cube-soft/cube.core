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
using Cube.FileSystem;
using Cube.Mixin.Logger;
using Cube.Mixin.String;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Source = Cube.FileSystem.IO;

namespace Cube.Mixin.IO
{
    /* --------------------------------------------------------------------- */
    ///
    /// Extension
    ///
    /// <summary>
    /// Provides extended methods of the IO class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Extension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// Creates a new stream from the specified file and executes
        /// the specified callback.
        /// </summary>
        ///
        /// <param name="io">I/O handler.</param>
        /// <param name="src">Path of the source file.</param>
        /// <param name="callback">User action.</param>
        ///
        /// <returns>Executed result.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Load<T>(this Source io, string src, Func<Stream, T> callback)
        {
            using (var ss = io.OpenRead(src)) return callback(ss);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadOrDefault
        ///
        /// <summary>
        /// Creates a new stream from the specified file and executes
        /// the specified callback. When an exception occurs, returns
        /// the specified object.
        /// </summary>
        ///
        /// <param name="io">I/O handler.</param>
        /// <param name="src">Path of the reading file.</param>
        /// <param name="callback">User action.</param>
        /// <param name="error">
        /// Returned object when an exception occurs.
        /// </param>
        ///
        /// <returns>Executed result.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T LoadOrDefault<T>(this Source io, string src, Func<Stream, T> callback, T error) =>
            io.LogWarn(() => io.Load(src, callback), error);

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// Creates a new memory stream, executes the specified callback,
        /// and writes the result to the specified file.
        /// </summary>
        ///
        /// <param name="io">I/O handler.</param>
        /// <param name="dest">Path of the writing file.</param>
        /// <param name="callback">User action.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Save(this Source io, string dest, Action<Stream> callback)
        {
            using (var ss = new MemoryStream())
            {
                callback(ss);
                using (var ds = io.Create(dest))
                {
                    ss.Position = 0;
                    ss.CopyTo(ds);
                }
            }
        }

        #region GetTypeName

        /* ----------------------------------------------------------------- */
        ///
        /// GetTypeName
        ///
        /// <summary>
        /// Gets a value that represents kind of the specified file.
        /// </summary>
        ///
        /// <param name="io">I/O handler.</param>
        /// <param name="info">File information.</param>
        ///
        /// <returns>Typename of the file.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetTypeName(this Source io, Information info) =>
            GetTypeName(io, info?.FullName);

        /* ----------------------------------------------------------------- */
        ///
        /// GetTypeName
        ///
        /// <summary>
        /// Gets a value that represents type of the specified file.
        /// </summary>
        ///
        /// <param name="io">I/O handler.</param>
        /// <param name="path">Path of the source file.</param>
        ///
        /// <returns>Typename of the file.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetTypeName(this Source io, string path)
        {
            if (!path.HasValue()) return string.Empty;

            var dest   = new Cube.FileSystem.Shell32.SHFILEINFO();
            var status = Cube.FileSystem.Shell32.NativeMethods.SHGetFileInfo(
                path,
                0x0080, // FILE_ATTRIBUTE_NORMAL
                ref dest,
                (uint)Marshal.SizeOf(dest),
                0x0410 // SHGFI_TYPENAME | SHGFI_USEFILEATTRIBUTES
            );

            return (status != IntPtr.Zero) ? dest.szTypeName : string.Empty;
        }

        #endregion

        #region GetUniqueName

        /* ----------------------------------------------------------------- */
        ///
        /// GetUniqueName
        ///
        /// <summary>
        /// Gets a unique name with the specified path.
        /// </summary>
        ///
        /// <param name="io">I/O handler.</param>
        /// <param name="path">Base path.</param>
        ///
        /// <returns>Unique name.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetUniqueName(this Source io, string path) =>
            GetUniqueName(io, io.Get(path));

        /* ----------------------------------------------------------------- */
        ///
        /// GetUniqueName
        ///
        /// <summary>
        /// Gets a unique name with the specified file information.
        /// </summary>
        ///
        /// <param name="io">I/O handler.</param>
        /// <param name="src">Base information.</param>
        ///
        /// <returns>Unique name.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetUniqueName(this Source io, Information src)
        {
            if (src == null) return null;
            if (!src.Exists) return src.FullName;

            var dir  = src.DirectoryName;
            var name = src.NameWithoutExtension;
            var ext  = src.Extension;

            return Enumerable.Range(1, int.MaxValue)
                             .Select(e => io.Combine(dir, $"{name} ({e}){ext}"))
                             .First(e => !io.Exists(e));
        }

        #endregion

        #endregion
    }
}
