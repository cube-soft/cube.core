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
using System.Threading.Tasks;

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
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Timer_Properties
        ///
        /// <summary>
        /// 各種プロパティの初期値を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Timer_Properties()
        {
            using (var timer = new WakeableTimer())
            {
                Assert.That(timer.Interval,      Is.EqualTo(TimeSpan.FromSeconds(1)));
                Assert.That(timer.LastPublished, Is.Null);
                Assert.That(timer.State,         Is.EqualTo(TimerState.Stop));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Start
        ///
        /// <summary>
        /// タイマーを開始するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Start()
        {
            var count = 0;
            using (var timer = new WakeableTimer())
            {
                var disposable = timer.Subscribe(() => ++count);
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Start();
                TaskEx.Delay(300).Wait();
                timer.Stop();
                disposable.Dispose();
            }
            Assert.That(count, Is.AtLeast(2));
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
                TaskEx.Delay(100).Wait();
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
                timer.Subscribe(() => ++count);
                timer.Interval = TimeSpan.FromMilliseconds(200);
                timer.Start(TimeSpan.Zero);
                TaskEx.Delay(100).Wait();
                timer.Stop();
            }
            Assert.That(count, Is.AtLeast(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Start_ShortInterval
        ///
        /// <summary>
        /// Interval に非常に短い時間を設定した時に、バースト的にイベントが
        /// 発生していない事を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Start_ShortInterval()
        {
            var count = 0;
            using (var timer = new WakeableTimer())
            {
                timer.Interval = TimeSpan.FromMilliseconds(10);
                timer.Subscribe(() => ++count);
                timer.Start();
                TaskEx.Delay(100).Wait();
            }
            Assert.That(count, Is.AtMost(12));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Resume_Immediately
        ///
        /// <summary>
        /// Suspend からの復帰後に即実行されるケースのテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Resume_Immediately()
        {
            var count = 0;
            using (var timer = new WakeableTimer())
            {
                timer.Subscribe(() => ++count);
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Start(TimeSpan.FromMilliseconds(50));
                timer.Suspend();
                TaskEx.Delay(100).Wait();
                timer.Start();
                TaskEx.Delay(100).Wait();
                timer.Stop();
            }
            Assert.That(count, Is.AtLeast(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// State_Scenario
        ///
        /// <summary>
        /// WakeableTimer の State の内容を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void State_Scenario()
        {
            using (var timer = new WakeableTimer())
            {
                Assert.That(timer.State, Is.EqualTo(TimerState.Stop));
                timer.Start();
                Assert.That(timer.State, Is.EqualTo(TimerState.Run));
                timer.Start();
                Assert.That(timer.State, Is.EqualTo(TimerState.Run));
                timer.Suspend();
                Assert.That(timer.State, Is.EqualTo(TimerState.Suspend));
                timer.Suspend();
                Assert.That(timer.State, Is.EqualTo(TimerState.Suspend));
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
        public void Reset()
        {
            using (var timer = new WakeableTimer())
            {
                var ms = 100;

                timer.Interval = TimeSpan.FromMilliseconds(ms);
                timer.Interval = TimeSpan.FromMilliseconds(ms); // ignore
                timer.Start();
                TaskEx.Delay(ms * 2).Wait();
                timer.Stop();

                var last = timer.LastPublished;
                Assert.That(last, Is.Not.EqualTo(DateTime.MinValue));

                timer.Reset();
                Assert.That(timer.LastPublished, Is.EqualTo(last));
                Assert.That(timer.Interval.TotalMilliseconds, Is.EqualTo(ms).Within(1.0));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Subscribe されたオブジェクトの実行中に Dispose された時の
        /// 挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Dispose()
        {
            var timer = new WakeableTimer();
            timer.Start();
            TaskEx.Delay(50).Wait();
            timer.Dispose();
            Assert.That(timer.State, Is.EqualTo(TimerState.Stop));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PowerMode_Scenario
        ///
        /// <summary>
        /// 電源状態の変化に伴う Suspend/Resume のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void PowerMode_Scenario()
        {
            using (var timer = new WakeableTimer())
            {
                var count   = 0;
                var chagned = 0;
                var power   = new PowerModeContext(Power.Mode);

                Power.Configure(power);

                timer.Interval = TimeSpan.FromMilliseconds(200);
                timer.PowerModeChanged += (s, e) => ++chagned;
                timer.Subscribe(() => ++count);
                timer.Start();

                TaskEx.Delay(100).Wait();
                power.Mode = PowerModes.Suspend;
                TaskEx.Delay(100).Wait();
                power.Mode = PowerModes.Resume;
                TaskEx.Delay(200).Wait();
                timer.Stop();

                Assert.That(Power.Mode, Is.EqualTo(PowerModes.Resume));
                Assert.That(chagned, Is.EqualTo(2), nameof(timer.PowerModeChanged));
            }
        }

        #endregion
    }
}
