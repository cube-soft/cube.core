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
using Cube.Mixin.Syntax;
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
        /// Create_Throws
        ///
        /// <summary>
        /// Tests to create a new instance of the PresentableBase inherited
        /// class when the SynchronizationContext.Current is null.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_Throws()
        {
            Assert.That(SynchronizationContext.Current, Is.Null);
            Assert.That(() => new Presenter(), Throws.ArgumentNullException);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Tests the Subscribe and Send methods.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Subscribe()
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
        /// Send
        ///
        /// <summary>
        /// Tests the Send method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(DialogStatus.Ok,     ExpectedResult = 5)]
        [TestCase(DialogStatus.Yes,    ExpectedResult = 5)]
        [TestCase(DialogStatus.Cancel, ExpectedResult = 0)]
        public int Send(DialogStatus value)
        {
            var n = 0;
            using (var src = new Presenter(new SynchronizationContext()))
            using (src.Subscribe<DialogMessage>(e => e.Value = value))
            {
                5.Times(i => src.SendMessage(new DialogMessage(),
                    e => n++,
                    e => e.Any(DialogStatus.Ok, DialogStatus.Yes)
                ).Wait());
            }
            return n;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Send
        ///
        /// <summary>
        /// Tests the Send method with CancelMessage objects.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(true, ExpectedResult = 0)]
        [TestCase(false, ExpectedResult = 5)]
        public int Send_CancelMessage(bool cancel)
        {
            var n = 0;
            using (var src = new Presenter(new SynchronizationContext()))
            using (src.Subscribe<OpenFileMessage>(e => e.Cancel = cancel))
            {
                5.Times(i => src.SendMessage(new OpenFileMessage(), e => n++).Wait());
            }
            return n;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Send_Throws
        ///
        /// <summary>
        /// Tests the Send method with a null object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Send_Throws()
        {
            using (var src = new Presenter(new SynchronizationContext()))
            {
                Assert.That(() => src.SendMessage(default(object)), Throws.ArgumentNullException);
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

            Assert.That(dest.Text,    Does.StartWith(nameof(TrackAsync)));
            Assert.That(dest.Title,   Is.EqualTo("Error"));
            Assert.That(dest.Icon,    Is.EqualTo(DialogIcon.Error));
            Assert.That(dest.Buttons, Is.EqualTo(DialogButtons.Ok));
            Assert.That(dest.Value,   Is.EqualTo(DialogStatus.Ok));
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

            Assert.That(dest.Text,    Does.StartWith(nameof(TrackSync)));
            Assert.That(dest.Title,   Is.EqualTo("Error"));
            Assert.That(dest.Icon,    Is.EqualTo(DialogIcon.Error));
            Assert.That(dest.Buttons, Is.EqualTo(DialogButtons.Ok));
            Assert.That(dest.Value,   Is.EqualTo(DialogStatus.Ok));
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

        /* ----------------------------------------------------------------- */
        ///
        /// GetDispatcher
        ///
        /// <summary>
        /// Tests the GetDispatcher method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetDispatcher()
        {
            using (var src = new Presenter(new SynchronizationContext()))
            {
                Assert.That(src.GetDispatcher(), Is.Not.Null);
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
        private void Wait(CancellationTokenSource cts) => Task.Delay(10000, cts.Token).Wait();

        /* ----------------------------------------------------------------- */
        ///
        /// Presenter
        ///
        /// <summary>
        /// Inherits the PresentableBase class simply.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private class Presenter : Presentable<Person>
        {
            public Presenter() : base(new Person()) { }
            public Presenter(SynchronizationContext ctx) : base(new Person(), new Aggregator(), ctx) { }
            public void PostMessage<T>() where T : new() => Post<T>();
            public void SendMessage<T>() where T : new() => Send<T>();
            public void SendMessage<T>(T m) => Send(m);
            public Task SendMessage<T>(Message<T> m, Action<T> e, Func<T, bool> f) => Send(m, e, f);
            public Task SendMessage<T>(CancelMessage<T> m, Action<T> e) => Send(m, e);
            public void TrackSync(Action e) => Track(e, DialogMessage.Create, true);
            public Task TrackAsync(Action e) => Track(e);
            public IDispatcher GetDispatcher() => GetDispatcher(false);
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
