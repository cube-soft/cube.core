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
using System.IO;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// IRefreshable
    ///
    /// <summary>
    /// Information オブジェクトの各種プロパティの内容を更新するための
    /// インターフェースです。
    /// </summary>
    ///
    /// <remarks>
    /// Information オブジェクトのプロパティは読み取り専用であるため、
    /// 外部から更新する事はできません。そのため、更新の際には
    /// Invoke メソッド経由で取得できるオブジェクトに対して更新処理を
    /// 実行する必要があります。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public interface IRefreshable
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// 更新処理を実行します。
        /// </summary>
        ///
        /// <param name="src">更新対象となるオブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        void Invoke(InformationCore src);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Refreshable
    ///
    /// <summary>
    /// .NET Framework 標準ライブラリを用いて IRefreshable を実装した
    /// クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Refreshable : IRefreshable
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Information オブジェクトの情報を更新します。
        /// </summary>
        ///
        /// <param name="src">更新対象オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
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

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// FileSystemInfo オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private FileSystemInfo Create(string path) =>
            Directory.Exists(path) ?
            new DirectoryInfo(path) as FileSystemInfo :
            new FileInfo(path) as FileSystemInfo;

        /* ----------------------------------------------------------------- */
        ///
        /// TryCast
        ///
        /// <summary>
        /// FileInfo オブジェクトへのキャストを試行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private FileInfo TryCast(FileSystemInfo src) => src as FileInfo;

        #endregion
    }
}
