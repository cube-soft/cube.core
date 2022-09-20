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
using System.IO;
using System.Linq;
using Cube.Backports;

/* ------------------------------------------------------------------------- */
///
/// IoEx
///
/// <summary>
/// Provides utility methods for a path, file, or directory.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class IoEx
{
    #region Load and Save

    /* --------------------------------------------------------------------- */
    ///
    /// Load
    ///
    /// <summary>
    /// Creates a new stream from the specified file and executes
    /// the specified callback.
    /// </summary>
    ///
    /// <param name="src">Path of the source file.</param>
    /// <param name="callback">User action.</param>
    ///
    /// <returns>Executed result.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static T Load<T>(string src, Func<Stream, T> callback)
    {
        using var ss = Io.Open(src);
        return callback(ss);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Load
    ///
    /// <summary>
    /// Creates a new stream from the specified file and executes
    /// the specified callback.
    /// </summary>
    ///
    /// <param name="src">Path of the source file.</param>
    /// <param name="callback">User action.</param>
    ///
    /// <returns>Executed result.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static T Load<T>(string src, Func<TextReader, T> callback)
    {
        var code = System.Text.Encoding.UTF8;
        using var ss = new StreamReader(Io.Open(src), code);
        return callback(ss);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Save
    ///
    /// <summary>
    /// Creates a new memory stream, executes the specified callback,
    /// and writes the result to the specified file.
    /// </summary>
    ///
    /// <param name="dest">Path of the writing file.</param>
    /// <param name="callback">User action.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Save(string dest, Action<Stream> callback)
    {
        using var ss = new MemoryStream();
        callback(ss);

        using var ds = Io.Create(dest);
        ss.Position = 0;
        ss.CopyTo(ds);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Save
    ///
    /// <summary>
    /// Creates a new memory stream, executes the specified callback,
    /// and writes the result to the specified file.
    /// </summary>
    ///
    /// <param name="dest">Path of the writing file.</param>
    /// <param name="callback">User action.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Save(string dest, Action<TextWriter> callback)
    {
        var code = System.Text.Encoding.UTF8;
        using var ss = new StreamWriter(new MemoryStream(), code);
        callback(ss);

        using var ds = Io.Create(dest);
        ss.BaseStream.Position = 0;
        ss.BaseStream.CopyTo(ds);
    }

    #endregion

    #region Touch

    /* --------------------------------------------------------------------- */
    ///
    /// Touch
    ///
    /// <summary>
    /// Creates a new file or updates the timestamp of the specified
    /// path.
    /// </summary>
    ///
    /// <param name="src">Path to create or update.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Touch(string src) => Touch(src, DateTime.Now);

    /* --------------------------------------------------------------------- */
    ///
    /// Touch
    ///
    /// <summary>
    /// Creates a new file or updates the timestamp of the specified
    /// path.
    /// </summary>
    ///
    /// <param name="src">Path to create or update.</param>
    /// <param name="timestamp">Timestamp to set.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Touch(string src, DateTime timestamp)
    {
        using (Io.Create(src)) { }
        Io.SetLastWriteTime(src, timestamp);
    }

    #endregion

    #region Rename

    /* --------------------------------------------------------------------- */
    ///
    /// Rename
    ///
    /// <summary>
    /// Changes the filename of a path string.
    /// </summary>
    ///
    /// <param name="src">Source path.</param>
    /// <param name="filename">Filename to rename.</param>
    ///
    /// <returns>Renamed path.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string Rename(string src, string filename) =>
        Io.Combine(Io.Get(src).DirectoryName, filename);

    /* --------------------------------------------------------------------- */
    ///
    /// RenameExtension
    ///
    /// <summary>
    /// Changes the extension of a path string.
    /// </summary>
    ///
    /// <param name="src">Source path.</param>
    /// <param name="extension">Extension to rename.</param>
    ///
    /// <returns>Renamed path.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string RenameExtension(string src, string extension)
    {
        var e = Io.Get(src);
        return Io.Combine(e.DirectoryName, $"{e.BaseName}{extension}");
    }

    #endregion

    #region GetUniqueName

    /* --------------------------------------------------------------------- */
    ///
    /// GetUniqueName
    ///
    /// <summary>
    /// Gets a unique name with the specified path.
    /// </summary>
    ///
    /// <param name="src">Base path.</param>
    ///
    /// <returns>Unique name.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string GetUniqueName(string src) => GetUniqueName(src, (e, i) =>
    {
        var fi = Io.Get(e);
        return Io.Combine(fi.DirectoryName, $"{fi.BaseName}({i}){fi.Extension}");
    });

    /* --------------------------------------------------------------------- */
    ///
    /// GetUniqueName
    ///
    /// <summary>
    /// Gets a unique name with the specified path.
    /// </summary>
    ///
    /// <param name="src">Path to check.</param>
    /// <param name="converter">Function to convert path.</param>
    ///
    /// <returns>Unique name.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string GetUniqueName(string src, Func<string, int, string> converter) =>
        Io.Exists(src) ?
        Enumerable.Range(1, int.MaxValue).Select(e => converter(src, e)).First(e => !Io.Exists(e)) :
        src;

    #endregion

    #region GetEntitySource

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
    public static EntitySource GetEntitySource(string src) => Io.GetController().GetEntitySource(src);

    #endregion
}
