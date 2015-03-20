/* ------------------------------------------------------------------------- */
///
/// UnixTimeExtensions.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;

namespace Cube.Extensions
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Extensions.UnixTimeExtensions
    /// 
    /// <summary>
    /// DateTiem と UnixTime の相互変換を行うための拡張クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    static class UnixTimeExtensions
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
        public static int ToUnixTime(this DateTime time)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var utc = time.ToUniversalTime();
            return (int)utc.Subtract(epoch).TotalSeconds;
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
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unix).ToLocalTime();
        }
    }
}
