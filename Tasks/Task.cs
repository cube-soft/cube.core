/* ------------------------------------------------------------------------- */
///
/// Task.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;

namespace Cube.Tasks
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Tasks.Task
    /// 
    /// <summary>
    /// System.Threading.Tasks.Task クラスに関する静的メソッド群を定義した
    /// クラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// Task Parallel Library for .NET 3.5 に定義されていないメソッドを
    /// 代替するためのクラスです。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class Task
    {
        /* --------------------------------------------------------------------- */
        ///
        /// Delay
        /// 
        /// <summary>
        /// 遅延後に完了するタスクを作成します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public static System.Threading.Tasks.Task Delay(int msec)
        {
            return System.Threading.Tasks.Task.Factory.StartNew(() => {
                var mres = new System.Threading.ManualResetEventSlim();
                mres.Wait(msec);
            });
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Delay
        /// 
        /// <summary>
        /// 遅延後に完了するタスクを作成します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public static System.Threading.Tasks.Task Delay(int msec, System.Threading.CancellationToken token)
        {
            return System.Threading.Tasks.Task.Factory.StartNew(() => {
                var mres = new System.Threading.ManualResetEventSlim();
                mres.Wait(msec, token);
            });
        }
    }
}
