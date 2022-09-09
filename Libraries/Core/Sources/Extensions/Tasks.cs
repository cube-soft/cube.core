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
namespace Cube.Tasks.Extensions;

using System;
using System.Threading.Tasks;

/* ------------------------------------------------------------------------- */
///
/// Methods
///
/// <summary>
/// Provides extended methods of the Task and related classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Methods
{
    /* --------------------------------------------------------------------- */
    ///
    /// Forget
    ///
    /// <summary>
    /// Executes the specified task in the Fire&amp;Forget pattern.
    /// </summary>
    ///
    /// <param name="src">Source object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Forget(this Task src) =>
        src.ContinueWith(e => Logger.Warn(e.Exception), TaskContinuationOptions.OnlyOnFaulted);

    /* --------------------------------------------------------------------- */
    ///
    /// Timeout
    ///
    /// <summary>
    /// Sets a timeout of executing the specified task.
    /// </summary>
    ///
    /// <param name="src">Source object.</param>
    /// <param name="value">Timeout value.</param>
    ///
    /// <returns>Task object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static async Task Timeout(this Task src, TimeSpan value)
    {
        var timeout = Task.Delay(value);
        var dest = await Task.WhenAny(src, timeout).ConfigureAwait(false);
        if (dest == timeout) throw new TimeoutException();
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Timeout
    ///
    /// <summary>
    /// Sets a timeout of executing the specified task.
    /// </summary>
    ///
    /// <param name="src">Source object.</param>
    /// <param name="value">Timeout value.</param>
    ///
    /// <returns>Task(T) object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static async Task<T> Timeout<T>(this Task<T> src, TimeSpan value)
    {
        await ((Task)src).Timeout(value).ConfigureAwait(false);
        return await src.ConfigureAwait(false);
    }
}
