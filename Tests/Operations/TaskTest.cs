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
using System.Threading.Tasks;
using NUnit.Framework;
using Cube.Tasks;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// TaskTest
    /// 
    /// <summary>
    /// Cube.Tasks.Oprations のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class TaskTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Forget
        ///
        /// <summary>
        /// Task の Fire&amp;Forget のテストを実行します。
        /// </summary>
        /// 
        /// <remarks>
        /// 例外発生時にはログに出力されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Forget() => Assert.DoesNotThrow(() =>
        {
            TaskEx.Run(() => throw new InvalidOperationException()).Forget();
        });

        /* ----------------------------------------------------------------- */
        ///
        /// Timeout
        ///
        /// <summary>
        /// TimeoutException が発生するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Timeout() => Assert.That(
            () => TaskEx.Run(() => { while (true) { } })
            .Timeout(TimeSpan.FromMilliseconds(50))
            .Wait(),
            Throws
            .TypeOf<AggregateException>()
            .And
            .InnerException
            .TypeOf<TimeoutException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Timeout_NotThrow
        ///
        /// <summary>
        /// Timeout(TimeSpan) 実行後、TimeoutException が発生せずに処理が
        /// 終了するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(5u, ExpectedResult = 5)]
        public long Timeout_NotThrow(uint n)
            => TaskEx.Run(() => Fibonacci(n))
            .Timeout(TimeSpan.FromSeconds(100))
            .Result;

        #region Helper methods

        /* ----------------------------------------------------------------- */
        ///
        /// Fibonacci
        ///
        /// <summary>
        /// Timeout をテストするためのダミー処理です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private long Fibonacci(uint n)
            => n == 0 ? 0 :
               n == 1 ? 1 :
               Fibonacci(n - 1) + Fibonacci(n - 2);

        #endregion
    }
}
