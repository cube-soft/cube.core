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
    /// OnceInitializerTest
    ///
    /// <summary>
    /// Provides functionality to test the <c>OnceInitializer</c> class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class OnceInitializerTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Tests to initialize and destroy through the OnceInitializer
        /// instance.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Invoke()
        {
            var dest = 0;
            var src  = new OnceInitializer(() => ++dest, () => --dest);

            src.Invoke();
            src.Invoke();
            src.Invoke();
            Assert.That(src.Invoked, Is.True);
            Assert.That(dest, Is.EqualTo(1));

            src.Dispose();
            Assert.That(dest, Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke_Throws
        ///
        /// <summary>
        /// Tests to raise the ObjectDisposedException exception.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Invoke_Throws() => Assert.That(() =>
        {
            var dest = 0;
            var src  = new OnceInitializer(() => ++dest, () => --dest);
            src.Invoke();
            src.Dispose();
            src.Invoke();
        }, Throws.TypeOf<ObjectDisposedException>());
    }
}
