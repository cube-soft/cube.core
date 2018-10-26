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
    /// Tests for the Locale class.
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
            using (Locale.Subscribe(() => ++count))
            {
                Locale.Set(Lanugage.English);
                Locale.Set(Lanugage.Japanese);
                Locale.Set(Lanugage.Japanese);
                Locale.Set(Lanugage.German);
                Locale.Set(Lanugage.Auto);
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
            using (Locale.Subscribe(() => ++count))
            {
                Locale.Configure(e => false);
                Locale.Set(Lanugage.English);
                Locale.Set(Lanugage.Japanese);
                Locale.Set(Lanugage.Japanese);
                Locale.Set(Lanugage.German);
                Locale.Set(Lanugage.Auto);
            }
            Assert.That(count, Is.EqualTo(0));
        }

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
