/* ------------------------------------------------------------------------- */
///
/// Button.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.ComponentModel;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.Button
    /// 
    /// <summary>
    /// ボタンを作成するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Button : System.Windows.Forms.Button
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Button
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Button()
            : base()
        {
            _painter = new ButtonPainter(this);
            Reset();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Appearance
        ///
        /// <summary>
        /// ボタンの外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonAppearance Appearance
        {
            get { return _painter.Appearance; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// BorderColor
        /// 
        /// <summary>
        /// ボタンを囲む境界線の色を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public System.Drawing.Color BorderColor
        {
            get { return _painter.BorderColor; }
            set { _painter.BorderColor = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// BorderSize
        /// 
        /// <summary>
        /// ボタンを囲む境界線のサイズをピクセル単位で指定する値を
        /// 取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        public int BorderSize
        {
            get { return _painter.BorderSize; }
            set { _painter.BorderSize = value; }
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Reset
        ///
        /// <summary>
        /// 初期状態に再設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Reset()
        {
            base.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            base.UseVisualStyleBackColor = false;
        }

        #endregion

        #region Fields
        private ButtonPainter _painter = null;
        #endregion
    }
}
