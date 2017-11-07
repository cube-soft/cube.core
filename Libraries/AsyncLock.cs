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

namespace Cube
{
    /* ----------------------------------------------------------------- */
    ///
    /// AsyncLock
    /// 
    /// <summary>
    /// async/await でのロック用クラスです。
    /// </summary>
    ///
    /* ----------------------------------------------------------------- */
    public sealed class AsyncLock
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// AsyncLock
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public AsyncLock()
        {
            _disposable = Task.FromResult((IDisposable)(new ReleaseSemaphore(this)));
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// LockAsync
        /// 
        /// <summary>
        /// 非同期でロックします。
        /// </summary>
        /// 
        /// <returns>ロック解除用オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public Task<IDisposable> LockAsync()
        {
            var wait = _semaphore.WaitAsync();
            return wait.IsCompleted ? _disposable : wait.ContinueWith(
                (_, state) => (IDisposable)state,
                _disposable.Result,
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default
            );
        }

        #endregion

        #region Implementations

        private sealed class ReleaseSemaphore : IDisposable
        {
            internal ReleaseSemaphore(AsyncLock obj) { _obj = obj; }
            public void Dispose() => _obj._semaphore.Release();
            private readonly AsyncLock _obj;
        }

        #region Fields
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly Task<IDisposable> _disposable;
        #endregion

        #endregion
    }
}
