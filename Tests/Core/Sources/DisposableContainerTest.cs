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
    /// DisposableContainerTest
    ///
    /// <summary>
    /// Test the DisposableContainer class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class DisposableContainerTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Add
        ///
        /// <summary>
        /// Tests the Add and Dispose methods.
        /// objects.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Add()
        {
            var n   = 0;
            var src = new DisposableContainer();

            src.Add(() => ++n);
            src.Add(() => ++n);
            src.Add(
                Disposable.Create(() => ++n),
                Disposable.Create(() => ++n),
                Disposable.Create(() => ++n)
            );

            Assert.That(n, Is.EqualTo(0));
            src.Dispose();
            Assert.That(n, Is.EqualTo(5));
            src.Add(() => ++n);
            Assert.That(n, Is.EqualTo(6));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Make_Empty
        ///
        /// <summary>
        /// Tests the constructor and Dispose method with no disposable
        /// objects.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Make_Empty()
        {
            Assert.DoesNotThrow(() => { using (new DisposableContainer()) { } });
        }

        #endregion
    }
}
