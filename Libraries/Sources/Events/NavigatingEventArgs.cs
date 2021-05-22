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
using System.ComponentModel;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// NavigatingEventArgs
    ///
    /// <summary>
    /// Represents the event arguments when a screen transition occurs in a
    /// Web browser.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NavigatingEventArgs : CancelEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// NavigatingEventArgs
        ///
        /// <summary>
        /// Initializes a new instance of the NavigatingEventArgs class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="url">URL.</param>
        /// <param name="frame">Target frame.</param>
        ///
        /* ----------------------------------------------------------------- */
        public NavigatingEventArgs(string url, string frame) : base(false)
        {
            Url   = url;
            Frame = frame;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Url
        ///
        /// <summary>
        /// Get the URL of the transition destination.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Url { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Frame
        ///
        /// <summary>
        /// Get the target frame of the transition destination.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Frame { get; private set; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// NavigateErrorEventArgs
    ///
    /// <summary>
    /// Represents the event arguments when an error occurs while navigating
    /// in a web browser.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NavigatingErrorEventArgs : NavigatingEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// NavigatingErrorEventArgs
        ///
        /// <summary>
        /// Initializes a new instance of the NavigatingErrorEventArgs class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="url">URL.</param>
        /// <param name="frame">Target frame.</param>
        /// <param name="code">HTTP status code.</param>
        ///
        /* ----------------------------------------------------------------- */
        public NavigatingErrorEventArgs(string url, string frame, int code) : base(url, frame)
        {
            StatusCode = code;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// StatusCode
        ///
        /// <summary>
        /// Gets the HTTP status code.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int StatusCode { get; }

        #endregion
    }
}
