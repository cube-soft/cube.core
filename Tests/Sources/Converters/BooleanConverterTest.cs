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
using Cube.Xui.Converters;
using NUnit.Framework;
using System;
using System.Windows;

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// BooleanConverterTest
    ///
    /// <summary>
    /// bool 値に関する Converter のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class BooleanConverterTest : ConvertHelper
    {
        #region Tests

        #region Inverse

        /* ----------------------------------------------------------------- */
        ///
        /// Inverse
        ///
        /// <summary>
        /// 真偽値を反転させるテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(true,  ExpectedResult = false)]
        [TestCase(false, ExpectedResult = true)]
        public bool Inverse(bool src) => Convert<bool>(new Inverse(), src);

        /* ----------------------------------------------------------------- */
        ///
        /// Inverse_Back
        ///
        /// <summary>
        /// 真偽値を反転させるテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(true,  ExpectedResult = false)]
        [TestCase(false, ExpectedResult = true)]
        public bool Inverse_Back(bool src) => ConvertBack<bool>(new Inverse(), src);

        /* ----------------------------------------------------------------- */
        ///
        /// Inverse_ProvideValue
        ///
        /// <summary>
        /// ProvideValue のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Inverse_ProvideValue()
        {
            var src = new Inverse();
            Assert.That(src.ProvideValue(null), Is.EqualTo(src));
        }

        #endregion

        #region BooleanToGeneric

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToGeneric
        ///
        /// <summary>
        /// BooleanToGeneric(T).Convert のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("src", "compared", ExpectedResult = "src")]
        public string BooleanToGeneric(string src, string compared) =>
            Convert<string>(new BooleanToGeneric<string>(src, compared,
                (x, y) => string.CompareOrdinal((string)x, compared) > 0),
                src
            );

        #endregion

        #region BooleanToInteger

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToInteger
        ///
        /// <summary>
        /// 真偽値を整数値に変換するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(true,  ExpectedResult = 1)]
        [TestCase(false, ExpectedResult = 0)]
        public int BooleanToInteger(bool src) =>
            Convert<int>(new BooleanToInteger(), src);

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToInteger_Back
        ///
        /// <summary>
        /// BooleanToInteger.ConvertBack 実行時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void BooleanToInteger_Back() => Assert.That(
            () => ConvertBack<bool>(new BooleanToInteger(), 1),
            Throws.TypeOf<NotSupportedException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToInteger_ProvideValue
        ///
        /// <summary>
        /// ProvideValue のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void BooleanToInteger_ProvideValue()
        {
            var src = new BooleanToInteger();
            Assert.That(src.ProvideValue(null), Is.EqualTo(src));
        }

        #endregion

        #region BooleanToVisibility

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToVisibility
        ///
        /// <summary>
        /// 真偽値を Visibility に変換するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(true,  ExpectedResult = Visibility.Visible)]
        [TestCase(false, ExpectedResult = Visibility.Collapsed)]
        public Visibility BooleanToVisibility(bool src) =>
            Convert<Visibility>(new BooleanToVisibility(), src);

        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToVisibility_Func
        ///
        /// <summary>
        /// 関数オブジェクトを引数に初期化するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void BooleanToVisibility_Func() => Assert.That(
            Convert<Visibility>(new BooleanToVisibility(e => (int)e > 0), 1),
            Is.EqualTo(Visibility.Visible)
        );

        #endregion

        #endregion
    }
}
