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
using System.Diagnostics;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// UriProcess
    ///
    /// <summary>
    /// Provides functionality to start a new process with the provided
    /// Uri object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class UriProcess
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Start
        ///
        /// <summary>
        /// Starts a new process with the specified Uri object.
        /// </summary>
        ///
        /// <param name="src">URL.</param>
        ///
        /// <returns>Process object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Process Start(Uri src) => Process.Start(src.ToString());

        #endregion
    }
}
