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
using System.Collections.Generic;
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
    [TestFixture]
    class UriQueryTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// With_Value
        /// 
        /// <summary>
        /// 様々な型を指定した時のテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("string", "value")]
        [TestCase("int", 5)]
        [TestCase("double", 3.14)]
        public void With_Value<T>(string key, T value)
            => Assert.That(
                Create().With(key, value).ToString,
                Is.EqualTo($"{Create()}?{key}={value}")
            );

        /* ----------------------------------------------------------------- */
        ///
        /// With_DateTime
        /// 
        /// <summary>
        /// 時刻を付与するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(2015, 3, 19, 14, 57, 57, 1426777077)]
        public void With_DateTime(int y, int m, int d, int hh, int mm, int ss, long unix)
            => Assert.That(
                Create().With(new DateTime(y, m, d, hh, mm, ss, DateTimeKind.Utc)).ToString(),
                Is.EqualTo($"{Create()}?ts={unix}")
            );

        /* ----------------------------------------------------------------- */
        ///
        /// With_MultiQuery
        /// 
        /// <summary>
        /// 複数個のクエリーを結合するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void With_MultiQuery()
            => Assert.That(
                Create().With("key1", "value1").With("key2", "value2").ToString(),
                Is.EqualTo($"{Create()}?key1=value1&key2=value2")
            );

        /* ----------------------------------------------------------------- */
        ///
        /// With_Null
        /// 
        /// <summary>
        /// 引数に null が設定された時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void With_Null()
        {
            Assert.That(default(Uri).With("key", "value"), Is.Null);
            Assert.That(Create().With(default(Dictionary<string, string>)), Is.EqualTo(Create()));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// With_SoftwareVersion
        /// 
        /// <summary>
        /// SoftwareVersion オブジェクトを結合するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void With_SoftwareVersion()
            => Assert.That(
                Create().With(new SoftwareVersion
                {
                    Number = new Version(1, 2, 0, 0),
                    Digit  = 2,
                    Suffix = "beta"
                }).ToString(),
                Is.EqualTo($"{Create()}?ver=1.2beta")
            );

        /* ----------------------------------------------------------------- */
        ///
        /// With_Utm
        /// 
        /// <summary>
        /// UTM クエリーを結合するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void With_Utm()
            => Assert.That(
                Create().With(new UtmQuery
                {
                    Source   = "cube",
                    Medium   = "tests",
                    Campaign = "january",
                    Term     = "dummy",
                    Content = "content"
                }).ToString(),
                Is.EqualTo($"{Create()}?utm_source=cube&utm_medium=tests&utm_campaign=january&utm_term=dummy&utm_content=content")
            );

        /* ----------------------------------------------------------------- */
        ///
        /// With_Utm_Null
        /// 
        /// <summary>
        /// 無効な UTM クエリーを設定した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void With_Utm_Null()
            => Assert.That(
                Create().With(default(UtmQuery)),
                Is.EqualTo(Create())
            );

        /* ----------------------------------------------------------------- */
        ///
        /// WithoutQuery
        /// 
        /// <summary>
        /// クエリーを除去するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("http://www.example.com/index.html?foo&bar=bas")]
        [TestCase("http://www.example.net/")]
        [TestCase("http://www.example.net")]
        public void WithoutQuery(string url)
        {
            var src = new Uri(url);
            var expected = new Uri($"{src.Scheme}://{src.Host}{src.AbsolutePath}");
            Assert.That(src.WithoutQuery(), Is.EqualTo(expected));
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
