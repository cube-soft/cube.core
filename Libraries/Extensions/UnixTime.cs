/* ------------------------------------------------------------------------- */
///
/// UnixTimeExtensions.cs
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

namespace Cube.Extensions
{
    /* --------------------------------------------------------------------- */
    ///
    /// UnixTime
    /// 
    /// <summary>
    /// DateTiem と UnixTime の相互変換を行うための拡張クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class UnixTime
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ToUnixTime
        /// 
        /// <summary>
        /// DateTime オブジェクトから UNIX 時刻へ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static long ToUnixTime(this DateTime time)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var utc = time.ToUniversalTime();
            return (long)utc.Subtract(epoch).TotalSeconds;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToDateTime
        /// 
        /// <summary>
        /// UNIX 時刻から DateTime オブジェクトへ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime ToDateTime(this int unix)
        {
            var us = (uint)unix;
            return ToDateTime(us);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToDateTime
        /// 
        /// <summary>
        /// UNIX 時刻から DateTime オブジェクトへ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime ToDateTime(this long unix)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unix);
        }
    }
}
