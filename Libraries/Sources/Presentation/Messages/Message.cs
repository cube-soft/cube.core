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
    #region Message<TValue>

    /* --------------------------------------------------------------------- */
    ///
    /// Message(TValue)
    ///
    /// <summary>
    /// Represents the common message.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Message<TValue>
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        ///
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title { get; set; } = string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// Gets or sets the user defined value.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TValue Value { get; set; }

        #endregion
    }

    #endregion

    #region CancelMessage<TValue>

    /* --------------------------------------------------------------------- */
    ///
    /// CancelMessage
    ///
    /// <summary>
    /// Represents the message with Cancel property.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class CancelMessage<TValue> : Message<TValue>
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Cancel
        ///
        /// <summary>
        /// Gets or sets a value indicating whether to cancel the operation.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Cancel { get; set; }

        #endregion
    }

    #endregion
}
