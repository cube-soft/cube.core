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
    [TypeConverter(typeof(OnlyExpandableConverter))]
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
        [DefaultValue(typeof(Color), "")]
        public Color BackColor
        {
            get => _backColor;
            set => SetProperty(ref _backColor, value);
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
        [DefaultValue(null)]
        public Image BackgroundImage
        {
            get => _backgroundImage;
            set => SetProperty(ref _backgroundImage, value);
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
        [DefaultValue(typeof(Color), "")]
        public Color BorderColor
        {
            get => _borderColor;
            set => SetProperty(ref _borderColor, value);
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
            get => _borderSize;
            set => SetProperty(ref _borderSize, value);
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
        [DefaultValue(null)]
        public Image Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
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
        [DefaultValue(typeof(Color), "")]
        public Color ContentColor
        {
            get => _contentColor;
            set => SetProperty(ref _contentColor, value);
        }

        #endregion

        #region Fields
        private Color _backColor = Color.Empty;
        private Color _borderColor = Color.Empty;
        private Color _contentColor = Color.Empty;
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
    [TypeConverter(typeof(OnlyExpandableConverter))]
    public class ButtonStyleContainer : ObservableProperty
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ButtonStyleContainer
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ButtonStyleContainer()
        {
            NormalStyle.PropertyChanged    += (s, e) => RaisePropertyChanged(nameof(NormalStyle));
            CheckedStyle.PropertyChanged   += (s, e) => RaisePropertyChanged(nameof(CheckedStyle));
            DisabledStyle.PropertyChanged  += (s, e) => RaisePropertyChanged(nameof(DisabledStyle));
            MouseOverStyle.PropertyChanged += (s, e) => RaisePropertyChanged(nameof(MouseOverStyle));
            MouseDownStyle.PropertyChanged += (s, e) => RaisePropertyChanged(nameof(MouseDownStyle));
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// NormalStyle
        ///
        /// <summary>
        /// 通常時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonStyle NormalStyle { get; } = new ButtonStyle
        {
            ContentColor = SystemColors.ControlText,
            BorderColor  = SystemColors.ActiveBorder,
            BorderSize   = 1
        };

        /* ----------------------------------------------------------------- */
        ///
        /// CheckedStyle
        ///
        /// <summary>
        /// チェック時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonStyle CheckedStyle { get; } = new ButtonStyle();

        /* ----------------------------------------------------------------- */
        ///
        /// DisabledStyle
        ///
        /// <summary>
        /// 無効時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonStyle DisabledStyle { get; } = new ButtonStyle()
        {
            BackColor    = SystemColors.Control,
            ContentColor = SystemColors.GrayText,
            BorderColor  = SystemColors.InactiveBorder
        };

        /* ----------------------------------------------------------------- */
        ///
        /// MouseOverStyle
        ///
        /// <summary>
        /// マウスオーバ時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonStyle MouseOverStyle { get; } = new ButtonStyle();

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDownStyle
        ///
        /// <summary>
        /// マウス押下時の外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonStyle MouseDownStyle { get; } = new ButtonStyle();

        #endregion
    }
}
