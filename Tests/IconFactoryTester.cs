/* ------------------------------------------------------------------------- */
///
/// IconFactoryTester.cs
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

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Tests.IconFactoryTester
    /// 
    /// <summary>
    /// IconFactory のテストを行うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class IconFactoryTester
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestCreateStockIcon
        /// 
        /// <summary>
        /// システムで用意されているアイコンを生成するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Cube.StockIcons.Application, Cube.IconSize.Small, 16)]
        [TestCase(Cube.StockIcons.Application, Cube.IconSize.Large, 32)]
        public void TestCreateStockIcon(Cube.StockIcons id, Cube.IconSize size, int width)
        {
            Assert.DoesNotThrow(() =>
            {
                var icon = Cube.IconFactory.Create(id, size);
                Assert.That(icon, Is.Not.Null);
                Assert.That(icon.Width, Is.EqualTo(width));
            });
        }
    }
}
