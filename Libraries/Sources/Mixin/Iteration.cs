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
using Cube.Mixin.Logger;
using System;

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
        public static void Times(this int n, Action action) => Times(n, z => action());

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
        /// <param name="src">Source object.</param>
        /// <param name="n">Number of trials.</param>
        /// <param name="action">User action.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Try<T>(this T src, int n, Action action) => src.Try(n, z => action());

        /* ----------------------------------------------------------------- */
        ///
        /// Try
        ///
        /// <summary>
        /// Tries the specified action up to the specified number of times
        /// until the action succeeds.
        /// </summary>
        ///
        /// <param name="src">Source object.</param>
        /// <param name="n">Number of trials.</param>
        /// <param name="action">User action.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Try<T>(this T src, int n, Action<int> action)
        {
            for (var i = 0; i < n; ++i)
            {
                try { action(i); return; }
                catch (Exception err)
                {
                    src.LogWarn($"{err.Message} ({i + 1}/{n})");
                    if (i + 1 >= n) throw;
                }
            }
        }

        #endregion
    }
}