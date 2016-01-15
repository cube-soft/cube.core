/* ------------------------------------------------------------------------- */
///
/// DriveTest.cs
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
    /// DriveTest
    /// 
    /// <summary>
    /// Drive のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class DriveTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Drives
        ///
        /// <summary>
        /// ドライブ情報を取得するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetDrives()
        {
            var drives = Cube.FileSystem.Drive.GetDrives();
            Assert.That(drives.Length, Is.AtLeast(1));

            var drive = drives[0];
            Assert.That(drive.Index,       Is.EqualTo(0));
            Assert.That(drive.Letter,      Is.EqualTo("C:"));
            Assert.That(drive.VolumeLabel, Is.Not.Null.Or.Empty);
            Assert.That(drive.Type,        Is.EqualTo(Cube.FileSystem.DriveType.HardDisk));
            Assert.That(drive.Format,      Is.Not.Null.Or.Empty);
            Assert.That(drive.Model,       Is.Not.Null.Or.Empty);
            Assert.That(drive.Interface,   Is.EqualTo("IDE"));
            Assert.That(drive.Size,        Is.AtLeast(100000000UL));
            Assert.That(drive.FreeSpace,   Is.LessThan(drive.Size));
        }
    }
}
