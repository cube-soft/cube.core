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
    public class ButtonStyle : ObservableProperty
    {
        #region Properties

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
        [DefaultValue(typeof(Color), "Control")]
        public Color BackColor
        {
            get { return _backColor; }
            set { SetProperty(ref _backColor, value); }
        }

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
        public Image BackgroundImage
        {
            get { return _backgroundImage; }
            set { SetProperty(ref _backgroundImage, value); }
        }

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
        [DefaultValue(typeof(Color), "ActiveBorder")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { SetProperty(ref _borderColor, value); }
        }

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
        public int BorderSize
        {
            get { return _borderSize; }
            set { SetProperty(ref _borderSize, value); }
        }

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
        public Image Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

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
        [DefaultValue(typeof(Color), "ControlText")]
        public Color ContentColor
        {
            get { return _contentColor; }
            set { SetProperty(ref _contentColor, value); }
        }

        #endregion

        #region Fields
        private Color _backColor = SystemColors.Control;
        private Color _borderColor = SystemColors.ActiveBorder;
        private Color _contentColor = SystemColors.ControlText;
        private Image _backgroundImage = null;
        private Image _image = null;
        private int _borderSize = -1;
        #endregion
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
        /// NormalStyle
        ///
        /// <summary>
        /// 通常時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TypeConverter(typeof(NullExpandableObjectConverter))]
        public ButtonStyle NormalStyle { get; set; } = new ButtonStyle();

        /* ----------------------------------------------------------------- */
        ///
        /// CheckedStyle
        ///
        /// <summary>
        /// チェック時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TypeConverter(typeof(NullExpandableObjectConverter))]
        public ButtonStyle CheckedStyle { get; set; } = new ButtonStyle();

        /* ----------------------------------------------------------------- */
        ///
        /// DisabledStyle
        ///
        /// <summary>
        /// 無効時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TypeConverter(typeof(NullExpandableObjectConverter))]
        public ButtonStyle DisabledStyle { get; set; } = new ButtonStyle();

        /* ----------------------------------------------------------------- */
        ///
        /// MouseOverStyle
        ///
        /// <summary>
        /// マウスオーバ時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TypeConverter(typeof(NullExpandableObjectConverter))]
        public ButtonStyle MouseOverStyle { get; set; } = new ButtonStyle();

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDownStyle
        ///
        /// <summary>
        /// マウス押下時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TypeConverter(typeof(NullExpandableObjectConverter))]
        public ButtonStyle MouseDownStyle { get; set; } = new ButtonStyle();
    }
}
