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
namespace Cube.Forms.Demo;

using System.Linq;
using System.Threading;
using Cube.Generics.Extensions;

/* ------------------------------------------------------------------------- */
///
/// FileViewModel
///
/// <summary>
/// Represents the ViewModel for the window to test the OpenFileDialog,
/// SaveFileDialog, and SaveDirectoryDialog.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class FileViewModel : PresentableBase
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// FileViewModel
    ///
    /// <summary>
    /// Initializes a new instance of the FileViewModel class with the
    /// specified context.
    /// </summary>
    ///
    /// <param name="context">Synchronization context.</param>
    ///
    /* --------------------------------------------------------------------- */
    public FileViewModel(SynchronizationContext context) : base(new(), context) { }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Value
    ///
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Value
    {
        get => Get(() => string.Empty);
        set => Set(value);
    }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// ShowOpenFileDialog
    ///
    /// <summary>
    /// Invokes the command to show a dialog to test the OpenFileDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void ShowOpenFileDialog() => Send(
        new OpenFileMessage() { Value = new[] { Value } },
        e => Value = e.First(),
        true
    );

    /* --------------------------------------------------------------------- */
    ///
    /// ShowSaveFileDialog
    ///
    /// <summary>
    /// Invokes the command to show a dialog to test the SaveFileDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void ShowSaveFileDialog() => Send(
        new SaveFileMessage() { Value = Value },
        e => Value = e,
        true
    );

    /* --------------------------------------------------------------------- */
    ///
    /// ShowOpenDirectoryDialog
    ///
    /// <summary>
    /// Invokes the command to show a dialog to test the OpenDirectoryDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void ShowOpenDirectoryDialog() => Send(
        new OpenDirectoryMessage() { Value = Value },
        e => Value = e,
        true
    );

    #endregion
}
