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
using System.Drawing;
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
            View.Paint      += (s, e) => OnPaint(e);
            View.MouseEnter += (s, e) => OnMouseEnter(e);
            View.MouseLeave += (s, e) => OnMouseLeave(e);
            View.MouseDown  += (s, e) => OnMouseDown(e);
            View.MouseUp    += (s, e) => OnMouseUp(e);
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

        /* ----------------------------------------------------------------- */
        ///
        /// BorderColor
        /// 
        /// <summary>
        /// ボタンを囲む境界線の色を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
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
        public int BorderSize
        {
            get { return _borderSize; }
            set { _borderSize = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsChecked
        /// 
        /// <summary>
        /// ボタンがチェック状態かどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsChecked
        {
            get { return _checked; }
            protected set { _checked = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsMouseDown
        /// 
        /// <summary>
        /// マウスがクリック状態かどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsMouseDown
        {
            get { return _mouseDown; }
            protected set { _mouseDown = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsMouseOver
        /// 
        /// <summary>
        /// マウスポインタがボタンの境界範囲内に存在するかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsMouseOver
        {
            get { return _mouseOver; }
            protected set { _mouseOver = value; }
        }

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnPaint
        /// 
        /// <summary>
        /// 描画対象となるボタンの Paint イベントを捕捉するハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPaint(PaintEventArgs e)
        {

        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseEnter
        /// 
        /// <summary>
        /// 描画対象となるボタンの MouseEnter イベントを捕捉するハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnMouseEnter(EventArgs e)
        {
            IsMouseOver = true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseLeave
        /// 
        /// <summary>
        /// 描画対象となるボタンの MouseLeave イベントを捕捉するハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnMouseLeave(EventArgs e)
        {
            IsMouseOver = false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseDown
        /// 
        /// <summary>
        /// 描画対象となるボタンの MouseDown イベントを捕捉するハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            IsMouseDown = (e.Button == MouseButtons.Left);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseUp
        /// 
        /// <summary>
        /// 描画対象となるボタンの MouseUp イベントを捕捉するハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            IsMouseDown = false;
        }

        #endregion

        #region Fields
        private ButtonBase _view = null;
        private ButtonAppearance _appearance = new ButtonAppearance();
        private Color _borderColor = Color.Empty;
        private int _borderSize = 0;
        private bool _checked = false;
        private bool _mouseOver = false;
        private bool _mouseDown = false;
        #endregion
    }
}
