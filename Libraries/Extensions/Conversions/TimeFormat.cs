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

namespace Cube.Conversions
{
    /* --------------------------------------------------------------------- */
    ///
    /// TimeFormat
    ///
    /// <summary>
    /// DateTiem オブジェクトの変換を行う拡張用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class TimeFormat
    {
        #region Methods

        #region ToUnixTime

        /* ----------------------------------------------------------------- */
        ///
        /// ToUnixTime
        ///
        /// <summary>
        /// DateTime? オブジェクトから UNIX 時刻へ変換します。
        /// </summary>
        ///
        /// <param name="time">対象となる DateTime? オブジェクト</param>
        ///
        /// <returns>変換後の UNIX 時刻</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static long ToUnixTime(this DateTime? time)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(time?.ToUniversalTime().Subtract(epoch).TotalSeconds ?? 0.0);
        }

        #endregion

        #region ToDateTime

        /* ----------------------------------------------------------------- */
        ///
        /// ToDateTime
        ///
        /// <summary>
        /// UNIX 時刻から DateTime? オブジェクトへ変換します。
        /// </summary>
        ///
        /// <param name="unix">対象となる UNIX 時刻</param>
        ///
        /// <returns>変換後の DateTime? オブジェクト</returns>
        ///
        /// <remarks>
        /// int の性質上、2106/02/07T06:28:15+0:00 までの日時しか表現
        /// する事ができません。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime? ToDateTime(this int unix) =>
            ToUniversalTime(unix);

        /* ----------------------------------------------------------------- */
        ///
        /// ToDateTime
        ///
        /// <summary>
        /// UNIX 時刻から DateTime? オブジェクトへ変換します。
        /// </summary>
        ///
        /// <param name="unix">対象となる UNIX 時刻</param>
        ///
        /// <returns>変換後の DateTime? オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime? ToDateTime(this long unix) =>
            ToUniversalTime(unix);

        /* ----------------------------------------------------------------- */
        ///
        /// ToDateTime
        ///
        /// <summary>
        /// 文字列からフォーマットに従って DateTime? オブジェクトへ変換します。
        /// </summary>
        ///
        /// <param name="time">変換対象となる文字列</param>
        /// <param name="format">変換書式</param>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime? ToDateTime(this string time, string format) =>
            ToUniversalTime(time, format);

        #endregion

        #region ToUniversalTime

        /* ----------------------------------------------------------------- */
        ///
        /// ToUniversalTime
        ///
        /// <summary>
        /// UNIX 時刻から DateTime? オブジェクトへ変換します。
        /// </summary>
        ///
        /// <param name="unix">対象となる UNIX 時刻</param>
        ///
        /// <returns>変換後の DateTime? オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime? ToUniversalTime(this long unix) =>
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unix);

        /* ----------------------------------------------------------------- */
        ///
        /// ToUniversalTime
        ///
        /// <summary>
        /// UNIX 時刻から DateTime? オブジェクトへ変換します。
        /// </summary>
        ///
        /// <param name="unix">対象となる UNIX 時刻</param>
        ///
        /// <returns>変換後の DateTime? オブジェクト</returns>
        ///
        /// <remarks>
        /// int の性質上、2106/02/07T06:28:15+0:00 までの日時しか表現
        /// する事ができません。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime? ToUniversalTime(this int unix) =>
            ToUniversalTime((uint)unix);

        /* ----------------------------------------------------------------- */
        ///
        /// ToUniversaltime
        ///
        /// <summary>
        /// 文字列からフォーマットに従って DateTime? オブジェクトへ
        /// 変換します。
        /// </summary>
        ///
        /// <param name="time">変換対象となる文字列</param>
        /// <param name="format">変換書式</param>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime? ToUniversalTime(this string time, string format)
        {
            if (string.IsNullOrEmpty(time)) return default(DateTime?);
            var dest = DateTime.ParseExact(time, format,
                           System.Globalization.DateTimeFormatInfo.InvariantInfo,
                           System.Globalization.DateTimeStyles.AssumeUniversal |
                           System.Globalization.DateTimeStyles.AdjustToUniversal
                       );
            return new DateTime(dest.Ticks, DateTimeKind.Utc);
        }

        #endregion

        #region ToLocalTime

        /* ----------------------------------------------------------------- */
        ///
        /// ToLocalTime
        ///
        /// <summary>
        /// UNIX 時刻から DateTime? オブジェクトへ変換します。
        /// </summary>
        ///
        /// <param name="unix">対象となる UNIX 時刻</param>
        ///
        /// <returns>変換後の DateTime? オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime? ToLocalTime(this long unix)
        {
            var dest = ToUniversalTime(unix);
            System.Diagnostics.Debug.Assert(dest.HasValue);
            return dest.Value.ToLocalTime();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToLocalTime
        ///
        /// <summary>
        /// UNIX 時刻から DateTime? オブジェクトへ変換します。
        /// </summary>
        ///
        /// <param name="unix">対象となる UNIX 時刻</param>
        ///
        /// <returns>変換後の DateTime? オブジェクト</returns>
        ///
        /// <remarks>
        /// int の性質上、2106/02/07T06:28:15+0:00 までの日時しか表現
        /// する事ができません。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime? ToLocalTime(this int unix) =>
            ToLocalTime((uint)unix);

        /* ----------------------------------------------------------------- */
        ///
        /// ToLocalTime
        ///
        /// <summary>
        /// 文字列からフォーマットに従って DateTime? オブジェクトへ
        /// 変換します。
        /// </summary>
        ///
        /// <param name="time">対象となる文字列</param>
        /// <param name="format">変換書式</param>
        ///
        /// <returns>変換後の DateTime? オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime? ToLocalTime(this string time, string format)
        {
            if (string.IsNullOrEmpty(time)) return default(DateTime?);
            var dest = DateTime.ParseExact(time, format,
                           System.Globalization.DateTimeFormatInfo.InvariantInfo,
                           System.Globalization.DateTimeStyles.AssumeLocal
                       );
            return new DateTime(dest.Ticks, DateTimeKind.Local);
        }

        #endregion

        #endregion
    }
}
