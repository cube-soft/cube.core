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
namespace Cube.Tests.Extensions;

using Cube.Text.Extensions;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// StringTest
///
/// <summary>
/// Tests extended methods of the string class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class StringTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// HasValue
    ///
    /// <summary>
    /// Executes the test of the HasValue extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("0", ExpectedResult = true)]
    [TestCase("Hello", ExpectedResult = true)]
    [TestCase("こんにちは", ExpectedResult = true)]
    [TestCase("", ExpectedResult = false)]
    [TestCase(default(string), ExpectedResult = false)]
    public bool HasValue(string src) => src.HasValue();

    /* --------------------------------------------------------------------- */
    ///
    /// Quote
    ///
    /// <summary>
    /// Executes the test of the Quote extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Hello", ExpectedResult = "\"Hello\"")]
    [TestCase("こんにちは", ExpectedResult = "\"こんにちは\"")]
    [TestCase("\"Already\"", ExpectedResult = "\"\"Already\"\"")]
    [TestCase("", ExpectedResult = "\"\"")]
    [TestCase(default(string), ExpectedResult = "\"\"")]
    public string Quote(string src) => src.Quote();

    /* --------------------------------------------------------------------- */
    ///
    /// FuzzyEquals
    ///
    /// <summary>
    /// Executes the test of the FuzzyEquals extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Hello", "Hello", ExpectedResult = true)]
    [TestCase("Hello", "world", ExpectedResult = false)]
    [TestCase("ABC", "abc", ExpectedResult = true)]
    [TestCase("ＡＢＣ", "ＡＢＣ", ExpectedResult = true)]
    [TestCase("ＡＢＣ", "ａｂｃ", ExpectedResult = true)]
    [TestCase("ＡＢＣ", "ABC", ExpectedResult = false)]
    [TestCase("12,345.678", "12.345,678", ExpectedResult = false)]
    [TestCase("", "abc", ExpectedResult = false)]
    [TestCase("", "", ExpectedResult = true)]
    [TestCase("", null, ExpectedResult = false)]
    public bool FuzzyEquals(string src, string cmp) => src.FuzzyEquals(cmp);

    /* --------------------------------------------------------------------- */
    ///
    /// FuzzyStartsWith
    ///
    /// <summary>
    /// Executes the test of the FuzzyStartsWith extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Hello", "Hello", ExpectedResult = true)]
    [TestCase("Hello", "H", ExpectedResult = true)]
    [TestCase("Hello", "world", ExpectedResult = false)]
    [TestCase("ABC", "abc", ExpectedResult = true)]
    [TestCase("ABC", "a", ExpectedResult = true)]
    [TestCase("ＡＢＣ", "ＡＢＣ", ExpectedResult = true)]
    [TestCase("ＡＢＣ", "Ａ", ExpectedResult = true)]
    [TestCase("ＡＢＣ", "ａｂｃ", ExpectedResult = true)]
    [TestCase("ＡＢＣ", "ａ", ExpectedResult = true)]
    [TestCase("ＡＢＣ", "ABC", ExpectedResult = false)]
    [TestCase("ＡＢＣ", "A", ExpectedResult = false)]
    [TestCase("12,345.678", "12.345,678", ExpectedResult = false)]
    [TestCase("", "abc", ExpectedResult = false)]
    [TestCase("", "", ExpectedResult = true)]
    public bool FuzzyStartsWith(string src, string cmp) => src.FuzzyStartsWith(cmp);

    /* --------------------------------------------------------------------- */
    ///
    /// FuzzyEndsWith
    ///
    /// <summary>
    /// Executes the test of the FuzzyEndsWith extended method.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Hello", "Hello", ExpectedResult = true)]
    [TestCase("Hello", "o", ExpectedResult = true)]
    [TestCase("Hello", "world", ExpectedResult = false)]
    [TestCase("ABC", "abc", ExpectedResult = true)]
    [TestCase("ABC", "c", ExpectedResult = true)]
    [TestCase("ＡＢＣ", "ＡＢＣ", ExpectedResult = true)]
    [TestCase("ＡＢＣ", "Ｃ", ExpectedResult = true)]
    [TestCase("ＡＢＣ", "ａｂｃ", ExpectedResult = true)]
    [TestCase("ＡＢＣ", "ｃ", ExpectedResult = true)]
    [TestCase("ＡＢＣ", "ABC", ExpectedResult = false)]
    [TestCase("ＡＢＣ", "C", ExpectedResult = false)]
    [TestCase("12,345.678", "12.345,678", ExpectedResult = false)]
    [TestCase("", "abc", ExpectedResult = false)]
    [TestCase("", "", ExpectedResult = true)]
    public bool FuzzyEndsWith(string src, string cmp) => src.FuzzyEndsWith(cmp);

    #endregion
}
