/* ------------------------------------------------------------------------- */
///
/// ToggleButton.cs
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
    /// Cube.Forms.ToggleButton
    /// 
    /// <summary>
    /// トグルボタンを作成するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ToggleButton : System.Windows.Forms.CheckBox
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ToggleButton
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ToggleButton()
            : base()
        {
            _painter = new ToggleButtonPainter(this);

            BorderColor = System.Drawing.Color.Black;
            BorderSize = 1;
            base.Appearance = System.Windows.Forms.Appearance.Button;
            base.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            base.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            base.FlatAppearance.BorderSize = 0;
            base.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            base.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            base.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
            base.UseVisualStyleBackColor = false;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Surface
        ///
        /// <summary>
        /// ボタンの外観を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Surface Surface
        {
            get { return _painter.Surface; }
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
        [DefaultValue(typeof(System.Drawing.Color), "0x000000")]
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
        [DefaultValue(1)]
        public int BorderSize
        {
            get { return _painter.BorderSize; }
            set { _painter.BorderSize = value; }
        }

        #region Hiding properties

        [Browsable(false)]
        public new System.Windows.Forms.Appearance Appearance
        {
            get { return base.Appearance; }
            set { base.Appearance = value; }
        }

        [Browsable(false)]
        public new System.Windows.Forms.FlatButtonAppearance FlatAppearance
        {
            get { return base.FlatAppearance; }
        }

        [Browsable(false)]
        public new System.Windows.Forms.FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
            set { base.FlatStyle = value; }
        }

        [Browsable(false)]
        public new bool UseVisualStyleBackColor
        {
            get { return base.UseVisualStyleBackColor; }
            set { base.UseVisualStyleBackColor = value; }
        }

        [Browsable(false)]
        public new System.Drawing.ContentAlignment CheckAlign
        {
            get { return base.CheckAlign; }
            set { base.CheckAlign = value; }
        }

        #endregion

        #endregion

        #region Fields
        private ButtonPainter _painter = null;
        #endregion
    }
}
