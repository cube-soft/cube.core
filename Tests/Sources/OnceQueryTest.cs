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
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// OnceQueryTest
    ///
    /// <summary>
    /// OnceQuery のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class OnceQueryTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// OnceQuery(T) のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("password")]
        public void Invoke(string obj)
        {
            var src  = new OnceQuery<string>(obj);
            var dest = QueryEventArgs.Create("OnceQuery(T)");
            src.Request(dest);

            Assert.That(dest.Result, Is.EqualTo(obj));
            Assert.That(dest.Cancel, Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke_Twice
        ///
        /// <summary>
        /// 複数回実行した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("twice")]
        public void Invoke_Twice(string obj) => Assert.That(() =>
        {
            var src  = new OnceQuery<string>(obj);
            var dest = QueryEventArgs.Create("TwiceQuery(T)");
            src.Request(dest);
            src.Request(dest);
        }, Throws.TypeOf<TwiceException>());

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// OnceQuery(T, U) のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(10)]
        public void Invoke(int obj)
        {
            var src  = new OnceQuery<string, int>(obj);
            var dest = new QueryEventArgs<string, int>("OnceQuery(T, U)");
            src.Request(dest);

            Assert.That(dest.Result, Is.EqualTo(obj));
            Assert.That(dest.Cancel, Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke_Twice
        ///
        /// <summary>
        /// 複数回実行した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(-1)]
        public void Invoke_Twice(int obj) => Assert.That(() =>
        {
            var src  = new OnceQuery<string, int>(obj);
            var dest = new QueryEventArgs<string, int>("TwiceQuery(T)");
            src.Request(dest);
            src.Request(dest);
        }, Throws.TypeOf<TwiceException>());
    }
}
