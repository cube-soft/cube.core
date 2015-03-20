/* ------------------------------------------------------------------------- */
///
/// ToggleButtonPainter.cs
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
    /// Cube.Forms.ToggleButtonPainter
    /// 
    /// <summary>
    /// トグルボタンの外観を描画するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class ToggleButtonPainter : ButtonPainter
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ToggleButtonPainter
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ToggleButtonPainter(System.Windows.Forms.CheckBox view)
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
            var control = View as System.Windows.Forms.CheckBox;
            if (control == null) return;
            IsChecked = control.Checked;
        }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// InvalidateViewSurface
        /// 
        /// <summary>
        /// 外観の描画に関して CheckBox オブジェクトと競合するプロパティを
        /// 無効にします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void InvalidateViewSurface()
        {
            var radio = View as System.Windows.Forms.CheckBox;
            if (radio != null)
            {
                radio.Appearance = System.Windows.Forms.Appearance.Button;
            }
            base.InvalidateViewSurface();
        }

        #endregion
    }
}
