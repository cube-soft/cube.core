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
namespace Cube.Xui.Tests.Converters;

using Cube.DataContract;
using Cube.Xui.Converters;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// StringConverterTest
///
/// <summary>
/// Tests the string related converter classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class StringConverterTest : ConvertHelper
{
    #region UpperCase

    /* --------------------------------------------------------------------- */
    ///
    /// UpperCase
    ///
    /// <summary>
    /// Tests the UpperCase class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Hello", ExpectedResult = "HELLO")]
    [TestCase("Ｂｙｅ", ExpectedResult = "ＢＹＥ")]
    [TestCase("Hello 日本", ExpectedResult = "HELLO 日本")]
    [TestCase(Format.Json, ExpectedResult = "JSON")]
    [TestCase(null, ExpectedResult = "")]
    public string UpperCase(object src) => Convert<string>(new UpperCase(), src);

    #endregion

    #region LowerCase

    /* --------------------------------------------------------------------- */
    ///
    /// LowerCase
    ///
    /// <summary>
    /// Tests the LowerCase class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("Hello", ExpectedResult = "hello")]
    [TestCase("Ｂｙｅ", ExpectedResult = "ｂｙｅ")]
    [TestCase("Hello 日本", ExpectedResult = "hello 日本")]
    [TestCase(Format.Json, ExpectedResult = "json")]
    [TestCase(null, ExpectedResult = "")]
    public string LowerCase(object src) => Convert<string>(new LowerCase(), src);

    #endregion
}
