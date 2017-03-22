/* ------------------------------------------------------------------------- */
///
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;
using Microsoft.Win32;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// SchedulerTest
    /// 
    /// <summary>
    /// Scheduler のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Parallelizable]
    [TestFixture]
    class SchedulerTest
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
            using (var sch = new Scheduler())
            {
                Assert.That(sch.Interval,     Is.EqualTo(TimeSpan.FromSeconds(1)));
                Assert.That(sch.InitialDelay, Is.EqualTo(TimeSpan.Zero));
                Assert.That(sch.LastExecuted, Is.EqualTo(DateTime.MinValue));
                Assert.That(sch.PowerMode,    Is.EqualTo(PowerModes.Resume));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Start_Stop_State
        /// 
        /// <summary>
        /// Scheduler の State の内容を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Start_Stop_State()
        {
            using (var sch = new Scheduler())
            {
                Assert.That(sch.State, Is.EqualTo(SchedulerState.Stop));
                sch.Start();
                Assert.That(sch.State, Is.EqualTo(SchedulerState.Run));
                sch.Restart();
                Assert.That(sch.State, Is.EqualTo(SchedulerState.Run));
                sch.Stop();
                Assert.That(sch.State, Is.EqualTo(SchedulerState.Stop));
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
            using (var sch = new Scheduler())
            {
                var interval = TimeSpan.FromMilliseconds(100);
                sch.Interval = interval;
                sch.InitialDelay = TimeSpan.Zero;
                sch.Start();
                sch.Stop();
                Assert.That(sch.LastExecuted, Is.Not.EqualTo(DateTime.MinValue));

                sch.Reset();
                Assert.That(sch.LastExecuted, Is.EqualTo(DateTime.MinValue));
                Assert.That(sch.Interval, Is.EqualTo(interval));
            }
        }
        /* ----------------------------------------------------------------- */
        ///
        /// Run_InitialDelay
        /// 
        /// <summary>
        /// InitialDelay を設定するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Run_InitialDelay()
        {
            var count = 0;

            using (var sch = new Scheduler())
            {
                sch.InitialDelay = TimeSpan.FromSeconds(1);
                sch.Execute += (s, e) => ++count;
                sch.Start();
                sch.Stop();
            }

            Assert.That(count, Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Run_Immediately
        /// 
        /// <summary>
        /// InitialDelay にゼロを設定したテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Run_Immediately()
        {
            var count = 0;

            using (var sch = new Scheduler())
            {
                sch.InitialDelay = TimeSpan.Zero;
                sch.Execute += (s, e) => ++count;
                sch.Start();
                sch.Stop();
            }

            Assert.That(count, Is.EqualTo(1));
        }
    }
}
