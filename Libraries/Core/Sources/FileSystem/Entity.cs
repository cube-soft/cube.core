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

/* ------------------------------------------------------------------------- */
///
/// Entity
///
/// <summary>
/// Represents the file or directory information.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[Serializable]
public class Entity
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// Entity
    ///
    /// <summary>
    /// Creates a new instance of the Entity class with the specified
    /// source object.
    /// </summary>
    ///
    /// <param name="src">Source object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Entity(Entity src)
    {
        RawName        = src.RawName;
        Exists         = src.Exists;
        IsDirectory    = src.IsDirectory;
        Name           = src.Name;
        BaseName       = src.BaseName;
        Extension      = src.Extension;
        FullName       = src.FullName;
        DirectoryName  = src.DirectoryName;
        Length         = src.Length;
        Attributes     = src.Attributes;
        CreationTime   = src.CreationTime;
        LastWriteTime  = src.LastWriteTime;
        LastAccessTime = src.LastAccessTime;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Entity
    ///
    /// <summary>
    /// Creates a new instance of the Entity class with the specified
    /// file or directory path.
    /// </summary>
    ///
    /// <param name="src">File or directory path.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Entity(string src) : this(Io.GetController().GetEntitySource(src)) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Entity
    ///
    /// <summary>
    /// Creates a new instance of the Entity class with the specified
    /// source object.
    /// </summary>
    ///
    /// <param name="src">Source object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public Entity(EntitySource src) : this(src, true) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Entity
    ///
    /// <summary>
    /// Creates a new instance of the Entity class with the specified
    /// arguments.
    /// </summary>
    ///
    /// <param name="src">Source object.</param>
    /// <param name="dispose">
    /// Value indicating whether to dispose the specified src object
    /// after initialization.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public Entity(EntitySource src, bool dispose)
    {
        if (src is null) throw new ArgumentNullException(nameof(src));

        try
        {
            src.Setup();

            RawName        = src.RawName;
            Exists         = src.Exists;
            IsDirectory    = src.IsDirectory;
            Name           = src.Name;
            BaseName       = src.BaseName;
            Extension      = src.Extension;
            FullName       = src.FullName;
            DirectoryName  = src.DirectoryName;
            Length         = src.Length;
            Attributes     = src.Attributes;
            CreationTime   = src.CreationTime;
            LastWriteTime  = src.LastWriteTime;
            LastAccessTime = src.LastAccessTime;
        }
        finally { if (dispose) src.Dispose(); }
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// RawName
    ///
    /// <summary>
    /// Gets the original path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string RawName { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Exists
    ///
    /// <summary>
    /// Gets the value indicating whether the Source exists.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool Exists { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// IsDirectory
    ///
    /// <summary>
    /// Gets a value indicating whether the provided path is a directory.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool IsDirectory { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Name
    ///
    /// <summary>
    /// Gets the filename.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Name { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// BaseName
    ///
    /// <summary>
    /// Gets the filename without extension.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string BaseName { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Extension
    ///
    /// <summary>
    /// Gets the extension part of the filename.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Extension { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// FullName
    ///
    /// <summary>
    /// Gets the full path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string FullName { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// DirectoryName
    ///
    /// <summary>
    /// Gets the directory name.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string DirectoryName { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Length
    ///
    /// <summary>
    /// Gets the file-size.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public long Length { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Attributes
    ///
    /// <summary>
    /// Gets the attributes of the file or directory.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public FileAttributes Attributes { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// CreationTime
    ///
    /// <summary>
    /// Gets the creation time of the file or directory.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public DateTime CreationTime { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// LastWriteTime
    ///
    /// <summary>
    /// Gets the last written time of the file or directory.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public DateTime LastWriteTime { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// LastAccessTime
    ///
    /// <summary>
    /// Gets the last accessed time of the file or directory.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public DateTime LastAccessTime { get; }

    #endregion
}
