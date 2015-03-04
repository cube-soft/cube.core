/* ------------------------------------------------------------------------- */
///
/// ButtonAppearance.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.Drawing;
using System.ComponentModel;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.ButtonAppearance
    /// 
    /// <summary>
    /// ボタンの外観を定義するためのクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// 通常時の外観は、それぞれ ButtonBase クラスの BackColor, Image,
    /// BackgroundImage を参照する事とします。また、通常時の BorderColor に
    /// ついては、ButtonBase を継承したクラスで定義されているものとします。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    [TypeConverter(typeof(NullExpandableObjectConverter))]
    public class ButtonAppearance
    {
        /* ----------------------------------------------------------------- */
        ///
        /// CheckedBorderColor
        ///
        /// <summary>
        /// ボタンがチェック状態になっている時の、ボタンを囲む境界線の色を
        /// 取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Color CheckedBorderColor { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// CheckedBackColor
        ///
        /// <summary>
        /// ボタンがチェック状態になっている時の、背景色を取得または
        /// 設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Color CheckedBackColor { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// CheckedImage
        ///
        /// <summary>
        /// ボタンがチェック状態になっている時の、イメージを取得または
        /// 設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Image CheckedImage { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// CheckedBackgroundImage
        ///
        /// <summary>
        /// ボタンがチェック状態になっている時の、背景イメージを取得または
        /// 設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Image CheckedBackgroundImage { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDownBorderColor
        ///
        /// <summary>
        /// マウスクリック時の境界線の色を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Color MouseDownBorderColor { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDownBackColor
        ///
        /// <summary>
        /// マウスクリック時の背景色を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Color MouseDownBackColor { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDownImage
        ///
        /// <summary>
        /// マウスクリック時のイメージを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Image MouseDownImage { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDownBackgroundImage
        ///
        /// <summary>
        /// マウスクリック時の背景イメージを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Image MouseDownBackgroundImage { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseOverBorderColor
        ///
        /// <summary>
        /// マウスオーバ時の境界線の色を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Color MouseOverBorderColor { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseOverBackColor
        ///
        /// <summary>
        /// マウスオーバ時の背景色を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Color MouseOverBackColor { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseOverImage
        ///
        /// <summary>
        /// マウスオーバ時のイメージを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Image MouseOverImage { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseOverBackgroundImage
        ///
        /// <summary>
        /// マウスオーバ時の背景イメージを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public Image MouseOverBackgroundImage { get; set; }
    }
}
