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
using System;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// AfsController
    ///
    /// <summary>
    /// AlphaFS を利用した Information オブジェクトの情報更新用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public class AfsController : Controller
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        ///
        /// <summary>
        /// Information オブジェクトの情報を更新します。
        /// </summary>
        ///
        /// <param name="src">更新対象オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public override void Refresh(Controllable src)
        {
            var obj = CreateCore(src.Source);

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
        /// CreateCore
        ///
        /// <summary>
        /// FileSystemInfo オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private FileSystemInfo CreateCore(string path) =>
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
