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

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableTest
    ///
    /// <summary>
    /// Represents tests of the Bindable class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class BindableTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Set
        ///
        /// <summary>
        /// Executes the test to set value.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Set()
        {
            var n   = 5;
            var src = new Bindable<int>(() => n, e => n = e, Dispatcher.Vanilla);

            Assert.That(src.Value, Is.EqualTo(n).And.EqualTo(5));
            src.Value = 10;
            Assert.That(src.Value, Is.EqualTo(n).And.EqualTo(10));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Set_InvalidOperationException
        ///
        /// <summary>
        /// Confirms the behavior when setting value without any setter
        /// functions.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Set_InvalidOperationException()
        {
            var src = new Bindable<int>(() => 8, Dispatcher.Vanilla);

            Assert.That(src.Value, Is.EqualTo(8));
            Assert.That(() => src.Value = 7, Throws.InvalidOperationException);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        ///
        /// <summary>
        /// Confirms the behavior of the PropertyChanged event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Refresh()
        {
            var src = new Bindable<Person>(Dispatcher.Vanilla);

            var count = 0;
            src.PropertyChanged += (s, e) => ++count;
            var value = new Person();
            src.Value = value;

            value.Name = "Jack";
            value.Age  = 20;
            src.Refresh();
            Assert.That(count, Is.EqualTo(2));

            src.Value = value;
            Assert.That(count, Is.EqualTo(2));
        }

        #endregion
    }
}
