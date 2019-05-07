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
using Cube.Mixin.Iteration;
using NUnit.Framework;
using System;

namespace Cube.Tests.Mixin
{
    /* --------------------------------------------------------------------- */
    ///
    /// IterationTest
    ///
    /// <summary>
    /// Represents tests for the IterateExtension class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class IterationTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Times
        ///
        /// <summary>
        /// Executes the test of Times extended method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Times()
        {
            var actual = 0;
            10.Times(() => actual++);
            Assert.That(actual, Is.EqualTo(10));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Times_WithIndex
        ///
        /// <summary>
        /// Executes the test of Times extended method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Times_WithIndex()
        {
            var actual = 0;
            10.Times(i => actual += i);
            Assert.That(actual, Is.EqualTo(45));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Try
        ///
        /// <summary>
        /// Executes the test of Try extended method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Try()
        {
            var n = 0;
            this.Try(10, () => ThrowIfOdd(++n));
            Assert.That(n, Is.EqualTo(2));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Try_Throws
        ///
        /// <summary>
        /// Executes the test of Try extended method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Try_Throws()
        {
            var n = 0;
            Assert.That(
                () => this.Try(10, () => { ++n; ThrowIfOdd(1); }),
                Throws.TypeOf<ArgumentException>()
                      .And.Message.EqualTo("Odd number")
            );
            Assert.That(n, Is.EqualTo(10));
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// ThrowIfOdd
        ///
        /// <summary>
        /// Throws if the specified value is odd number.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ThrowIfOdd(int n)
        {
            if (n % 2 != 0) throw new ArgumentException("Odd number");
        }

        #endregion
    }
}
