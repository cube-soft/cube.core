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
using System.IO;
using System.Reflection;
using NUnit.Framework;

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
    [TestFixture]
    class FileInfoTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Properties
        ///
        /// <summary>
        /// System.IO.FileInfo を参照しているプロパティをテストします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Properties()
        {
            var src  = Assembly.GetExecutingAssembly().Location;
            var info = new Cube.FileSystem.FileInfo(src);
            Assert.That(info.Exists, Is.True);
            Assert.That(info.Name, Is.EqualTo(Path.GetFileName(src)));
            Assert.That(info.FullName, Is.EqualTo(src));
            Assert.That(info.DirectoryName, Is.EqualTo(Path.GetDirectoryName(src)));
            Assert.That(info.Extension, Is.EqualTo(Path.GetExtension(src)));
            Assert.That(info.Length, Is.AtLeast(1000));
            Assert.That(info.IsReadOnly, Is.False);
            Assert.That(info.CreationTime, Is.LessThanOrEqualTo(DateTime.Now));
            Assert.That(info.LastWriteTime, Is.LessThanOrEqualTo(DateTime.Now));
            Assert.That(info.LastAccessTime, Is.LessThanOrEqualTo(DateTime.Now));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TypeName
        ///
        /// <summary>
        /// TypeName が取得できているかをテストします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TypeName()
        {
            var info = new Cube.FileSystem.FileInfo(Assembly.GetExecutingAssembly().Location);
            Assert.That(info.TypeName, Is.Not.Null.Or.Empty);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Icon
        ///
        /// <summary>
        /// Icon が取得できているかをテストします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Icon()
        {
            var info = new Cube.FileSystem.FileInfo(Assembly.GetExecutingAssembly().Location);
            Assert.That(info.Icon, Is.Not.Null);
            Assert.That(info.IconSize, Is.EqualTo(Cube.IconSize.Small));
        }
    }
}
