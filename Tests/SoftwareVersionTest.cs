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
    /// SoftwareVersionTest
    /// 
    /// <summary>
    /// SoftwareVersion のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class SoftwareVersionTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Digit
        ///
        /// <summary>
        /// Digit プロパティの設定テストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1, ExpectedResult = 2)]
        [TestCase(2, ExpectedResult = 2)]
        [TestCase(3, ExpectedResult = 3)]
        [TestCase(4, ExpectedResult = 4)]
        [TestCase(5, ExpectedResult = 4)]
        public int Digit(int digit)
            => new SoftwareVersion { Digit = digit }.Digit;

        /* ----------------------------------------------------------------- */
        ///
        /// ToString_Assembly
        ///
        /// <summary>
        /// バージョンを表す文字列を取得するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void ToString_Assembly()
        {
            var asm   = AssemblyReader.Default;
            var major = asm.Version.Major;
            var minor = asm.Version.Minor;
            var arch  = (IntPtr.Size == 4) ? "x86" : "x64";

            var version = new SoftwareVersion(asm.Assembly)
            {
                Digit  = 2,
                Prefix = "begin-",
                Suffix = "-end"
            };

            Assert.That(version.ToString(true),  Is.EqualTo($"begin-{major}.{minor}-end ({arch})"));
            Assert.That(version.ToString(false), Is.EqualTo($"begin-{major}.{minor}-end"));
            Assert.That(version.ToString(),      Is.EqualTo(version.ToString(false)));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse
        ///
        /// <summary>
        /// バージョンを表す文字列を解析するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("1.0")]
        [TestCase("1.0.0")]
        [TestCase("1.0.0.0")]
        [TestCase("1.0.0.0-suffix")]
        [TestCase("v1.0.0.0-suffix")]
        [TestCase("v1.0.0.0-p21")]
        [TestCase("p21-v1.0.0.0-suffix")]
        public void Parse(string src)
        {
            var version = new SoftwareVersion(src);
            Assert.That(version.ToString(false), Is.EqualTo(src));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse_Invalid
        ///
        /// <summary>
        /// 無効な文字列を設定した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("1")]
        [TestCase("InvalidVersionNumber")]
        public void Parse_Invalid(string src)
        {
            var version = new SoftwareVersion(src);
            Assert.That(version.ToString(false), Is.EqualTo("1.0.0.0"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse_Null
        ///
        /// <summary>
        /// 無効な Assembly オブジェクトを設定した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Parse_Null()
        {
            var version = new SoftwareVersion(default(Assembly));
            Assert.That(version.ToString(false), Is.EqualTo("1.0.0.0"));
        }
    }
}
