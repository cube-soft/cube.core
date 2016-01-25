/* ------------------------------------------------------------------------- */
///
/// AssemblyReaderTest.cs
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

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// AssemblyReaderTest
    /// 
    /// <summary>
    /// AssemblyReader のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class AssemblyReaderTest
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
        public void Location_ExecutingAssembly()
        {
            Assert.That(
                Create().Location,
                Is.Not.Null.Or.Empty
            );
        }

        [Test]
        public void Title_ExecutingAssembly()
        {
            Assert.That(
                Create().Title,
                Is.EqualTo("Cube.Core testing project")
            );
        }

        [Test]
        public void Description_ExecutingAssembly()
        {
            Assert.That(
                Create().Description,
                Is.EqualTo("Test Cube.Core using NUnit framework.")
            );
        }

        [Test]
        public void Configuration_ExecutingAssembly()
        {
            Assert.That(
                Create().Configuration,
                Is.Empty
            );
        }

        [Test]
        public void Company_ExecutingAssembly()
        {
            Assert.That(
                Create().Company,
                Is.EqualTo("CubeSoft, Inc.")
            );
        }

        [Test]
        public void Product_ExecutingAssembly()
        {
            Assert.That(
                Create().Product,
                Is.EqualTo("Cube.Core.Tests")
            );
        }

        [Test]
        public void Copyright_ExecutingAssembly()
        {
            Assert.That(
                Create().Copyright,
                Is.EqualTo("Copyright © 2010 CubeSoft, Inc.")
            );
        }

        [Test]
        public void Trademark_ExecutingAssembly()
        {
            Assert.That(
                Create().Trademark,
                Is.EqualTo("dummy trademark")
            );
        }

        [Test]
        public void Culture_ExecutingAssembly()
        {
            Assert.That(
                Create().Culture,
                Is.Empty
            );
        }

        [Test]
        public void Version_ExecutingAssembly()
        {
            Assert.That(
                Create().Version,
                Is.AtLeast(new Version(1, 2, 0, 0))
            );
        }

        [Test]
        public void FileVersion_ExecutingAssembly()
        {
            Assert.That(
                Create().FileVersion,
                Is.AtLeast(new Version(1, 2, 0, 0))
            );
        }

        [Test]
        public void Icon_ExecutingAssembly()
        {
            Assert.That(
                Create().Icon.Size,
                Is.EqualTo(new Size(16, 16))
            );
        }

        #endregion

        #region Helper methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// AssemblyReader オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public AssemblyReader Create()
        {
            return new AssemblyReader(Assembly.GetExecutingAssembly());
        }

        #endregion
    }
}
