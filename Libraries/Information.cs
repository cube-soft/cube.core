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
    /// ファイルまたはディレクトリの情報を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Information
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
        /// <param name="src">ファイルまたはディレクトリのパス</param>
        ///
        /* ----------------------------------------------------------------- */
        public Information(string src) : this(src, new RefreshController()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Information
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="src">ファイルまたはディレクトリのパス</param>
        /// <param name="controller">情報更新用オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public Information(string src, RefreshController controller)
        {
            Core       = new InformationCore(src);
            Controller = controller;
            Refresh();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Core
        ///
        /// <summary>
        /// 内部情報を保持するためのオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected InformationCore Core { get; }

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

        /* ----------------------------------------------------------------- */
        ///
        /// Source
        ///
        /// <summary>
        /// オリジナルのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Source => Core.Source;

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
        public bool Exists => Core.Exists;

        /* ----------------------------------------------------------------- */
        ///
        /// IsDirectory
        ///
        /// <summary>
        /// ディレクトリかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsDirectory => Core.IsDirectory;

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// ファイル名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name => Core.Name;

        /* ----------------------------------------------------------------- */
        ///
        /// NameWithoutExtension
        ///
        /// <summary>
        /// 拡張子を除いたファイル名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string NameWithoutExtension => Core.NameWithoutExtension;

        /* ----------------------------------------------------------------- */
        ///
        /// Extension
        ///
        /// <summary>
        /// 拡張子を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Extension => Core.Extension;

        /* ----------------------------------------------------------------- */
        ///
        /// FullName
        ///
        /// <summary>
        /// 完全なパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string FullName => Core.FullName;

        /* ----------------------------------------------------------------- */
        ///
        /// DirectoryName
        ///
        /// <summary>
        /// ファイルまたはディレクトリの親ディレクトリのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string DirectoryName => Core.DirectoryName;

        /* ----------------------------------------------------------------- */
        ///
        /// Length
        ///
        /// <summary>
        /// ファイルサイズを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public long Length => Core.Length;

        /* ----------------------------------------------------------------- */
        ///
        /// Attributes
        ///
        /// <summary>
        /// ファイルまたはディレクトリの属性を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FileAttributes Attributes => Core.Attributes;

        /* ----------------------------------------------------------------- */
        ///
        /// CreationTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリの作成日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime CreationTime => Core.CreationTime;

        /* ----------------------------------------------------------------- */
        ///
        /// LastWriteTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリの最終更新日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastWriteTime => Core.LastWriteTime;

        /* ----------------------------------------------------------------- */
        ///
        /// LastAccessTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリの最終アクセス日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastAccessTime => Core.LastAccessTime;

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
        public void Refresh() => Controller.Invoke(Core);

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
            /// <param name="src">更新対象オブジェクト</param>
            ///
            /* ------------------------------------------------------------- */
            public virtual void Invoke(InformationCore src)
            {
                var obj = Create(src.Source);

                src.Exists               = obj.Exists;
                src.Name                 = obj.Name;
                src.Extension            = obj.Extension;
                src.FullName             = obj.FullName;
                src.Attributes           = obj.Attributes;
                src.CreationTime         = obj.CreationTime;
                src.LastAccessTime       = obj.LastAccessTime;
                src.LastWriteTime        = obj.LastWriteTime;
                src.Length               = obj.Exists ? (TryCast(obj)?.Length ?? 0) : 0;
                src.IsDirectory          = obj is DirectoryInfo;
                src.NameWithoutExtension = Path.GetFileNameWithoutExtension(src.Source);
                src.DirectoryName        = TryCast(obj)?.DirectoryName ??
                                           Path.GetDirectoryName(src.Source);
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
    }
}
