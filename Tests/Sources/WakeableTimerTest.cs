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
using System.Threading;
using System.Threading.Tasks;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// WakeableTimerTest
    ///
    /// <summary>
    /// Tests for the WakeableTimer class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class WakeableTimerTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Properties
        ///
        /// <summary>
        /// Confirms initial values of the WakeableTimer class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Properties() => Create(timer =>
        {
            Assert.That(timer.State, Is.EqualTo(TimerState.Stop));
            Assert.That(timer.Interval, Is.EqualTo(TimeSpan.FromSeconds(1)));
            Assert.That(timer.LastPublished.HasValue, Is.False);
        });

        /* ----------------------------------------------------------------- */
        ///
        /// Properties_Disposed
        ///
        /// <summary>
        /// Confirms properties after disposed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Properties_Disposed()
        {
            var src = new WakeableTimer();
            src.Start();
            Task.Delay(100).Wait();
            Assert.That(src.State, Is.EqualTo(TimerState.Run));
            src.Dispose();
            Assert.That(src.State, Is.EqualTo(TimerState.Stop));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Transition_State
        ///
        /// <summary>
        /// Confirms the transition of the State property.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Transition_State() => Create(src =>
        {
            Assert.That(src.State, Is.EqualTo(TimerState.Stop));
            src.Start();
            Assert.That(src.State, Is.EqualTo(TimerState.Run));
            src.Start(); // ignore
            Assert.That(src.State, Is.EqualTo(TimerState.Run));
            src.Suspend();
            Assert.That(src.State, Is.EqualTo(TimerState.Suspend));
            src.Suspend();
            Assert.That(src.State, Is.EqualTo(TimerState.Suspend));
            src.Start();
            Assert.That(src.State, Is.EqualTo(TimerState.Run));
            src.Stop();
            Assert.That(src.State, Is.EqualTo(TimerState.Stop));
            src.Stop(); // ignore
            Assert.That(src.State, Is.EqualTo(TimerState.Stop));
        });

        /* ----------------------------------------------------------------- */
        ///
        /// Transition_PowerMode
        ///
        /// <summary>
        /// Confirms the transition of Power.Mode and State properties.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Transition_PowerMode() => Create(src =>
        {
            var pmc   = new PowerModeContext(Power.Mode);
            var count = 0;
            var dummy = 0;

            Power.Configure(pmc);

            src.PowerModeChanged += (s, e) => ++count;
            src.Subscribe(() => ++dummy);
            src.Start();

            pmc.Mode = PowerModes.Suspend;
            Assert.That(Power.Mode, Is.EqualTo(PowerModes.Suspend));
            Assert.That(src.State,  Is.EqualTo(TimerState.Suspend));

            pmc.Mode = PowerModes.Resume;
            Assert.That(Power.Mode, Is.EqualTo(PowerModes.Resume));
            Assert.That(src.State,  Is.EqualTo(TimerState.Run));

            src.Stop();
            Assert.That(src.State,  Is.EqualTo(TimerState.Stop));
            Assert.That(Power.Mode, Is.EqualTo(PowerModes.Resume));
        });

        /* ----------------------------------------------------------------- */
        ///
        /// Start
        ///
        /// <summary>
        /// Executes the test of the normal scenario.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public Task Start() => CreateAsync(async (src) =>
        {
            src.Interval = TimeSpan.FromMilliseconds(100);
            src.Interval = TimeSpan.FromMilliseconds(100); // ignore
            Assert.That(src.LastPublished.HasValue, Is.False);
            Assert.That(await InvokeAsync(src, 0, 1), "Timeout");

            var time = src.LastPublished;
            Assert.That(time, Is.Not.EqualTo(DateTime.MinValue));

            src.Reset();
            Assert.That(src.LastPublished, Is.EqualTo(time));
            Assert.That(src.Interval.TotalMilliseconds, Is.EqualTo(100).Within(1.0));
        });

        /* ----------------------------------------------------------------- */
        ///
        /// Start_InitialDelay
        ///
        /// <summary>
        /// Confirms the behavior when the initial delay is set.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public Task Start_InitialDelay() => CreateAsync(async (src) =>
        {
            src.Interval = TimeSpan.FromHours(1);
            Assert.That(src.LastPublished.HasValue, Is.False);
            Assert.That(await InvokeAsync(src, 200, 1), "Timeout");
            Assert.That(src.LastPublished, Is.Not.EqualTo(DateTime.MinValue));
        });

        /* ----------------------------------------------------------------- */
        ///
        /// Start_Burstly
        ///
        /// <summary>
        /// Confirms the behavior when the Interval is set to the very
        /// short time.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public Task Start_Burstly() => CreateAsync(async (src) =>
        {
            var cts   = new CancellationTokenSource();
            var count = 0;

            src.Interval = TimeSpan.FromMilliseconds(10);
            src.SubscribeAsync(async () =>
            {
                ++count;
                await Task.Delay(200).ConfigureAwait(false);
                src.Stop();
                cts.Cancel();
            });

            Assert.That(await InvokeAsync(src, 0, cts), "Timeout");
            Assert.That(count, Is.EqualTo(1));
        });

        /* ----------------------------------------------------------------- */
        ///
        /// Resume
        ///
        /// <summary>
        /// Executes the test of Suspend/Resume commands.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public Task Resume() => CreateAsync(async (src) =>
        {
            var cts   = new CancellationTokenSource();
            var count = 0;

            src.Interval = TimeSpan.FromSeconds(1);
            src.Start(TimeSpan.FromMilliseconds(100));
            src.Subscribe(() =>
            {
                ++count;
                src.Stop();
                cts.Cancel();
            });
            src.Start();
            src.Suspend();

            Assert.That(count, Is.EqualTo(0));
            await Task.Delay(300).ConfigureAwait(false);
            Assert.That(count, Is.EqualTo(0));
            Assert.That(await InvokeAsync(src, 0, cts), "Timeout");
            Assert.That(count, Is.EqualTo(1));
        });

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Creates a new instance of the WakeableTimer and executes
        /// the specified action.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Create(Action<WakeableTimer> action)
        {
            using (var src = new WakeableTimer()) action(src);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateAsync
        ///
        /// <summary>
        /// Creates a new instance of the WakeableTimer and executes
        /// the specified action as an asynchronous operation.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private async Task CreateAsync(Func<WakeableTimer, Task> func)
        {
            using (var src = new WakeableTimer()) await func(src).ConfigureAwait(false);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WaitAsync
        ///
        /// <summary>
        /// Waits until the Cancel event is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private async Task<bool> WaitAsync(CancellationTokenSource cts)
        {
            try { await Task.Delay(3000, cts.Token).ConfigureAwait(false); }
            catch (TaskCanceledException) { return true; }
            return false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// InvokeAsync
        ///
        /// <summary>
        /// Waits for the timer to execue the specified number of callbacks.
        /// </summary>
        ///
        /// <param name="src">Timer object.</param>
        /// <param name="msec">Initial delay.</param>
        /// <param name="cts">Cancellation token.</param>
        ///
        /// <returns>true for success.</returns>
        ///
        /* ----------------------------------------------------------------- */
        private Task<bool> InvokeAsync(WakeableTimer src, int msec, CancellationTokenSource cts)
        {
            if (msec <= 0) src.Start();
            else src.Start(TimeSpan.FromMilliseconds(msec));
            return WaitAsync(cts);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// InvokeAsync
        ///
        /// <summary>
        /// Waits for the timer to execue the specified number of callbacks.
        /// </summary>
        ///
        /// <param name="src">Timer object.</param>
        /// <param name="msec">Initial delay.</param>
        /// <param name="count">
        /// Number of callbacks that the timer waits.
        /// </param>
        ///
        /// <returns>true for success.</returns>
        ///
        /* ----------------------------------------------------------------- */
        private Task<bool> InvokeAsync(WakeableTimer src, int msec, int count)
        {
            var n   = 0;
            var cts = new CancellationTokenSource();

            src.Subscribe(() =>
            {
                if (++n >= count)
                {
                    src.Stop();
                    cts.Cancel();
                }
            });

            return InvokeAsync(src, msec, cts);
        }

        #endregion
    }
}
