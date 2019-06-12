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
using System.Linq;

namespace Cube.Mixin.Iteration
{
    /* --------------------------------------------------------------------- */
    ///
    /// Extension
    ///
    /// <summary>
    /// Provides extended methods about iteration.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Extension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Make
        ///
        /// <summary>
        /// Makes the specified number of sequence with the specified
        /// function.
        /// </summary>
        ///
        /// <param name="n">Number of sequence.</param>
        /// <param name="func">Function to create an element.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<T> Make<T>(this int n, Func<int, T> func) =>
            Enumerable.Range(0, n).Select(i => func(i));

        /* ----------------------------------------------------------------- */
        ///
        /// Times
        ///
        /// <summary>
        /// Executes the specified action in the specified number of times.
        /// </summary>
        ///
        /// <param name="n">Number of times.</param>
        /// <param name="action">User action.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Times(this int n, Action<int> action)
        {
            for (var i = 0; i < n; ++i) action(i);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Try
        ///
        /// <summary>
        /// Tries the specified action up to the specified number of times
        /// until the action succeeds.
        /// </summary>
        ///
        /// <param name="n">Number of trials.</param>
        /// <param name="action">User action.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static bool Try(this int n, Action<int> action) => n.Try(action, default);

        /* ----------------------------------------------------------------- */
        ///
        /// Try
        ///
        /// <summary>
        /// Tries the specified action up to the specified number of times
        /// until the action succeeds.
        /// </summary>
        ///
        /// <param name="n">Number of trials.</param>
        /// <param name="action">User action.</param>
        /// <param name="error">
        /// Action to be invoked when an exception occurs.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public static bool Try(this int n, Action<int> action, Action<int, Exception> error)
        {
            for (var i = 0; i < n; ++i)
            {
                try { action(i); return true; }
                catch (Exception err) { error?.Invoke(i, err); }
            }
            return false;
        }

        #endregion
    }
}
