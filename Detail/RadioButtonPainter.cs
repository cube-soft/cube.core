/* ------------------------------------------------------------------------- */
///
/// RadioButtonPainter.cs
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
    /// Cube.Forms.RadioButtonPainter
    /// 
    /// <summary>
    /// ラジオボタンの外観を描画するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class RadioButtonPainter : ButtonPainter
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// RadioButtonPainter
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RadioButtonPainter(System.Windows.Forms.RadioButton view)
            : base(view)
        {
            view.CheckedChanged += (s, e) => OnCheckedChanged(e);
        }

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnCheckedChanged
        /// 
        /// <summary>
        /// 描画対象となるボタンの CheckedChanged イベントを捕捉する
        /// ハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            var control = View as System.Windows.Forms.RadioButton;
            if (control == null) return;
            IsChecked = control.Checked;
            control.Invalidate();
        }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// InvalidateViewSurface
        /// 
        /// <summary>
        /// 外観の描画に関して RadioButton オブジェクトと競合するプロパティを
        /// 無効にします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void InvalidateViewSurface()
        {
            var radio = View as System.Windows.Forms.RadioButton;
            if (radio != null)
            {
                radio.Appearance = System.Windows.Forms.Appearance.Button;
            }
            base.InvalidateViewSurface();
        }

        #endregion
    }
}
