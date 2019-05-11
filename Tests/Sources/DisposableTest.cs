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
    /// DisposableTest
    ///
    /// <summary>
    /// Test the Disposable class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class DisposableTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Create_Dispose
        ///
        /// <summary>
        /// Tests the Disposable.Create method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Dispose()
        {
            var dest = 0;
            var src  = Disposable.Create(() => dest++);

            src.Dispose();
            Assert.That(dest, Is.EqualTo(1));
            Assert.DoesNotThrow(() => src.Dispose());
            Assert.That(dest, Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_ArgumentNullException
        ///
        /// <summary>
        /// Tests the Disposable.Create method with the null object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_ArgumentNullException()
        {
            Assert.That(() => Disposable.Create(null), Throws.ArgumentNullException);
        }

        #endregion
    }
}
