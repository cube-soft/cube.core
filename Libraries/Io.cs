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
    /// IO
    ///
    /// <summary>
    /// ファイル操作を実行するクラスの基底となるクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class IO
    {
        #region Properties

        #endregion

        #region Methods

        #region Get

        /* ----------------------------------------------------------------- */
        ///
        /// Get
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
        public Information Get(string path) => GetCore(path);

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
        protected virtual Information GetCore(string path) =>
            new Information(path, GetRefreshable());

        #endregion

        #region GetRefreshable

        /* ----------------------------------------------------------------- */
        ///
        /// GetRefreshable
        ///
        /// <summary>
        /// Information の各種プロパティを更新するためのオブジェクトを
        /// 取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IRefreshable GetRefreshable() => GetRefreshableCore();

        /* ----------------------------------------------------------------- */
        ///
        /// GetRefreshableCore
        ///
        /// <summary>
        /// Information の各種プロパティを更新するためのオブジェクトを
        /// 取得します。
        /// </summary>
        ///
        /// <remarks>
        /// Refreshable クラスにはオブジェクト毎に保持する状態が存在
        /// しないため、複数のオブジェクトで Refreshable オブジェクトを
        /// 共有する事とします。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual IRefreshable GetRefreshableCore() => _shared ?? (
            _shared = new Refreshable()
        );

        #endregion

        #region GetFiles

        /* ----------------------------------------------------------------- */
        ///
        /// GetFiles
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
        public string[] GetFiles(string path) => GetFilesCore(path);

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
        protected virtual string[] GetFilesCore(string path) =>
            Directory.Exists(path) ? Directory.GetFiles(path) : null;

        #endregion

        #region GetDirectories

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectories
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
        public string[] GetDirectories(string path) => GetDirectoriesCore(path);

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
        protected virtual string[] GetDirectoriesCore(string path) =>
            Directory.Exists(path) ? Directory.GetDirectories(path) : null;

        #endregion

        #region SetAttributes

        /* ----------------------------------------------------------------- */
        ///
        /// SetAttributes
        ///
        /// <summary>
        /// ファイルまたはディレクトリに属性を設定します。
        /// </summary>
        ///
        /// <param name="path">対象となるパス</param>
        /// <param name="attr">属性</param>
        ///
        /* ----------------------------------------------------------------- */
        public void SetAttributes(string path, FileAttributes attr) =>
            SetAttributesCore(path, attr);

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
        protected virtual void SetAttributesCore(string path, FileAttributes attr)
        {
            if (Directory.Exists(path)) new DirectoryInfo(path) { Attributes = attr };
            else if (File.Exists(path)) File.SetAttributes(path, attr);
        }

        #endregion

        #region SetCreationTime

        /* ----------------------------------------------------------------- */
        ///
        /// SetCreationTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリに作成日時を設定します。
        /// </summary>
        ///
        /// <param name="path">対象となるパス</param>
        /// <param name="time">作成日時</param>
        ///
        /* ----------------------------------------------------------------- */
        public void SetCreationTime(string path, DateTime time) =>
            SetCreationTimeCore(path, time);

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
        protected virtual void SetCreationTimeCore(string path, DateTime time)
        {
            if (Directory.Exists(path)) Directory.SetCreationTime(path, time);
            else if (File.Exists(path)) File.SetCreationTime(path, time);
        }

        #endregion

        #region SetLastWriteTime

        /* ----------------------------------------------------------------- */
        ///
        /// SetLastWriteTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリに最終更新日時を設定します。
        /// </summary>
        ///
        /// <param name="path">対象となるパス</param>
        /// <param name="time">最終更新日時</param>
        ///
        /* ----------------------------------------------------------------- */
        public void SetLastWriteTime(string path, DateTime time) =>
            SetLastWriteTimeCore(path, time);

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
        protected virtual void SetLastWriteTimeCore(string path, DateTime time)
        {
            if (Directory.Exists(path)) Directory.SetLastWriteTime(path, time);
            else if (File.Exists(path)) File.SetLastWriteTime(path, time);
        }

        #endregion

        #region SetLastAccessTime

        /* ----------------------------------------------------------------- */
        ///
        /// SetLastAccessTime
        ///
        /// <summary>
        /// ファイルまたはディレクトリに最終アクセス日時を設定します。
        /// </summary>
        ///
        /// <param name="path">対象となるパス</param>
        /// <param name="time">最終アクセス日時</param>
        ///
        /* ----------------------------------------------------------------- */
        public void SetLastAccessTime(string path, DateTime time) =>
            SetLastAccessTimeCore(path, time);

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
        protected virtual void SetLastAccessTimeCore(string path, DateTime time)
        {
            if (Directory.Exists(path)) Directory.SetLastAccessTime(path, time);
            else if (File.Exists(path)) File.SetLastAccessTime(path, time);
        }

        #endregion

        #region Combine

        /* ----------------------------------------------------------------- */
        ///
        /// Combine
        ///
        /// <summary>
        /// パスを結合します。
        /// </summary>
        ///
        /// <param name="paths">結合するパス一覧</param>
        ///
        /* ----------------------------------------------------------------- */
        public string Combine(params string[] paths) => CombineCore(paths);

        /* ----------------------------------------------------------------- */
        ///
        /// CombineCore
        ///
        /// <summary>
        /// パスを結合します。
        /// </summary>
        ///
        /// <param name="paths">結合するパス一覧</param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual string CombineCore(params string[] paths) => Path.Combine(paths);

        #endregion

        #region Exists

        /* ----------------------------------------------------------------- */
        ///
        /// Exists
        ///
        /// <summary>
        /// ファイルまたはディレクトリが存在するかどうかを判別します。
        /// </summary>
        ///
        /// <param name="path">対象となるパス</param>
        ///
        /* ----------------------------------------------------------------- */
        public bool Exists(string path) => ExistsCore(path);

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
        protected virtual bool ExistsCore(string path) =>
            File.Exists(path) || Directory.Exists(path);

        #endregion

        #region Delete

        /* ----------------------------------------------------------------- */
        ///
        /// Delete
        ///
        /// <summary>
        /// ファイルまたはディレクトリを削除します。
        /// </summary>
        ///
        /// <param name="path">削除するファイルのパス</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Delete(string path) =>
            Action(nameof(Delete), () => DeleteCore(path), path);

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
        protected virtual void DeleteCore(string path)
        {
            if (Directory.Exists(path))
            {
                foreach (var f in Directory.GetFiles(path)) DeleteCore(f);
                foreach (var d in Directory.GetDirectories(path)) DeleteCore(d);
                SetAttributes(path, FileAttributes.Normal);
                Directory.Delete(path, false);
            }
            else if (File.Exists(path))
            {
                SetAttributes(path, FileAttributes.Normal);
                File.Delete(path);
            }
        }

        #endregion

        #region CreateDirectory

        /* ----------------------------------------------------------------- */
        ///
        /// CreateDirectory
        ///
        /// <summary>
        /// ディレクトリを作成します。
        /// </summary>
        ///
        /// <param name="path">ディレクトリのパス</param>
        ///
        /* ----------------------------------------------------------------- */
        public void CreateDirectory(string path) =>
            Action(nameof(CreateDirectory), () => CreateDirectoryCore(path), path);

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
        protected virtual void CreateDirectoryCore(string path) =>
            Directory.CreateDirectory(path);

        #endregion

        #region Create

        /* ----------------------------------------------------------------- */
        ///
        /// Create
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
        public FileStream Create(string path) => Func(nameof(Create), () =>
        {
            CreateParentDirectory(Get(path));
            return CreateCore(path);
        }, path);

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
        protected virtual FileStream CreateCore(string path) => File.Create(path);

        #endregion

        #region OpenRead

        /* ----------------------------------------------------------------- */
        ///
        /// OpenRead
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
        public FileStream OpenRead(string path) =>
            Func(nameof(OpenRead), () => OpenReadCore(path), path);

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
        protected virtual FileStream OpenReadCore(string path) => File.OpenRead(path);

        #endregion

        #region OpenWrite

        /* ----------------------------------------------------------------- */
        ///
        /// OpenWrite
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
        public FileStream OpenWrite(string path) => Func(nameof(OpenWrite), () =>
        {
            CreateParentDirectory(Get(path));
            return OpenWriteCore(path);
        }, path);

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
        protected virtual FileStream OpenWriteCore(string path) => File.OpenWrite(path);

        #endregion

        #region Move

        /* ----------------------------------------------------------------- */
        ///
        /// Move
        ///
        /// <summary>
        /// ファイルを移動します。
        /// </summary>
        ///
        /// <param name="src">移動前のパス</param>
        /// <param name="dest">移動後のパス</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Move(string src, string dest) => Move(src, dest, false);

        /* ----------------------------------------------------------------- */
        ///
        /// Move
        ///
        /// <summary>
        /// ファイルを移動します。
        /// </summary>
        ///
        /// <param name="src">移動前のパス</param>
        /// <param name="dest">移動後のパス</param>
        /// <param name="overwrite">上書きするかどうかを表す値</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Move(string src, string dest, bool overwrite)
        {
            var info = Get(src);
            if (info.IsDirectory) MoveDirectory(info, Get(dest), overwrite);
            else MoveFile(info, Get(dest), overwrite);
        }

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
        protected virtual void MoveCore(string src, string dest) => File.Move(src, dest);

        #endregion

        #region Copy

        /* ----------------------------------------------------------------- */
        ///
        /// Copy
        ///
        /// <summary>
        /// ファイルをコピーします。
        /// </summary>
        ///
        /// <param name="src">コピー元のパス</param>
        /// <param name="dest">コピー先のパス</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Copy(string src, string dest) => Copy(src, dest, false);

        /* ----------------------------------------------------------------- */
        ///
        /// Copy
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
        public void Copy(string src, string dest, bool overwrite)
        {
            var info = Get(src);
            if (info.IsDirectory) CopyDirectory(info, Get(dest), overwrite);
            else CopyFile(info, Get(dest), overwrite);
        }

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
        protected virtual void CopyCore(string src, string dest, bool overwrite) =>
            File.Copy(src, dest, overwrite);

        #endregion

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Failed
        ///
        /// <summary>
        /// 操作に失敗した時に発生するイベントです。
        /// </summary>
        ///
        /// <remarks>
        /// Key には失敗したメソッド名、Value には失敗した時に送出された例外
        /// オブジェクトが設定されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public event FailedEventHandler Failed;

        /* ----------------------------------------------------------------- */
        ///
        /// OnFailed
        ///
        /// <summary>
        /// Failed イベントを発生させます。
        /// </summary>
        ///
        /// <remarks>
        /// Failed イベントにハンドラが設定されていない場合、
        /// 例外をそのまま送出します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnFailed(FailedEventArgs e) => Failed(this, e);

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// CreateDirectory
        ///
        /// <summary>
        /// ディレクトリを生成し、各種属性をコピーします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void CreateDirectory(string path, Information src)
        {
            CreateDirectory(path);
            SetCreationTime(path, src.CreationTime);
            SetLastWriteTime(path, src.LastWriteTime);
            SetLastAccessTime(path, src.LastAccessTime);
            SetAttributes(path, src.Attributes);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateParentDirectory
        ///
        /// <summary>
        /// 親ディレクトリを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void CreateParentDirectory(Information info)
        {
            var dir = info.DirectoryName;
            if (!Exists(dir)) CreateDirectory(dir);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CopyDirectory
        ///
        /// <summary>
        /// ディレクトリおよびその中身をコピーします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool CopyDirectory(Information src, Information dest, bool overwrite)
        {
            if (!dest.Exists) CreateDirectory(dest.FullName, src);

            var result = true;

            foreach (var file in GetFiles(src.FullName))
            {
                var si = Get(file);
                var di = Get(Combine(dest.FullName, si.Name));
                result &= CopyFile(si, di, overwrite);
            }

            foreach (var dir in GetDirectories(src.FullName))
            {
                var si = Get(dir);
                var di = Get(Combine(dest.FullName, si.Name));
                result &= CopyDirectory(si, di, overwrite);
            }

            return result;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CopyFile
        ///
        /// <summary>
        /// ファイルをコピーします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool CopyFile(Information src, Information dest, bool overwrite) =>
            Action(nameof(Copy), () =>
            {
                CreateParentDirectory(dest);
                CopyCore(src.FullName, dest.FullName, overwrite);
            }, src.FullName, dest.FullName);

        /* ----------------------------------------------------------------- */
        ///
        /// MoveDirectory
        ///
        /// <summary>
        /// ディレクトリおよびその中身を移動します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool MoveDirectory(Information src, Information dest, bool overwrite)
        {
            if (!dest.Exists) CreateDirectory(dest.FullName, src);

            var result = true;

            foreach (var file in GetFiles(src.FullName))
            {
                var si = Get(file);
                var di = Get(Combine(dest.FullName, si.Name));
                result &= MoveFile(si, di, overwrite);
            }

            foreach (var dir in GetDirectories(src.FullName))
            {
                var si = Get(dir);
                var di = Get(Combine(dest.FullName, si.Name));
                result &= MoveDirectory(si, di, overwrite);
            }

            if (result && src.Exists) Delete(src.FullName);

            return result;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MoveFile
        ///
        /// <summary>
        /// ファイルを移動します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool MoveFile(Information src, Information dest, bool overwrite)
        {
            if (!overwrite || !dest.Exists) return MoveFile(src, dest);
            else
            {
                var tmp = Combine(src.DirectoryName, Guid.NewGuid().ToString("D"));
                var ti = Get(tmp);

                if (!MoveFile(dest, ti)) return false;
                if (!MoveFile(src, dest)) return MoveFile(ti, dest); // recover
                else Delete(tmp);
            }
            return true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MoveFile
        ///
        /// <summary>
        /// ファイルを移動します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool MoveFile(Information src, Information dest) =>
            Action(nameof(Move), () =>
            {
                CreateParentDirectory(dest);
                MoveCore(src.FullName, dest.FullName);
            }, src.FullName, dest.FullName);

        /* ----------------------------------------------------------------- */
        ///
        /// Action
        ///
        /// <summary>
        /// 操作を実行します。
        /// </summary>
        ///
        /// <remarks>
        /// 操作に失敗した場合、イベントハンドラで Cancel が設定されるまで
        /// 実行し続けます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private bool Action(string name, Action f, params string[] paths)
        {
            while (true)
            {
                try
                {
                    f();
                    return true;
                }
                catch (Exception err)
                {
                    if (Failed == null) throw;
                    var args = new FailedEventArgs(name, paths, err);
                    OnFailed(args);
                    if (args.Cancel) return false;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Func
        ///
        /// <summary>
        /// 操作を実行します。
        /// </summary>
        ///
        /// <remarks>
        /// 操作に失敗した場合、イベントハンドラで Cancel が設定されるまで
        /// 実行し続けます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private T Func<T>(string name, Func<T> f, params string[] paths)
        {
            while (true)
            {
                try { return f(); }
                catch (Exception err)
                {
                    if (Failed == null) throw;
                    var args = new FailedEventArgs(name, paths, err);
                    OnFailed(args);
                    if (args.Cancel) return default(T);
                }
            }
        }

        #endregion

        #region Fields
        private static IRefreshable _shared;
        #endregion
    }
}
