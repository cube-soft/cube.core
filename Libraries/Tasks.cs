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
using Cube.Log;
using System;
using System.Threading.Tasks;

namespace Cube.Tasks
{
    /* --------------------------------------------------------------------- */
    ///
    /// TaskExtension
    ///
    /// <summary>
    /// Task の拡張メソッド用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class TaskExtension
    {
        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// Forget
        ///
        /// <summary>
        /// 実行されたタスクを待たずに終了します。
        /// 例外発生時にはログに出力します。
        /// </summary>
        ///
        /// <param name="src">Task オブジェクト</param>
        ///
        /* --------------------------------------------------------------------- */
        public static void Forget(this Task src) => src.ContinueWith(e =>
        {
            e.LogWarn(e.Exception.ToString());
        }, TaskContinuationOptions.OnlyOnFaulted);

        /* --------------------------------------------------------------------- */
        ///
        /// Timeout
        ///
        /// <summary>
        /// タスクにタイムアウトを設定します。
        /// </summary>
        ///
        /// <param name="src">Task オブジェクト</param>
        /// <param name="value">タイムアウト時間</param>
        ///
        /// <returns>Task オブジェクト</returns>
        ///
        /* --------------------------------------------------------------------- */
        public static async Task Timeout(this Task src, TimeSpan value)
        {
            var cmp = Task.Delay(value);
            if (await Task.WhenAny(src, cmp).ConfigureAwait(false) == cmp)
            {
                throw new TimeoutException();
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Timeout
        ///
        /// <summary>
        /// タスクにタイムアウトを設定します。
        /// </summary>
        ///
        /// <param name="src">Task(T) オブジェクト</param>
        /// <param name="value">タイムアウト時間</param>
        ///
        /// <returns>Task(T) オブジェクト</returns>
        ///
        /* --------------------------------------------------------------------- */
        public static async Task<T> Timeout<T>(this Task<T> src, TimeSpan value)
        {
            await ((Task)src).Timeout(value).ConfigureAwait(false);
            return await src.ConfigureAwait(false);
        }

        #endregion
    }
}
