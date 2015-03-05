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
using Cube.Extensions.Forms;

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
        /// <remarks>
        /// TODO:
        ///   - テキストの描画処理を実装する
        ///   - 背景イメージの描画の内、ImageLayout が Stretch, Zoom, Tile
        ///     が未実装なので実装する
        ///     ※ 実装場所は Cube.Extensions.Forms.GraphicsExtensions
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPaint(PaintEventArgs e)
        {
            if (e == null || e.Graphics == null) return;

            var gs = e.Graphics;
            gs.FillBackground(GetBackColor());
            gs.DrawImage(GetBackgroundImage(), View.BackgroundImageLayout);
            gs.DrawImage(GetImage(), View.ImageAlign);
            gs.DrawBorder(GetBorderColor(), BorderSize);
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

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// GetBorderColor
        /// 
        /// <summary>
        /// 現在の境界線の色を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Color GetBorderColor()
        {
            return IsChecked   ? Appearance.CheckedBorderColor   :
                   IsMouseDown ? Appearance.MouseDownBorderColor :
                   IsMouseOver ? Appearance.MouseOverBorderColor :
                   BorderColor;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetBackColor
        /// 
        /// <summary>
        /// 現在の背景色を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Color GetBackColor()
        {
            return IsChecked   ? Appearance.CheckedBackColor   :
                   IsMouseDown ? Appearance.MouseDownBackColor :
                   IsMouseOver ? Appearance.MouseOverBackColor :
                   View.BackColor;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetImage
        /// 
        /// <summary>
        /// 現在のイメージを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Image GetImage()
        {
            return IsChecked   ? Appearance.CheckedImage   :
                   IsMouseDown ? Appearance.MouseDownImage :
                   IsMouseOver ? Appearance.MouseOverImage :
                   View.Image;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetBackgroundImage
        /// 
        /// <summary>
        /// 現在の背景イメージを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Image GetBackgroundImage()
        {
            return IsChecked   ? Appearance.CheckedBackgroundImage   :
                   IsMouseDown ? Appearance.MouseDownBackgroundImage :
                   IsMouseOver ? Appearance.MouseOverBackgroundImage :
                   View.BackgroundImage;
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
