/* ------------------------------------------------------------------------- */
///
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

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
    [Parallelizable]
    [TestFixture]
    class ArgumentsTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Parse
        ///
        /// <summary>
        /// オプション以外の項目を正常に解析できる事を確認するための
        /// テストです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        #region Parse

        [TestCaseSource(nameof(Parse_TestCases))]
        public int Parse(IEnumerable<string> args)
            => new Arguments(args).Get().Count();

        public static IEnumerable<TestCaseData> Parse_TestCases
        {
            get
            {
                yield return new TestCaseData(new List<string> { "foo", "bar", "bas" }).Returns(3);
                yield return new TestCaseData(new List<string> { "foo", "--bar", "--", "bas" }).Returns(2);
                yield return new TestCaseData(new List<string> { "foo", "--", "bar", "hoge", "fuga" }).Returns(4);
                yield return new TestCaseData(new List<string>()).Returns(0);
            }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        ///
        /// Parse_Options
        ///
        /// <summary>
        /// オプション項目が正常に解析できる事を確認するためのテストです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        #region Parse options

        [TestCaseSource(nameof(Parse_Options_TestCases))]
        public string Parse_Options(IEnumerable<string> args, string option)
            => new Arguments(args).Get(option);

        [TestCaseSource(nameof(Parse_Options_Count_TestCases))]
        public int Parse_Options_Count(IEnumerable<string> args)
            => new Arguments(args).GetOptions().Count;

        public static IEnumerable<TestCaseData> Parse_Options_TestCases
        {
            get
            {
                yield return new TestCaseData(new List<string> { "foo", "--bar", "bas" }, "bar").Returns("bas");
                yield return new TestCaseData(new List<string> { "foo", "--bar", "bas" }, "bas").Returns(null);
                yield return new TestCaseData(new List<string> { "foo", "--bar" }, "bar").Returns(null);
                yield return new TestCaseData(new List<string> { "foo", "--bar", "bas", "--bar", "hoge" }, "bar").Returns("hoge");
                yield return new TestCaseData(new List<string> { "foo", "--bar", "--hoge", "fuga" }, "bar").Returns(null);
                yield return new TestCaseData(new List<string> { "foo", "--bar", "--hoge", "fuga" }, "hoge").Returns("fuga");
                yield return new TestCaseData(new List<string> { "foo", "--bar", "--", "bas" }, "bar").Returns(null);
            }
        }

        public static IEnumerable<TestCaseData> Parse_Options_Count_TestCases
        {
            get
            {
                yield return new TestCaseData(new List<string> { "foo", "--bar", "bas" }).Returns(1);
                yield return new TestCaseData(new List<string> { "foo", "--bar" }).Returns(1);
                yield return new TestCaseData(new List<string> { "foo", "--bar", "bas", "--bar", "hoge" }).Returns(1);
                yield return new TestCaseData(new List<string> { "foo", "--bar", "--hoge", "fuga" }).Returns(2);
                yield return new TestCaseData(new List<string> { "foo", "--bar", "--", "bas" }).Returns(1);
                yield return new TestCaseData(new List<string> { "foo", "bas" }).Returns(0);
                yield return new TestCaseData(new List<string> { "foo", "--", "bas" }).Returns(0);
            }
        }

        #endregion
    }
}
