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
namespace Cube.Tests;

using Cube.FileSystem;
using Cube.Reflection.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// SourceFileFixture
///
/// <summary>
/// Provides functionality to get example files.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public abstract class SourceFileFixture
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// SourceFileFixture
    ///
    /// <summary>
    /// Initializes a new instance of the SourceFileFixture class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected SourceFileFixture()
    {
        Root     = GetType().Assembly.GetDirectoryName();
        Examples = Io.Combine(Root, nameof(Examples));
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Root
    ///
    /// <summary>
    /// Gets the path of the root directory that has test resources.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected string Root { get; }

    /* --------------------------------------------------------------------- */
    ///
    /// Examples
    ///
    /// <summary>
    /// Gets the path of the directory that has example files for tests.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected string Examples { get; }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// GetSource
    ///
    /// <summary>
    /// Gets the absolute path with the specified file or directory,
    /// assuming that it is in the Examples directory.
    /// </summary>
    ///
    /// <param name="paths">
    /// List of file or directory names to be combined as a path.
    /// </param>
    ///
    /// <returns>Combined path.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected string GetSource(params string[] paths) => Io.Combine(Examples, Io.Combine(paths));

    /* --------------------------------------------------------------------- */
    ///
    /// Teardown
    ///
    /// <summary>
    /// Invokes the tear-down operation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TearDown]
    protected virtual void Teardown() => Io.Configure(new());

    #endregion
}
