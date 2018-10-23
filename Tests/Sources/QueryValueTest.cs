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
    /// QueryValueTest
    ///
    /// <summary>
    /// QueryValue のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class QueryValueTest
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
            var src  = new QueryValue<string>(obj);
            var dest = QueryEventArgs.Create("OnceQuery(T)");
            src.Request(dest);

            Assert.That(src.Value,   Is.EqualTo(obj));
            Assert.That(dest.Result, Is.EqualTo(obj));
            Assert.That(dest.Cancel, Is.False);
        }

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
            var src  = new QueryValue<string, int>(obj);
            var dest = new QueryEventArgs<string, int>("OnceQuery(T, U)");
            src.Request(dest);

            Assert.That(src.Value,   Is.EqualTo(obj));
            Assert.That(dest.Result, Is.EqualTo(obj));
            Assert.That(dest.Cancel, Is.False);
        }
    }
}
