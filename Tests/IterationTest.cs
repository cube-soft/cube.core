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
using Cube.Iteration;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// IterationTest
    ///
    /// <summary>
    /// Cube.Iteration.Operations のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class IterationTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Times
        ///
        /// <summary>
        /// 指定回数だけ繰り返す拡張メソッドのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Times()
        {
            var actual = 0;
            10.Times(() => actual++);
            Assert.That(actual, Is.EqualTo(10));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Times_WithIndex
        ///
        /// <summary>
        /// 指定回数だけ繰り返す拡張メソッドのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Times_WithIndex()
        {
            var actual = 0;
            10.Times(i => actual += i);
            Assert.That(actual, Is.EqualTo(45));
        }
    }
}
