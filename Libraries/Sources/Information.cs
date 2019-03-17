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
    [Serializable]
    public class Information
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Information
        ///
        /// <summary>
        /// Creates a new instance of the Information class with the
        /// specified path.
        /// </summary>
        ///
        /// <param name="src">Path of the source file.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Information(string src) : this(src, new Controller()) { }

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
        public Information(Information src) : this(src.Source, src.Controller) { }

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
        ///
        /* ----------------------------------------------------------------- */
        public Information(string src, Controller controller)
        {
            Controller  = controller;
            Refreshable = controller.Create(src);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Refreshable
        ///
        /// <summary>
        /// 内部情報を保持するためのオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected Refreshable Refreshable { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Controller
        ///
        /// <summary>
        /// 情報更新用オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected Controller Controller { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Source
        ///
        /// <summary>
        /// オリジナルのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Source => Refreshable.Source;

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
        public bool Exists => Refreshable.Exists;

        /* ----------------------------------------------------------------- */
        ///
        /// IsDirectory
        ///
        /// <summary>
        /// ディレクトリかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsDirectory => Refreshable.IsDirectory;

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// ファイル名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name => Refreshable.Name;

        /* ----------------------------------------------------------------- */
        ///
        /// NameWithoutExtension
        ///
        /// <summary>
        /// 拡張子を除いたファイル名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string NameWithoutExtension => Refreshable.NameWithoutExtension;

        /* ----------------------------------------------------------------- */
        ///
        /// Extension
        ///
        /// <summary>
        /// 拡張子を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Extension => Refreshable.Extension;

        /* ----------------------------------------------------------------- */
        ///
        /// FullName
        ///
        /// <summary>
        /// 完全なパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string FullName => Refreshable.FullName;

        /* ----------------------------------------------------------------- */
        ///
        /// DirectoryName
        ///
        /// <summary>
        /// ファイルまたはディレクトリの親ディレクトリのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string DirectoryName => Refreshable.DirectoryName;

        /* ----------------------------------------------------------------- */
        ///
        /// Length
        ///
        /// <summary>
        /// ファイルサイズを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public long Length => Refreshable.Length;

        /* ----------------------------------------------------------------- */
        ///
        /// Attributes
        ///
        /// <summary>
        /// ファイルまたはディレクトリの属性を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FileAttributes Attributes => Refreshable.Attributes;

        /* ----------------------------------------------------------------- */
        ///
        /// CreationTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリの作成日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime CreationTime => Refreshable.CreationTime;

        /* ----------------------------------------------------------------- */
        ///
        /// LastWriteTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリの最終更新日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastWriteTime => Refreshable.LastWriteTime;

        /* ----------------------------------------------------------------- */
        ///
        /// LastAccessTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリの最終アクセス日時を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime LastAccessTime => Refreshable.LastAccessTime;

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
        public void Refresh() => Controller.Refresh(Refreshable);

        #endregion
    }
}
