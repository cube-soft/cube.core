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
using System;
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
    /// <remarks>
    /// TODO: Parallelizable 属性を付与すると Icon_ExecutingAssembly() の
    /// テストに失敗する場合がある。要調査。
    /// </remarks>
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
        public void Assembly_IsNotNull()
            => Assert.That(Create().Assembly, Is.Not.Null);

        [Test]
        public void Assembly_Location_IsNotNullOrEmpty()
            => Assert.That(Create().Location, Is.Not.Null.And.Not.Empty);

        [TestCase(ExpectedResult = "Cube.Core testing project")]
        public string Assembly_Title()
            => Create().Title;

        [TestCase(ExpectedResult = "NUnit framework を用いて Cube.Core プロジェクトをテストします。")]
        public string Assembly_Description()
            => Create().Description;

        [TestCase(ExpectedResult = "CubeSoft")]
        public string Assembly_Company()
            => Create().Company;

        [TestCase(ExpectedResult = "Cube.Core.Tests")]
        public string Assembly_Product()
            => Create().Product;

        [TestCase(ExpectedResult = "Copyright © 2010 CubeSoft, Inc.")]
        public string Assembly_Copyright()
            => Create().Copyright;

        [TestCase(ExpectedResult = "ここに商標を設定します。")]
        public string Assembly_Trademark()
            => Create().Trademark;

        [Test]
        public void Assembly_Configuration_IsNullOrEmpty()
            => Assert.That(Create().Configuration, Is.Null.Or.Empty);

        [Test]
        public void Assembly_Culture_IsNullOrEmpty()
            => Assert.That(Create().Culture, Is.Null.Or.Empty);

        [TestCase(1, 2, 0, 0)]
        public void Assembly_Version_IsAtLeast(int major, int minor, int build, int revision)
            => Assert.That(Create().Version, Is.AtLeast(new Version(major, minor, build, revision)));

        [TestCase(1, 2, 0, 0)]
        public void Assembly_FileVersion_IsAtLeast(int major, int minor, int build, int revision)
            => Assert.That(Create().FileVersion, Is.AtLeast(new Version(major, minor, build, revision)));

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
            => new AssemblyReader(Assembly.GetExecutingAssembly());

        #endregion
    }
}
