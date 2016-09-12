/* ------------------------------------------------------------------------- */
///
/// ButtonStyle.cs
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
using System.Drawing;
using System.ComponentModel;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// ButtonStyle
    /// 
    /// <summary>
    /// ボタンの外観を定義するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TypeConverter(typeof(NullExpandableObjectConverter))]
    public class ButtonStyle
    {
        /* ----------------------------------------------------------------- */
        ///
        /// BackColor
        ///
        /// <summary>
        /// コントロールの背景色を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Color BackColor { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// BackgroundImage
        ///
        /// <summary>
        /// コントロールの背景イメージを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Image BackgroundImage { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// BorderColor
        ///
        /// <summary>
        /// コントロールを囲む境界線の色を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Color BorderColor { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// BorderSize
        ///
        /// <summary>
        /// コントロールを囲む境界線のサイズ (ピクセル単位) を取得または
        /// 設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(-1)]
        public int BorderSize { get; set; } = -1;

        /* ----------------------------------------------------------------- */
        ///
        /// Image
        ///
        /// <summary>
        /// コントロールに表示されるイメージを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Image Image { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// ContentColor
        ///
        /// <summary>
        /// コントロール上に表示されるテキストの色を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Color ContentColor { get; set; }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ButtonStyleContainer
    /// 
    /// <summary>
    /// ボタンの外観を定義するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TypeConverter(typeof(NullExpandableObjectConverter))]
    public class ButtonStyleContainer
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Normal
        ///
        /// <summary>
        /// 通常時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TypeConverter(typeof(NullExpandableObjectConverter))]
        public ButtonStyle Normal { get; set; } = new ButtonStyle();

        /* ----------------------------------------------------------------- */
        ///
        /// Checked
        ///
        /// <summary>
        /// チェック時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TypeConverter(typeof(NullExpandableObjectConverter))]
        public ButtonStyle Checked { get; set; } = new ButtonStyle();

        /* ----------------------------------------------------------------- */
        ///
        /// Disabled
        ///
        /// <summary>
        /// 無効時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TypeConverter(typeof(NullExpandableObjectConverter))]
        public ButtonStyle Disabled { get; set; } = new ButtonStyle();

        /* ----------------------------------------------------------------- */
        ///
        /// MouseOver
        ///
        /// <summary>
        /// マウスオーバ時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TypeConverter(typeof(NullExpandableObjectConverter))]
        public ButtonStyle MouseOver { get; set; } = new ButtonStyle();

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDown
        ///
        /// <summary>
        /// マウス押下時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TypeConverter(typeof(NullExpandableObjectConverter))]
        public ButtonStyle MouseDown { get; set; } = new ButtonStyle();
    }
}
