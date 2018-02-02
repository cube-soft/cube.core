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
using System;
using NUnit.Framework;
using Cube.Conversions;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// TimeFormatTest
    ///
    /// <summary>
    /// DateTime に関するテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class TimeTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ToUniversalTime
        ///
        /// <summary>
        /// 指定された日時をいったん UNIX 時刻に変換し、
        /// 再度 DateTime オブジェクトに変換するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1970,  1,  1,  0,  0,  0)]
        [TestCase(2000,  1,  1,  0,  0,  0)]
        [TestCase(2038,  1, 19,  3, 14,  7)]
        [TestCase(2104,  1,  1,  0,  0,  0)]
        [TestCase(2999, 12, 31, 23, 59, 59)]
        public void ToUniversalTime(int y, int m, int d, int hh, int mm, int ss)
        {
            var src = new DateTime(y, m, d, hh, mm, ss, 0, DateTimeKind.Utc);
            var actual = src.ToUnixTime().ToUniversalTime();
            Assert.That(actual, Is.EqualTo(src));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToLocalTime
        ///
        /// <summary>
        /// 指定された日時をいったん UNIX 時刻に変換し、
        /// 再度 DateTime オブジェクトに変換するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1970,  1,  1,  0,  0,  0)]
        [TestCase(2000,  1,  1,  0,  0,  0)]
        [TestCase(2038,  1, 19,  3, 14,  7)]
        [TestCase(2104,  1,  1,  0,  0,  0)]
        [TestCase(2999, 12, 31, 23, 59, 59)]
        public void ToLocalTime(int y, int m, int d, int hh, int mm, int ss)
        {
            var src = new DateTime(y, m, d, hh, mm, ss, 0, DateTimeKind.Local);
            var actual = src.ToUnixTime().ToLocalTime();
            Assert.That(actual, Is.EqualTo(src));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToDateTime
        ///
        /// <summary>
        /// 指定された日時をいったん UNIX 時刻に変換し、
        /// 再度 DateTime オブジェクトに変換するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1970,  1,  1,  0,  0,  0)]
        [TestCase(2999, 12, 31, 23, 59, 59)]
        public void ToDateTime(int y, int m, int d, int hh, int mm, int ss)
        {
            var src = new DateTime(y, m, d, hh, mm, ss, 0, DateTimeKind.Utc);
            var actual = src.ToUnixTime().ToDateTime();
            Assert.That(actual, Is.EqualTo(src));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToDateTime
        ///
        /// <summary>
        /// int 型で表された UNIX 時刻を DateTime オブジェクトに変換する
        /// テストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(0x7fffffffu, 2038, 1, 19, 3, 14,  7)]
        [TestCase(0x80000000u, 2038, 1, 19, 3, 14,  8)]
        [TestCase(0xffffffffu, 2106, 2,  7, 6, 28, 15)]
        public void ToDateTime(uint unix, int y, int m, int d, int hh, int mm, int ss)
        {
            var src = (int)unix;
            var expected = new DateTime(y, m, d, hh, mm, ss, 0, DateTimeKind.Utc);
            Assert.That(src.ToDateTime(), Is.EqualTo(expected));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToLocalTime
        ///
        /// <summary>
        /// int 型で表された UNIX 時刻を現地時刻で DateTime オブジェクトに
        /// 変換するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(0x7fffffffu, 2038, 1, 19, 3, 14,  7)]
        [TestCase(0x80000000u, 2038, 1, 19, 3, 14,  8)]
        [TestCase(0xffffffffu, 2106, 2,  7, 6, 28, 15)]
        public void ToLocalTime(uint unix, int y, int m, int d, int hh, int mm, int ss)
        {
            var src = (int)unix;
            var expected = new DateTime(y, m, d, hh, mm, ss, 0, DateTimeKind.Utc).ToLocalTime();
            Assert.That(src.ToLocalTime(), Is.EqualTo(expected));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse_UniversalTime
        ///
        /// <summary>
        /// 指定された文字列を UTC 時刻として解析します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("2017/02/03 12:34:55", "yyyy/MM/dd HH:mm:ss", 2017, 2, 3, 12, 34, 55)]
        [TestCase("", "", 1, 1, 1, 0, 0, 0)]
        public void Parse_UniversalTime(string src, string fmt, int y, int m, int d, int hh, int mm, int ss)
        {
            var expected = new DateTime(y, m, d, hh, mm, ss, 0, DateTimeKind.Utc);
            var actual   = src.ToDateTime(fmt);

            Assert.That(actual.Kind, Is.EqualTo(DateTimeKind.Utc));
            Assert.That(actual,      Is.EqualTo(expected));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse_LocalTime
        ///
        /// <summary>
        /// 指定された文字列をローカル時刻として解析します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("2017/02/03 12:34:55", "yyyy/MM/dd HH:mm:ss", 2017, 2, 3, 12, 34, 55)]
        [TestCase("", "", 1, 1, 1, 0, 0, 0)]
        public void Parse_LocalTime(string src, string fmt, int y, int m, int d, int hh, int mm, int ss)
        {
            var expected = new DateTime(y, m, d, hh, mm, ss, 0, DateTimeKind.Local);
            var actual   = src.ToLocalTime(fmt);

            Assert.That(actual.Kind, Is.EqualTo(DateTimeKind.Local));
            Assert.That(actual,      Is.EqualTo(expected));
        }
    }
}
