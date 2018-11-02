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
using GalaSoft.MvvmLight.Command;
using NUnit.Framework;
using System;

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableElementTest
    ///
    /// <summary>
    /// Represents tests of the BindableElement(T) class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class BindableElementTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Properties
        ///
        /// <summary>
        /// Confirms values of properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("Hello, world!", 10)]
        public void Properties(string text, int n)
        {
            using (var src = new BindableElement<int>(n, () => text))
            {
                Assert.That(src.Text,    Is.EqualTo(text));
                Assert.That(src.Value,   Is.EqualTo(n));
                Assert.That(src.Command, Is.Null);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Set_Throws
        ///
        /// <summary>
        /// Confirms the behavior when setting value without any setter
        /// functions.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Set_Throws()
        {
            using (var src = new BindableElement<string>(() => "Get", () => "Text"))
            {
                Assert.That(src.Text,    Is.EqualTo("Text"));
                Assert.That(src.Value,   Is.EqualTo("Get"));
                Assert.That(src.Command, Is.Null);
                Assert.That(() => src.Value = "Dummy", Throws.TypeOf<InvalidOperationException>());
                Assert.DoesNotThrow(() => src.Command = new RelayCommand(() => { }));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetLanguage
        ///
        /// <summary>
        /// Executes the test to change the language settings.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SetLanguage()
        {
            var count = 0;
            using (var src = new BindableElement<int>(() => "Language"))
            {
                src.PropertyChanged += (s, e) =>
                {
                    Assert.That(e.PropertyName, Is.EqualTo(nameof(src.Text)));
                    ++count;
                };

                Locale.Set(Language.French);
                Locale.Set(Language.Russian);
            }
            Assert.That(count, Is.InRange(1, 2));
        }

        #endregion
    }
}
