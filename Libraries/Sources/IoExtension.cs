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
using Cube.Log;
using System;
using System.Diagnostics;
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
    /// Cube.FileSystem.IO に対する拡張用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class IoExtension
    {
        #region Methods

        #region Load

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// ファイルを開いて内容を読み込みます。
        /// </summary>
        ///
        /// <param name="io">入出力用オブジェクト</param>
        /// <param name="src">ファイルのパス</param>
        /// <param name="func">入力ストリームに対する処理</param>
        ///
        /// <returns>変換結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Load<T>(this IO io, string src, Func<Stream, T> func) =>
            Load(io, src, func, default(T));

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// ファイルを開いて内容を読み込みます。
        /// </summary>
        ///
        /// <param name="io">入出力用オブジェクト</param>
        /// <param name="src">ファイルのパス</param>
        /// <param name="func">入力ストリームに対する処理</param>
        /// <param name="error">エラー時に返される値</param>
        ///
        /// <returns>変換結果</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Load<T>(this IO io, string src, Func<Stream, T> func, T error) => io.LogWarn(() =>
        {
            if (io.Exists(src) && io.Get(src).Length > 0)
            {
                using (var ss = io.OpenRead(src)) return func(ss);
            }
            return error;
        }, error);

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// ファイルを開いて内容を読み込みます。
        /// </summary>
        ///
        /// <param name="io">入出力用オブジェクト</param>
        /// <param name="src">ファイルのパス</param>
        /// <param name="action">入力ストリームに対する処理</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Load(this IO io, string src, Action<Stream> action) => io.Load(src, e =>
        {
            action(e);
            return true;
        }, false);

        #endregion

        #region Save

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// ファイルに保存します。
        /// </summary>
        ///
        /// <param name="io">入出力用オブジェクト</param>
        /// <param name="dest">ファイルのパス</param>
        /// <param name="action">出力ストリームに対する処理</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Save(this IO io, string dest, Action<Stream> action) => io.LogWarn(() =>
        {
            using (var ss = new MemoryStream())
            {
                action(ss);
                using (var ds = io.Create(dest))
                {
                    ss.Position = 0;
                    ss.CopyTo(ds);
                }
            }
        });

        #endregion

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
        /// <remarks>
        /// 現在は Operator オブジェクトは使用していません。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetTypeName(this IO io, string path)
        {
            if (string.IsNullOrEmpty(path)) return null;

            var dest   = new Shell32.SHFILEINFO();
            var result = Shell32.NativeMethods.SHGetFileInfo(path,
                0x0080, // FILE_ATTRIBUTE_NORMAL
                ref dest,
                (uint)Marshal.SizeOf(dest),
                0x0410 // SHGFI_TYPENAME | SHGFI_USEFILEATTRIBUTES
            );

            return result != IntPtr.Zero ? dest.szTypeName : null;
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
            GetUniqueName(io, io?.Get(path));

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
            Debug.Assert(io != null);

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
