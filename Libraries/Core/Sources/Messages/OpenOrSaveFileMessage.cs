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
namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// OpenOrSaveFileMessage(TValue)
    ///
    /// <summary>
    /// Represents shared information to show either the OpenFileDialog
    /// or SaveFileDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class OpenOrSaveFileMessage<TValue> : CancelMessage<TValue>
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// CheckPathExists
        ///
        /// <summary>
        /// Gets or sets a value indicating whether a file dialog
        /// displays a warning if the user specifies a file name that
        /// does not exist.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool CheckPathExists { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// InitialDirectory
        ///
        /// <summary>
        /// Gets or sets the initial directory that is displayed by a file
        /// dialog.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string InitialDirectory { get; set; } = string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// Filter
        ///
        /// <summary>
        /// Gets or sets the filter string that determines what types of
        /// files are displayed from a from dialog.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Filter { get; set; } = "All Files (*.*)|*.*";

        /* ----------------------------------------------------------------- */
        ///
        /// FilterIndex
        ///
        /// <summary>
        /// Gets or sets a value to select the initial filter.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int FilterIndex { get; set; }

        #endregion
    }
}
