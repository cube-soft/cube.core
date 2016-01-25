/* ------------------------------------------------------------------------- */
///
/// FileInfo.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileInfo
    /// 
    /// <summary>
    /// ファイル情報を取得するためのクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// このクラスで取得できる情報には、System.IO.FileInfo で取得できる
    /// 情報に加えて、SHGetFileInfo() で取得できる情報も含まれます。
    /// ただし、System.IO.FileInfo のように、このクラスを介してファイルの
    /// 操作を行う事はできません。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class FileInfo
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// FileInfo
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FileInfo(string filename, IconSize size = IconSize.Small)
        {
            _base = new System.IO.FileInfo(filename);
            TypeName = Exists ? GetTypeName() : string.Empty;
            IconSize = size;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// ファイルの名前を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name
        {
            get { return _base.Name; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FullName
        ///
        /// <summary>
        /// ファイルの絶対パスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string FullName
        {
            get { return _base.FullName; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DirectoryName
        ///
        /// <summary>
        /// ディレクトリの絶対パスを表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string DirectoryName
        {
            get { return _base.DirectoryName; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Extension
        ///
        /// <summary>
        /// ファイルの拡張子部分を表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Extension
        {
            get { return _base.Extension; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Exists
        ///
        /// <summary>
        /// ファイルが存在するかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Exists
        {
            get { return _base.Exists; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Length
        ///
        /// <summary>
        /// 現在のファイルのサイズをバイト単位で取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public long Length
        {
            get { return _base.Length; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Attributes
        ///
        /// <summary>
        /// 現在のファイルの属性を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FileAttributes Attributes
        {
            get { return _base.Attributes; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsReadOnly
        ///
        /// <summary>
        /// 現在のファイルが読み取り専用であるかどうかを判断する値を
        /// 取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsReadOnly
        {
            get { return _base.IsReadOnly; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreationTime
        ///
        /// <summary>
        /// 現在のファイルの作成日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime CreationTime
        {
            get { return _base.CreationTime; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreationTimeUtc
        ///
        /// <summary>
        /// 現在のファイルの作成日時を世界協定時刻 (UTC) で取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime CreationTimeUtc
        {
            get { return _base.CreationTimeUtc; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LastAccessTime
        ///
        /// <summary>
        /// 現在のファイルに最後にアクセスした時刻を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastAccessTime
        {
            get { return _base.LastAccessTime; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LastAccessTimeUtc
        ///
        /// <summary>
        /// 現在のファイルに最後にアクセスした時刻を世界協定時刻 (UTC) で
        /// 取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastAccessTimeUtc
        {
            get { return _base.LastAccessTimeUtc; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LastWriteTime
        ///
        /// <summary>
        /// 現在のファイルに最後に書き込みがなされた時刻を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastWriteTime
        {
            get { return _base.LastWriteTime; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LastWriteTimeUtc
        ///
        /// <summary>
        /// 現在のファイルに最後に書き込みがなされた時刻を世界協定時刻 (UTC)
        /// で取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastWriteTimeUtc
        {
            get { return _base.LastWriteTimeUtc; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TypeName
        ///
        /// <summary>
        /// ファイルの種類を表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string TypeName { get; private set; } = string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// Icon
        ///
        /// <summary>
        /// ファイルに関連付けられたアイコンを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Icon Icon
        {
            get { return Exists? IconFactory.Create(FullName, IconSize) : null; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IconSize
        ///
        /// <summary>
        /// ファイルに関連付けられたアイコンのサイズを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IconSize IconSize { get; set; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        ///
        /// <summary>
        /// オブジェクトの状態を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Refresh()
        {
            _base.Refresh();
            TypeName = Exists ? GetTypeName() : string.Empty;
        }

        #endregion

        #region Other private methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetTypeName
        ///
        /// <summary>
        /// ファイルの種類を表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string GetTypeName()
        {
            var attr   = Shell32.FILE_ATTRIBUTE_NORMAL;
            var flags  = Shell32.SHGFI_TYPENAME | Shell32.SHGFI_USEFILEATTRIBUTES;
            var shfi   = new SHFILEINFO();
            var result = Shell32.SHGetFileInfo(FullName, attr, ref shfi, (uint)Marshal.SizeOf(shfi), flags);

            return (result != IntPtr.Zero) ? shfi.szTypeName : string.Empty;
        }

        #endregion

        #region Fields
        private System.IO.FileInfo _base;
        #endregion
    }
}
