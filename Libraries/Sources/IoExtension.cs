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
using Cube.Generics;
using Cube.Log;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Cube.FileSystem.Mixin
{
    /* --------------------------------------------------------------------- */
    ///
    /// IoExtension
    ///
    /// <summary>
    /// Provides extended methods for the IO class.
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
        /// <param name="io">I/O handler.</param>
        /// <param name="src">Path of the source file.</param>
        /// <param name="callback">User action.</param>
        ///
        /// <returns>Executed result.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Load<T>(this IO io, string src, Func<Stream, T> callback)
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
        public static T LoadOrDefault<T>(this IO io, string src, Func<Stream, T> callback, T error) =>
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
        public static void Save(this IO io, string dest, Action<Stream> callback)
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
        /// ファイルの種類を表す文字列を取得します。
        /// </summary>
        ///
        /// <param name="io">ファイル操作用オブジェクト</param>
        /// <param name="info">ファイル情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetTypeName(this IO io, Information info) =>
            GetTypeName(io, info?.FullName);

        /* ----------------------------------------------------------------- */
        ///
        /// GetTypeName
        ///
        /// <summary>
        /// ファイルの種類を表す文字列を取得します。
        /// </summary>
        ///
        /// <param name="io">ファイル操作用オブジェクト</param>
        /// <param name="path">対象となるパス</param>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetTypeName(this IO io, string path)
        {
            if (!path.HasValue()) return string.Empty;

            var dest   = new Shell32.SHFILEINFO();
            var status = Shell32.NativeMethods.SHGetFileInfo(path,
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
        /// 指定されたパスを基にした一意なパスを取得します。
        /// </summary>
        ///
        /// <param name="io">ファイル操作用オブジェクト</param>
        /// <param name="path">対象となるパス</param>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetUniqueName(this IO io, string path) =>
            GetUniqueName(io, io.Get(path));

        /* ----------------------------------------------------------------- */
        ///
        /// GetUniqueName
        ///
        /// <summary>
        /// IInformation オブジェクトを基にした一意なパスを取得します。
        /// </summary>
        ///
        /// <param name="io">ファイル操作用オブジェクト</param>
        /// <param name="src">ファイル情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetUniqueName(this IO io, Information src)
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

        #region ChangeExtension

        /* ----------------------------------------------------------------- */
        ///
        /// ChangeExtension
        ///
        /// <summary>
        /// 拡張子を変更します。
        /// </summary>
        ///
        /// <param name="io">ファイル操作用オブジェクト</param>
        /// <param name="path">ファイルのパス</param>
        /// <param name="ext">拡張子</param>
        ///
        /// <returns>変更後のパス</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string ChangeExtension(this IO io, string path, string ext) =>
            ChangeExtension(io, io.Get(path), ext);

        /* ----------------------------------------------------------------- */
        ///
        /// ChangeExtension
        ///
        /// <summary>
        /// 拡張子を変更します。
        /// </summary>
        ///
        /// <param name="io">ファイル操作用オブジェクト</param>
        /// <param name="info">ファイル情報</param>
        /// <param name="ext">拡張子</param>
        ///
        /// <returns>変更後のパス</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string ChangeExtension(this IO io, Information info, string ext) =>
            io.Combine(info.DirectoryName, $"{info.NameWithoutExtension}{ext}");

        #endregion

        #endregion
    }
}
