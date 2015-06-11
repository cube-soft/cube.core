/* ------------------------------------------------------------------------- */
///
/// DeviceTester.cs
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
using NUnit.Framework;

namespace Cube.Tests.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Tests.DeviceTester
    /// 
    /// <summary>
    /// Drive クラスのテストを行うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class DeviceTester
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestProperties
        /// 
        /// <summary>
        /// デバイスに関するプロパティを取得するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestProperties()
        {
            Assert.DoesNotThrow(() =>
            {
                var drives = Cube.FileSystem.Drive.GetDrives();
                Assert.That(drives.Length, Is.AtLeast(1));

                var device = new Cube.FileSystem.Device(drives[0]);
                Assert.That(device.Index, Is.EqualTo(0));
                Assert.That(device.Path, Is.Not.Null.Or.Empty);
                Assert.That(device.Handle, Is.AtLeast(1));
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestDetach
        /// 
        /// <summary>
        /// デバイスを取り外すテストを行います。
        /// </summary>
        /// 
        /// <remarks>
        /// C: ドライブを取り外そうとして失敗するテストを行います。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestDetach()
        {
            Assert.Throws<Cube.FileSystem.VetoException>(() =>
            {
                var drives = Cube.FileSystem.Drive.GetDrives();
                Assert.That(drives.Length, Is.AtLeast(1));

                var device = new Cube.FileSystem.Device(drives[0]);
                device.Detach();
            });
        }
    }
}
