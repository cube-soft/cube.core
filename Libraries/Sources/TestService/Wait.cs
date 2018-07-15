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
    /// Provides functionality to wait for the result of test operation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Wait
    {
        #region Methods

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
            For(predicate, 10000);

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
            For(predicate, (long)timeout.TotalMilliseconds);

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
            InvokeAsync(predicate, timeout).Result;

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
        public static bool For(CancellationToken token) => For(token, 10000);

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
            For(token, TimeSpan.FromMilliseconds(timeout));

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
            InvokeAsync(token, timeout).Result;

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// InvokeAsync
        ///
        /// <summary>
        /// Waits for the result of the specified predicate to be true
        /// as an asynchronous operation.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static async Task<bool> InvokeAsync(Func<bool> predicate, long timeout)
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
        /// InvokeAsync
        ///
        /// <summary>
        /// Waits until the CancellationToken is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static async Task<bool> InvokeAsync(CancellationToken token, TimeSpan timeout)
        {
            try { await Task.Delay(timeout, token); }
            catch (OperationCanceledException) { return true; }
            return false;
        }

        #endregion
    }
}
