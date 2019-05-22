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

    #region ProgressMessage<TValue>

    /* --------------------------------------------------------------------- */
    ///
    /// ProgressMessage(TValue)
    ///
    /// <summary>
    /// Represents the message with Ratio property.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ProgressMessage<TValue> : Message<TValue>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Ratio
        ///
        /// <summary>
        /// Gets the current progress ratio.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public double Ratio { get; set; }
    }

    #endregion

    #region QueryMessage<TQuery, TValue>

    /* --------------------------------------------------------------------- */
    ///
    /// QueryMessage(TQuery, TValue)
    ///
    /// <summary>
    /// Represents the message that has Query, Value (result), and Cancel
    /// properties.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class QueryMessage<TQuery, TValue> : CancelMessage<TValue>
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Query
        ///
        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TQuery Query { get; set; }

        #endregion
    }

    #endregion
}
