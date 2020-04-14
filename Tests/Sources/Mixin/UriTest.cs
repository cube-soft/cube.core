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
using Cube.Mixin.Uri;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Cube.Tests.Mixin
{
    /* --------------------------------------------------------------------- */
    ///
    /// UriTest
    ///
    /// <summary>
    /// Uri クラスの拡張メソッドをテストするためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class UriTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// ToUri
        ///
        /// <summary>
        /// 文字列から Uri オブジェクトに変換するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("http://www.cube-soft.jp/1.html", ExpectedResult = "http://www.cube-soft.jp/1.html")]
        [TestCase("https://www.cube-soft.jp/2.html", ExpectedResult = "https://www.cube-soft.jp/2.html")]
        [TestCase("www.cube-soft.jp/3.html", ExpectedResult = "http://www.cube-soft.jp/3.html")]
        [TestCase("//www.cube-soft.jp/4.html", ExpectedResult = "http://www.cube-soft.jp/4.html")]
        [TestCase("/5.html", ExpectedResult = "http://localhost/5.html")]
        [TestCase("", ExpectedResult = "")]
        public string ToUri(string src) => src.ToUri()?.ToString() ?? string.Empty;

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
        {
            var dest = $"{Create()}?{key}={value}";
            var src  = Create().With(key, value);
            Assert.That(src.ToString(), Is.EqualTo(dest));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// With_DateTime
        ///
        /// <summary>
        /// 時刻を付与するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(2015, 3, 19, 14, 57, 57, 1426777077)]
        public void With_DateTime(int y, int m, int d, int hh, int mm, int ss, long unix)
        {
            var dest = $"{Create()}?ts={unix}";
            var src  = Create().With(new DateTime(y, m, d, hh, mm, ss, DateTimeKind.Utc));
            Assert.That(src.ToString(), Is.EqualTo(dest));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// With_Query
        ///
        /// <summary>
        /// Tests the With extended method with the specified dictionary.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void With_Query()
        {
            var dest = $"{Create()}?key1=value1&key2=value2&key3=value3";
            var src  = Create().With(new Dictionary<string, string>
            {
                { "key1", "value1" },
                { "key2", "value2" },
                { "key3", "value3" },
            });

            Assert.That(src.ToString(), Is.EqualTo(dest));
        }

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
        {
            var dest = $"{Create()}?key1=value1&key2=value2";
            var src  = Create().With("key1", "value1").With("key2", "value2");
            Assert.That(src.ToString(), Is.EqualTo(dest));
        }

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
        {
            var asm  = typeof(UriTest).Assembly;
            var dest = $"{Create()}?ver=1.19beta";
            var src  = Create().With(new SoftwareVersion(asm)
            {
                Digit = 2,
                Suffix = "beta"
            });

            Assert.That(src.ToString(), Is.EqualTo(dest));
        }

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
        {
            var dest = $"{Create()}?utm_source=cube&utm_medium=tests&utm_campaign=january&utm_term=dummy&utm_content=content";
            var src  = Create().With(new Utm
            {
                Source   = "cube",
                Medium   = "tests",
                Campaign = "january",
                Term     = "dummy",
                Content  = "content"
            });

            Assert.That(src.ToString(), Is.EqualTo(dest));
        }

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
        {
            var dest = Create();
            var src  = Create().With(default(Utm));
            Assert.That(src, Is.EqualTo(dest));
        }

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
            var src  = new Uri(url);
            var dest = new Uri($"{src.Scheme}://{src.Host}{src.AbsolutePath}");
            Assert.That(src.WithoutQuery(), Is.EqualTo(dest));
        }

        #endregion

        #region Others

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
