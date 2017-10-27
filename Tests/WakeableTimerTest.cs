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
using System;
using System.Threading.Tasks;
using Microsoft.Win32;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// WakeableTimerTest
    /// 
    /// <summary>
    /// WakeableTimer のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class WakeableTimerTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Properties_Default
        /// 
        /// <summary>
        /// 各種プロパティの初期値を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Properties_Default()
        {
            using (var timer = new WakeableTimer())
            {
                Assert.That(timer.Interval,     Is.EqualTo(TimeSpan.FromSeconds(1)));
                Assert.That(timer.LastExecuted, Is.EqualTo(DateTime.MinValue));
                Assert.That(timer.PowerMode,    Is.EqualTo(PowerModes.Resume));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Start_InitialDelay
        /// 
        /// <summary>
        /// InitialDelay を設定するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Start_InitialDelay()
        {
            var count = 0;

            using (var timer = new WakeableTimer())
            {
                timer.Subscribe(() => ++count);
                timer.Start(TimeSpan.FromSeconds(1));
                timer.Stop();
            }

            Assert.That(count, Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Start_Immediately
        /// 
        /// <summary>
        /// InitialDelay にゼロを設定したテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Start_Immediately()
        {
            var count = 0;

            using (var timer = new WakeableTimer())
            {
                var disposable = timer.Subscribe(() => ++count);
                timer.Start(TimeSpan.Zero);
                timer.Stop();
                disposable.Dispose();
                timer.Start(TimeSpan.Zero);
                timer.Stop();
            }

            Assert.That(count, Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Start_Stop_State
        /// 
        /// <summary>
        /// WakeableTimer の State の内容を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Start_Stop_State()
        {
            using (var timer = new WakeableTimer())
            {
                Assert.That(timer.State, Is.EqualTo(TimerState.Stop));
                timer.Start();
                Assert.That(timer.State, Is.EqualTo(TimerState.Run));
                timer.Start();
                Assert.That(timer.State, Is.EqualTo(TimerState.Run));
                timer.Stop();
                Assert.That(timer.State, Is.EqualTo(TimerState.Stop));
                timer.Stop();
                Assert.That(timer.State, Is.EqualTo(TimerState.Stop));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Reset
        /// 
        /// <summary>
        /// Reset のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public async Task Reset()
        {
            using (var timer = new WakeableTimer())
            {
                var ms = 50;

                timer.Interval = TimeSpan.FromMilliseconds(ms);
                timer.Interval = TimeSpan.FromMilliseconds(ms); // ignore
                timer.Start();
                await Task.Delay(ms * 2);
                timer.Stop();

                var last = timer.LastExecuted;
                Assert.That(last, Is.Not.EqualTo(DateTime.MinValue));

                timer.Reset();
                Assert.That(timer.LastExecuted, Is.EqualTo(last));
                Assert.That(timer.Interval.TotalMilliseconds, Is.EqualTo(ms).Within(1.0));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Resume
        /// 
        /// <summary>
        /// Suspend/Resume のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase( 0, 2)]
        [TestCase(50, 1)]
        public async Task Resume(int delay, int expected)
        {
            using (var timer = new WakeableTimer())
            {
                var ms      = Math.Max(delay, 100);
                var count   = 0;
                var chagned = 0;

                timer.Interval = TimeSpan.FromMilliseconds(ms);
                timer.Subscribe(() => ++count);
                timer.PowerModeChanged += (s, e) => ++chagned;
                timer.Start(TimeSpan.FromMilliseconds(delay));

                // force change
                Cube.Power.Mode = PowerModes.Suspend;
                await Task.Delay(ms * 2);
                Cube.Power.Mode = PowerModes.Resume;
                await Task.Delay(150);

                timer.Stop();

                Assert.That(timer.PowerMode, Is.EqualTo(PowerModes.Resume));
                Assert.That(chagned, Is.EqualTo(2));
                Assert.That(count, Is.EqualTo(expected));
            }
        }
    }
}
