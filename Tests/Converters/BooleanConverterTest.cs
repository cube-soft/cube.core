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

        #endregion

        #endregion
    }
}
