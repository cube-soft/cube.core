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

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// LocaleTest
    ///
    /// <summary>
    /// Tests the Locale class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class LocaleTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Set
        ///
        /// <summary>
        /// Executes the test to set the new value of Language.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Set()
        {
            var count = 0;
            using (Locale.Subscribe(e => ++count))
            {
                Locale.Set(Language.English);
                Assert.That(Locale.Get(), Is.EqualTo(Language.English));

                Locale.Set(Language.Japanese);
                Assert.That(Locale.Get(), Is.EqualTo(Language.Japanese));

                Locale.Set(Language.Japanese);
                Assert.That(Locale.Get(), Is.EqualTo(Language.Japanese));

                Locale.Set(Language.German);
                Assert.That(Locale.Get(), Is.EqualTo(Language.German));

                Locale.Set(Language.Auto);
                Assert.That(Locale.Get(), Is.EqualTo(Language.Auto));
            }
            Assert.That(count, Is.EqualTo(4));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Configure
        ///
        /// <summary>
        /// Executes the test to customize the setter function.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Configure()
        {
            var count = 0;
            using (Locale.Subscribe(e => ++count))
            {
                Locale.Configure(new Accessor<Language>(() => Language.Japanese, e => { }));

                Locale.Set(Language.English);
                Assert.That(Locale.Get(), Is.EqualTo(Language.Japanese));

                Locale.Set(Language.Japanese);
                Assert.That(Locale.Get(), Is.EqualTo(Language.Japanese));

                Locale.Set(Language.Japanese);
                Assert.That(Locale.Get(), Is.EqualTo(Language.Japanese));

                Locale.Set(Language.German);
                Assert.That(Locale.Get(), Is.EqualTo(Language.Japanese));

                Locale.Set(Language.Auto);
                Assert.That(Locale.Get(), Is.EqualTo(Language.Japanese));
            }
            Assert.That(count, Is.EqualTo(3));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetCultureInfo
        ///
        /// <summary>
        /// Executes the test to get the CultureInfo object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Language.English,    ExpectedResult = "en-us")]
        [TestCase(Language.French,     ExpectedResult = "fr-fr")]
        [TestCase(Language.German,     ExpectedResult = "de-de")]
        [TestCase(Language.Japanese,   ExpectedResult = "ja-jp")]
        [TestCase(Language.Portuguese, ExpectedResult = "pt-pt")]
        [TestCase(Language.Russian,    ExpectedResult = "ru-ru")]
        [TestCase(Language.Spanish,    ExpectedResult = "es-es")]
        public string GetCultureInfo(Language src) =>
            src.ToCultureInfo().Name.ToLowerInvariant();

        /* ----------------------------------------------------------------- */
        ///
        /// GetCultureInfo_Auto
        ///
        /// <summary>
        /// Executes the test to get the language code from the Auto value.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetCultureInfo_Auto() =>
            Assert.That(Language.Auto.ToCultureInfo(), Is.Not.Null);

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Setup
        ///
        /// <summary>
        /// Executes before each test.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [SetUp]
        public void Setup() => Locale.Configure();

        #endregion
    }
}
