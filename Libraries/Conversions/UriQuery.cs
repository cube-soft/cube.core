/* ------------------------------------------------------------------------- */
///
/// UriEx.cs
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
using System.Collections.Generic;

namespace Cube.Conversions
{
    /* --------------------------------------------------------------------- */
    ///
    /// UriQuery
    /// 
    /// <summary>
    /// Sytem.Uri の拡張メソッド用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class UriQuery
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
            if (uri == null) return uri;

            var builder = new UriBuilder(uri);
            if (query?.Count > 0)
            {
                foreach (var item in query)
                {
                    var s = $"{item.Key}={item.Value}";
                    builder.Query = builder != null && builder.Query.Length > 1 ?
                                    $"{builder.Query.Substring(1)}&{s}" :
                                    s;
                }
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
            => With(uri, "t", time.ToUnixTime());

        /* ----------------------------------------------------------------- */
        ///
        /// WithVersion
        /// 
        /// <summary>
        /// Uri オブジェクトにバージョン情報を付与します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Uri With(this Uri uri, Version version, int digit)
        {
            var n = Math.Min(Math.Max(digit, 1), 4);
            return With(uri, "v", version.ToString(n));
        }
    }
}
