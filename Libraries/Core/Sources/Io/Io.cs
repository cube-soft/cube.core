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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cube.Mixin.Logging;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// IO
    ///
    /// <summary>
    /// Provides functionality to do something to a file or directory.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class IO
    {
        #region Methods

        #region Get

        /* ----------------------------------------------------------------- */
        ///
        /// Get
        ///
        /// <summary>
        /// Gets the Entity object from the specified path.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        ///
        /// <returns>Entity object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public Entity Get(string path) => GetCore(path);

        /* ----------------------------------------------------------------- */
        ///
        /// GetCore
        ///
        /// <summary>
        /// Gets the Entity object from the specified path.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        ///
        /// <returns>Entity object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual Entity GetCore(string path) =>
            new Entity(path, GetController());

        #endregion

        #region GetController

        /* ----------------------------------------------------------------- */
        ///
        /// GetController
        ///
        /// <summary>
        /// Gets the EntityController object.
        /// </summary>
        ///
        /// <returns>EntityController object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public EntityController GetController() => GetControllerCore();

        /* ----------------------------------------------------------------- */
        ///
        /// GetControllerCore
        ///
        /// <summary>
        /// Gets the Controller object.
        /// </summary>
        ///
        /// <returns>Controller object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual EntityController GetControllerCore() => _shared ??= new EntityController();

        #endregion

        #region GetFiles

        /* ----------------------------------------------------------------- */
        ///
        /// GetFiles
        ///
        /// <summary>
        /// Returns the names of files (including their paths).
        /// </summary>
        ///
        /// <param name="path">
        /// The relative or absolute path to the directory to search.
        /// This string is not case-sensitive.
        /// </param>
        ///
        /// <returns>
        /// An array of the full names (including paths) for the files
        /// in the specified directory, or an empty array if no files are
        /// found.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> GetFiles(string path) => GetFiles(path, "*");

        /* ----------------------------------------------------------------- */
        ///
        /// GetFiles
        ///
        /// <summary>
        /// Returns the names of files (including their paths) that
        /// match the specified search pattern in the specified directory.
        /// </summary>
        ///
        /// <param name="path">
        /// The relative or absolute path to the directory to search.
        /// This string is not case-sensitive.
        /// </param>
        ///
        /// <param name="pattern">
        /// The search string to match against the names of subdirectories
        /// in path. This parameter can contain a combination of valid
        /// literal and wildcard characters, but doesn't support regular
        /// expressions.
        /// </param>
        ///
        /// <returns>
        /// An array of the full names (including paths) for the files
        /// in the specified directory that match the specified search
        /// pattern, or an empty array if no files are found.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> GetFiles(string path, string pattern) =>
            GetFiles(path, pattern, SearchOption.TopDirectoryOnly);

        /* ----------------------------------------------------------------- */
        ///
        /// GetFiles
        ///
        /// <summary>
        /// Returns the names of files (including their paths) that
        /// match the specified search pattern in the specified directory,
        /// using a value to determine whether to search subdirectories.
        /// </summary>
        ///
        /// <param name="path">
        /// The relative or absolute path to the directory to search.
        /// This string is not case-sensitive.
        /// </param>
        ///
        /// <param name="pattern">
        /// The search string to match against the names of subdirectories
        /// in path. This parameter can contain a combination of valid
        /// literal and wildcard characters, but doesn't support regular
        /// expressions.
        /// </param>
        ///
        /// <param name="option">
        /// One of the enumeration values that specifies whether the
        /// search operation should include all subdirectories or only
        /// the current directory.
        /// </param>
        ///
        /// <returns>
        /// An array of the full names (including paths) for the files
        /// in the specified directory that match the specified search
        /// pattern and option, or an empty array if no files are found.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> GetFiles(string path, string pattern, SearchOption option) =>
            GetFilesCore(path, pattern, option) ?? Enumerable.Empty<string>();

        /* ----------------------------------------------------------------- */
        ///
        /// GetFilesCore
        ///
        /// <summary>
        /// Returns the names of files (including their paths) that
        /// match the specified search pattern in the specified directory,
        /// using a value to determine whether to search subdirectories.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual IEnumerable<string> GetFilesCore(string path, string pattern, SearchOption option) =>
            Directory.Exists(path) ? Directory.GetFiles(path, pattern, option) : null;

        #endregion

        #region GetDirectories

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectories
        ///
        /// <summary>
        /// Returns the names of the subdirectories.
        /// </summary>
        ///
        /// <param name="path">
        /// The relative or absolute path to the directory to search.
        /// This string is not case-sensitive.
        /// </param>
        ///
        /// <returns>
        /// An array of the full names (including paths) for the
        /// directories in the specified directory, or an empty array
        /// if no directories are found.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> GetDirectories(string path) => GetDirectories(path, "*");

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectories
        ///
        /// <summary>
        /// Returns the names of the subdirectories (including their paths)
        /// that match the specified search pattern in the specified
        /// directory.
        /// </summary>
        ///
        /// <param name="path">
        /// The relative or absolute path to the directory to search.
        /// This string is not case-sensitive.
        /// </param>
        ///
        /// <param name="pattern">
        /// The search string to match against the names of subdirectories
        /// in path. This parameter can contain a combination of valid
        /// literal and wildcard characters, but doesn't support regular
        /// expressions.
        /// </param>
        ///
        /// <returns>
        /// An array of the full names (including paths) for the
        /// directories in the specified directory that match the specified
        /// search pattern, or an empty array if no directories are found.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> GetDirectories(string path, string pattern) =>
            GetDirectories(path, pattern, SearchOption.TopDirectoryOnly);

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectories
        ///
        /// <summary>
        /// Returns the names of the subdirectories (including their paths)
        /// that match the specified search pattern in the specified
        /// directory, and optionally searches subdirectories.
        /// </summary>
        ///
        /// <param name="path">
        /// The relative or absolute path to the directory to search.
        /// This string is not case-sensitive.
        /// </param>
        ///
        /// <param name="pattern">
        /// The search string to match against the names of subdirectories
        /// in path. This parameter can contain a combination of valid
        /// literal and wildcard characters, but doesn't support regular
        /// expressions.
        /// </param>
        ///
        /// <param name="option">
        /// One of the enumeration values that specifies whether the
        /// search operation should include all subdirectories or only
        /// the current directory.
        /// </param>
        ///
        /// <returns>
        /// An array of the full names (including paths) for the
        /// directories in the specified directory that match the specified
        /// search pattern and option, or an empty array if no directories
        /// are found.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> GetDirectories(string path, string pattern, SearchOption option) =>
            GetDirectoriesCore(path, pattern, option) ?? new string[0];

        /* ----------------------------------------------------------------- */
        ///
        /// GetDirectoriesCore
        ///
        /// <summary>
        /// Returns the names of the subdirectories (including their paths)
        /// that match the specified search pattern in the specified
        /// directory, and optionally searches subdirectories.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual IEnumerable<string> GetDirectoriesCore(string path, string pattern, SearchOption option) =>
            Directory.Exists(path) ? Directory.GetDirectories(path, pattern, option) : null;

        #endregion

        #region Combine

        /* ----------------------------------------------------------------- */
        ///
        /// Combine
        ///
        /// <summary>
        /// Combiles the specified paths.
        /// </summary>
        ///
        /// <param name="paths">Collection of paths.</param>
        ///
        /// <returns>Combined path.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public string Combine(params string[] paths) => CombineCore(paths);

        /* ----------------------------------------------------------------- */
        ///
        /// CombineCore
        ///
        /// <summary>
        /// Combiles the specified paths.
        /// </summary>
        ///
        /// <param name="paths">Collection of paths.</param>
        ///
        /// <returns>Combined path.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual string CombineCore(params string[] paths) => Path.Combine(paths);

        #endregion

        #region SetAttributes

        /* ----------------------------------------------------------------- */
        ///
        /// SetAttributes
        ///
        /// <summary>
        /// Sets the specified attributes to the specified file or directory.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        /// <param name="attr">Attributes to set.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void SetAttributes(string path, FileAttributes attr) =>
            SetAttributesCore(path, attr);

        /* ----------------------------------------------------------------- */
        ///
        /// SetAttributesCore
        ///
        /// <summary>
        /// Sets the specified attributes to the specified file or directory.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        /// <param name="attr">Attributes to set.</param>
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
        /// Sets the specified creation time to the specified file or
        /// directory.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        /// <param name="time">Creation time.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void SetCreationTime(string path, DateTime time) =>
            SetCreationTimeCore(path, time);

        /* ----------------------------------------------------------------- */
        ///
        /// SetCreationTimeCore
        ///
        /// <summary>
        /// Sets the specified creation time to the specified file or
        /// directory.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        /// <param name="time">Creation time.</param>
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
        /// Sets the specified last updated time to the specified file or
        /// directory.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        /// <param name="time">Last updated time.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void SetLastWriteTime(string path, DateTime time) =>
            SetLastWriteTimeCore(path, time);

        /* ----------------------------------------------------------------- */
        ///
        /// SetLastWriteTimeCore
        ///
        /// <summary>
        /// Sets the specified last updated time to the specified file or
        /// directory.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        /// <param name="time">Last updated time.</param>
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
        /// Sets the specified last accessed time to the specified file or
        /// directory.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        /// <param name="time">Last accessed time.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void SetLastAccessTime(string path, DateTime time) =>
            SetLastAccessTimeCore(path, time);

        /* ----------------------------------------------------------------- */
        ///
        /// SetLastAccessTimeCore
        ///
        /// <summary>
        /// Sets the specified last accessed time to the specified file or
        /// directory.
        /// </summary>
        ///
        /// <param name="path">Target path.</param>
        /// <param name="time">Last accessed time.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void SetLastAccessTimeCore(string path, DateTime time)
        {
            if (Directory.Exists(path)) Directory.SetLastAccessTime(path, time);
            else if (File.Exists(path)) File.SetLastAccessTime(path, time);
        }

        #endregion

        #region Delete

        /* ----------------------------------------------------------------- */
        ///
        /// Delete
        ///
        /// <summary>
        /// Deletes the specified file or directory.
        /// </summary>
        ///
        /// <param name="path">Path to delete.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Delete(string path) =>
            Action(nameof(Delete), () => DeleteCore(path), path);

        /* ----------------------------------------------------------------- */
        ///
        /// TryDelete
        ///
        /// <summary>
        /// Tries to delete the specified file or directory.
        /// </summary>
        ///
        /// <param name="path">Path to delete.</param>
        ///
        /// <returns>true for success.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public bool TryDelete(string path)
        {
            try { DeleteCore(path); return true; }
            catch (Exception err) { this.LogWarn(err); }
            return false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DeleteCore
        ///
        /// <summary>
        /// Deletes the specified file or directory.
        /// </summary>
        ///
        /// <param name="path">Path to delete.</param>
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
        /// Creates a directory.
        /// </summary>
        ///
        /// <param name="path">Path to create.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void CreateDirectory(string path) =>
            Action(nameof(CreateDirectory), () => CreateDirectoryCore(path), path);

        /* ----------------------------------------------------------------- */
        ///
        /// CreateDirectoryCore
        ///
        /// <summary>
        /// Creates a directory.
        /// </summary>
        ///
        /// <param name="path">Path to create.</param>
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
        /// Creates or opens the specified file and gets the stream.
        /// </summary>
        ///
        /// <param name="path">Path to create or open file.</param>
        ///
        /// <returns>FileStream object to write.</returns>
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
        /// Creates or opens the specified file and gets the stream.
        /// </summary>
        ///
        /// <param name="path">Path to create or open file.</param>
        ///
        /// <returns>FileStream object to write.</returns>
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
        /// Opens the specified file as read-only.
        /// </summary>
        ///
        /// <param name="path">File path.</param>
        ///
        /// <returns>Read-only stream.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public FileStream OpenRead(string path) =>
            Func(nameof(OpenRead), () => OpenReadCore(path), path);

        /* ----------------------------------------------------------------- */
        ///
        /// OpenReadCore
        ///
        /// <summary>
        /// Opens the specified file as read-only.
        /// </summary>
        ///
        /// <param name="path">File path.</param>
        ///
        /// <returns>Read-only stream.</returns>
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
        /// Creates or opens the specified file and gets the stream.
        /// </summary>
        ///
        /// <param name="path">Path to create or open file.</param>
        ///
        /// <returns>FileStream object to write.</returns>
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
        /// Creates or opens the specified file and gets the stream.
        /// </summary>
        ///
        /// <param name="path">Path to create or open file.</param>
        ///
        /// <returns>FileStream object to write.</returns>
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
        /// Moves the specified file.
        /// </summary>
        ///
        /// <param name="src">Source path.</param>
        /// <param name="dest">Destination path.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Move(string src, string dest) => Move(src, dest, false);

        /* ----------------------------------------------------------------- */
        ///
        /// Move
        ///
        /// <summary>
        /// Moves the specified file.
        /// </summary>
        ///
        /// <param name="src">Source path.</param>
        /// <param name="dest">Destination path.</param>
        /// <param name="overwrite">Overwrite or not.</param>
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
        /// Moves the specified file.
        /// </summary>
        ///
        /// <param name="src">Source path.</param>
        /// <param name="dest">Destination path.</param>
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
        /// Copies the specified file.
        /// </summary>
        ///
        /// <param name="src">Source path.</param>
        /// <param name="dest">Destination path.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Copy(string src, string dest) => Copy(src, dest, false);

        /* ----------------------------------------------------------------- */
        ///
        /// Copy
        ///
        /// <summary>
        /// Copies the specified file.
        /// </summary>
        ///
        /// <param name="src">Source path.</param>
        /// <param name="dest">Destination path.</param>
        /// <param name="overwrite">Overwrite or not.</param>
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
        /// Copies the specified file.
        /// </summary>
        ///
        /// <param name="src">Source path.</param>
        /// <param name="dest">Destination path.</param>
        /// <param name="overwrite">Overwrite or not.</param>
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
        /// Occurs when any operations are failed.
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
        /// Raises the Failed event.
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
        /// Creates a directory and sets the attributes.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void CreateDirectory(string path, Entity src)
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
        /// Creates the parent directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void CreateParentDirectory(Entity info)
        {
            var dir = Get(info.DirectoryName);
            if (!dir.Exists) CreateDirectory(dir.FullName);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CopyDirectory
        ///
        /// <summary>
        /// Copies the specified directory and files.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool CopyDirectory(Entity src, Entity dest, bool overwrite)
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
        /// Copies the file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool CopyFile(Entity src, Entity dest, bool overwrite) =>
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
        /// Moves the directory.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool MoveDirectory(Entity src, Entity dest, bool overwrite)
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
        /// Moves the file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool MoveFile(Entity src, Entity dest, bool overwrite)
        {
            if (!overwrite || !dest.Exists) return MoveFile(src, dest);
            else
            {
                var tmp = Combine(src.DirectoryName, Guid.NewGuid().ToString("D"));
                var ti  = Get(tmp);

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
        /// Moves the file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool MoveFile(Entity src, Entity dest) =>
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
        /// Invokes the action.
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
                try { f(); return true; }
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
        /// Invokes the function.
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
                    if (args.Cancel) return default;
                }
            }
        }

        #endregion

        #region Fields
        private static EntityController _shared;
        #endregion
    }
}
