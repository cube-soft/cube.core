/* ------------------------------------------------------------------------- */
///
/// DriveTester.cs
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

namespace Cube.Tests.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Tests.DriveTester
    /// 
    /// <summary>
    /// Drive クラスのテストを行うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class DriveTester
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestGetDrives
        /// 
        /// <summary>
        /// ドライブ一覧を取得するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestGetDrives()
        {
            Assert.DoesNotThrow(() =>
            {
                var drives = Cube.FileSystem.Drive.GetDrives();
                Assert.That(drives.Length, Is.AtLeast(1));
                Assert.That(drives[0].Index, Is.EqualTo(0));
                Assert.That(drives[0].Letter, Is.EqualTo("C:"));
                Assert.That(drives[0].Type, Is.EqualTo(System.IO.DriveType.Fixed));
                Assert.That(drives[0].Format, Is.EqualTo("NTFS"));
                Assert.That(drives[0].Model, Is.Not.Null.Or.Empty);
                Assert.That(drives[0].MediaType, Is.Not.Null.Or.Empty);
                Assert.That(drives[0].InterfaceType, Is.Not.Null.Or.Empty);
                Assert.That(drives[0].Size, Is.AtLeast(1024 * 1024 * 1024));
                Assert.That(drives[0].FreeSpace, Is.AtLeast(1));
            });
        }
    }
}
