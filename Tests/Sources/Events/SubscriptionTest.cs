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
using System.Linq;
using System.Threading.Tasks;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// SubscriptionTest
    ///
    /// <summary>
    /// Tests for the Subscription class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class SubscriptionTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Executes the test for registering and removing subscription.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Subscribe()
        {
            var n   = 0;
            var src = new Subscription();

            Parallel.ForEach(Enumerable.Range(0, 10), i =>
            {
                var d0 = src.Subscribe(() => ++n);
                var d1 = src.SubscribeAsync(() => { n *= 2; return Task.FromResult(0); });

                d0.Dispose();
                d1.Dispose();
            });

            Assert.That(src.Count, Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SubscribeT
        ///
        /// <summary>
        /// Executes the test for registering and removing subscription.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SubscribeT()
        {
            var n   = 0;
            var src = new Subscription<int>();

            Parallel.ForEach(Enumerable.Range(0, 10), i =>
            {
                var d0 = src.Subscribe(e => n += e);
                var d1 = src.SubscribeAsync(e => { n *= e; return Task.FromResult(0); });

                d0.Dispose();
                d1.Dispose();
            });

            Assert.That(src.Count, Is.EqualTo(0));
        }

        #endregion
    }
}
