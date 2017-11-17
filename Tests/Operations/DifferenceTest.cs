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
    /// Cube.Collections.Operations.Diff のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class DifferenceTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Diff
        /// 
        /// <summary>
        /// 差分検出のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Diff(string older, string newer, Result<char> expected)
        {
            var actual = newer.Diff(older).First();
            Assert.That(actual.Condition, Is.EqualTo(expected.Condition));
            Assert.That(actual.Older, Is.EquivalentTo(expected.Older));
            Assert.That(actual.Newer, Is.EquivalentTo(expected.Newer));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Diff_OlderIsEmpty
        /// 
        /// <summary>
        /// 変更前のテキストが空の場合のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("")]
        [TestCase(null)]
        public void Diff_OlderIsEmpty(string older)
        {
            var actual = "empty".Diff(older).First();
            Assert.That(actual.Condition, Is.EqualTo(Condition.Inserted));
            Assert.That(actual.Older, Is.Null);
            Assert.That(actual.Newer, Is.EquivalentTo("empty"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Diff_NewerIsEmpty
        /// 
        /// <summary>
        /// 変更後のテキストが空の場合のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("")]
        [TestCase(null)]
        public void Diff_NewerIsEmpty(string newer)
        {
            var actual = newer.Diff("empty").First();
            Assert.That(actual.Condition, Is.EqualTo(Condition.Deleted));
            Assert.That(actual.Older, Is.EquivalentTo("empty"));
            Assert.That(actual.Newer, Is.Null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Diff_IgnoreCase
        /// 
        /// <summary>
        /// 大文字・小文字を無視した比較のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Diff_IgnoreCase()
        {
            var actual = "AbCdEfG".Diff(
                "aBcDeFg",
                (x, y) => char.ToLower(x) == char.ToLower(y),
                true
            );

            Assert.That(actual.Count(), Is.EqualTo(0));
        }

        #endregion

        #region TestCases

        /* ----------------------------------------------------------------- */
        ///
        /// TestCases
        /// 
        /// <summary>
        /// テストに使用するデータを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return TestCase("Hello, world.", "Hello, sunset.", Condition.Changed, "world", "sunset");
                yield return TestCase("Hello, sunset.", "Hello, world.", Condition.Changed, "sunset", "world");
            }
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
