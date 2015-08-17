/* ------------------------------------------------------------------------- */
///
/// ScheduleTester.cs
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
using System.Runtime.Serialization;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Tests.ScheduleTester
    /// 
    /// <summary>
    /// Scheduler クラスをテストするためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class ScheduleTester
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestExecute
        /// 
        /// <summary>
        /// Execute イベントのテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestExecute()
        {
            Assert.DoesNotThrow(() =>
            {
                var scheduler = new Cube.Scheduler();
                scheduler.Interval = TimeSpan.FromMilliseconds(10);

                var result = 0;
                scheduler.Execute += (s, e) => { ++result; };

                Assert.That(result, Is.EqualTo(0));
                scheduler.Start();
                System.Threading.Thread.Sleep(30);
                scheduler.Stop();
                Assert.That(result, Is.AtLeast(1));
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestState
        /// 
        /// <summary>
        /// Start/Suspend/Resume/Stop のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestState()
        {
            Assert.DoesNotThrow(() =>
            {
                var scheduler = new Cube.Scheduler();

                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));

                scheduler.Start();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Run));

                scheduler.Suspend();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Suspend));

                scheduler.Resume();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Run));

                scheduler.Stop();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestSuspendToStop
        /// 
        /// <summary>
        /// Suspend から直接 Stop した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestSuspendToStop()
        {
            Assert.DoesNotThrow(() =>
            {
                var scheduler = new Cube.Scheduler();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));

                scheduler.Start();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Run));

                scheduler.Suspend();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Suspend));

                scheduler.Stop();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestResumeBeforeStart
        /// 
        /// <summary>
        /// Start 前に Resume を実行した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestResumeBeforeStart()
        {
            Assert.DoesNotThrow(() =>
            {
                var scheduler = new Cube.Scheduler();

                scheduler.Resume();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestResumeAfterStop
        /// 
        /// <summary>
        /// Stop 後に Resume を実行した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestResumeAfterStop()
        {
            Assert.DoesNotThrow(() =>
            {
                var scheduler = new Cube.Scheduler();

                scheduler.Start();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Run));

                scheduler.Stop();
                scheduler.Resume();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestSuspendBeforeStart
        /// 
        /// <summary>
        /// Start 前に Suspend を実行した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestSuspendBeforeStart()
        {
            Assert.DoesNotThrow(() =>
            {
                var scheduler = new Cube.Scheduler();

                scheduler.Suspend();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestSuspendAfterStop
        /// 
        /// <summary>
        /// Stop 後に Suspend を実行した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestSuspendAfterStop()
        {
            Assert.DoesNotThrow(() =>
            {
                var scheduler = new Cube.Scheduler();

                scheduler.Start();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Run));

                scheduler.Stop();
                scheduler.Suspend();
                Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));
            });
        }
    }
}
