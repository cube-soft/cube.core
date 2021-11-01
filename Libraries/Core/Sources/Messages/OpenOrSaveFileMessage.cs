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
using System;
using System.Collections.Generic;
using System.Linq;
using Cube.Collections;
using Cube.FileSystem;
using Cube.Mixin.Collections;
using Cube.Mixin.String;

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
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// OpenOrSaveFileMessage
        ///
        /// <summary>
        /// Initializes a new instance of the OpenOrSaveFileMessage class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="src">Entity of the initial path.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected OpenOrSaveFileMessage(Entity src)
        {
            if (src != null) InitialDirectory = src.IsDirectory ? src.FullName : src.DirectoryName;
        }

        #endregion

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
        public IEnumerable<FileDialogFilter> Filters { get; set; } =
            new FileDialogFilter("All Files", ".*").ToEnumerable();

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

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Update
        ///
        /// <summary>
        /// Updates some properties with the current settings.
        /// </summary>
        ///
        /// <remarks>
        /// The method currently may change the value of the FilterIndex
        /// property.
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public void Update() => FilterIndex = GetFilterIndex();

        /* ----------------------------------------------------------------- */
        ///
        /// GetValue
        ///
        /// <summary>
        /// Gets the value.
        /// </summary>
        ///
        /// <returns>String value.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected abstract string GetValue();

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// GetFilterIndex
        ///
        /// <summary>
        /// Gets the filter index with the current settings.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private int GetFilterIndex()
        {
            var src = GetValue();
            if (!src.HasValue()) return 0;

            var opt = StringComparison.InvariantCultureIgnoreCase;
            return Filters.Select((e, i) => KeyValuePair.Create(i + 1, e.Targets))
                          .FirstOrDefault(e => e.Value.Any(e2 => src.EndsWith(e2, opt)))
                          .Key;
        }

        #endregion
    }
}
