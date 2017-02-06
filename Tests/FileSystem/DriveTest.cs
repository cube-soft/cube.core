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
using System.Linq;
using Cube.FileSystem;

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
    [Parallelizable]
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
        #region Drives

        [TestCase(1)]
        public void Drives_Length_IsAtLeast(int expected)
        {
            Assert.That(
                Drives.Length,
                Is.AtLeast(expected)
            );
        }

        [TestCase(0, 0)]
        public void Drives_Index(int index, int expected)
        {
            Assert.That(
                Drives[index].Index,
                Is.EqualTo(expected)
            );
        }

        [TestCase(0, "C:")]
        public void Drives_Letter(int index, string expected)
        {
            Assert.That(
                Drives[index].Letter,
                Is.EqualTo(expected)
            );
        }

        [TestCase(0, DriveType.HardDisk)]
        public void Drives_Type(int index, DriveType expected)
        {
            Assert.That(
                Drives[index].Type,
                Is.EqualTo(expected)
            );
        }

        [TestCase(0, "NTFS")]
        public void Drives_Format(int index, string expected)
        {
            Assert.That(
                Drives[index].Format,
                Is.EqualTo(expected)
            );
        }

        [TestCase(0, "IDE")]
        public void Drives_Interface(int index, string expected)
        {
            Assert.That(
                Drives[index].Interface,
                Is.EqualTo(expected)
            );
        }

        [TestCase(0)]
        public void Drives_VolumeLabel_IsNotNullOrEmpty(int index)
        {
            Assert.That(
                Drives[index].VolumeLabel,
                Is.Not.Null.Or.Empty
            );
        }

        [TestCase(0)]
        public void Drives_Model_IsNotNullOrEmpty(int index)
        {
            Assert.That(
                Drives[index].Model,
                Is.Not.Null.Or.Empty
            );
        }

        [TestCase(0, 100000000UL)]
        public void Drives_Size_IsAtLeast(int index, ulong expected)
        {
            Assert.That(
                Drives[index].Size,
                Is.AtLeast(expected)
            );
        }

        [TestCase(0)]
        public void Drives_FreeSpace_IsLessThanSize(int index)
        {
            Assert.That(
                Drives[index].FreeSpace,
                Is.LessThan(Drives[index].Size)
            );
        }

        #endregion

        #region Helper methods

        /* ----------------------------------------------------------------- */
        ///
        /// OneTimeSetup
        ///
        /// <summary>
        /// 初期化処理を一度だけ実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Drives = Drive.GetDrives().ToArray();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Drives
        ///
        /// <summary>
        /// ドライブ情報一覧を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Drive[] Drives { get; set; }

        #endregion
    }
}
