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
using Cube.Generics;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// StringExtensionTest
    ///
    /// <summary>
    /// StringExtension のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class StringExtensionTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// HasValue
        ///
        /// <summary>
        /// 1 文字以上の文字列を保持しているかどうかを判別するテストを
        /// 実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("0",             ExpectedResult = true )]
        [TestCase("Hello",         ExpectedResult = true )]
        [TestCase("こんにちは",    ExpectedResult = true )]
        [TestCase("",              ExpectedResult = false)]
        [TestCase(default(string), ExpectedResult = false)]
        public bool HasValue(string src) => src.HasValue();

        /* ----------------------------------------------------------------- */
        ///
        /// Quote
        ///
        /// <summary>
        /// 文字列を引用符で囲むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("Hello",         ExpectedResult = "\"Hello\"")]
        [TestCase("こんにちは",    ExpectedResult = "\"こんにちは\"")]
        [TestCase("\"Already\"",   ExpectedResult = "\"\"Already\"\"")]
        [TestCase("",              ExpectedResult = "\"\"")]
        [TestCase(default(string), ExpectedResult = "\"\"")]
        public string Quote(string src) => src.Quote();

        #endregion
    }
}
