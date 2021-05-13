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
using System.Threading;
using System.Threading.Tasks;
using Cube.Tests;
using NUnit.Framework;

namespace Cube.FileSystem.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// WaitTest
    ///
    /// <summary>
    /// Tests the Wait class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class WaitTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// WaitFor
        ///
        /// <summary>
        /// Tests to wait for the specified predicate to be true.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void WaitFor()
        {
            var count = 0;
            TaskEx.Run(() => { for (var i = 0; i < 10; ++i) ++count; });
            Assert.That(Wait.For(() => count == 10), Is.True);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WaitFor_Timeout
        ///
        /// <summary>
        /// Tests for timeout.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void WaitFor_Timeout()
        {
            var count = 0;
            TaskEx.Run(() => { for (var i = 0; i < 5; ++i) ++count; });
            Assert.That(Wait.For(() => count == 10, 1000), Is.False);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WaitFor_Cancel
        ///
        /// <summary>
        /// Tests for cancellation.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void WaitFor_Cancel()
        {
            var count = 0;
            var cts   = new CancellationTokenSource();
            TaskEx.Run(() =>
            {
                for (var i = 0; i < 100; ++i) ++count;
                cts.Cancel();
            });
            Assert.That(Wait.For(cts.Token), Is.True);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WaitFor_Cancel_Timeout
        ///
        /// <summary>
        /// Tests for timeout.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void WaitFor_Cancel_Timeout()
        {
            var count = 0;
            var cts = new CancellationTokenSource();
            TaskEx.Run(() => { for (var i = 0; i < 100; ++i) ++count; });
            Assert.That(Wait.For(cts.Token, 1000), Is.False);
        }

        #endregion
    }
}
