/* ------------------------------------------------------------------------- */
///
/// IconFactoryTest.cs
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
using System.Drawing;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// IconFactoryTest
    /// 
    /// <summary>
    /// IconFactory のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Parallelizable]
    [TestFixture]
    class IconFactoryTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Create_StockIcon
        /// 
        /// <summary>
        /// システムで用意されているアイコンを生成するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(StockIcons.Application, IconSize.Large, 32)]
        [TestCase(StockIcons.Application, IconSize.Jumbo, 256)]
        public void Create_StockIcon(StockIcons id, IconSize size, int expected)
        {
            Assert.That(
                IconFactory.Create(id, size).Size,
                Is.EqualTo(new Size(expected, expected))
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_FileIcon
        /// 
        /// <summary>
        /// ファイルからアイコンを抽出して生成するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(@"C:\Windows\notepad.exe", IconSize.Small, 16)]
        [TestCase(@"C:\Windows\notepad.exe", IconSize.ExtraLarge, 48)]
        public void Create_FileIcon(string path, IconSize size, int expected)
        {
            Assert.That(
                IconFactory.Create(path, size).Size,
                Is.EqualTo(new Size(expected, expected))
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create_ZeroIcon
        /// 
        /// <summary>
        /// IconSize.Zero を指定した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create_ZeroIcon()
        {
            Assert.That(
                IconFactory.Create(@"C:\Windows\notepad.exe", IconSize.Zero),
                Is.Null
            );
        }
    }
}
