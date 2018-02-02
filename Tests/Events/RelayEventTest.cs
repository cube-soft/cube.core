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

namespace Cube.Tests.Events
{
    /* --------------------------------------------------------------------- */
    ///
    /// RelayEventTest
    ///
    /// <summary>
    /// RelayEvent のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class RelayEventTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Publish_Subscribe
        ///
        /// <summary>
        /// Publish/Subscribe のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Publish_Subscribe()
        {
            var count = 0;
            var ev = new RelayEvent();
            ev.Subscribe(() => ++count);
            ev.Publish();
            ev.Publish();
            ev.Publish();
            Assert.That(count, Is.EqualTo(3));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Unsubscribe
        ///
        /// <summary>
        /// Unsubscribe のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Unsubscribe()
        {
            var count = 0;
            var ev = new RelayEvent();
            var disposable = ev.Subscribe(() => count++);
            ev.Publish();
            ev.Publish();
            disposable.Dispose();
            disposable.Dispose(); // ignore
            ev.Publish();
            Assert.That(count, Is.EqualTo(2));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Publish_Subscribe
        ///
        /// <summary>
        /// Publish/Subscribe のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1)]
        [TestCase("pi")]
        [TestCase(true)]
        public void Publish_Subscribe<T>(T value)
        {
            var result = default(T);
            var ev = new RelayEvent<T>();
            ev.Subscribe(x => { result = x; });
            ev.Publish(value);
            Assert.That(result, Is.EqualTo(value));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Unsubscribe
        ///
        /// <summary>
        /// Unsubscribe のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(2)]
        public void Unsubscribe(int value)
        {
            var count = 0;
            var ev = new RelayEvent<int>();
            var disposable = ev.Subscribe(n => count += n);
            ev.Publish(value);
            ev.Publish(value);
            disposable.Dispose();
            disposable.Dispose(); // ignore
            ev.Publish(value);
            Assert.That(count, Is.EqualTo(value * 2));
        }
    }
}
