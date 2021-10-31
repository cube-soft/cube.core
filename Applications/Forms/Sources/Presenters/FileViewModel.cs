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
using System.Threading;

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileViewModel
    ///
    /// <summary>
    /// Represents the ViewModel for the window to test the OpenFileDialog,
    /// SaveFileDialog, and SaveDirectoryDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
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
        public string Value { get; set; }

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
        public void ShowOpenFileDialog() => Send(new OpenFileMessage(Value));

        /* --------------------------------------------------------------------- */
        ///
        /// ShowSaveFileDialog
        ///
        /// <summary>
        /// Invokes the command to show a dialog to test the SaveFileDialog.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void ShowSaveFileDialog() => Send(new SaveFileMessage(Value));

        /* --------------------------------------------------------------------- */
        ///
        /// ShowOpenDirectoryDialog
        ///
        /// <summary>
        /// Invokes the command to show a dialog to test the OpenDirectoryDialog.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void ShowOpenDirectoryDialog() => Send(new OpenDirectoryMessage(Value));

        #endregion
    }
}
