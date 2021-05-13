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

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// Wait
    ///
    /// <summary>
    /// Provides functionality to wait for the result of test operation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Wait
    {
        #region Wait.For

        /* ----------------------------------------------------------------- */
        ///
        /// For
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
        public static bool For(Func<bool> predicate) =>
            ForAsync(predicate).Result;

        /* ----------------------------------------------------------------- */
        ///
        /// For
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
        public static bool For(Func<bool> predicate, TimeSpan timeout) =>
            ForAsync(predicate, timeout).Result;

        /* ----------------------------------------------------------------- */
        ///
        /// For
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
        public static bool For(Func<bool> predicate, long timeout) =>
            ForAsync(predicate, timeout).Result;

        /* ----------------------------------------------------------------- */
        ///
        /// For
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
        public static bool For(CancellationToken token) => ForAsync(token).Result;

        /* ----------------------------------------------------------------- */
        ///
        /// For
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
        public static bool For(CancellationToken token, long timeout) =>
            ForAsync(token, timeout).Result;

        /* ----------------------------------------------------------------- */
        ///
        /// For
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
        public static bool For(CancellationToken token, TimeSpan timeout) =>
            ForAsync(token, timeout).Result;

        #endregion

        #region Wait.ForAsync

        /* ----------------------------------------------------------------- */
        ///
        /// ForAsync
        ///
        /// <summary>
        /// Waits for the result of the specified predicate to be true
        /// as an asynchronous operation.
        /// </summary>
        ///
        /// <param name="predicate">Predicate object.</param>
        ///
        /// <returns>false for timeout.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Task<bool> ForAsync(Func<bool> predicate) =>
            ForAsync(predicate, 10000);

        /* ----------------------------------------------------------------- */
        ///
        /// ForAsync
        ///
        /// <summary>
        /// Waits for the result of the specified predicate to be true
        /// as an asynchronous operation.
        /// </summary>
        ///
        /// <param name="predicate">Predicate object.</param>
        /// <param name="timeout">Timeout value.</param>
        ///
        /// <returns>false for timeout.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Task<bool> ForAsync(Func<bool> predicate, TimeSpan timeout) =>
            ForAsync(predicate, (long)timeout.TotalMilliseconds);

        /* ----------------------------------------------------------------- */
        ///
        /// ForAsync
        ///
        /// <summary>
        /// Waits for the result of the specified predicate to be true
        /// as an asynchronous operation.
        /// </summary>
        ///
        /// <param name="predicate">Predicate object.</param>
        /// <param name="timeout">Timeout value.</param>
        ///
        /// <returns>false for timeout.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static async Task<bool> ForAsync(Func<bool> predicate, long timeout)
        {
            var unit = 100;
            for (var i = 0L; i < (timeout / unit) + 1; ++i)
            {
                if (predicate()) return true;
                await TaskEx.Delay(unit).ConfigureAwait(false);
            }
            return false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ForAsync
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
        public static Task<bool> ForAsync(CancellationToken token) =>
            ForAsync(token, 10000);

        /* ----------------------------------------------------------------- */
        ///
        /// ForAsync
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
        public static Task<bool> ForAsync(CancellationToken token, long timeout) =>
            ForAsync(token, TimeSpan.FromMilliseconds(timeout));

        /* ----------------------------------------------------------------- */
        ///
        /// ForAsync
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
        public static async Task<bool> ForAsync(CancellationToken token, TimeSpan timeout)
        {
            try { await TaskEx.Delay(timeout, token); }
            catch (OperationCanceledException) { return true; }
            return false;
        }

        #endregion
    }
}
