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
    #region Message

    /* --------------------------------------------------------------------- */
    ///
    /// Message
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

    #region CallbackMessage<T>

    /* --------------------------------------------------------------------- */
    ///
    /// CallbackMessage
    ///
    /// <summary>
    /// Represents the message that has a callback function.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class CallbackMessage<TValue, TAction> : Message<TValue> where TAction : Delegate
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Callback
        ///
        /// <summary>
        /// Gets or sets the callback function that is invoked by a view.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TAction Callback { get; set; }
    }

    #endregion

    #region CloseMessage

    /* --------------------------------------------------------------------- */
    ///
    /// CloseMessage
    ///
    /// <summary>
    /// Represents the message that is sent when closing a window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class CloseMessage { }

    #endregion

    #region UpdateSourcesMessage

    /* --------------------------------------------------------------------- */
    ///
    /// UpdateSourcesMessage
    ///
    /// <summary>
    /// Represents the message that is sent when updating source values.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class UpdateSourcesMessage { }

    #endregion
}
