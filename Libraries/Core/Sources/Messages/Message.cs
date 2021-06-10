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
    public class Message<TValue> : EventArgs
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Text
        ///
        /// <summary>
        /// Gets or sets a text for the message.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Text { get; set; } = string.Empty;

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
    }

    #endregion

    #region QueryMessage<TSource, TValue>

    /* --------------------------------------------------------------------- */
    ///
    /// QueryMessage(TSource, TValue)
    ///
    /// <summary>
    /// Represents the message that has Query, Value (result), and Cancel
    /// properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class QueryMessage<TSource, TValue> : CancelMessage<TValue>
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Source
        ///
        /// <summary>
        /// Gets or sets the source information at the time of query.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TSource Source { get; set; }

        #endregion
    }

    #endregion

    #region CloseMessage

    /* --------------------------------------------------------------------- */
    ///
    /// CloseMessage
    ///
    /// <summary>
    /// Represents the message to close the displayed window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class CloseMessage { }

    #endregion

    #region ActivateMessage

    /* --------------------------------------------------------------------- */
    ///
    /// ActivateMessage
    ///
    /// <summary>
    /// Represents the message to activate the target window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ActivateMessage { }

    #endregion

    #region ApplyMessage

    /* --------------------------------------------------------------------- */
    ///
    /// ApplyMessage
    ///
    /// <summary>
    /// Represents the message that is sent when setting the current
    /// values to the associated model objects.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ApplyMessage { }

    #endregion
}
