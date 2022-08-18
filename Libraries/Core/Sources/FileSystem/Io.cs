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
using Cube.Mixin.String;

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
    public static void Configure(IoController src) => _controller = src;

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
    public static Entity Get(string path) => new(_controller.GetEntitySource(path), true);

    /* --------------------------------------------------------------------- */
    ///
    /// GetOrDefault
    ///
    /// <summary>
    /// Gets the Entity object from the specified path. If the specified
    /// path is empty or some exception occurs, the method returns null.
    /// </summary>
    ///
    /// <param name="path">Target path.</param>
    ///
    /// <returns>Entity object or null.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static Entity GetOrDefault(string path)
    {
        try { return path.HasValue() ? Get(path) : default; }
        catch (Exception e) { typeof(Io).LogDebug(e.Message); }
        return default;
    }

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
        CreateParentDirectory(Get(path));
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
        if (!Exists(path)) _controller.CreateDirectory(path);
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
    public static void SetAttributes(string path, FileAttributes attr) =>
        _controller.SetAttributes(path, attr);

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
        _controller.SetCreationTime(path, time);

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
        _controller.SetLastWriteTime(path, time);

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
        _controller.SetLastAccessTime(path, time);

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
    public static void Delete(string path) => _controller.Delete(path);

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
        var si = Get(src);
        if (si.IsDirectory) MoveDirectory(si, Get(dest), overwrite);
        else MoveFile(si, Get(dest), overwrite);
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
        var si = Get(src);
        if (si.IsDirectory) CopyDirectory(si, Get(dest), overwrite);
        else CopyFile(si, Get(dest), overwrite);
    }

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
    /// CreateParentDirectory
    ///
    /// <summary>
    /// Creates the parent directory.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void CreateParentDirectory(Entity info)
    {
        var dir = Get(info.DirectoryName);
        if (!dir.Exists) CreateDirectory(dir.FullName);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CreateDirectory
    ///
    /// <summary>
    /// Creates a directory and sets the attributes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void CreateDirectory(string path, Entity src)
    {
        CreateDirectory(path);
        SetCreationTime(path, src.CreationTime);
        SetLastWriteTime(path, src.LastWriteTime);
        SetLastAccessTime(path, src.LastAccessTime);
        SetAttributes(path, src.Attributes);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CopyDirectory
    ///
    /// <summary>
    /// Copies the specified directory and files.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void CopyDirectory(Entity src, Entity dest, bool overwrite)
    {
        if (!dest.Exists) CreateDirectory(dest.FullName, src);

        foreach (var file in GetFiles(src.FullName))
        {
            var si = Get(file);
            var di = Get(Combine(dest.FullName, si.Name));
            CopyFile(si, di, overwrite);
        }

        foreach (var dir in GetDirectories(src.FullName))
        {
            var si = Get(dir);
            var di = Get(Combine(dest.FullName, si.Name));
            CopyDirectory(si, di, overwrite);
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CopyFile
    ///
    /// <summary>
    /// Copies the file.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void CopyFile(Entity src, Entity dest, bool overwrite)
    {
        CreateParentDirectory(dest);
        _controller.Copy(src.FullName, dest.FullName, overwrite);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MoveDirectory
    ///
    /// <summary>
    /// Moves the directory.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void MoveDirectory(Entity src, Entity dest, bool overwrite)
    {
        if (!dest.Exists) CreateDirectory(dest.FullName, src);

        foreach (var file in GetFiles(src.FullName))
        {
            var si = Get(file);
            var di = Get(Combine(dest.FullName, si.Name));
            MoveFile(si, di, overwrite);
        }

        foreach (var dir in GetDirectories(src.FullName))
        {
            var si = Get(dir);
            var di = Get(Combine(dest.FullName, si.Name));
            MoveDirectory(si, di, overwrite);
        }

        Delete(src.FullName);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MoveFile
    ///
    /// <summary>
    /// Moves the file.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void MoveFile(Entity src, Entity dest, bool overwrite)
    {
        if (!dest.Exists) { MoveFile(src, dest); return; }
        if (!overwrite) return;

        var tmp = Get(Combine(src.DirectoryName, Guid.NewGuid().ToString("N")));
        MoveFile(dest, tmp);

        try
        {
            MoveFile(src, dest);
            typeof(Io).LogWarn(() => Delete(tmp.FullName));
        }
        catch
        {
            MoveFile(tmp, dest); // recover
            throw;
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MoveFile
    ///
    /// <summary>
    /// Moves the file.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void MoveFile(Entity src, Entity dest)
    {
        CreateParentDirectory(dest);
        _controller.Move(src.FullName, dest.FullName);
    }

    #endregion

    #region Fields
    private static IoController _controller = new();
    #endregion
}
