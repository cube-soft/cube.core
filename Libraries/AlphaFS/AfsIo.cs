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
    /// AfsIO
    ///
    /// <summary>
    /// AlphaFS を利用した IO クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class AfsIO : IO
    {
        /* ----------------------------------------------------------------- */
        ///
        /// GetCore
        ///
        /// <summary>
        /// ファイルまたはディレクトリの情報を保持するオブジェクトを
        /// 取得します。
        /// </summary>
        ///
        /// <param name="path">対象となるパス</param>
        ///
        /// <returns>IInformation オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected override IInformation GetCore(string path) => new AfsInformation(path);

        /* ----------------------------------------------------------------- */
        ///
        /// GetFilesCore
        ///
        /// <summary>
        /// ディレクトリ下にあるファイルの一覧を取得します。
        /// </summary>
        ///
        /// <param name="path">パス</param>
        ///
        /// <returns>ファイル一覧</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected override string[] GetFilesCore(string path) =>
            Directory.Exists(path) ? Directory.GetFiles(path) : null;

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectoriesCore
        ///
        /// <summary>
        /// ディレクトリ下にあるディレクトリの一覧を取得します。
        /// </summary>
        ///
        /// <param name="path">パス</param>
        ///
        /// <returns>ディレクトリ一覧</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected override string[] GetDirectoriesCore(string path) =>
            Directory.Exists(path) ? Directory.GetDirectories(path) : null;

        /* ----------------------------------------------------------------- */
        ///
        /// SetAttributesCore
        ///
        /// <summary>
        /// ファイルまたはディレクトリに属性を設定します。
        /// </summary>
        ///
        /// <param name="path">対象となるパス</param>
        /// <param name="attr">属性</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void SetAttributesCore(string path, System.IO.FileAttributes attr)
        {
            if (Directory.Exists(path)) new DirectoryInfo(path) { Attributes = attr };
            else if (File.Exists(path)) File.SetAttributes(path, attr);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetCreationTimeCore
        ///
        /// <summary>
        /// ファイルまたはディレクトリに作成日時を設定します。
        /// </summary>
        ///
        /// <param name="path">対象となるパス</param>
        /// <param name="time">作成日時</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void SetCreationTimeCore(string path, DateTime time)
        {
            if (Directory.Exists(path)) Directory.SetCreationTime(path, time);
            else if (File.Exists(path)) File.SetCreationTime(path, time);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetLastWriteTimeCore
        ///
        /// <summary>
        /// ファイルまたはディレクトリに最終更新日時を設定します。
        /// </summary>
        ///
        /// <param name="path">対象となるパス</param>
        /// <param name="time">最終更新日時</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void SetLastWriteTimeCore(string path, DateTime time)
        {
            if (Directory.Exists(path)) Directory.SetLastWriteTime(path, time);
            else if (File.Exists(path)) File.SetLastWriteTime(path, time);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetLastAccessTimeCore
        ///
        /// <summary>
        /// ファイルまたはディレクトリに最終アクセス日時を設定します。
        /// </summary>
        ///
        /// <param name="path">対象となるパス</param>
        /// <param name="time">最終アクセス日時</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void SetLastAccessTimeCore(string path, DateTime time)
        {
            if (Directory.Exists(path)) Directory.SetLastAccessTime(path, time);
            else if (File.Exists(path)) File.SetLastAccessTime(path, time);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CombineCore
        ///
        /// <summary>
        /// パスを結合します。
        /// </summary>
        ///
        /// <param name="paths">パス一覧</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override string CombineCore(params string[] paths) =>
            Path.Combine(paths);

        /* ----------------------------------------------------------------- */
        ///
        /// ExistsCore
        ///
        /// <summary>
        /// ファイルまたはディレクトリが存在するかどうかを判別します。
        /// </summary>
        ///
        /// <param name="path">対象となるパス</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override bool ExistsCore(string path) =>
            File.Exists(path) || Directory.Exists(path);

        /* ----------------------------------------------------------------- */
        ///
        /// DeleteCore
        ///
        /// <summary>
        /// ファイルまたはディレクトリを削除します。
        /// </summary>
        ///
        /// <param name="path">削除対象となるパス</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void DeleteCore(string path)
        {
            if (Directory.Exists(path))
            {
                foreach (var f in Directory.GetFiles(path)) DeleteCore(f);
                foreach (var d in Directory.GetDirectories(path)) DeleteCore(d);
                SetAttributes(path, System.IO.FileAttributes.Normal);
                Directory.Delete(path, false);
            }
            else if (File.Exists(path))
            {
                SetAttributes(path, System.IO.FileAttributes.Normal);
                File.Delete(path);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateCore
        ///
        /// <summary>
        /// ファイルを新規作成します。
        /// </summary>
        ///
        /// <param name="path">ファイルのパス</param>
        ///
        /// <returns>書き込み用ストリーム</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected override System.IO.FileStream CreateCore(string path) =>
            File.Create(path);

        /* ----------------------------------------------------------------- */
        ///
        /// OpenReadCore
        ///
        /// <summary>
        /// ファイルを読み込み専用で開きます。
        /// </summary>
        ///
        /// <param name="path">ファイルのパス</param>
        ///
        /// <returns>読み込み用ストリーム</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected override System.IO.FileStream OpenReadCore(string path) =>
            File.OpenRead(path);

        /* ----------------------------------------------------------------- */
        ///
        /// OpenWriteCore
        ///
        /// <summary>
        /// ファイルを新規作成、または上書き用で開きます。
        /// </summary>
        ///
        /// <param name="path">ファイルのパス</param>
        ///
        /// <returns>書き込み用ストリーム</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected override System.IO.FileStream OpenWriteCore(string path) =>
            File.OpenWrite(path);

        /* ----------------------------------------------------------------- */
        ///
        /// CreateDirectoryCore
        ///
        /// <summary>
        /// ディレクトリを作成します。
        /// </summary>
        ///
        /// <param name="path">ディレクトリのパス</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void CreateDirectoryCore(string path) =>
            Directory.CreateDirectory(path);

        /* ----------------------------------------------------------------- */
        ///
        /// MoveCore
        ///
        /// <summary>
        /// ファイルを移動します。
        /// </summary>
        ///
        /// <param name="src">移動前のパス</param>
        /// <param name="dest">移動後のパス</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void MoveCore(string src, string dest) =>
            File.Move(src, dest);

        /* ----------------------------------------------------------------- */
        ///
        /// CopyCore
        ///
        /// <summary>
        /// ファイルをコピーします。
        /// </summary>
        ///
        /// <param name="src">コピー元のパス</param>
        /// <param name="dest">コピー先のパス</param>
        /// <param name="overwrite">上書きするかどうかを示す値</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void CopyCore(string src, string dest, bool overwrite) =>
            File.Copy(src, dest, overwrite);
    }
}
