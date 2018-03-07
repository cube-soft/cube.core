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
using System.Collections;
using System.Collections.Generic;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// ArgumentsTest
    ///
    /// <summary>
    /// Arguments のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class ArgumentsTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Parse
        ///
        /// <summary>
        /// 非オプション項目を正常に解析できる事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(Parse_TestCases))]
        public int Parse(int id, IEnumerable<string> args)
        {
            var src = new Arguments(args);

            Assert.That(src[0], Is.Not.Null);
            Assert.That(src[src.Count - 1], Is.Not.Null);
            foreach (var item in (IEnumerable)src) Assert.That(item, Is.Not.Null);

            return src.Count;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse_Empty
        ///
        /// <summary>
        /// 空の配列で初期化した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Parse_Empty() => Assert.That(
            new Arguments(new string[0]).Count,
            Is.EqualTo(0)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Parse_Options
        ///
        /// <summary>
        /// オプション項目が正常に解析できる事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(Parse_Options_TestCases))]
        public string Parse_Options(int id, IEnumerable<string> args, string key)
        {
            try { return new Arguments(args).Options[key]; }
            catch { return null; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse_Options
        ///
        /// <summary>
        /// オプション項目が正常に解析できる事を確認するためのテストです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(Parse_Options_Count_TestCases))]
        public int Parse_Options_Count(int id, IEnumerable<string> args) =>
            new Arguments(args).Options.Count;

        #endregion

        #region TestCases

        /* ----------------------------------------------------------------- */
        ///
        /// Parse_TestCases
        ///
        /// <summary>
        /// Parse テストのテストケースを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<TestCaseData> Parse_TestCases
        {
            get
            {
                yield return new TestCaseData(0, new List<string> { "foo", "bar", "bas" }).Returns(3);
                yield return new TestCaseData(1, new List<string> { "foo", "--bar", "--", "bas" }).Returns(2);
                yield return new TestCaseData(2, new List<string> { "foo", "--", "bar", "hoge", "fuga" }).Returns(4);
                yield return new TestCaseData(3, new List<string> { "foo", "", "--bar" }).Returns(1);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse_Options_TestCases
        ///
        /// <summary>
        /// Parse_Options テストのテストケースを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<TestCaseData> Parse_Options_TestCases
        {
            get
            {
                yield return new TestCaseData(0, new List<string> { "foo", "--bar", "bas" }, "bar").Returns("bas");
                yield return new TestCaseData(1, new List<string> { "foo", "--bar", "bas" }, "bas").Returns(null);
                yield return new TestCaseData(2, new List<string> { "foo", "--bar" }, "bar").Returns(null);
                yield return new TestCaseData(3, new List<string> { "foo", "--bar", "bas", "--bar", "hoge" }, "bar").Returns("hoge");
                yield return new TestCaseData(4, new List<string> { "foo", "--bar", "--hoge", "fuga" }, "bar").Returns(null);
                yield return new TestCaseData(5, new List<string> { "foo", "--bar", "--hoge", "fuga" }, "hoge").Returns("fuga");
                yield return new TestCaseData(6, new List<string> { "foo", "--bar", "--", "bas" }, "bar").Returns(null);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse_Options_Count_TestCases
        ///
        /// <summary>
        /// Parse_Options_Count テストのテストケースを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<TestCaseData> Parse_Options_Count_TestCases
        {
            get
            {
                yield return new TestCaseData(0, new List<string> { "foo", "--bar", "bas" }).Returns(1);
                yield return new TestCaseData(1, new List<string> { "foo", "--bar" }).Returns(1);
                yield return new TestCaseData(2, new List<string> { "foo", "--bar", "bas", "--bar", "hoge" }).Returns(1);
                yield return new TestCaseData(3, new List<string> { "foo", "--bar", "--hoge", "fuga" }).Returns(2);
                yield return new TestCaseData(4, new List<string> { "foo", "--bar", "--", "bas" }).Returns(1);
                yield return new TestCaseData(5, new List<string> { "foo", "bas" }).Returns(0);
                yield return new TestCaseData(6, new List<string> { "foo", "--", "bas" }).Returns(0);
            }
        }

        #endregion
    }
}
