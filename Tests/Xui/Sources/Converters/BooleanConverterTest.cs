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

using System;
using System.Windows;
using Cube.Xui.Converters;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// BooleanConverterTest
///
/// <summary>
/// Tests the Converter classes related to boolean type.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class BooleanConverterTest : ConvertHelper
{
    #region Positive

    /* --------------------------------------------------------------------- */
    ///
    /// Positive
    ///
    /// <summary>
    /// Tests the Positive converter class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(1,     ExpectedResult = true)]
    [TestCase(0,     ExpectedResult = false)]
    [TestCase(-1,    ExpectedResult = false)]
    [TestCase(100L,  ExpectedResult = true)]
    [TestCase(-100L, ExpectedResult = false)]
    [TestCase('a',   ExpectedResult = true)]
    public bool Positive<T>(T src) => Convert<bool>(new Positive(), src);

    #endregion

    #region PositiveOrZero

    /* --------------------------------------------------------------------- */
    ///
    /// PositiveOrZero
    ///
    /// <summary>
    /// Tests the PositiveOrZero converter class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(1,     ExpectedResult = true)]
    [TestCase(0,     ExpectedResult = true)]
    [TestCase(-1,    ExpectedResult = false)]
    [TestCase(100L,  ExpectedResult = true)]
    [TestCase(-100L, ExpectedResult = false)]
    [TestCase('a',   ExpectedResult = true)]
    public bool PositiveOrZero<T>(T src) => Convert<bool>(new PositiveOrZero(), src);

    #endregion

    #region Negative

    /* --------------------------------------------------------------------- */
    ///
    /// Negative
    ///
    /// <summary>
    /// Tests the Negative converter class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(1,     ExpectedResult = false)]
    [TestCase(0,     ExpectedResult = false)]
    [TestCase(-1,    ExpectedResult = true)]
    [TestCase(100L,  ExpectedResult = false)]
    [TestCase(-100L, ExpectedResult = true)]
    [TestCase('a',   ExpectedResult = false)]
    public bool Negative<T>(T src) => Convert<bool>(new Negative(), src);

    #endregion

    #region NegativeOrZero

    /* --------------------------------------------------------------------- */
    ///
    /// NegativeOrZero
    ///
    /// <summary>
    /// Tests the NegativeOrZero converter class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(1,     ExpectedResult = false)]
    [TestCase(0,     ExpectedResult = true)]
    [TestCase(-1,    ExpectedResult = true)]
    [TestCase(100L,  ExpectedResult = false)]
    [TestCase(-100L, ExpectedResult = true)]
    [TestCase('a',   ExpectedResult = false)]
    public bool NegativeOrZero<T>(T src) => Convert<bool>(new NegativeOrZero(), src);

    #endregion

    #region Inverse

    /* --------------------------------------------------------------------- */
    ///
    /// Inverse
    ///
    /// <summary>
    /// Tests the Inverse converter class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(true,  ExpectedResult = false)]
    [TestCase(false, ExpectedResult = true)]
    public bool Inverse(bool src) => Convert<bool>(new Inverse(), src);

    /* --------------------------------------------------------------------- */
    ///
    /// Inverse_Back
    ///
    /// <summary>
    /// Tests the ConvertBack method of the Inverse class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(true,  ExpectedResult = false)]
    [TestCase(false, ExpectedResult = true)]
    public bool Inverse_Back(bool src) => ConvertBack<bool>(new Inverse(), src);

    /* --------------------------------------------------------------------- */
    ///
    /// Inverse_ProvideValue
    ///
    /// <summary>
    /// Tests the ProvideValue method of the Inverse class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Inverse_ProvideValue()
    {
        var src = new Inverse();
        Assert.That(src.ProvideValue(null), Is.EqualTo(src));
    }

    #endregion

    #region BooleanToValue

    /* --------------------------------------------------------------------- */
    ///
    /// BooleanToValue
    ///
    /// <summary>
    /// Tests the BooleanToValue(T) converter class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase("src", "compared", ExpectedResult = "src")]
    public string BooleanToValue(string src, string compared) =>
        Convert<string>(new BooleanToValue<string>(src, compared,
            (x, y) => string.CompareOrdinal((string)x, compared) > 0),
            src
        );

    #endregion

    #region BooleanToInteger

    /* --------------------------------------------------------------------- */
    ///
    /// BooleanToInteger
    ///
    /// <summary>
    /// Tests the BooleanToInteger converter class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(true,  ExpectedResult = 1)]
    [TestCase(false, ExpectedResult = 0)]
    public int BooleanToInteger(bool src) =>
        Convert<int>(new BooleanToInteger(), src);

    /* --------------------------------------------------------------------- */
    ///
    /// BooleanToInteger_Back
    ///
    /// <summary>
    /// Tests the ConvertBack method of the BooleanToInteger class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void BooleanToInteger_Back() => Assert.That(
        () => ConvertBack<bool>(new BooleanToInteger(), 1),
        Throws.TypeOf<NotSupportedException>()
    );

    /* --------------------------------------------------------------------- */
    ///
    /// BooleanToInteger_ProvideValue
    ///
    /// <summary>
    /// Tests the ProvideValue method of the BooleanToInteger class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void BooleanToInteger_ProvideValue()
    {
        var src = new BooleanToInteger();
        Assert.That(src.ProvideValue(null), Is.EqualTo(src));
    }

    #endregion

    #region BooleanToVisibility

    /* --------------------------------------------------------------------- */
    ///
    /// BooleanToVisibility
    ///
    /// <summary>
    /// Tests the BooleanToVisibility converter class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(true,  ExpectedResult = Visibility.Visible)]
    [TestCase(false, ExpectedResult = Visibility.Collapsed)]
    public Visibility BooleanToVisibility(bool src) =>
        Convert<Visibility>(new BooleanToVisibility(), src);

    /* --------------------------------------------------------------------- */
    ///
    /// BooleanToVisibility_Func
    ///
    /// <summary>
    /// Tests the BooleanToVisibility converter class with a function.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void BooleanToVisibility_Func() => Assert.That(
        Convert<Visibility>(new BooleanToVisibility(e => (int)e > 0), 1),
        Is.EqualTo(Visibility.Visible)
    );

    #endregion
}
