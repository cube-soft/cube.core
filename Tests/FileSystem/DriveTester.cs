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
    public class DriveTester
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestGet
        /// 
        /// <summary>
        /// ドライブの情報を取得するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("C:")]
        public void TestGet(string letter)
        {
            Assert.DoesNotThrow(() =>
            {
                var drive = new Cube.FileSystem.Drive(letter);
                Assert.That(drive.Index, Is.EqualTo(0));
                Assert.That(drive.Letter, Is.EqualTo(letter));
                Assert.That(drive.Type, Is.EqualTo(System.IO.DriveType.Fixed));
                Assert.That(drive.Format, Is.EqualTo("NTFS"));
                Assert.That(drive.Model, Is.Not.Null.Or.Empty);
                Assert.That(drive.MediaType, Is.EqualTo(Cube.FileSystem.MediaType.HardDisk));
                Assert.That(drive.InterfaceType, Is.Not.Null.Or.Empty);
                Assert.That(drive.Size, Is.AtLeast(1024 * 1024 * 1024));
                Assert.That(drive.FreeSpace, Is.AtLeast(1));
            });
        }

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
            });
        }
    }
}
