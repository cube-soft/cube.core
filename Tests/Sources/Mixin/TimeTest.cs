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
using Cube.Mixin.Time;
using NUnit.Framework;
using System;

namespace Cube.Tests.Mixin
{
    /* --------------------------------------------------------------------- */
    ///
    /// TimeTest
    ///
    /// <summary>
    /// Tests extended methods of the DateTime and related classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class TimeTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// ToUniversalTime
        ///
        /// <summary>
        /// Tests the ToUniversalTime extended method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(0x7fffffffu, 2038, 1, 19, 3, 14,  7)]
        [TestCase(0x80000000u, 2038, 1, 19, 3, 14,  8)]
        [TestCase(0xffffffffu, 2106, 2,  7, 6, 28, 15)]
        public void ToUniversalTime(uint unix, int y, int m, int d, int hh, int mm, int ss)
        {
            var src = (int)unix;
            var expected = new DateTime(y, m, d, hh, mm, ss, 0, DateTimeKind.Utc);
            Assert.That(src.ToUniversalTime(), Is.EqualTo(expected));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToLocalTime
        ///
        /// <summary>
        /// Tests the ToLocalTime extended method.
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
        /// Convert_Unix_Utc
        ///
        /// <summary>
        /// Confirms the result when converting the DateTime object to the
        /// UNIX time and reconverting to the DateTime object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1970,  1,  1,  0,  0,  0)]
        [TestCase(2000,  1,  1,  0,  0,  0)]
        [TestCase(2038,  1, 19,  3, 14,  7)]
        [TestCase(2104,  1,  1,  0,  0,  0)]
        [TestCase(2999, 12, 31, 23, 59, 59)]
        public void Convert_Unix_Utc(int y, int m, int d, int hh, int mm, int ss)
        {
            var src  = (DateTime?)new DateTime(y, m, d, hh, mm, ss, 0, DateTimeKind.Utc);
            var dest = src.ToUnixTime().ToUniversalTime();
            Assert.That(dest, Is.EqualTo(src));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Convert_Unix_Local
        ///
        /// <summary>
        /// Confirms the result when converting the DateTime object to the
        /// UNIX time and reconverting to the DateTime object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1970,  1,  1,  0,  0,  0)]
        [TestCase(2000,  1,  1,  0,  0,  0)]
        [TestCase(2038,  1, 19,  3, 14,  7)]
        [TestCase(2104,  1,  1,  0,  0,  0)]
        [TestCase(2999, 12, 31, 23, 59, 59)]
        public void Convert_Unix_Local(int y, int m, int d, int hh, int mm, int ss)
        {
            var src  = (DateTime?)new DateTime(y, m, d, hh, mm, ss, 0, DateTimeKind.Local);
            var dest = src.ToUnixTime().ToLocalTime();
            Assert.That(dest, Is.EqualTo(src));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse_UniversalTime
        ///
        /// <summary>
        /// Tests the extended method of parsing the specified date-time
        /// string.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("2017/02/03 12:34:55", "yyyy/MM/dd HH:mm:ss", 2017, 2, 3, 12, 34, 55)]
        [TestCase("", "", 1, 1, 1, 0, 0, 0)]
        public void Parse_UniversalTime(string src, string fmt, int y, int m, int d, int hh, int mm, int ss)
        {
            var expected = new DateTime(y, m, d, hh, mm, ss, 0, DateTimeKind.Utc);
            var dest     = src.ToUniversalTime(fmt) ?? CreateMinValue(DateTimeKind.Utc);

            Assert.That(dest.Kind, Is.EqualTo(DateTimeKind.Utc));
            Assert.That(dest,      Is.EqualTo(expected));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse_LocalTime
        ///
        /// <summary>
        /// Tests the extended method of parsing the specified date-time
        /// string.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("2017/02/03 12:34:55", "yyyy/MM/dd HH:mm:ss", 2017, 2, 3, 12, 34, 55)]
        [TestCase("", "", 1, 1, 1, 0, 0, 0)]
        public void Parse_LocalTime(string src, string fmt, int y, int m, int d, int hh, int mm, int ss)
        {
            var expected = new DateTime(y, m, d, hh, mm, ss, 0, DateTimeKind.Local);
            var dest     = src.ToLocalTime(fmt) ?? CreateMinValue(DateTimeKind.Local);

            Assert.That(dest.Kind, Is.EqualTo(DateTimeKind.Local));
            Assert.That(dest,      Is.EqualTo(expected));
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// CreateMinValue
        ///
        /// <summary>
        /// Creates a new instance of the DateTime class with the specified
        /// kind.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private DateTime CreateMinValue(DateTimeKind kind) => new DateTime(1, 1, 1, 0, 0, 0, kind);

        #endregion
    }
}
