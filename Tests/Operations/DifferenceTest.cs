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
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cube.Collections;
using Cube.Differences;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// DifferenceTest
    /// 
    /// <summary>
    /// Cube.Collections.Operations.Difference のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class DifferenceTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Difference
        /// 
        /// <summary>
        /// 差分検出のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(Compare_TestCases))]
        public void Difference(string older, string newer, Result<char> expected)
        {
            var actual = newer.Difference(older).First();
            Assert.That(actual.Condition, Is.EqualTo(expected.Condition));
            Assert.That(actual.Older, Is.EquivalentTo(expected.Older));
            Assert.That(actual.Newer, Is.EquivalentTo(expected.Newer));
        }

        public static IEnumerable<TestCaseData> Compare_TestCases
        {
            get
            {
                yield return TestCase("Hello, world.", "Hello, sunset.", Condition.Changed, "world", "sunset");
                yield return TestCase("Hello, sunset.", "Hello, world.", Condition.Changed, "sunset", "world");
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Difference_OlderIsEmpty
        /// 
        /// <summary>
        /// 変更前のテキストが空の場合のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("")]
        [TestCase(null)]
        public void Difference_OlderIsEmpty(string older)
        {
            var actual = "empty".Difference(older).First();
            Assert.That(actual.Condition, Is.EqualTo(Condition.Inserted));
            Assert.That(actual.Older, Is.Null);
            Assert.That(actual.Newer, Is.EquivalentTo("empty"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Difference_NewerIsEmpty
        /// 
        /// <summary>
        /// 変更後のテキストが空の場合のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("")]
        [TestCase(null)]
        public void Difference_NewerIsEmpty(string newer)
        {
            var actual = newer.Difference("empty").First();
            Assert.That(actual.Condition, Is.EqualTo(Condition.Deleted));
            Assert.That(actual.Older, Is.EquivalentTo("empty"));
            Assert.That(actual.Newer, Is.Null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Difference_IgnoreCase
        /// 
        /// <summary>
        /// 大文字・小文字を無視した比較のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Difference_IgnoreCase()
        {
            var actual = "AbCdEfG".Difference(
                "aBcDeFg",
                (x, y) => char.ToLower(x) == char.ToLower(y),
                true
            );

            Assert.That(actual.Count(), Is.EqualTo(0));
        }

        #endregion

        #region Helper methods

        /* ----------------------------------------------------------------- */
        ///
        /// TestCase
        /// 
        /// <summary>
        /// TestCaseData オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static TestCaseData TestCase(string older, string newer,
            Condition cond, string oresult, string nresult)
            => new TestCaseData(older, newer, new Result<char>(cond, oresult, nresult));

        #endregion
    }
}
