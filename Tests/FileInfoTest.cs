/* ------------------------------------------------------------------------- */
///
/// FileInfoTest.cs
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
using System.Drawing;
using System.Reflection;
using NUnit.Framework;
using Cube.FileSystem;
using IoEx = System.IO;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileInfoTest
    /// 
    /// <summary>
    /// FileInfo のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Parallelizable]
    [TestFixture]
    class FileInfoTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Properties
        ///
        /// <summary>
        /// 各種プロパティのテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        #region Properties

        [Test]
        public void Exists_Dummy_IsFalse()
        {
            var dummy = new FileInfo("dummy-filename");
            Assert.That(
                dummy.Exists,
                Is.False
            );
        }

        [Test]
        public void Exists_Assembly_IsTrue()
        {
            Assert.That(
                FileInfo.Exists,
                Is.True
            );
        }

        [Test]
        public void FullName_Assembly()
        {
            Assert.That(
                FileInfo.FullName,
                Is.EqualTo(Assembly.GetExecutingAssembly().Location)
            );
        }

        [Test]
        public void Name_Assembly()
        {
            Assert.That(
                FileInfo.Name,
                Is.EqualTo(IoEx.Path.GetFileName(Assembly.GetExecutingAssembly().Location))
            );
        }

        [Test]
        public void Extension_Assembly()
        {
            Assert.That(
                FileInfo.Extension,
                Is.EqualTo(IoEx.Path.GetExtension(Assembly.GetExecutingAssembly().Location))
            );
        }

        [Test]
        public void DirectoryName_Assembly()
        {
            Assert.That(
                FileInfo.DirectoryName,
                Is.EqualTo(IoEx.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            );
        }

        [TestCase(1000)]
        public void Length_Assembly_IsAtLeast(int expected)
        {
            Assert.That(
                FileInfo.Length,
                Is.AtLeast(expected)
            );
        }

        [Test]
        public void IsReadOnly_Assembly_IsFalse()
        {
            Assert.That(
                FileInfo.IsReadOnly,
                Is.False
            );
        }

        [Test]
        public void CreationTime_Assembly_IsAtMostNow()
        {
            Assert.That(
                FileInfo.CreationTime,
                Is.AtMost(DateTime.Now)
            );
        }

        [Test]
        public void LastWriteTime_Assembly_IsAtMostNow()
        {
            Assert.That(
                FileInfo.LastWriteTime,
                Is.AtMost(DateTime.Now)
            );
        }

        [Test]
        public void LastAccessTime_Assembly_IsAtMostNow()
        {
            Assert.That(
                FileInfo.LastAccessTime,
                Is.AtMost(DateTime.Now)
            );
        }

        [Test]
        public void TypeName_Assembly_IsNotNullOrEmpty()
        {
            Assert.That(
                FileInfo.TypeName,
                Is.Not.Null.Or.Empty
            );
        }

        [TestCase(16, 16)]
        public void Icon_Assembly(int width, int height)
        {
            Assert.That(
                FileInfo.Icon.Size,
                Is.EqualTo(new Size(width, height))
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
            FileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FileInfo
        ///
        /// <summary>
        /// ファイル情報を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FileInfo FileInfo { get; set; }

        #endregion
    }
}
