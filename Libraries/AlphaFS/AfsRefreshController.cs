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
using Alphaleonis.Win32.Filesystem;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// AfsRefreshController
    ///
    /// <summary>
    /// AlphaFS を利用した Information の情報を更新するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class AfsRefreshController : Information.RefreshController
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
        /* ----------------------------------------------------------------- */
        public override void Invoke(Information src, string path)
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
            src.Length               = obj.Exists ? (TryCast(obj)?.Length ?? 0) : 0;
            src.IsDirectory          = obj is DirectoryInfo;
            src.NameWithoutExtension = Path.GetFileNameWithoutExtension(path);
            src.DirectoryName        = TryCast(obj)?.DirectoryName ??
                                       Path.GetDirectoryName(path);
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
