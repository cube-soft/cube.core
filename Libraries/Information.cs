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
    /// 標準ライブラリを利用した IInformation の実装クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Information : IInformation
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Information
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="path">ファイルまたはディレクトリのパス</param>
        ///
        /* ----------------------------------------------------------------- */
        public Information(string path) : this(path, new RefreshController()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Information
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="path">ファイルまたはディレクトリのパス</param>
        /// <param name="controller">情報更新用オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public Information(string path, RefreshController controller)
        {
            _source = path;
            Controller = controller;
            Refresh();
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
        public bool Exists { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// IsDirectory
        ///
        /// <summary>
        /// ディレクトリかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsDirectory { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// ファイル名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// NameWithoutExtension
        ///
        /// <summary>
        /// 拡張子を除いたファイル名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string NameWithoutExtension { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Extension
        ///
        /// <summary>
        /// 拡張子を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Extension { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// FullName
        ///
        /// <summary>
        /// 完全なパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string FullName { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// DirectoryName
        ///
        /// <summary>
        /// ファイルまたはディレクトリの親ディレクトリのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string DirectoryName { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Length
        ///
        /// <summary>
        /// ファイルサイズを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public long Length { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Attributes
        ///
        /// <summary>
        /// ファイルまたはディレクトリの属性を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FileAttributes Attributes { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// CreationTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリの作成日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime CreationTime { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// LastWriteTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリの最終更新日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastWriteTime { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// LastAccessTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリの最終アクセス日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastAccessTime { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Controller
        ///
        /// <summary>
        /// 情報更新用オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RefreshController Controller { get; }

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
        public void Refresh() => Controller.Invoke(this, _source);

        #endregion

        #region RefreshController

        /* ----------------------------------------------------------------- */
        ///
        /// RefreshController
        ///
        /// <summary>
        /// Information オブジェクトの情報を更新するためのクラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public class RefreshController
        {
            /* ------------------------------------------------------------- */
            ///
            /// Invoke
            ///
            /// <summary>
            /// Information オブジェクトの情報を更新します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public virtual void Invoke(Information src, string path)
            {
                var obj = Create(path);

                src.Exists               = obj.Exists;
                src.Name                 = obj.Name;
                src.Extension            = obj.Extension;
                src.FullName             = obj.FullName;
                src.Attributes           = obj.Attributes;
                src.CreationTime         = obj.CreationTime;
                src.LastAccessTime       = obj.LastAccessTime;
                src.LastWriteTime        = obj.LastWriteTime;
                src.Length               = TryCast(obj)?.Length ?? 0;
                src.IsDirectory          = obj is DirectoryInfo;
                src.NameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                src.DirectoryName        = TryCast(obj)?.DirectoryName ??
                                           Path.GetDirectoryName(path);
            }

            /* ------------------------------------------------------------- */
            ///
            /// Create
            ///
            /// <summary>
            /// FileSystemInfo オブジェクトを生成します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            private FileSystemInfo Create(string path) =>
                Directory.Exists(path) ?
                new DirectoryInfo(path) as FileSystemInfo :
                new FileInfo(path) as FileSystemInfo;

            /* ------------------------------------------------------------- */
            ///
            /// TryCast
            ///
            /// <summary>
            /// FileInfo オブジェクトへのキャストを試行します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            private FileInfo TryCast(FileSystemInfo src) => src as FileInfo;
        }

        #endregion

        #region Fields
        private readonly string _source;
        #endregion
    }
}
