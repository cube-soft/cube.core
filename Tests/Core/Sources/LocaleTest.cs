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
namespace Cube.Tests;

using Cube.Globalization;
using NUnit.Framework;

/* ------------------------------------------------------------------------- */
///
/// LocaleTest
///
/// <summary>
/// Tests the Locale class.
/// </summary>
///
/* ------------------------------------------------------------------------- */
[TestFixture]
class LocaleTest
{
    #region Tests

    /* --------------------------------------------------------------------- */
    ///
    /// Reset
    ///
    /// <summary>
    /// Executes the test to set the new value of Language.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Reset()
    {
        var count = 0;
        using (Locale.Subscribe(e => ++count))
        {
            Locale.Reset(Language.English);
            Assert.That(Locale.GetCurrentLanguage(), Is.EqualTo(Language.English));

            Locale.Reset(Language.Japanese);
            Assert.That(Locale.GetCurrentLanguage(), Is.EqualTo(Language.Japanese));

            Locale.Reset(Language.Japanese);
            Assert.That(Locale.GetCurrentLanguage(), Is.EqualTo(Language.Japanese));

            Locale.Reset(Language.German);
            Assert.That(Locale.GetCurrentLanguage(), Is.EqualTo(Language.German));

            Locale.Reset(Language.Auto);
            Assert.That(Locale.GetCurrentLanguage(), Is.EqualTo(Language.Auto));
        }
        Assert.That(count, Is.EqualTo(4));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Configure
    ///
    /// <summary>
    /// Executes the test to customize the setter function.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void Configure()
    {
        var count = 0;
        using (Locale.Subscribe(e => ++count))
        {
            Locale.Configure(new Accessor<Language>(() => Language.Japanese, e => { }));

            Locale.Reset(Language.English);
            Assert.That(Locale.GetCurrentLanguage(), Is.EqualTo(Language.Japanese));

            Locale.Reset(Language.Japanese);
            Assert.That(Locale.GetCurrentLanguage(), Is.EqualTo(Language.Japanese));

            Locale.Reset(Language.Japanese);
            Assert.That(Locale.GetCurrentLanguage(), Is.EqualTo(Language.Japanese));

            Locale.Reset(Language.German);
            Assert.That(Locale.GetCurrentLanguage(), Is.EqualTo(Language.Japanese));

            Locale.Reset(Language.Auto);
            Assert.That(Locale.GetCurrentLanguage(), Is.EqualTo(Language.Japanese));
        }
        Assert.That(count, Is.EqualTo(3));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetCultureInfo
    ///
    /// <summary>
    /// Executes the test to get the CultureInfo object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestCase(Language.English,    ExpectedResult = "en-us")]
    [TestCase(Language.French,     ExpectedResult = "fr-fr")]
    [TestCase(Language.German,     ExpectedResult = "de-de")]
    [TestCase(Language.Japanese,   ExpectedResult = "ja-jp")]
    [TestCase(Language.Portuguese, ExpectedResult = "pt-pt")]
    [TestCase(Language.Russian,    ExpectedResult = "ru-ru")]
    [TestCase(Language.Spanish,    ExpectedResult = "es-es")]
    public string GetCultureInfo(Language src) => src.ToCultureInfo().Name.ToLowerInvariant();

    /* --------------------------------------------------------------------- */
    ///
    /// GetDefaultValues
    ///
    /// <summary>
    /// Tests some methods about default values.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Test]
    public void GetDefaultValues()
    {
        Assert.That(Language.Auto.ToCultureInfo(), Is.Not.Null);
        Assert.That(Language.Unknown.ToCultureInfo(), Is.Null);
        Assert.That(Locale.GetDefaultCultureInfo(), Is.Not.Null);
        Assert.That(Locale.GetDefaultLanguage(), Is.Not.EqualTo(Language.Unknown));
    }

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// Setup
    ///
    /// <summary>
    /// Executes before each test.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [SetUp]
    public void Setup() => Locale.Configure();

    #endregion
}
