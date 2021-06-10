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
using Cube.Mixin.Logging;
using Source = Cube.FileSystem.IO;

namespace Cube.Backports
{
    /* --------------------------------------------------------------------- */
    ///
    /// IoExtension
    ///
    /// <summary>
    /// Provides extended methods for the IO class for compatibility with
    /// .NET Framework 3.5.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class IoExtension
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
        /* ----------------------------------------------------------------- */
        public static T Load<T>(this Source io, string src, Func<TextReader, T> callback)
        {
            var code = System.Text.Encoding.UTF8;
            using var ss = new StreamReader(io.OpenRead(src), code);
            return callback(ss);
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
        /* ----------------------------------------------------------------- */
        public static T LoadOrDefault<T>(this Source io, string src, Func<TextReader, T> callback, T error)
        {
            try { return io.Load(src, callback); }
            catch (Exception e) { io.GetType().LogWarn(e); }
            return error;
        }

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
        public static void Save(this Source io, string dest, Action<TextWriter> callback)
        {
            var code = System.Text.Encoding.UTF8;
            using var ss = new StreamWriter(new MemoryStream(), code);
            callback(ss);

            using var ds = io.Create(dest);
            ss.BaseStream.Position = 0;
            ss.BaseStream.CopyTo(ds);
        }

        #endregion
    }
}
