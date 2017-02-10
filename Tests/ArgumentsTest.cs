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
using System.Linq;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// ArgumentsTest
    /// 
    /// <summary>
    /// プログラムオプション等の引数を解析するためのクラスです。
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

        [TestCaseSource(typeof(ArgumentsTest), "Parse_Source")]
        public void Parse(string[] args, int expected)
        {
            var parser = new Arguments(args);
            Assert.That(parser.Get().Count, Is.EqualTo(expected));
        }

        private static object Parse_Source = new[]
        {
            new object[]
            {
                new string[] { "foo", "bar", "bas" },
                3
            },

            new object[]
            {
                new string[] { "foo", "--bar", "--", "bas" },
                2
            },

            new object[]
            {
                new string[] { "foo", "--", "bar", "hoge", "fuga" },
                4
            },

            new object[]
            {
                new string[] { },
                0
            }
        };

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

        [TestCaseSource(typeof(ArgumentsTest), "Parse_Options_Source")]
        public void Parse_Options(string[] args, string option, string expected)
        {
            var parser = new Arguments(args);
            Assert.That(parser.Get(option), Is.EqualTo(expected));
        }

        private static object[] Parse_Options_Source = new[]
        {
            new object[]
            {
                new string[] { "foo", "--bar", "bas" },
                "bar", "bas"
            },

            new object[]
            {
                new string[] { "foo", "--bar", "bas" },
                "bas", null
            },

            new object[]
            {
                new string[] { "foo", "--bar" },
                "bar", null
            },

            new object[]
            {
                new string[] { "foo", "--bar", "bas", "--bar", "hoge" },
                "bar", "hoge"
            },

            new object[]
            {
                new string[] { "foo", "--bar", "--hoge", "fuga" },
                "bar", null
            },

            new object[]
            {
                new string[] { "foo", "--bar", "--hoge", "fuga" },
                "hoge", "fuga"
            },

            new object[]
            {
                new string[] { "foo", "--bar", "--", "bas" },
                "bar", null
            }
        };

        #endregion
    }
}
