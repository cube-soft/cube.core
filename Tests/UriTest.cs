/* ------------------------------------------------------------------------- */
///
/// UriTest.cs
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
using Cube.Extensions;

namespace Cube.Tests
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
        /* ----------------------------------------------------------------- */
        ///
        /// WithValue
        /// 
        /// <summary>
        /// 様々な型を With に指定した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("string", "value")]
        [TestCase("int", 5)]
        [TestCase("double", 3.14)]
        public void WithValue<T>(string key, T value)
        {
            var uri = new Uri("http://www.cube-soft.jp/index.html");
            var actual = uri.With(key, value);
            var expected = string.Format("{0}?{1}={2}", uri, key, value);
            Assert.That(actual.ToString(), Is.EqualTo(expected));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WithDateTime
        /// 
        /// <summary>
        /// 時刻を付与するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void WithDateTime()
        {
            var time = new DateTime(2015, 3, 19, 23, 57, 57); // 1426777077
            var uri = new Uri("http://www.cube-soft.jp/index.html");
            var actual = uri.With(time);
            var expected = string.Format("{0}?t=1426777077", uri);
            Assert.That(actual.ToString(), Is.EqualTo(expected));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WithNull
        /// 
        /// <summary>
        /// 引数に null を指定した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void WithNull()
        {
            var uri = new Uri("http://www.cube-soft.jp/index.html");
            var actual = uri.With(null);
            Assert.That(actual.ToString(), Is.EqualTo(uri.ToString()));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WithMutiQuery
        /// 
        /// <summary>
        /// 複数個のクエリーを With で結合した時のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void WithMultiQuery()
        {
            var uri = new Uri("http://www.cube-soft.jp/");
            var actual = uri.With("key1", "value1").With("key2", "value2");
            var expected = string.Format("{0}?{1}={2}&{3}={4}", uri, "key1", "value1", "key2", "value2");
            Assert.That(actual.ToString(), Is.EqualTo(expected));
        }
    }
}
