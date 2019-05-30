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
using System.Collections.Generic;
using System.Linq;

namespace Cube
{
    #region OpenDirectoryMessage

    /* --------------------------------------------------------------------- */
    ///
    /// OpenDirectoryMessage
    ///
    /// <summary>
    /// Represents information to show the FolderBrowserDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OpenDirectoryMessage : CancelMessage<string>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// OpenDirectoryMessage
        ///
        /// <summary>
        /// Initializes a new isntance of the OpenDirectoryMessage class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public OpenDirectoryMessage() { Value = string.Empty; }

        /* ----------------------------------------------------------------- */
        ///
        /// NewButton
        ///
        /// <summary>
        /// Gets or sets a value indicating whether the New Folder button
        /// appears in the FolderBrowserDialog.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool NewButton { get; set; } = true;
    }

    #endregion

    #region OpenOrSaveFileMessage<TAction>

    /* --------------------------------------------------------------------- */
    ///
    /// OpenOrSaveFileMessage(TAction)
    ///
    /// <summary>
    /// Represents shared information to show either the OpenFileDialog
    /// or SaveFileDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class OpenOrSaveFileMessage<TValue> : CancelMessage<TValue>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// OpenOrSaveFileMessage
        ///
        /// <summary>
        /// Initializes a new instance of the OpenOrSaveFileMessage class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected OpenOrSaveFileMessage() { }

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
    }

    #endregion

    #region OpenFileMessage

    /* --------------------------------------------------------------------- */
    ///
    /// OpenFileMessage
    ///
    /// <summary>
    /// Represents information to show the OpenFileDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OpenFileMessage : OpenOrSaveFileMessage<IEnumerable<string>>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// OpenFileMessage
        ///
        /// <summary>
        /// Initializes a new instance of the OpenFileMessage class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public OpenFileMessage()
        {
            Value = Enumerable.Empty<string>();
            CheckPathExists = true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Multiselect
        ///
        /// <summary>
        /// Gets or sets an option indicating whether the OpenFileDialog
        /// allows users to select multiple files.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Multiselect { get; set; }
    }

    #endregion

    #region SaveFileMessage

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileMessage
    ///
    /// <summary>
    /// Represents information to show the SaveFileDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SaveFileMessage : OpenOrSaveFileMessage<string>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// SaveFileMessage
        ///
        /// <summary>
        /// Initializes a new instance of the SaveFileMessage class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SaveFileMessage() { Value = string.Empty; }

        /* ----------------------------------------------------------------- */
        ///
        /// OverwritePrompt
        ///
        /// <summary>
        /// Gets or sets a value indicating whether SaveFileDialog displays
        /// a warning if the user specifies the name of a file that already
        /// exists.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool OverwritePrompt { get; set; } = true;
    }

    #endregion
}
