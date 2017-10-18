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
using Alphaleonis.Win32.Filesystem;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// AfsInformation
    /// 
    /// <summary>
    /// AlphaFS を利用した IInformation の実装クラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    internal class AfsInformation : IInformation
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// AlphaInformation
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="path">ファイルまたはディレクトリのパス</param>
        /// 
        /* ----------------------------------------------------------------- */
        public AfsInformation(string path)
        {
            Reset(path);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Exists
        ///
        /// <summary>
        /// ファイルまたはディレクトリが存在するかどうかを示す値を
        /// 取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Exists => RawObject.Exists;

        /* ----------------------------------------------------------------- */
        ///
        /// IsDirectory
        ///
        /// <summary>
        /// ディレクトリかどうかを示す値を取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool IsDirectory => RawObject is DirectoryInfo;

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// ファイル名を取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public string Name => RawObject.Name;

        /* ----------------------------------------------------------------- */
        ///
        /// NameWithoutExtension
        ///
        /// <summary>
        /// 拡張子を除いたファイル名を取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public string NameWithoutExtension => Path.GetFileNameWithoutExtension(Name);

        /* ----------------------------------------------------------------- */
        ///
        /// Extension
        ///
        /// <summary>
        /// 拡張子を取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public string Extension => RawObject.Extension;

        /* ----------------------------------------------------------------- */
        ///
        /// FullName
        ///
        /// <summary>
        /// 完全なパスを取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public string FullName => RawObject.FullName;

        /* ----------------------------------------------------------------- */
        ///
        /// DirectoryName
        ///
        /// <summary>
        /// ファイルまたはディレクトリの親ディレクトリのパスを取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public string DirectoryName
            => TryCast()?.DirectoryName ?? Path.GetDirectoryName(FullName);

        /* ----------------------------------------------------------------- */
        ///
        /// Length
        ///
        /// <summary>
        /// ファイルサイズを取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public long Length => TryCast()?.Length ?? 0;

        /* ----------------------------------------------------------------- */
        ///
        /// Attributes
        ///
        /// <summary>
        /// ファイルまたはディレクトリの属性を取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public System.IO.FileAttributes Attributes => RawObject.Attributes;

        /* ----------------------------------------------------------------- */
        ///
        /// CreationTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリの作成日時を取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public DateTime CreationTime => RawObject.CreationTime;

        /* ----------------------------------------------------------------- */
        ///
        /// LastWriteTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリの最終更新日時を取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public DateTime LastWriteTime => RawObject.LastWriteTime;

        /* ----------------------------------------------------------------- */
        ///
        /// LastAccessTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリの最終アクセス日時を取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public DateTime LastAccessTime => RawObject.LastAccessTime;

        /* ----------------------------------------------------------------- */
        ///
        /// RawObject
        ///
        /// <summary>
        /// 実装オブジェクトを取得します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public FileSystemInfo RawObject { get; private set; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        ///
        /// <summary>
        /// オブジェクトを最新の状態に更新します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public void Refresh() => Reset(FullName);

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Reset
        ///
        /// <summary>
        /// RawObject をリセットします。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void Reset(string path)
        {
            RawObject = Directory.Exists(path) ?
                        new DirectoryInfo(path) as FileSystemInfo :
                        new FileInfo(path) as FileSystemInfo;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FileInfo
        ///
        /// <summary>
        /// FileInfo オブジェクトへのキャストを施行します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private FileInfo TryCast() => RawObject as FileInfo;

        #endregion
    }
}
