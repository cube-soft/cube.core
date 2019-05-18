﻿/* ------------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    ///
    /// Message
    ///
    /// <summary>
    /// Represents the common message.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Message<T>
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
        public string Title { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Text
        ///
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Text { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public T Value { get; set; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ExceptionMessage
    ///
    /// <summary>
    /// Represents the message that has an exception.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ExceptionMessage : Message<Exception> { }
}