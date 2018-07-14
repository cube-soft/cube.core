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
using System.Threading;
using System.Threading.Tasks;

namespace Cube.FileSystem.TestService
{
    /* --------------------------------------------------------------------- */
    ///
    /// Wait
    ///
    /// <summary>
    /// Provides functionality to wait the result of test operation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Wait
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Do
        ///
        /// <summary>
        /// Waits for the result of the specified predicate to be true.
        /// </summary>
        ///
        /// <param name="predicate">Predicate object.</param>
        ///
        /// <returns>false for timeout.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool Do(Func<bool> predicate) =>
            Do(predicate, 10000);

        /* ----------------------------------------------------------------- */
        ///
        /// Do
        ///
        /// <summary>
        /// Waits for the result of the specified predicate to be true.
        /// </summary>
        ///
        /// <param name="predicate">Predicate object.</param>
        /// <param name="timeout">Timeout value.</param>
        ///
        /// <returns>false for timeout.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool Do(Func<bool> predicate, TimeSpan timeout) =>
            Do(predicate, (long)timeout.TotalMilliseconds);

        /* ----------------------------------------------------------------- */
        ///
        /// Do
        ///
        /// <summary>
        /// Waits for the result of the specified predicate to be true.
        /// </summary>
        ///
        /// <param name="predicate">Predicate object.</param>
        /// <param name="timeout">Timeout value.</param>
        ///
        /// <returns>false for timeout.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool Do(Func<bool> predicate, long timeout) =>
            DoAsync(predicate, timeout).Result;

        /* ----------------------------------------------------------------- */
        ///
        /// Do
        ///
        /// <summary>
        /// Waits until the CancellationToken is fired.
        /// </summary>
        ///
        /// <param name="token">Cancellation token.</param>
        ///
        /// <returns>false for timeout.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool Do(CancellationToken token) =>
            Do(token, 10000);

        /* ----------------------------------------------------------------- */
        ///
        /// Do
        ///
        /// <summary>
        /// Waits until the CancellationToken is fired.
        /// </summary>
        ///
        /// <param name="token">Cancellation token.</param>
        /// <param name="timeout">Timeout value.</param>
        ///
        /// <returns>false for timeout.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool Do(CancellationToken token, long timeout) =>
            Do(token, TimeSpan.FromMilliseconds(timeout));

        /* ----------------------------------------------------------------- */
        ///
        /// Do
        ///
        /// <summary>
        /// Waits until the CancellationToken is fired.
        /// </summary>
        ///
        /// <param name="token">Cancellation token.</param>
        /// <param name="timeout">Timeout value.</param>
        ///
        /// <returns>false for timeout.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool Do(CancellationToken token, TimeSpan timeout) =>
            DoAsync(token, timeout).Result;

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// DoAsync
        ///
        /// <summary>
        /// Waits for the result of the specified predicate to be true
        /// as an asynchronous operation.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static async Task<bool> DoAsync(Func<bool> predicate, long timeout)
        {
            var unit = 100;
            for (var i = 0; i < (timeout / unit) + 1; ++i)
            {
                if (predicate()) return true;
                await Task.Delay(unit).ConfigureAwait(false);
            }
            return false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DoAsync
        ///
        /// <summary>
        /// Waits until the CancellationToken is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static async Task<bool> DoAsync(CancellationToken token, TimeSpan timeout)
        {
            try { await Task.Delay(timeout, token); }
            catch (OperationCanceledException) { return true; }
            return false;
        }

        #endregion
    }
}
