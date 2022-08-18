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
    public SaveFileMessage() : base(nameof(SaveFileMessage)) => Setup(default);

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileMessage
    ///
    /// <summary>
    /// Initializes a new instance of the SaveFileMessage class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Entity for the initial path.</param>
    ///
    /* --------------------------------------------------------------------- */
    public SaveFileMessage(Entity src) : base(nameof(SaveFileMessage)) => Setup(src);

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileMessage
    ///
    /// <summary>
    /// Initializes a new instance of the SaveFileMessage class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Entity for the initial path.</param>
    /// <param name="text">Message text.</param>
    ///
    /* --------------------------------------------------------------------- */
    public SaveFileMessage(Entity src, string text) : base(text) => Setup(src);

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileMessage
    ///
    /// <summary>
    /// Initializes a new instance of the SaveFileMessage class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Initial path.</param>
    ///
    /* --------------------------------------------------------------------- */
    public SaveFileMessage(string src) : this(Io.GetOrDefault(src)) { }

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileMessage
    ///
    /// <summary>
    /// Initializes a new instance of the SaveFileMessage class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Initial path.</param>
    /// <param name="text">Message text.</param>
    ///
    /* --------------------------------------------------------------------- */
    public SaveFileMessage(string src, string text) : this(Io.GetOrDefault(src), text) { }

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
    private void Setup(Entity src)
    {
        if (src is not null)
        {
            Value = src.IsDirectory ? string.Empty : src.Name;
            InitialDirectory = src.IsDirectory ? src.FullName : src.DirectoryName;
        }
        else Value = string.Empty;
    }

    #endregion
}
