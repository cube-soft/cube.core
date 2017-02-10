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
using NUnit.Framework;
using Cube.Conversions;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// UriQueryTest
    /// 
    /// <summary>
    /// Uri クラスの拡張メソッドをテストするためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Parallelizable]
    [TestFixture]
    class UriQueryTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// With_Value
        /// 
        /// <summary>
        /// 様々な型を With に指定した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("string", "value")]
        [TestCase("int", 5)]
        [TestCase("double", 3.14)]
        public void With_Value<T>(string key, T value)
        {
            Assert.That(
                Create().With(key, value).ToString,
                Is.EqualTo($"{Create()}?{key}={value}")
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// With_DateTime
        /// 
        /// <summary>
        /// 時刻を付与するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(2015, 3, 19, 23, 57, 57, 1426777077)]
        public void With_DateTime(int y, int m, int d, int hh, int mm, int ss, long unix)
        {
            var time = new DateTime(y, m, d, hh, mm, ss);
            Assert.That(
                Create().With(time).ToString(),
                Is.EqualTo($"{Create()}?t={unix}")
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// With_MultiQuery
        /// 
        /// <summary>
        /// 複数個のクエリーを With で結合した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void With_MultiQuery()
        {
            Assert.That(
                Create().With("key1", "value1").With("key2", "value2").ToString(),
                Is.EqualTo($"{Create()}?key1=value1&key2=value2")
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// With_SoftwareVersion
        /// 
        /// <summary>
        /// SoftwareVersion オブジェクトを With で結合した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void With_SoftwareVersion()
        {
            Assert.That(
                Create().With(new SoftwareVersion
                {
                    Number = new Version(1, 2, 0, 0),
                    Digit  = 2,
                    Suffix = "beta"
                }).ToString(),
                Is.EqualTo($"{Create()}?ver=1.2beta")
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// With_Utm
        /// 
        /// <summary>
        /// UTM クエリーを With で結合した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void With_Utm()
        {
            Assert.That(
                Create().With(new UtmQuery
                {
                    Source   = "cube",
                    Medium   = "tests",
                    Campaign = "january",
                    Term     = "dummy",
                    Content  = "content"
                }).ToString(),
                Is.EqualTo($"{Create()}?utm_source=cube&utm_medium=tests&utm_campaign=january&utm_term=dummy&utm_content=content")
            );
        }

        #endregion

        #region Helper methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// ベースとなる Uri オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Uri Create() => new Uri("http://www.cube-soft.jp/");

        #endregion
    }
}
