/* ------------------------------------------------------------------------- */
///
/// UnixTimeTest.cs
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
    [TestFixture]
    class UnixTimeTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ToUniversalTime
        /// 
        /// <summary>
        /// 引数に指定された日時をいったん NTP タイムスタンプに変換し、
        /// 再度 DateTime オブジェクトに変換するテストを行います。
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
            Assert.That(
                src.ToUnixTime().ToUniversalTime(),
                Is.EqualTo(src)
            );
        }
    }
}
