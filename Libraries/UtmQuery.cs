/* ------------------------------------------------------------------------- */
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
namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// UtmQuery
    /// 
    /// <summary>
    /// Google Analytics のカスタムキャンペーン用のパラメータを定義したクラスです。
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
        /// プロパティにトラフィックを誘導した広告主、サイト、出版物等を識別する
        /// ための値 (utm_source) を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Source { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Medium
        ///
        /// <summary>
        /// 広告メディアやマーケティング メディアを識別するための値 (utm_medium)
        /// を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Medium { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Campaign
        ///
        /// <summary>
        /// 商品のキャンペーン名、テーマ、プロモーション コードなどを示す値
        /// (utm_campaign) を取得または設定します。
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
        /// 似通ったコンテンツや同じ広告内のリンクを区別するための値 (utm_content)
        /// を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Content { get; set; }

        #endregion
    }
}
