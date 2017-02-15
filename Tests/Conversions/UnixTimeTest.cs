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
    /// UnixTimeTest
    /// 
    /// <summary>
    /// UnixTime のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Parallelizable]
    [TestFixture]
    class UnixTimeTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ToUniversalTime
        /// 
        /// <summary>
        /// 指定された日時を UTC 時刻としていったん NTP タイムスタンプに
        /// 変換し、再度 DateTime オブジェクトに変換するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1970, 1,  1, 0,  0, 0)]
        [TestCase(2000, 1,  1, 0,  0, 0)]
        [TestCase(2038, 1, 19, 3, 14, 7)]
        [TestCase(2104, 1,  1, 0,  0, 0)]
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
        /// 指定された日時をローカル時刻としていったん NTP タイムスタンプに
        /// 変換し、再度 DateTime オブジェクトに変換するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1970, 1,  1, 0,  0, 0)]
        [TestCase(2000, 1,  1, 0,  0, 0)]
        [TestCase(2038, 1, 19, 3, 14, 7)]
        [TestCase(2104, 1,  1, 0,  0, 0)]
        public void ToLocalTime(int y, int m, int d, int hh, int mm, int ss)
        {
            var src = new DateTime(y, m, d, hh, mm, ss, 0, DateTimeKind.Local);
            var actual = src.ToUnixTime().ToLocalTime();
            Assert.That(actual, Is.EqualTo(src));
        }
    }
}
