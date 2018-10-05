/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using Cube.FileSystem.Mixin;
using Cube.FileSystem.TestService;
using Cube.Generics;
using NUnit.Framework;
using System.Reflection;

namespace Cube.FileSystem.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// FilesTest
    ///
    /// <summary>
    /// FileSystem.Operator の拡張メソッドのテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class IoExtensionTest : FileFixture
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// Executes the test for loading from a file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Load() => Assert.That(
            IO.Load(GetExamplesWith("Sample.txt"), e => e.Length),
            Is.EqualTo(13L)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Load_NotFound
        ///
        /// <summary>
        /// Confirms the behavior when loading from an inexistent file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LoadOrDefault_NotFound() => Assert.That(
            IO.LoadOrDefault(GetExamplesWith("NotFound.dummy"), e => e.Length, -1),
            Is.EqualTo(-1L)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// Executes the test for saving to a file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Save()
        {
            var dest = GetResultsWith(nameof(Save));
            IO.Save(dest, e => e.WriteByte((byte)'a'));
            Assert.That(IO.Get(dest).Length, Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save_Throws
        ///
        /// <summary>
        /// Confirms the behavior when saving to a file being handled by
        /// another process.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Save_Throws() => Assert.That(
            () => IO.Save(Assembly.GetExecutingAssembly().Location, e => e.WriteByte((byte)'a')),
            Throws.TypeOf<System.IO.IOException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetTypeName
        ///
        /// <summary>
        /// Executes the test for getting the name of filetype.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("Sample.txt",     ExpectedResult = true)]
        [TestCase("NotFound.dummy", ExpectedResult = true)]
        public bool GetTypeName(string filename) =>
            IO.GetTypeName(IO.Get(GetExamplesWith(filename))).HasValue();

        /* ----------------------------------------------------------------- */
        ///
        /// GetTypeName_Null
        ///
        /// <summary>
        /// Confirms the behavior when null is specified.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetTypeName_Null()
        {
            Assert.That(IO.GetTypeName(string.Empty), Is.Empty);
            Assert.That(IO.GetTypeName(default(string)), Is.Empty);
            Assert.That(IO.GetTypeName(default(Information)), Is.Empty);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetUniqueName
        ///
        /// <summary>
        /// Executes the test for getting a unique name.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetUniqueName()
        {
            var src = GetResultsWith($"UniqueTest.txt");
            var u1 = IO.GetUniqueName(src);
            Assert.That(u1, Is.EqualTo(src));

            IO.Copy(GetExamplesWith("Sample.txt"), u1);
            var u2 = IO.GetUniqueName(src);
            Assert.That(u2, Is.EqualTo(GetResultsWith($"UniqueTest (1).txt")));

            IO.Copy(GetExamplesWith("Sample.txt"), u2);
            var u3 = IO.GetUniqueName(IO.Get(src));
            Assert.That(u3, Is.EqualTo(GetResultsWith($"UniqueTest (2).txt")));

            IO.Copy(GetExamplesWith("Sample.txt"), u3);
            var u4 = IO.GetUniqueName(u3); // Not src
            Assert.That(u4, Is.EqualTo(GetResultsWith($"UniqueTest (2) (1).txt")));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ChangeExtension
        ///
        /// <summary>
        /// Executes the test for changing the extension of the specified
        /// path.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(@"C:\Foo\Bar\Bas.txt", ".pdf", ExpectedResult = @"C:\Foo\Bar\Bas.pdf")]
        [TestCase(@"C:\Foo\Bar\None",    ".txt", ExpectedResult = @"C:\Foo\Bar\None.txt")]
        public string ChangeExtension(string src, string ext) => IO.ChangeExtension(src, ext);

        #endregion
    }
}
