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
        [Test]
        public void Properties()
        {
            var reader = new AssemblyReader(Assembly.GetExecutingAssembly());
            Assert.That(reader.Title,         Is.EqualTo("Cube.Core testing project"));
            Assert.That(reader.Description,   Is.EqualTo("Test Cube.Core using NUnit framework."));
            Assert.That(reader.Configuration, Is.Empty);
            Assert.That(reader.Company,       Is.EqualTo("CubeSoft, Inc."));
            Assert.That(reader.Product,       Is.EqualTo("Cube.Core.Tests"));
            Assert.That(reader.Copyright,     Is.EqualTo("Copyright © 2010 CubeSoft, Inc."));
            Assert.That(reader.Trademark,     Is.EqualTo("dummy trademark"));
            Assert.That(reader.Culture,       Is.Empty);
            Assert.That(reader.Version,       Is.AtLeast(new Version(1, 2, 0, 0)));
            Assert.That(reader.FileVersion,   Is.AtLeast(new Version(1, 2, 0, 0)));
            Assert.That(reader.Icon,          Is.Not.Null);
        }
    }
}
