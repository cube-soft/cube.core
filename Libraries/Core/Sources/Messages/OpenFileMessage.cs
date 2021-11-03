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
using Cube.FileSystem;
using Cube.Mixin.Collections.Generic;
using Cube.Mixin.String;

namespace Cube
{
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
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// OpenFileMessage
        ///
        /// <summary>
        /// Initializes a new instance of the OpenFileMessage class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public OpenFileMessage() : this(string.Empty) { }

        /* ----------------------------------------------------------------- */
        ///
        /// OpenFileMessage
        ///
        /// <summary>
        /// Initializes a new instance of the OpenFileMessage class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="src">Initial path.</param>
        ///
        /* ----------------------------------------------------------------- */
        public OpenFileMessage(string src) : this(src.HasValue() ? Io.Get(src) : default) { }

        /* ----------------------------------------------------------------- */
        ///
        /// OpenFileMessage
        ///
        /// <summary>
        /// Initializes a new instance of the OpenFileMessage class with
        /// the specified arguments.
        /// </summary>
        ///
        /// <param name="src">Entity for the initial path.</param>
        ///
        /* ----------------------------------------------------------------- */
        public OpenFileMessage(Entity src) : base(src)
        {
            CheckPathExists = true;
            var empty = src?.IsDirectory ?? true;
            Value = empty ? Enumerable.Empty<string>() : src.Name.ToEnumerable();
        }

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetValue
        ///
        /// <summary>
        /// Gets the first element of the Value property.
        /// </summary>
        ///
        /// <returns>String value.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected override string GetValue() => Value.FirstOrDefault();

        #endregion
    }
}
