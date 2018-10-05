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
using System;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// AssemblyReaderTest
    ///
    /// <summary>
    /// Tests for the AssemblyReader class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class AccessorTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Create_Integer
        ///
        /// <summary>
        /// Executes the test for initializing with the default value
        /// of type int.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Integer() => Assert.That(
            new Accessor<int>().Get(), Is.EqualTo(0)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Create_String
        ///
        /// <summary>
        /// Executes the test for initializing with the default value
        /// of type string.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_String() => Assert.That(
            new Accessor<string>().Get(), Is.Null
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Create_DateTime
        ///
        /// <summary>
        /// Executes the test for initializing with the default value
        /// of type DateTime.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_DateTime() => Assert.That(
            new Accessor<DateTime>().Get(), Is.EqualTo(DateTime.MinValue)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Create_Nullable
        ///
        /// <summary>
        /// Executes the test for initializing with the default value
        /// of type Nullable(DateTime).
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Nullable() => Assert.That(
            new Accessor<DateTime?>().Get(), Is.Null
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Create_Delegate
        ///
        /// <summary>
        /// Executes the test for initializing with delegations.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Delegate()
        {
            var n   = 100;
            var src = new Accessor<int>(() => n, e => { n = e; return true; });

            Assert.That(src.Get(), Is.EqualTo(100));
            src.Set(200);
            Assert.That(src.Get(), Is.EqualTo(n).And.EqualTo(200));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Set_Throws
        ///
        /// <summary>
        /// Confirms the behavior when trying to set value to the
        /// Accessor taht is not pecified the setter delegation.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Set_Throws() => Assert.That(
            () => new Accessor<int>(() => 10).Set(1000),
            Throws.TypeOf<InvalidOperationException>()
        );

        #endregion
    }
}
