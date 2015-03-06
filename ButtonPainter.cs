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
using System.Collections.Generic;
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
            View.Refresh();
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Select
        /// 
        /// <summary>
        /// 描画に使用するオブジェクトを選択して返します。
        /// </summary>
        /// 
        /// <param name="normal">通常時のオブジェクト</param>
        /// <param name="check">ボタンがチェック状態のオブジェクト</param>
        /// <param name="over">マウスオーバ時のオブジェクト</param>
        /// <param name="down">マウスクリック時のオブジェクト</param>
        /// 
        /// <remarks>
        /// オブジェクトを使用する優先順位は以下の通りです。
        ///
        ///   1. マウスクリック時のオブジェクト (down)
        ///   2. マウスオーバ時のオブジェクト (over)
        ///   3. ボタンがチェック状態のオブジェクト (check)
        ///   4. 通常時のオブジェクト (normal)
        /// 
        /// 例えば、マウスクリック時 (IsMouseDown == true) の選択方法は、
        /// down が有効なオブジェクトであれば down を使用し、down が無効で
        /// over が有効なオブジェクトであれば over を使用します。
        /// そして、両方とも無効なオブジェクトであれば check と normal の
        /// 内、より適切なオブジェクトを使用します。
        /// 
        /// check と normal どちらのオブジェクトを使用するかについては、
        /// ボタンがチェック状態 (IsChecked == true) であり、かつ、check が
        /// 有効なオブジェクトであれば check を、それ以外の場合は normal を
        /// 使用します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private T Select<T>(T normal, T check, T over, T down)
        {
            var x0 = IsChecked && !EqualityComparer<T>.Default.Equals(check, default(T)) ? check : normal;
            var x1 = !EqualityComparer<T>.Default.Equals(over, default(T)) ? over : default(T);
            var x2 = !EqualityComparer<T>.Default.Equals(down, default(T)) ? down : over;

            var dest = IsMouseDown ? x2 :
                       IsMouseOver ? x1 : x0;
            return !EqualityComparer<T>.Default.Equals(dest, default(T)) ? dest : normal;
        }

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
            return Select(BorderColor,
                          Appearance.CheckedBorderColor,
                          Appearance.MouseOverBorderColor,
                          Appearance.MouseDownBorderColor);
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
            return Select(View.BackColor,
                          Appearance.CheckedBackColor,
                          Appearance.MouseOverBackColor,
                          Appearance.MouseDownBackColor);
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
            return Select(View.Image,
                          Appearance.CheckedImage,
                          Appearance.MouseOverImage,
                          Appearance.MouseDownImage);
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
            return Select(View.BackgroundImage,
                          Appearance.CheckedBackgroundImage,
                          Appearance.MouseOverBackgroundImage,
                          Appearance.MouseDownBackgroundImage);
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
