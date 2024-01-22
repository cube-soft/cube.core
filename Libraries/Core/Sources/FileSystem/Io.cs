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
namespace Cube.FileSystem;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cube.Text.Extensions;

/* ------------------------------------------------------------------------- */
///
/// Io
///
/// <summary>
/// Provides functionality to do something to a path, file, or directory.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Io
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Configure
    ///
    /// <summary>
    /// Sets the specified object as the controller of the class.
    /// </summary>
    ///
    /// <param name="src">I/O controller.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Configure(IoController src) => Interlocked.Exchange(ref _controller, src);

    /* --------------------------------------------------------------------- */
    ///
    /// Exists
    ///
    /// <summary>
    /// Determines if the specified file or directory exists.
    /// </summary>
    ///
    /// <param name="path">Path to check.</param>
    ///
    /// <returns>true for exists.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static bool Exists(string path) => _controller.Exists(path);

    /* --------------------------------------------------------------------- */
    ///
    /// IsDirectory
    ///
    /// <summary>
    /// Determines if the specified path is directory.
    /// </summary>
    ///
    /// <param name="path">Path to check.</param>
    ///
    /// <returns>true for exists and is directory.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static bool IsDirectory(string path) => _controller.IsDirectory(path);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    [Obsolete("Use the constructor of the Entity class instead.")]
    public static Entity Get(string path) => new(_controller.GetEntitySource(path), true);

    /* --------------------------------------------------------------------- */
    ///
    /// Open
    ///
    /// <summary>
    /// Opens the specified file as read-only.
    /// </summary>
    ///
    /// <param name="path">File path.</param>
    ///
    /// <returns>Read-only stream.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static FileStream Open(string path) => _controller.Open(path);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static FileStream Create(string path)
    {
        CreateDirectory(GetDirectoryName(path));
        return _controller.Create(path);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CreateDirectory
    ///
    /// <summary>
    /// Creates a directory. If a file or directory with the specified
    /// path exists, the method will be skipped.
    /// </summary>
    ///
    /// <param name="path">Path to create.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void CreateDirectory(string path)
    {
        if (path.HasValue() && !Exists(path)) _controller.CreateDirectory(path);
    }

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static void SetAttributes(string path, FileAttributes attr)
    {
        if (Exists(path)) _controller.SetAttributes(path, attr);
    }

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static void SetCreationTime(string path, DateTime time) =>
        Set(path, e => _controller.SetCreationTime(e, time));

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static void SetLastWriteTime(string path, DateTime time) =>
        Set(path, e => _controller.SetLastWriteTime(e, time));

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static void SetLastAccessTime(string path, DateTime time) =>
        Set(path, e => _controller.SetLastAccessTime(e, time));

    /* --------------------------------------------------------------------- */
    ///
    /// SetTime
    ///
    /// <summary>
    /// Sets the specified time objects to the specified file or directory.
    /// </summary>
    ///
    /// <param name="path">Target path.</param>
    /// <param name="create">Creation time.</param>
    /// <param name="write">Last updated time.</param>
    /// <param name="access">Last accessed time.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void SetTime(string path, DateTime create, DateTime write, DateTime access) => Set(path, e =>
    {
        _controller.SetCreationTime(e, create);
        _controller.SetLastWriteTime(e, write);
        _controller.SetLastAccessTime(e, access);
    });

    /* --------------------------------------------------------------------- */
    ///
    /// Delete
    ///
    /// <summary>
    /// Deletes the specified file or directory.
    /// If the specified path is a directory and has subdirectories,
    /// the method will recursively remove all of them.
    /// </summary>
    ///
    /// <param name="path">Path to delete.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Delete(string path)
    {
        if (IsDirectory(path))
        {
            foreach (var f in GetFiles(path)) Delete(f);
            foreach (var d in GetDirectories(path)) Delete(d);
            SetAttributes(path, FileAttributes.Normal | FileAttributes.Directory);
            _controller.Delete(path);
        }
        else if (Exists(path))
        {
            SetAttributes(path, FileAttributes.Normal);
            _controller.Delete(path);
        }
    }

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static void Move(string src, string dest, bool overwrite)
    {
        if (IsDirectory(src)) MoveRecursive(src, dest, overwrite);
        else MoveOne(src, dest, overwrite);
    }

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static void Copy(string src, string dest, bool overwrite)
    {
        if (IsDirectory(src)) CopyRecursive(src, dest, overwrite);
        else CopyOne(src, dest, overwrite);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Combine
    ///
    /// <summary>
    /// Combines the specified paths.
    /// </summary>
    ///
    /// <param name="paths">Collection of paths.</param>
    ///
    /// <returns>Combined path.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string Combine(params string[] paths) => _controller.Combine(paths);

    /* --------------------------------------------------------------------- */
    ///
    /// GetFileName
    ///
    /// <summary>
    /// Gets the filename and extension of the specified path string.
    /// </summary>
    ///
    /// <param name="src">Path of the file or directory.</param>
    ///
    /// <returns>
    /// Filename and extension of the specified path string.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string GetFileName(string src) => _controller.GetFileName(src);

    /* --------------------------------------------------------------------- */
    ///
    /// GetBaseName
    ///
    /// <summary>
    /// Gets the filename of the specified path string without the
    /// extension.
    /// </summary>
    ///
    /// <param name="src">Path of the file or directory.</param>
    ///
    /// <returns>
    /// Filename of the specified path string without the extension.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string GetBaseName(string src) => _controller.GetBaseName(src);

    /* --------------------------------------------------------------------- */
    ///
    /// GetExtension
    ///
    /// <summary>
    /// Gets the extension of the specified path string.
    /// </summary>
    ///
    /// <param name="src">Path of the file or directory.</param>
    ///
    /// <returns>Extension of the specified path string.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string GetExtension(string src) => _controller.GetExtension(src);

    /* --------------------------------------------------------------------- */
    ///
    /// GetDirectoryName
    ///
    /// <summary>
    /// Gets the directory name for the specified path.
    /// </summary>
    ///
    /// <param name="src">Path of the file or directory.</param>
    ///
    /// <returns>Directory name for the specified path.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string GetDirectoryName(string src) => _controller.GetDirectoryName(src);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static IEnumerable<string> GetFiles(
        string path,
        string pattern = "*",
        SearchOption option = SearchOption.TopDirectoryOnly
    ) => _controller.GetFiles(path, pattern, option);

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static IEnumerable<string> GetDirectories(
        string path,
        string pattern = "*",
        SearchOption option = SearchOption.TopDirectoryOnly
    ) => _controller.GetDirectories(path, pattern, option);

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// GetController
    ///
    /// <summary>
    /// Get the current I/O controller. The method mainly used by the
    /// IoEx static class.
    /// </summary>
    ///
    /// <returns>IoController object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    internal static IoController GetController() => _controller;

    /* --------------------------------------------------------------------- */
    ///
    /// CreateDirectory
    ///
    /// <summary>
    /// Creates a directory and sets the attributes.
    /// </summary>
    ///
    /// <remarks>
    /// NOTE: Use Logger.Try for a while because IOException may sometimes
    /// occur.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    private static void CreateDirectory(string path, Entity src)
    {
        CreateDirectory(path);
        Logger.Try(() => SetAttributes(path, FileAttributes.Normal | FileAttributes.Directory));
        Logger.Try(() => _controller.SetCreationTime(path, src.CreationTime));
        Logger.Try(() => _controller.SetLastWriteTime(path, src.LastWriteTime));
        Logger.Try(() => SetAttributes(path, src.Attributes));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetUnlockAttributes
    ///
    /// <summary>
    /// Get file attributes to allow modification.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static FileAttributes GetUnlockAttributes(FileAttributes src) =>
        src.HasFlag(FileAttributes.Directory) ?
        FileAttributes.Normal | FileAttributes.Directory :
        FileAttributes.Normal;

    /* --------------------------------------------------------------------- */
    ///
    /// Set
    ///
    /// <summary>
    /// Unlocks the specified file and invokes the specified setter action.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void Set(string path, Action<string> setter)
    {
        if (!Exists(path)) return;

        try { setter(path); }
        catch (UnauthorizedAccessException err)
        {
            Logger.Debug(err.Message);
            var e = new Entity(path);
            SetAttributes(path, GetUnlockAttributes(e.Attributes));
            try { setter(path); }
            finally { SetAttributes(path, e.Attributes); }
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CopyRecursive
    ///
    /// <summary>
    /// Copies files and directories existing under the specified directory
    /// recursively.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void CopyRecursive(string src, string dest, bool overwrite)
    {
        if (!Exists(dest)) CreateDirectory(dest, new(src));
        foreach (var e in GetFiles(src)) CopyOne(e, Combine(dest, GetFileName(e)), overwrite);
        foreach (var e in GetDirectories(src)) CopyRecursive(e, Combine(dest, GetFileName(e)), overwrite);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CopyOne
    ///
    /// <summary>
    /// Copies the file.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void CopyOne(string src, string dest, bool overwrite) =>
        MoveOrCopy(src, dest, (s, d) => _controller.Copy(s, d, overwrite));

    /* --------------------------------------------------------------------- */
    ///
    /// MoveRecursive
    ///
    /// <summary>
    /// Moves files and directories existing under the specified directory
    /// recursively.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void MoveRecursive(string src, string dest, bool overwrite)
    {
        if (!Exists(dest)) CreateDirectory(dest, new(src));
        foreach (var e in GetFiles(src)) MoveOne(e, Combine(dest, GetFileName(e)), overwrite);
        foreach (var e in GetDirectories(src)) MoveRecursive(e, Combine(dest, GetFileName(e)), overwrite);
        Delete(src);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MoveOne
    ///
    /// <summary>
    /// Moves the file.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void MoveOne(string src, string dest, bool overwrite)
    {
        static void move(string s, string d) => MoveOrCopy(s, d, _controller.Move);

        if (!Exists(dest)) { move(src, dest); return; }
        if (!overwrite) return;

        var tmp = Combine(Path.GetTempPath(), Guid.NewGuid().ToString("n"));
        move(dest, tmp);

        try
        {
            move(src, dest);
            Logger.Try(() => Delete(tmp));
        }
        catch
        {
            move(tmp, dest); // recover
            throw;
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MoveOrCopy
    ///
    /// <summary>
    /// Unlocks the specified file and invokes the specified move or copy
    /// action.
    /// </summary>
    ///
    /// <remarks>
    /// NOTE: Use Logger.Try for a while because IOException may sometimes
    /// occur.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    private static void MoveOrCopy(string src, string dest, Action<string, string> action)
    {
        CreateDirectory(GetDirectoryName(dest));

        var e = new Entity(src);
        var unlock = GetUnlockAttributes(e.Attributes);

        try
        {
            action(src, dest);

            try
            {
                _controller.SetCreationTime(dest, e.CreationTime);
                _controller.SetLastWriteTime(dest, e.LastWriteTime);
            }
            catch (UnauthorizedAccessException err)
            {
                Logger.Warn(err.Message);
                Logger.Try(() => SetAttributes(dest, unlock));
                Logger.Try(() => _controller.SetCreationTime(dest, e.CreationTime));
                Logger.Try(() => _controller.SetLastWriteTime(dest, e.LastWriteTime));
            }
            catch (Exception err) { Logger.Warn(err); }
        }
        catch (UnauthorizedAccessException err)
        {
            Logger.Warn(err.Message);
            if (Exists(dest)) SetAttributes(dest, unlock);
            action(src, dest);
            Logger.Try(() => SetAttributes(dest, unlock));
            Logger.Try(() => _controller.SetCreationTime(dest, e.CreationTime));
            Logger.Try(() => _controller.SetLastWriteTime(dest, e.LastWriteTime));
        }

        if (Exists(dest)) Logger.Try(() => SetAttributes(dest, e.Attributes));
    }

    #endregion

    #region Fields
    private static IoController _controller = new();
    #endregion
}
