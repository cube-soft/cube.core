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
                    5.Times(i => src.SendMessage<int>());
                    Assert.That(n, Is.EqualTo(10));

                    5.Times(i => src.SendMessage<long>());
                    Assert.That(n, Is.EqualTo(10));
                }

                5.Times(i => src.SendMessage<int>());
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
                src.PostMessage<int>();
                Assert.That(() => Wait(cts), Throws.TypeOf<AggregateException>());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TrackAsync
        ///
        /// <summary>
        /// Tests the async Track method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TrackAsync()
        {
            var dest = default(DialogMessage);
            using (var src = new Presenter(new SynchronizationContext()))
            using (src.Subscribe<DialogMessage>(e => dest = e))
            {
                src.TrackAsync(() => { /* OK */ }).Wait();
                src.TrackAsync(() => throw new ArgumentException(nameof(TrackAsync))).Wait();
            }

            Assert.That(dest.Value,   Does.StartWith(nameof(TrackAsync)));
            Assert.That(dest.Title,   Is.EqualTo("Error"));
            Assert.That(dest.Icon,    Is.EqualTo(DialogIcon.Error));
            Assert.That(dest.Buttons, Is.EqualTo(DialogButtons.Ok));
            Assert.That(dest.Status,  Is.EqualTo(DialogStatus.Ok));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TrackSync
        ///
        /// <summary>
        /// Tests the sync Track method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TrackSync()
        {
            var dest = default(DialogMessage);
            using (var src = new Presenter(new SynchronizationContext()))
            using (src.Subscribe<DialogMessage>(e => dest = e))
            {
                src.TrackSync(() => { /* OK */ });
                src.TrackSync(() => throw new ArgumentException(nameof(TrackSync)));
            }

            Assert.That(dest.Value,   Does.StartWith(nameof(TrackSync)));
            Assert.That(dest.Title,   Is.EqualTo("Error"));
            Assert.That(dest.Icon,    Is.EqualTo(DialogIcon.Error));
            Assert.That(dest.Buttons, Is.EqualTo(DialogButtons.Ok));
            Assert.That(dest.Status,  Is.EqualTo(DialogStatus.Ok));
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
                5.Times(i => src.TestValue = nameof(PropertyChanged));
                Assert.That(src.TestValue, Is.EqualTo(nameof(PropertyChanged)));
                Assert.That(n, Is.EqualTo(0));

                src.TestValue = string.Empty;
                src.PropertyChanged += (s, e) => { ++n; cts.Cancel(); };
                5.Times(i => src.TestValue = nameof(PropertyChanged));
                Assert.That(() => Wait(cts), Throws.TypeOf<AggregateException>());
                Assert.That(src.TestValue, Is.EqualTo(nameof(PropertyChanged)));
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
            public void SendMessage<T>() where T : new() => Send<T>();
            public void PostMessage<T>() where T : new() => Post<T>();
            public void TrackSync(Action e) => Track(e, DialogMessage.Create, true);
            public Task TrackAsync(Action e) => Track(e);
            protected override void Dispose(bool disposing) { }
            public string TestValue
            {
                get => _test;
                set => SetProperty(ref _test, value);
            }
            private string _test;
        }

        #endregion
    }
}
