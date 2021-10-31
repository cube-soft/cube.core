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
using Cube.FileSystem;
using Cube.Mixin.String;

namespace Cube
{
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
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SaveFileMessage
        ///
        /// <summary>
        /// Initializes a new instance of the SaveFileMessage class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SaveFileMessage() : this(string.Empty) { }

        /* ----------------------------------------------------------------- */
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
        /* ----------------------------------------------------------------- */
        public SaveFileMessage(string src) : this(src.HasValue() ? Io.Get(src) : default) { }

        /* ----------------------------------------------------------------- */
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
        /* ----------------------------------------------------------------- */
        public SaveFileMessage(Entity src)
        {
            if (src != null)
            {
                Value = src.Name;
                InitialDirectory = src.DirectoryName;
            }
            else Value = string.Empty;
        }

        #endregion

        #region Properties

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
}
