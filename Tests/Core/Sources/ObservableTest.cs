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
using System.Collections.Generic;
using Cube.Mixin.Observing;
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
                var dc = src.Subscribe(e => ++n);

                src.Value = "Hello";    // 1
                Assert.That(src.Value, Is.EqualTo("Hello"));
                src.Value = "";         // 2
                Assert.That(src.Value, Is.Empty);
                src.Value = null;       // 3
                Assert.That(src.Value, Is.Null);
                src.Value = "";         // 4
                Assert.That(src.Value, Is.Empty);
                src.Value = "";         // 4 (ignore)
                Assert.That(src.Value, Is.Empty);

                src.Age = 0;            // 4 (ignore)
                Assert.That(src.Age,   Is.EqualTo(0));
                src.Age = 20;           // 5
                Assert.That(src.Age,   Is.EqualTo(20));

                dc.Dispose();
                src.Value = null;
                Assert.That(src.Value, Is.Null);
                src.Value = "";
                Assert.That(src.Value, Is.Empty);
            }
            Assert.That(n, Is.EqualTo(5));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        ///
        /// <summary>
        /// Tests the Refresh method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Refresh()
        {
            var dest = new Dictionary<string, int>
            {
                { nameof(Mock.Value), 0 },
                { nameof(Mock.Age),   0 },
            };

            using (var src = new Mock())
            {
                var dc = src.Subscribe(e => dest[e]++);
                src.Refresh(nameof(Mock.Value), nameof(Mock.Age), nameof(Mock.Value));
                dc.Dispose();
            }

            Assert.That(dest[nameof(Mock.Value)], Is.EqualTo(2));
            Assert.That(dest[nameof(Mock.Age)],   Is.EqualTo(1));
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
                get => GetProperty<string>();
                set => SetProperty(value);
            }
            public int Age
            {
                get => GetProperty<int>();
                set => SetProperty(value);
            }
        }

        #endregion
    }
}
