﻿/* ------------------------------------------------------------------------- */
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
namespace Cube.Tests;

using Cube.FileSystem;

/* ------------------------------------------------------------------------- */
///
/// FileFixture
///
/// <summary>
/// Provides functionality to load or save files for tests.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class FileFixture : SourceFileFixture
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// FileFixture
    ///
    /// <summary>
    /// Initializes a new instance of the FileFixture class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected FileFixture() : base()
    {
        Name    = GetType().FullName;
        Results = Io.Combine(Root, nameof(Results), Name);

        Io.CreateDirectory(Results);
        Delete(Results);
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Results
    ///
    /// <summary>
    /// Gets the path of the directory that test results is saved.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected string Results { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Name
    ///
    /// <summary>
    /// Gets the class name.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected string Name { get; }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Gets the absolute path with the specified file or directory,
    /// assuming that it is in the Results directory.
    /// </summary>
    ///
    /// <param name="paths">
    /// List of file or directory names to be combined as a path.
    /// </param>
    ///
    /// <returns>Combined path.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected string Get(params string[] paths) => Io.Combine(Results, Io.Combine(paths));

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Delete
    ///
    /// <summary>
    /// Deletes all of files and directories in the specified path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void Delete(string directory)
    {
        foreach (var e in Io.GetFiles(directory)) { Io.Delete(e); }
        foreach (var e in Io.GetDirectories(directory)) { Delete(e); Io.Delete(e); }
    }

    #endregion
}
