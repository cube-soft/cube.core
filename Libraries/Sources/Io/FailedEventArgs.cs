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
using System.ComponentModel;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// FailedEventArgs
    ///
    /// <summary>
    /// Represents the arguments of the Failed event.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class FailedEventArgs : CancelEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// FailedEventArgs
        ///
        /// <summary>
        /// Initializes a new instance of the FailedEventArgs with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="name">Name of failed method.</param>
        /// <param name="paths">Path collection.</param>
        /// <param name="error">Exception object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public FailedEventArgs(string name, IEnumerable<string> paths, Exception error) :
            this(name, paths, error, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// FailedEventArgs
        ///
        /// <summary>
        /// Initializes a new instance of the FailedEventArgs with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="name">Name of failed method.</param>
        /// <param name="paths">Path collection.</param>
        /// <param name="error">Exception object.</param>
        /// <param name="cancel">Cancel or not.</param>
        ///
        /* ----------------------------------------------------------------- */
        public FailedEventArgs(string name, IEnumerable<string> paths,
            Exception error, bool cancel) : base(cancel)
        {
            Name      = name;
            Paths     = paths;
            Exception = error;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// Gets the name of failed method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Paths
        ///
        /// <summary>
        /// Gets the path collection specified for the failed method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> Paths { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Exception
        ///
        /// <summary>
        /// Gets the Exception object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Exception Exception { get; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// FailedEventHandler
    ///
    /// <summary>
    /// Represents the Failed event handler.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void FailedEventHandler(object sender, FailedEventArgs e);
}
