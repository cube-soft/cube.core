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
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// PresentableTest
    ///
    /// <summary>
    /// Tests the PresentableBase and related classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class PresentableTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Create_ArgumentNullException
        ///
        /// <summary>
        /// Tests to create a new instance of the PresentableBase inherited
        /// class when the SynchronizationContext.Current is null.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_ArgumentNullException()
        {
            Assert.That(SynchronizationContext.Current, Is.Null);
            Assert.That(() => new Presenter(), Throws.ArgumentNullException);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Tests the Subscribe and Send methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Send()
        {
            var n = 0;
            void action(int i) => ++n;

            using (var src = new Presenter(new SynchronizationContext()))
            {
                using (src.Subscribe<int>(action))
                using (src.Subscribe<int>(action))
                {
                    5.Times(i => src.TestSend<int>());
                    Assert.That(n, Is.EqualTo(10));

                    5.Times(i => src.TestSend<long>());
                    Assert.That(n, Is.EqualTo(10));
                }

                5.Times(i => src.TestSend<int>());
                Assert.That(n, Is.EqualTo(10));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Post
        ///
        /// <summary>
        /// Tests the Subscribe and Post methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Post()
        {
            var cts = new CancellationTokenSource();
            using (var src = new Presenter(new SynchronizationContext()))
            using (src.Subscribe<int>(e => cts.Cancel()))
            {
                src.TestPost<int>();
                Assert.That(() => Wait(cts), Throws.TypeOf<AggregateException>());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Post
        ///
        /// <summary>
        /// Tests the Track method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Track()
        {
            var dest = default(ExceptionMessage);
            using (var src = new Presenter(new SynchronizationContext()))
            using (src.Subscribe<ExceptionMessage>(e => dest = e))
            {
                src.TestTrack(() => { /* OK */ }).Wait();
                src.TestTrack(() => throw new ArgumentException(nameof(Track))).Wait();
            }

            Assert.That(dest.Title, Is.EqualTo("Error"));
            Assert.That(dest.Text,  Does.StartWith(nameof(Track)));
            Assert.That(dest.Value, Is.TypeOf<ArgumentException>());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PropertyChanged
        ///
        /// <summary>
        /// Tests the PropertyChanged event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void PropertyChanged()
        {
            var n   = 0;
            var cts = new CancellationTokenSource();
            using (var src = new Presenter(new SynchronizationContext()))
            {
                src.Refresh(nameof(PropertyChanged));
                Assert.That(n, Is.EqualTo(0));

                src.PropertyChanged += (s, e) => { ++n; cts.Cancel(); };
                src.Refresh(nameof(PropertyChanged));
                Assert.That(() => Wait(cts), Throws.TypeOf<AggregateException>());
                Assert.That(n, Is.EqualTo(1));
            }
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Wait
        ///
        /// <summary>
        /// Waits for the test result.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Wait(CancellationTokenSource cts) => TaskEx.Delay(10000, cts.Token).Wait();

        /* ----------------------------------------------------------------- */
        ///
        /// Presenter
        ///
        /// <summary>
        /// Inherits the PresentableBase class simply.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private class Presenter : PresentableBase
        {
            public Presenter() : base() { }
            public Presenter(SynchronizationContext ctx) : base(new Aggregator(), ctx) { }
            public void TestSend<T>() where T : new() => Send<T>();
            public void TestPost<T>() where T : new() => Post<T>();
            public Task TestTrack(Action e) => Track(e);
            public void Refresh(string name) => OnPropertyChanged(new PropertyChangedEventArgs(name));
            protected override void Dispose(bool disposing) { }
        }

        #endregion
    }
}
