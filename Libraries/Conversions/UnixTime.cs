/* ------------------------------------------------------------------------- */
///
/// UnixTime.cs
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

namespace Cube.Conversions
{
    /* --------------------------------------------------------------------- */
    ///
    /// UnixTime
    /// 
    /// <summary>
    /// DateTiem と UnixTime の相互変換を行う拡張メソッド用クラスです。
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
            => ToDateTime((uint)unix);

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
            => ToUniversalTime(unix);

        /* ----------------------------------------------------------------- */
        ///
        /// ToDateTime
        /// 
        /// <summary>
        /// 文字列からフォーマットに従って DateTime オブジェクトへ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime ToDateTime(this string time, string format)
            => ToUniversalTime(time, format);

        /* ----------------------------------------------------------------- */
        ///
        /// ToUniversalTime
        /// 
        /// <summary>
        /// UNIX 時刻から DateTime オブジェクトへ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime ToUniversalTime(this long unix)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unix);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToUniversalTime
        /// 
        /// <summary>
        /// UNIX 時刻から DateTime オブジェクトへ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime ToUniversalTime(this int unix)
            => ToUniversalTime((uint)unix);

        /* ----------------------------------------------------------------- */
        ///
        /// ToUniversaltime
        /// 
        /// <summary>
        /// 文字列からフォーマットに従って DateTime オブジェクトへ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime ToUniversalTime(this string time, string format)
        {
            if (string.IsNullOrEmpty(time)) return DateTime.MinValue;
            return DateTime.ParseExact(time, format,
                System.Globalization.DateTimeFormatInfo.InvariantInfo,
                System.Globalization.DateTimeStyles.AssumeUniversal
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToLocalTime
        /// 
        /// <summary>
        /// UNIX 時刻から DateTime オブジェクトへ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime ToLocalTime(this long unix)
            => ToUniversalTime(unix).ToLocalTime();

        /* ----------------------------------------------------------------- */
        ///
        /// ToLocalTime
        /// 
        /// <summary>
        /// UNIX 時刻から DateTime オブジェクトへ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime ToLocalTime(this int unix)
            => ToLocalTime((uint)unix);

        /* ----------------------------------------------------------------- */
        ///
        /// ToLocalTime
        /// 
        /// <summary>
        /// 文字列からフォーマットに従って DateTime オブジェクトへ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime ToLocalTime(this string time, string format)
        {
            if (string.IsNullOrEmpty(time)) return DateTime.MinValue;
            return DateTime.ParseExact(time, format,
                System.Globalization.DateTimeFormatInfo.InvariantInfo,
                System.Globalization.DateTimeStyles.AssumeLocal
            );
        }
    }
}
