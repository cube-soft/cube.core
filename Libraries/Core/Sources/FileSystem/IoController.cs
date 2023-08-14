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
using System.Linq;

/* ------------------------------------------------------------------------- */
///
/// IoController
///
/// <summary>
/// Provides functionality to control methods of the IO class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class IoController
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// GetEntitySource
    ///
    /// <summary>
    /// Gets the EntitySource object with the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Source path.</param>
    ///
    /// <returns>EntitySource object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public virtual EntitySource GetEntitySource(string src) => new(src);

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
    public virtual bool Exists(string path) => File.Exists(path) || Directory.Exists(path);

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
    public virtual bool IsDirectory(string path) => Directory.Exists(path);

    /* --------------------------------------------------------------------- */
    ///
    /// Open
    ///
    /// <summary>
    /// Opens the specified file as read-only.
    /// </summary>
    ///
    /// <param name="path">Path to open file.</param>
    ///
    /// <returns>Read-only stream.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public virtual FileStream Open(string path) => File.OpenRead(path);

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
    public virtual FileStream Create(string path) => File.Create(path);

    /* --------------------------------------------------------------------- */
    ///
    /// CreateDirectory
    ///
    /// <summary>
    /// Creates a directory.
    /// </summary>
    ///
    /// <param name="path">Path to create.</param>
    ///
    /* --------------------------------------------------------------------- */
    public virtual void CreateDirectory(string path) => Directory.CreateDirectory(path);

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
    public virtual void SetAttributes(string path, FileAttributes attr)
    {
        if (Directory.Exists(path)) _ = new DirectoryInfo(path) { Attributes = attr };
        else if (File.Exists(path)) File.SetAttributes(path, attr);
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
    public virtual void SetCreationTime(string path, DateTime time)
    {
        if (Directory.Exists(path)) Directory.SetCreationTime(path, time);
        else if (File.Exists(path)) File.SetCreationTime(path, time);
    }

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
    public virtual void SetLastWriteTime(string path, DateTime time)
    {
        if (Directory.Exists(path)) Directory.SetLastWriteTime(path, time);
        else if (File.Exists(path)) File.SetLastWriteTime(path, time);
    }

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
    public virtual void SetLastAccessTime(string path, DateTime time)
    {
        if (Directory.Exists(path)) Directory.SetLastAccessTime(path, time);
        else if (File.Exists(path)) File.SetLastAccessTime(path, time);
    }

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
    public virtual void Delete(string path)
    {
        if (Directory.Exists(path)) Directory.Delete(path, false);
        else if (File.Exists(path)) File.Delete(path);
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
    ///
    /* --------------------------------------------------------------------- */
    public virtual void Move(string src, string dest) => File.Move(src, dest);

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
    public virtual void Copy(string src, string dest, bool overwrite) =>
        File.Copy(src, dest, overwrite);

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
    public virtual string Combine(params string[] paths) => Path.Combine(paths);

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
    /// The characters after the last directory separator character in path.
    /// If the last character of path is a directory or volume separator
    /// character, this method returns Empty. If path is null, this method
    /// returns null.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public virtual string GetFileName(string src) => Path.GetFileName(src);

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
    /// The string returned by GetFileName method, minus the last period
    /// and all characters following it.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public virtual string GetBaseName(string src) => Path.GetFileNameWithoutExtension(src);

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
    /// <returns>
    /// The extension of the specified path, or null, or Empty. If path is
    /// null, GetExtension method returns null. If path does not have
    /// extension information, GetExtension method returns Empty.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public virtual string GetExtension(string src) => Path.GetExtension(src);

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
    /// <returns>
    /// Directory information for path, or null if path denotes a root
    /// directory or is null. Returns Empty if path does not contain
    /// directory information.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public virtual string GetDirectoryName(string src) => Path.GetDirectoryName(src);

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
    /// The search string to match against the names of files in path.
    /// This parameter can contain a combination of valid literal path
    /// and wildcard (* and ?) characters, but it doesn't support
    /// regular expressions.
    /// </param>
    ///
    /// <param name="option">
    /// One of the enumeration values that specifies whether the search
    /// operation should include only the current directory or should
    /// include all subdirectories.
    /// </param>
    ///
    /// <returns>
    /// An array of the full names (including paths) for the files
    /// in the specified directory that match the specified search
    /// pattern and option, or an empty array if no files are found.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public virtual IEnumerable<string> GetFiles(string path, string pattern, SearchOption option) =>
        Directory.Exists(path) ?
        Directory.EnumerateFiles(path, pattern, option) :
        Enumerable.Empty<string>();

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
    public virtual IEnumerable<string> GetDirectories(string path, string pattern, SearchOption option) =>
        Directory.Exists(path) ?
        Directory.EnumerateDirectories(path, pattern, option) :
        Enumerable.Empty<string>();

    #endregion
}
