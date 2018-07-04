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
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Cube.FileSystem.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// DisplayFilterTest
    ///
    /// <summary>
    /// DisplayFilter の拡張メソッドのテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class DisplayFilterTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// ToString
        ///
        /// <summary>
        /// フィルタ用文字列に変換するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public string ToString(string descr, bool ignore, string[] exts) =>
            new DisplayFilter(descr, ignore, exts).ToString();

        /* ----------------------------------------------------------------- */
        ///
        /// ToString_Null
        ///
        /// <summary>
        /// null で初期化した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void ToString_Null() => Assert.That(
            () => new DisplayFilter(null, null).ToString(),
            Throws.TypeOf<ArgumentNullException>()
        );

        #endregion

        #region TestCases

        /* ----------------------------------------------------------------- */
        ///
        /// TestCases
        ///
        /// <summary>
        /// テストケース一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData("All files", true,
                    new[] {".*" }
                ).Returns("All files (*.*)|*.*");

                yield return new TestCaseData("All files", false,
                    new[] { ".*" }
                ).Returns("All files (*.*)|*.*");

                yield return new TestCaseData("Text files", true,
                    new[] { ".txt" }
                ).Returns("Text files (*.txt)|*.txt;*.TXT");

                yield return new TestCaseData("Text files", false,
                    new[] { ".txt" }
                ).Returns("Text files (*.txt)|*.txt");

                yield return new TestCaseData("Image files", true,
                    new[] { ".Jpg", ".Jpeg", ".Png",}
                ).Returns("Image files (*.Jpg, *.Jpeg, *.Png)|*.jpg;*.JPG;*.jpeg;*.JPEG;*.png;*.PNG");

                yield return new TestCaseData("Image files", false,
                    new[] { ".Jpg", ".Jpeg", ".Png",}
                ).Returns("Image files (*.Jpg, *.Jpeg, *.Png)|*.Jpg;*.Jpeg;*.Png");

                yield return new TestCaseData("Text or customized files", true,
                    new[] { ".txt", ".1" }
                ).Returns("Text or customized files (*.txt, *.1)|*.txt;*.TXT;*.1");

                yield return new TestCaseData("Text or customized files", false,
                    new[] { ".txt", ".1" }
                ).Returns("Text or customized files (*.txt, *.1)|*.txt;*.1");
            }
        }

        #endregion
    }
}
