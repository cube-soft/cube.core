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
namespace Cube;

using Cube.FileSystem;

/* ------------------------------------------------------------------------- */
///
/// OpenDirectoryMessage
///
/// <summary>
/// Represents information to show the FolderBrowserDialog.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class OpenDirectoryMessage : CancelMessage<string>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// OpenDirectoryMessage
    ///
    /// <summary>
    /// Initializes a new instance of the OpenDirectoryMessage class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public OpenDirectoryMessage() : base(nameof(OpenDirectoryMessage)) => Setup(default);

    /* --------------------------------------------------------------------- */
    ///
    /// OpenDirectoryMessage
    ///
    /// <summary>
    /// Initializes a new instance of the OpenDirectoryMessage class
    /// with the specified path.
    /// </summary>
    ///
    /// <param name="src">Entity of the initial path.</param>
    ///
    /* --------------------------------------------------------------------- */
    public OpenDirectoryMessage(Entity src) : base(nameof(OpenDirectoryMessage)) => Setup(src);

    /* --------------------------------------------------------------------- */
    ///
    /// OpenDirectoryMessage
    ///
    /// <summary>
    /// Initializes a new instance of the OpenDirectoryMessage class
    /// with the specified path.
    /// </summary>
    ///
    /// <param name="src">Entity of the initial path.</param>
    /// <param name="text">Message text.</param>
    ///
    /* --------------------------------------------------------------------- */
    public OpenDirectoryMessage(Entity src, string text) : base(text) => Setup(src);

    /* --------------------------------------------------------------------- */
    ///
    /// OpenDirectoryMessage
    ///
    /// <summary>
    /// Initializes a new instance of the OpenDirectoryMessage class
    /// with the specified path.
    /// </summary>
    ///
    /// <param name="src">Initial path.</param>
    ///
    /* --------------------------------------------------------------------- */
    public OpenDirectoryMessage(string src) : this(Io.GetOrDefault(src)) { }

    /* --------------------------------------------------------------------- */
    ///
    /// OpenDirectoryMessage
    ///
    /// <summary>
    /// Initializes a new instance of the OpenDirectoryMessage class
    /// with the specified path.
    /// </summary>
    ///
    /// <param name="src">Initial path.</param>
    /// <param name="text">Message text.</param>
    ///
    /* --------------------------------------------------------------------- */
    public OpenDirectoryMessage(string src, string text) : this(Io.GetOrDefault(src), text) { }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// NewButton
    ///
    /// <summary>
    /// Gets or sets a value indicating whether the New Folder button
    /// appears in the FolderBrowserDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool NewButton { get; set; } = true;

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Setup
    ///
    /// <summary>
    /// Invokes the initialization.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void Setup(Entity src) => Value =
        src is null     ? string.Empty :
        src.IsDirectory ? src.FullName :
                          src.DirectoryName;


    #endregion
}
