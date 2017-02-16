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
        #region Tests for Drive properties

        [TestCase(1)]
        public void Drives_Length(int atleast)
            => Assert.That(Drives.Length, Is.AtLeast(atleast));

        [TestCase(0, ExpectedResult = 0)]
        public uint Drive_Index(int index)
            => Drives[index].Index;

        [TestCase(0, ExpectedResult = "C:")]
        public string Drive_Letter(int index)
            => Drives[index].Letter;

        [TestCase(0, ExpectedResult = DriveType.HardDisk)]
        public DriveType Drive_Type(int index)
            => Drives[index].Type;

        [TestCase(0, ExpectedResult = "NTFS")]
        public string Drive_Format(int index)
            => Drives[index].Format;

        [TestCase(0, ExpectedResult = "IDE")]
        public string Drive_Interface(int index)
            => Drives[index].Interface;

        [TestCase(0, 100000000UL)]
        public void Drive_Size(int index, ulong atleast)
            => Assert.That(Drives[index].Size, Is.AtLeast(atleast));

        [TestCase(0)]
        public void Drive_FreeSpace_IsLessThanSize(int index)
            => Assert.That(Drives[index].FreeSpace, Is.LessThan(Drives[index].Size));

        [TestCase(0)]
        public void Drive_VolumeLabel_IsNotNullOrEmpty(int index)
            => Assert.That(Drives[index].VolumeLabel, Is.Not.Null.Or.Empty);

        [TestCase(0)]
        public void Drive_Model_IsNotNullOrEmpty(int index)
            => Assert.That(Drives[index].Model, Is.Not.Null.Or.Empty);

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
        private Drive[] Drives { get; set; }

        #endregion
    }
}
