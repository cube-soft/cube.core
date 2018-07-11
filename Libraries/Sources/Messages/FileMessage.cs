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

namespace Cube.Xui
{
    #region FileSystemMessage

    /* --------------------------------------------------------------------- */
    ///
    /// FileSystemMessage
    ///
    /// <summary>
    /// Stores some information to show either the OpenFileDialog or
    /// SaveFileDialog or FolderBrowserDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class FileSystemMessage
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// FileName
        ///
        /// <summary>
        /// Gets or sets a string containing the full path of the file
        /// selected in a dialog.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string FileName { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        ///
        /// <summary>
        /// Gets or sets the text that appears in the title bar of a dialog.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Result
        ///
        /// <summary>
        /// Gets or sets the result of the user action.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Result { get; set; } = false;

        #endregion
    }

    #endregion

    #region FileMessage

    /* --------------------------------------------------------------------- */
    ///
    /// FileMessage
    ///
    /// <summary>
    /// Stores some information to show either the OpenFileDialog or
    /// SaveFileDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class FileMessage : FileSystemMessage
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
        public string InitialDirectory { get; set; }

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

        #endregion
    }

    #endregion

    #region OpenFileMessage

    /* --------------------------------------------------------------------- */
    ///
    /// OpenFileMessage
    ///
    /// <summary>
    /// Stores some information to show an OpenFileDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OpenFileMessage : FileMessage
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// OpenFileDialogMessage
        ///
        /// <summary>
        /// Initializes a new instance with the specified callback.
        /// </summary>
        ///
        /// <param name="callback">Callback object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public OpenFileMessage(OpenFileCallback callback)
        {
            Callback        = callback;
            CheckPathExists = true;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Callback
        ///
        /// <summary>
        /// Gets or sets the delegation of the user action after
        /// closing the OpenFileDialog.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public OpenFileCallback Callback { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Multiselect
        ///
        /// <summary>
        /// Gets or sets an option indicating whether OpenFileDialog
        /// allows users to select multiple files.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Multiselect { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// FileNames
        ///
        /// <summary>
        /// Gets an array that contains one file name for each selected
        /// file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> FileNames { get; set; }

        #endregion
    }

    #endregion

    #region SaveFileMessage

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileMessage
    ///
    /// <summary>
    /// Stores some information to show a SaveFileDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SaveFileMessage : FileMessage
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SaveFileDialogMessage
        ///
        /// <summary>
        /// Initializes a new instance with the specified callback.
        /// </summary>
        ///
        /// <param name="callback">Callback object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public SaveFileMessage(SaveFileCallback callback)
        {
            Callback        = callback;
            CheckPathExists = false;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Callback
        ///
        /// <summary>
        /// Gets or sets the delegation of the user action after
        /// closing the SaveileDialog.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SaveFileCallback Callback { get; }

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

        #endregion
    }

    #endregion

    #region OpenDirectoryMessage

    /* --------------------------------------------------------------------- */
    ///
    /// OpenDirectoryMessage
    ///
    /// <summary>
    /// Stores some information to show a FolderBrowserDialog.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OpenDirectoryMessage : FileSystemMessage
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DirectoryDialogMessage
        ///
        /// <summary>
        /// Initializes a new instance with the specified callback.
        /// </summary>
        ///
        /// <param name="callback">Callback object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public OpenDirectoryMessage(OpenDirectoryCallback callback)
        {
            Callback = callback;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Callback
        ///
        /// <summary>
        /// Gets or sets the delegation of the user action after
        /// closing the FolderBrowserDialog.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public OpenDirectoryCallback Callback { get; }

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

        #endregion
    }

    #endregion

    #region Callbacks

    /* --------------------------------------------------------------------- */
    ///
    /// OpenFileCallback
    ///
    /// <summary>
    /// Represents the method that will handle the user action after
    /// closing the OpenFileDialog.
    /// </summary>
    ///
    /// <param name="e">
    /// An object that contains the result of the OpenFileDialog.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public delegate void OpenFileCallback(OpenFileMessage e);

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileCallback
    ///
    /// <summary>
    /// Represents the method that will handle the user action after
    /// closing the SaveFileDialog.
    /// </summary>
    ///
    /// <param name="e">
    /// An object that contains the result of the SaveFileDialog.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public delegate void SaveFileCallback(SaveFileMessage e);

    /* --------------------------------------------------------------------- */
    ///
    /// OpenDirectoryCallback
    ///
    /// <summary>
    /// Represents the method that will handle the user action after
    /// closing the FolderBrowserDialog.
    /// </summary>
    ///
    /// <param name="e">
    /// An object that contains the result of the FolderBrowserDialog.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public delegate void OpenDirectoryCallback(OpenDirectoryMessage e);

    #endregion
}
