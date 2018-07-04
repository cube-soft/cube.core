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
using NUnit.Framework;

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
        /// ファイルを読み込むテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Load() => new IO().Load(
            GetExamplesWith("Sample.txt"),
            e => Assert.That(e.Length, Is.EqualTo(13L))
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Load_NotFound
        ///
        /// <summary>
        /// 存在しないファイルを読み込もうとした時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Load_NotFound() => Assert.That(
            new IO().Load(GetExamplesWith("NotFound.dummy"), e => e.Length, -1),
            Is.EqualTo(-1L)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// ファイルを保存するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Save()
        {
            var io   = new IO();
            var dest = GetResultsWith(nameof(Save));

            io.Save(dest, e => e.WriteByte((byte)'a'));
            Assert.That(io.Get(dest).Length, Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetTypeName
        ///
        /// <summary>
        /// ファイルの種類を表す文字列を取得するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("Sample.txt",     ExpectedResult = true)]
        [TestCase("NotFound.dummy", ExpectedResult = true)]
        public bool GetTypeName(string filename) =>
            !string.IsNullOrEmpty(IO.GetTypeName(IO.Get(GetExamplesWith(filename))));

        /* ----------------------------------------------------------------- */
        ///
        /// GetTypeName_Null
        ///
        /// <summary>
        /// 引数に null を指定した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetTypeName_Null()
        {
            Assert.That(IO.GetTypeName(string.Empty), Is.Null);
            Assert.That(IO.GetTypeName(default(string)), Is.Null);
            Assert.That(IO.GetTypeName(default(Information)), Is.Null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetUniqueName
        ///
        /// <summary>
        /// 一意なパスを取得するテストを実行します。
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
        /// GetUniqueName_Null
        ///
        /// <summary>
        /// 引数に null を指定した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetUniqueName_Null()
        {
            var dummy = default(IO);
            var src   = GetExamplesWith("Sample.txt");

            Assert.That(dummy.GetUniqueName(src), Is.Null);
            Assert.That(dummy.GetUniqueName(IO.Get(src)), Is.Null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ChangeExtension
        ///
        /// <summary>
        /// 拡張子を変更するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(@"C:\Foo\Bar\Bas.txt", ".pdf", ExpectedResult = @"C:\Foo\Bar\Bas.pdf")]
        [TestCase(@"C:\Foo\Bar\None",    ".txt", ExpectedResult = @"C:\Foo\Bar\None.txt")]
        public string ChangeExtension(string src, string ext) =>
            new IO().ChangeExtension(src, ext);

        #endregion
    }
}
