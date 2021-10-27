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
    /* --------------------------------------------------------------------- */
    ///
    /// NoticeCallback
    ///
    /// <summary>
    /// Represents the method called when a part of the notice window
    /// is clicked.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void NoticeCallback(NoticeMessage src, NoticeResult result);

    /* --------------------------------------------------------------------- */
    ///
    /// NoticeMessage
    ///
    /// <summary>
    /// Represents the information of a notice.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NoticeMessage : Message<object>
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        ///
        /// <summary>
        /// Gets or sets the title of the notice.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// DisplayTime
        ///
        /// <summary>
        /// Gets or sets the time to display the notice.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSpan DisplayTime { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// InitialDelay
        ///
        /// <summary>
        /// Gets or sets the time to delay the display of the notice.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSpan InitialDelay { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Priority
        ///
        /// <summary>
        /// Gets or sets the priority of the notice.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NoticePriority Priority { get; set; } = NoticePriority.Normal;

        /* ----------------------------------------------------------------- */
        ///
        /// Location
        ///
        /// <summary>
        /// Gets or sets the location to show the notice window.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NoticeLocation Location { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Style
        ///
        /// <summary>
        /// Gets or sets the notice style.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NoticeStyle Style { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Callback
        ///
        /// <summary>
        /// Gets or sets the callback action when the user clicks on the
        /// notice window.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NoticeCallback Callback { get; set; }

        #endregion
    }
}
