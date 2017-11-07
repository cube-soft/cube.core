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
using System.Threading.Tasks;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// AsyncLockTest
    /// 
    /// <summary>
    /// AsyncLock のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class AsyncLockTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// LockAsync
        /// 
        /// <summary>
        /// async/await でロックするテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LockAsync()
        {
            _counter = 0;

            Task.WaitAll(
                WithoutAsyncLock(),
                WithoutAsyncLock(),
                WithoutAsyncLock()
            );

            Assert.That(_counter, Is.EqualTo(3), "WithoutAsyncLock");

            _counter = 0;
            _obj = null;

            Task.WaitAll(
                WithAsyncLock(),
                WithAsyncLock(),
                WithAsyncLock());

            Assert.That(_counter, Is.EqualTo(1), "WithAsyncLock");
        }

        #endregion

        #region Helpers

        /* ----------------------------------------------------------------- */
        ///
        /// WithoutAsyncLock
        /// 
        /// <summary>
        /// ロック無で処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private async Task WithoutAsyncLock()
        {
            if (_obj == null)
            {
                await Task.Delay(500);
                _obj = new object();
                _counter++;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WithAsyncLock
        /// 
        /// <summary>
        /// ロック付で処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private async Task WithAsyncLock()
        {
            using (var _ = await _mutex.LockAsync())
            {
                if (_obj == null)
                {
                    await Task.Delay(500);
                    _obj = new object();
                    _counter++;
                }
            }
        }

        #region Fields
        private int _counter;
        private object _obj;
        private readonly AsyncLock _mutex = new AsyncLock();
        #endregion

        #endregion
    }
}
