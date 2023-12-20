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
/// SaveFileMessage
///
/// <summary>
/// Represents information to show the SaveFileDialog.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class SaveFileMessage : OpenOrSaveFileMessage<string>
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileMessage
    ///
    /// <summary>
    /// Initializes a new instance of the SaveFileMessage class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public SaveFileMessage() : this(nameof(SaveFileMessage)) { }

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileMessage
    ///
    /// <summary>
    /// Initializes a new instance of the SaveFileMessage class with
    /// the specified text.
    /// </summary>
    ///
    /// <param name="text">Message text.</param>
    ///
    /* --------------------------------------------------------------------- */
    public SaveFileMessage(string text) : base(text) => Value = string.Empty;

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// OverwritePrompt
    ///
    /// <summary>
    /// Gets or sets a value indicating whether SaveFileDialog displays
    /// a warning if the user specifies the name of a file that already
    /// exists.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool OverwritePrompt { get; set; } = true;

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Set
    ///
    /// <summary>
    /// Sets values to the Value and InitialDirectory properties using the
    /// specified object.
    /// </summary>
    ///
    /// <param name="src">File or directory path information.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void Set(Entity src)
    {
        if (src is null) return;
        Value = src.IsDirectory ? string.Empty : src.Name;
        InitialDirectory = src.IsDirectory ? src.FullName : src.DirectoryName;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetValue
    ///
    /// <summary>
    /// Gets the value of Value property.
    /// </summary>
    ///
    /// <returns>String value.</returns>
    ///
    /* --------------------------------------------------------------------- */
    protected override string GetValue() => Value;

    #endregion
}
