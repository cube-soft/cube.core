/* ------------------------------------------------------------------------- */
///
/// SchedulerTest.cs
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
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// SchedulerTest
    /// 
    /// <summary>
    /// Scheduler クラスをテストするためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class SchedulerTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// StateScenario
        /// 
        /// <summary>
        /// Start/Suspend/Resume/Stop のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void StateScenario()
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
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SuspendToStop
        /// 
        /// <summary>
        /// Suspend から直接 Stop した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SuspendToStop()
        {
            var scheduler = new Cube.Scheduler();
            Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));

            scheduler.Start();
            Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Run));

            scheduler.Suspend();
            Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Suspend));

            scheduler.Stop();
            Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ResumeBeforeStart
        /// 
        /// <summary>
        /// Start 前に Resume を実行した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void ResumeBeforeStart()
        {
            var scheduler = new Cube.Scheduler();

            scheduler.Resume();
            Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ResumeAfterStop
        /// 
        /// <summary>
        /// Stop 後に Resume を実行した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void ResumeAfterStop()
        {
            var scheduler = new Cube.Scheduler();

            scheduler.Start();
            Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Run));

            scheduler.Stop();
            scheduler.Resume();
            Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SuspendBeforeStart
        /// 
        /// <summary>
        /// Start 前に Suspend を実行した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SuspendBeforeStart()
        {
            var scheduler = new Cube.Scheduler();

            scheduler.Suspend();
            Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SuspendAfterStop
        /// 
        /// <summary>
        /// Stop 後に Suspend を実行した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void SuspendAfterStop()
        {
            var scheduler = new Cube.Scheduler();

            scheduler.Start();
            Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Run));

            scheduler.Stop();
            scheduler.Suspend();
            Assert.That(scheduler.State, Is.EqualTo(Cube.SchedulerState.Stop));
        }
    }
}
