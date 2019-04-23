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
using Cube.Mixin.String;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cube.Conversions
{
    /* --------------------------------------------------------------------- */
    ///
    /// UriFormat
    ///
    /// <summary>
    /// Sytem.Uri の拡張用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class UriFormat
    {
        #region Methods

        #region ToUri

        /* ----------------------------------------------------------------- */
        ///
        /// ToUri
        ///
        /// <summary>
        /// 文字列を Uri オブジェクトに変換します。
        /// </summary>
        ///
        /// <param name="src">URL を示す文字列</param>
        ///
        /// <returns>Uri オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Uri ToUri(this string src) =>
           !src.HasValue()       ? default(Uri) :
            src.Contains("://")  ? new Uri(src) :
            src.StartsWith("//") ? new Uri("http:" + src) :
            src.StartsWith("/")  ? new Uri("http://localhost" + src) :
                                   new Uri("http://" + src);

        #endregion

        #region With

        /* ----------------------------------------------------------------- */
        ///
        /// With
        ///
        /// <summary>
        /// Uri オブジェクトに指定したクエリーを付与します。
        /// </summary>
        ///
        /// <param name="uri">Uri オブジェクト</param>
        /// <param name="query">クエリー一覧</param>
        ///
        /// <returns>
        /// クエリーが付与された Uri オブジェクト
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Uri With(this Uri uri, IDictionary<string, string> query)
        {
            if (uri == null || query == null || query.Count <= 0) return uri;

            var dest = new UriBuilder(uri);
            var str  = string.Join("&", query.Select(x => $"{x.Key}={x.Value}").ToArray());
            dest.Query = dest.Query.Length > 1 ?
                         $"{dest.Query.Substring(1)}&{str}" :
                         str;
            return dest.Uri;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// With
        ///
        /// <summary>
        /// Uri オブジェクトに指定したクエリーを付与します。
        /// </summary>
        ///
        /// <param name="uri">Uri オブジェクト</param>
        /// <param name="key">クエリーのキー</param>
        /// <param name="value">クエリーの値</param>
        ///
        /// <returns>
        /// クエリーが付与された Uri オブジェクト
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Uri With<T>(this Uri uri, string key, T value) =>
            With(uri, new Dictionary<string, string> {{ key, value.ToString() }});

        /* ----------------------------------------------------------------- */
        ///
        /// With
        ///
        /// <summary>
        /// Uri オブジェクトに指定した時刻を付与します。
        /// </summary>
        ///
        /// <param name="uri">Uri オブジェクト</param>
        /// <param name="time">時刻</param>
        ///
        /// <returns>
        /// クエリーが付与された Uri オブジェクト
        /// </returns>
        ///
        /// <remarks>
        /// 時刻は UnixTime に変換した上で、ts=(unix) と言う形で
        /// Uri オブジェクトに付与されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static Uri With(this Uri uri, DateTime? time) =>
            With(uri, "ts", time.ToUnixTime());

        /* ----------------------------------------------------------------- */
        ///
        /// With
        ///
        /// <summary>
        /// Uri オブジェクトにバージョン情報を付与します。
        /// </summary>
        ///
        /// <param name="uri">Uri オブジェクト</param>
        /// <param name="version">バージョン情報</param>
        ///
        /// <returns>
        /// クエリーが付与された Uri オブジェクト
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Uri With(this Uri uri, SoftwareVersion version) =>
            With(uri, "ver", version.ToString(false));

        /* ----------------------------------------------------------------- */
        ///
        /// With
        ///
        /// <summary>
        /// Uri オブジェクトに UTM クエリーの情報を付与します。
        /// </summary>
        ///
        /// <param name="uri">Uri オブジェクト</param>
        /// <param name="utm">UTM クエリー</param>
        ///
        /// <returns>
        /// クエリーが付与された Uri オブジェクト
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Uri With(this Uri uri, UtmQuery utm)
        {
            if (utm == null) return uri;

            var query = new Dictionary<string, string>();
            if (utm.Source.HasValue()) query.Add("utm_source", utm.Source);
            if (utm.Medium.HasValue()) query.Add("utm_medium", utm.Medium);
            if (utm.Campaign.HasValue()) query.Add("utm_campaign", utm.Campaign);
            if (utm.Term.HasValue()) query.Add("utm_term", utm.Term);
            if (utm.Content.HasValue()) query.Add("utm_content", utm.Content);
            return With(uri, query);
        }

        #endregion

        #region WithoutQuery

        /* ----------------------------------------------------------------- */
        ///
        /// WithoutQuery
        ///
        /// <summary>
        /// クエリー部分を除去した Uri オブジェクトを取得します。
        /// </summary>
        ///
        /// <param name="uri">Uri オブジェクト</param>
        ///
        /// <returns>
        /// クエリーが除去された Uri オブジェクト
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Uri WithoutQuery(this Uri uri) =>
            new Uri(uri.GetLeftPart(UriPartial.Path));

        #endregion

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// UtmQuery
    ///
    /// <summary>
    /// Google Analytics のカスタムキャンペーン用のパラメータを定義した
    /// クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class UtmQuery
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Source
        ///
        /// <summary>
        /// プロパティにトラフィックを誘導した広告主、サイト、出版物等を
        /// 識別するための値 (utm_source) を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Source { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Medium
        ///
        /// <summary>
        /// 広告メディアやマーケティング メディアを識別するための
        /// 値 (utm_medium) を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Medium { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Campaign
        ///
        /// <summary>
        /// 商品のキャンペーン名、テーマ、プロモーション コードなどを
        /// 示す値 (utm_campaign) を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Campaign { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Term
        ///
        /// <summary>
        /// 検索広告のキーワード (utm_term) を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Term { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Content
        ///
        /// <summary>
        /// 似通ったコンテンツや同じ広告内のリンクを区別するための
        /// 値 (utm_content) を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Content { get; set; }

        #endregion
    }
}
