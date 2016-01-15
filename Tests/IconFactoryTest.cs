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
    [TestFixture]
    class IconFactoryTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// CreateStockIcon
        /// 
        /// <summary>
        /// システムで用意されているアイコンを生成するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Cube.StockIcons.Application, Cube.IconSize.Small,       16)]
        [TestCase(Cube.StockIcons.Application, Cube.IconSize.Large,       32)]
        [TestCase(Cube.StockIcons.Application, Cube.IconSize.ExtraLarge,  48)]
        [TestCase(Cube.StockIcons.Application, Cube.IconSize.Jumbo,      256)]
        public void CreateStockIcon(Cube.StockIcons id, Cube.IconSize size, int expected)
        {
            var icon = Cube.IconFactory.Create(id, size);
            Assert.That(icon, Is.Not.Null);
            Assert.That(icon.Width, Is.EqualTo(expected));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateFileIcon
        /// 
        /// <summary>
        /// ファイルからアイコンを抽出して生成するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(@"C:\Windows\notepad.exe", Cube.IconSize.Small,      16)]
        [TestCase(@"C:\Windows\notepad.exe", Cube.IconSize.ExtraLarge, 48)]
        public void CreateFileIcon(string path, Cube.IconSize size, int expected)
        {
            var icon = Cube.IconFactory.Create(path, size);
            Assert.That(icon, Is.Not.Null);
            Assert.That(icon.Width, Is.EqualTo(expected));
        }
    }
}
