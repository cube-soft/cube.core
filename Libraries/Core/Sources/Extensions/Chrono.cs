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
namespace Cube.Chrono.Extensions;

using System;

/* ------------------------------------------------------------------------- */
///
/// Methods
///
/// <summary>
/// Provides extended methods of the DateTime and related classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Methods
{
    #region ToUnixTime

    /* --------------------------------------------------------------------- */
    ///
    /// ToUnixTime
    ///
    /// <summary>
    /// Converts the specified DateTime object to the UNIX time.
    /// </summary>
    ///
    /// <param name="src">Source object.</param>
    ///
    /// <returns>Converted UNIX time.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static long ToUnixTime(this DateTime src)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (long)src.ToUniversalTime().Subtract(epoch).TotalSeconds;
    }

    #endregion

    #region ToUniversalTime

    /* --------------------------------------------------------------------- */
    ///
    /// ToUniversalTime
    ///
    /// <summary>
    /// Converts the specified UNIX time to the DateTime object.
    /// </summary>
    ///
    /// <param name="unix">UNIX time.</param>
    ///
    /// <returns>Converted object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static DateTime ToUniversalTime(this long unix) =>
        new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unix);

    /* --------------------------------------------------------------------- */
    ///
    /// ToUniversalTime
    ///
    /// <summary>
    /// Converts the specified UNIX time to the DateTime object.
    /// </summary>
    ///
    /// <param name="unix">UNIX time.</param>
    ///
    /// <returns>Converted object.</returns>
    ///
    /// <remarks>
    /// Due to the nature of int, only dates and times up to
    /// 2106/02/07T06:28:15+0:00 can be expressed.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public static DateTime ToUniversalTime(this int unix) => ToUniversalTime((uint)unix);

    /* --------------------------------------------------------------------- */
    ///
    /// ToUniversalTime
    ///
    /// <summary>
    /// Creates a new instance of the DateTime structure with the
    /// specified value and format.
    /// </summary>
    ///
    /// <param name="src">String value that represents the time.</param>
    /// <param name="format">Conversion format.</param>
    ///
    /// <returns>DateTime object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static DateTime ToUniversalTime(this string src, string format)
    {
        var dest = DateTime.ParseExact(src, format,
                       System.Globalization.DateTimeFormatInfo.InvariantInfo,
                       System.Globalization.DateTimeStyles.AssumeUniversal |
                       System.Globalization.DateTimeStyles.AdjustToUniversal
                   );
        return new(dest.Ticks, DateTimeKind.Utc);
    }

    #endregion

    #region ToLocalTime

    /* --------------------------------------------------------------------- */
    ///
    /// ToLocalTime
    ///
    /// <summary>
    /// Converts the specified UNIX time to the DateTime object.
    /// </summary>
    ///
    /// <param name="unix">UNIX time.</param>
    ///
    /// <returns>Converted object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static DateTime ToLocalTime(this long unix) => unix.ToUniversalTime().ToLocalTime();

    /* --------------------------------------------------------------------- */
    ///
    /// ToLocalTime
    ///
    /// <summary>
    /// Converts the specified UNIX time to the DateTime object.
    /// </summary>
    ///
    /// <param name="unix">UNIX time.</param>
    ///
    /// <returns>Converted object.</returns>
    ///
    /// <remarks>
    /// Due to the nature of int, only dates and times up to
    /// 2106/02/07T06:28:15+0:00 can be expressed.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public static DateTime ToLocalTime(this int unix) => ToLocalTime((uint)unix);

    /* --------------------------------------------------------------------- */
    ///
    /// ToLocalTime
    ///
    /// <summary>
    /// Creates a new instance of the DateTime structure with the
    /// specified value and format.
    /// </summary>
    ///
    /// <param name="src">String value that represents the time.</param>
    /// <param name="format">Conversion format.</param>
    ///
    /// <returns>DateTime object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static DateTime ToLocalTime(this string src, string format)
    {
        var dest = DateTime.ParseExact(src, format,
                       System.Globalization.DateTimeFormatInfo.InvariantInfo,
                       System.Globalization.DateTimeStyles.AssumeLocal
                   );
        return new(dest.Ticks, DateTimeKind.Local);
    }

    #endregion
}
