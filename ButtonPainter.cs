/* ------------------------------------------------------------------------- */
///
/// ButtonPainter.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.ButtonPainter
    /// 
    /// <summary>
    /// ボタンの外観を描画するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ButtonPainter
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ButtonPainter
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ButtonPainter(ButtonBase view)
        {
            View = view;
            View.MouseEnter += MouseEnterHandler;
            View.MouseLeave += MouseLeaveHandler;
            View.MouseDown  += MouseDownHandler;
            View.MouseUp    += MouseUpHandler;
            View.Paint      += PaintHandler;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// View
        /// 
        /// <summary>
        /// 描画する対象となるボタンオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ButtonBase View
        {
            get { return _view; }
            private set { _view = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Appearance
        /// 
        /// <summary>
        /// 外観を定義したオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ButtonAppearance Appearance
        {
            get { return _appearance; }
        }

        #endregion

        #region Implementations

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// PaintHandler
        /// 
        /// <summary>
        /// 描画対象となるボタンの Paint イベントが発生した時に実行される
        /// イベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void PaintHandler(object sender, PaintEventArgs e)
        {

        }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDownHandler
        /// 
        /// <summary>
        /// 描画対象となるボタンの MouseDown イベントが発生した時に実行される
        /// イベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void MouseDownHandler(object sender, MouseEventArgs e)
        {

        }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseUpHandler
        /// 
        /// <summary>
        /// 描画対象となるボタンの MouseUp イベントが発生した時に実行される
        /// イベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void MouseUpHandler(object sender, MouseEventArgs e)
        {

        }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseEnterHandler
        /// 
        /// <summary>
        /// 描画対象となるボタンの MouseEnter イベントが発生した時に
        /// 実行されるイベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void MouseEnterHandler(object sender, EventArgs e)
        {

        }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseLeaveHandler
        /// 
        /// <summary>
        /// 描画対象となるボタンの MouseLeave イベントが発生した時に
        /// 実行されるイベントハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void MouseLeaveHandler(object sender, EventArgs e)
        {

        }

        #endregion

        #endregion

        #region Fields
        private ButtonBase _view = null;
        private ButtonAppearance _appearance = new ButtonAppearance();
        #endregion
    }
}
