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

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// StringConverterTest
    ///
    /// <summary>
    /// ValueToString のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class StringConverterTest : ConvertHelper
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// ValueToString
        ///
        /// <summary>
        /// 文字列に変換するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void ValueToString() => Assert.That(
            Convert<string>(new ValueToString(), 1),
            Is.EqualTo("1")
        );

        /* ----------------------------------------------------------------- */
        ///
        /// ValueToString_Null
        ///
        /// <summary>
        /// 引数に null を指定した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void ValueToString_Null() => Assert.That(
            Convert<string>(new ValueToString(), null),
            Is.Empty
        );

        #endregion
    }
}
