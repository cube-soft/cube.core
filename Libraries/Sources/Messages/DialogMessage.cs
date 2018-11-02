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
using System.Reflection;
using System.Windows;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// DialogMessage
    ///
    /// <summary>
    /// Represents the information to show in the DialogBox.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class DialogMessage
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DialogMessage
        ///
        /// <summary>
        /// Initializes a new instance of the DialogMessage class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="content">Main text.</param>
        /// <param name="assembly">Assembly object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public DialogMessage(string content, Assembly assembly) :
            this(content, assembly.GetReader().Title) { }

        /* ----------------------------------------------------------------- */
        ///
        /// DialogMessage
        ///
        /// <summary>
        /// Initializes a new instance of the DialogMessage class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="content">Main text.</param>
        /// <param name="title">Title.</param>
        ///
        /* ----------------------------------------------------------------- */
        public DialogMessage(string content, string title) :
            this(content, title, null) { }

        /* ----------------------------------------------------------------- */
        ///
        /// DialogMessage
        ///
        /// <summary>
        /// Initializes a new instance of the DialogMessage class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="content">Main text.</param>
        /// <param name="assembly">Assembly object.</param>
        /// <param name="callback">Callback function.</param>
        ///
        /* ----------------------------------------------------------------- */
        public DialogMessage(string content, Assembly assembly, DialogCallback callback) :
            this(content, assembly.GetReader().Title, callback) { }

        /* ----------------------------------------------------------------- */
        ///
        /// DialogMessage
        ///
        /// <summary>
        /// Initializes a new instance of the DialogMessage class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="content">Main text.</param>
        /// <param name="title">Title.</param>
        /// <param name="callback">Callback function.</param>
        ///
        /* ----------------------------------------------------------------- */
        public DialogMessage(string content, string title, DialogCallback callback)
        {
            Content  = content;
            Title    = title;
            Callback = callback;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Callback
        ///
        /// <summary>
        /// Gets the callback function when the MessageBox is closed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DialogCallback Callback { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Content
        ///
        /// <summary>
        /// Gets or sets the content that is shown in the MessageBox.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Content { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        ///
        /// <summary>
        /// Gets or sets the title of the MessageBox.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Buttons
        ///
        /// <summary>
        /// Gets or sets the buttons that are shown in the MessageBox.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public MessageBoxButton Buttons { get; set; } = MessageBoxButton.OK;

        /* ----------------------------------------------------------------- */
        ///
        /// Image
        ///
        /// <summary>
        /// Gets or sets the icon that is shown in the MessageBox.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public MessageBoxImage Image { get; set; } = MessageBoxImage.Error;

        /* ----------------------------------------------------------------- */
        ///
        /// Result
        ///
        /// <summary>
        /// Gets or sets the value that represents the kind of clicking
        /// button.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public MessageBoxResult Result { get; set; } = MessageBoxResult.OK;

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// DialogCallback
    ///
    /// <summary>
    /// Represents the method that will handle the user action after
    /// closing the MessageBox.
    /// </summary>
    ///
    /// <param name="e">
    /// An object that contains the result of the MessageBox.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public delegate void DialogCallback(DialogMessage e);
}
