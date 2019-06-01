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
    /// ObservableTest
    ///
    /// <summary>
    /// Test the ObservableBase class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class ObservableTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Set
        ///
        /// <summary>
        /// Tests the setter and getter methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Set()
        {
            var n = 0;
            using (var src = new Mock())
            {
                Assert.That(src.Value, Is.Null);
                src.Value = "";
                Assert.That(src.Value, Is.Empty);
                src.PropertyChanged += (s, e) => ++n;

                src.Value = "Hello";
                Assert.That(src.Value, Is.EqualTo("Hello"));
                src.Value = "";
                Assert.That(src.Value, Is.Empty);
                src.Value = null;
                Assert.That(src.Value, Is.Null);
                src.Value = "";
                Assert.That(src.Value, Is.Empty);
                src.Value = ""; // ignore
                Assert.That(src.Value, Is.Empty);
                src.Refresh(nameof(src.Value), nameof(src.Value), nameof(src.Value));
            }
            Assert.That(n, Is.EqualTo(7));
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Mock
        ///
        /// <summary>
        /// Provides functionality to test the DisposableObservable class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private class Mock : ObservableBase
        {
            public Mock() : base() { }
            protected override void Dispose(bool disposing) { }
            public string Value
            {
                get => _value;
                set => SetProperty(ref _value, value);
            }
            private string _value;
        }

        #endregion
    }
}
