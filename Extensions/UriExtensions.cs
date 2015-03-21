/* ------------------------------------------------------------------------- */
///
/// UriExtensions.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;

namespace Cube.Extensions
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Extensions.UriExtensions
    /// 
    /// <summary>
    /// Sytem.Uri の拡張クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class UriExtensions
    {
        /* ----------------------------------------------------------------- */
        ///
        /// With
        /// 
        /// <summary>
        /// Uri オブジェクトに指定したクエリーを付与します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Uri With(this Uri uri, IDictionary<string, string> query)
        {
            var builder = new UriBuilder(uri);
            foreach (var item in query)
            {
                var s = string.Format("{0}={1}", item.Key, item.Value);
                builder.Query = (builder.Query != null && builder.Query.Length > 1) ?
                    builder.Query.Substring(1) + "&" + s : s;
            }
            return builder.Uri;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// With
        /// 
        /// <summary>
        /// Uri オブジェクトに指定したクエリーを付与します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Uri With<T>(this Uri uri, string key, T value)
        {
            var query = new Dictionary<string, string>();
            query.Add(key, value.ToString());
            return With(uri, query);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// With
        /// 
        /// <summary>
        /// Uri オブジェクトに指定した時刻を付与します。
        /// </summary>
        /// 
        /// <remarks>
        /// 時刻は UnixTime に変換した上で、t=(unix) と言う形で
        /// Uri オブジェクトに付与されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static Uri With(this Uri uri, DateTime time)
        {
            return With(uri, "t", time.ToUnixTime());
        }
    }
}
