/* ------------------------------------------------------------------------- */
///
/// NotifyStyle.cs
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
using System.Drawing;
using System.ComponentModel;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.NotifyStyle
    /// 
    /// <summary>
    /// 通知用フォームのスタイルを定義するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TypeConverter(typeof(NullExpandableObjectConverter))]
    public class NotifyStyle
    {
        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// BackColor
        /// 
        /// <summary>
        /// イメージ部分の背景色を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public Color BackColor { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// BorderColor
        /// 
        /// <summary>
        /// イメージ部分とテキスト部分の境界線色を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public Color BorderColor { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Title
        /// 
        /// <summary>
        /// タイトルのフォントを取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public Font Title { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// TitleColor
        /// 
        /// <summary>
        /// タイトルのフォント色を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public Color TitleColor { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Description
        /// 
        /// <summary>
        /// 本文のフォントを取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public Font Description { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// DescriptionColor
        /// 
        /// <summary>
        /// 本文のフォント色を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public Color DescriptionColor { get; set; }

        #endregion
    }
}
