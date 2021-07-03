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
using Cube.Collections;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// CacheCollectionTest
    ///
    /// <summary>
    /// Tests for the CacheCollection class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class CacheCollectionTest
    {
        #region Tests

        /* --------------------------------------------------------------------- */
        ///
        /// GetOrCreate
        ///
        /// <summary>
        /// Tests the GetOrCreate method of the CacheCollection class.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Test]
        public void GetOrCreate()
        {
            var n   = 0;
            var key = 10;

            using var src = new CacheCollection<int, int>(i => Sigma(i));
            src.Created += (s, e) => ++n;
            _ = Parallel.For(0, 10, e => src.GetOrCreate(key));

            Assert.That(Wait.For(() => n > 0), "Timeout");
            Assert.That(src.Count, Is.EqualTo(1), nameof(src.Count));
            Assert.That(src.TryGetValue(key, out _), Is.True, nameof(src.TryGetValue));
            Assert.That(src.TryGetValue(999, out _), Is.False, nameof(src.TryGetValue));
            Assert.That(src.Contains(key), Is.True, nameof(src.Contains));
            Assert.That(src.GetOrCreate(key), Is.EqualTo(55), nameof(src.GetOrCreate));
            Assert.That(src.Remove(key), Is.True, nameof(src.Remove));
            src.Clear();
            Assert.That(src.Count, Is.EqualTo(0));
        }

        /* --------------------------------------------------------------------- */
        ///
        /// GetOrCreate_Failed
        ///
        /// <summary>
        /// Tests to confirm that the GetOrCreate method is failed.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Test]
        public void GetOrCreate_Failed()
        {
            var n = 0;
            using var src = new CacheCollection<int, int>(e => throw new ArgumentException($"{e}"));
            src.Failed += (s, e) => ++n;
            _ = Parallel.For(0, 10, i => src.GetOrCreate(i));

            Assert.That(Wait.For(() => n == 10), "Timeout");
            Assert.That(src.Count, Is.EqualTo(0));
        }

        /* --------------------------------------------------------------------- */
        ///
        /// RemoveAndClear_Disposer
        ///
        /// <summary>
        /// Tests the Remove and Clear methods of the CacheCollection class.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [TestCase(true,  ExpectedResult = 10)]
        [TestCase(false, ExpectedResult =  0)]
        public int RemoveAndClear(bool disposer)
        {
            var cnt = 0;
            using var src = disposer ?
                            new CacheCollection<int, int>(n => Sigma(n), (k, v) => ++cnt) :
                            new CacheCollection<int, int>(n => Sigma(n));

            _ = Parallel.For(0, 10, i => src.GetOrCreate(i));
            Assert.That(Wait.For(() => src.Count == 10), "Timeout");

            foreach (var kv in src) Assert.That(kv.Value, Is.EqualTo(Sigma(kv.Key)));

            Assert.That(src.Remove(0), Is.True);
            Assert.That(src.Remove(20), Is.False);
            Assert.That(src.Count, Is.EqualTo(9));

            src.Clear();
            Assert.That(src.Count, Is.EqualTo(0));

            return cnt;
        }

        #endregion

        #region Others

        /* --------------------------------------------------------------------- */
        ///
        /// Sigma
        ///
        /// <summary>
        /// Creator function for the tests.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private int Sigma(int n)
        {
            var dest = 0;
            for (var i = 1; i <= n; ++i) dest += i;
            return dest;
        }

        #endregion
    }
}
