/* ------------------------------------------------------------------------- */
///
/// FlatButtonPainter.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Cube.Forms.Drawing;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// FlatButtonPainter
    /// 
    /// <summary>
    /// ボタンの外観を描画するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class FlatButtonPainter
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
        public FlatButtonPainter(ButtonBase view)
        {
            View = view;
            View.Paint      += (s, e) => OnPaint(e);
            View.MouseEnter += (s, e) => OnMouseEnter(e);
            View.MouseLeave += (s, e) => OnMouseLeave(e);
            View.MouseDown  += (s, e) => OnMouseDown(e);
            View.MouseUp    += (s, e) => OnMouseUp(e);

            InitializeSurface();
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
        public ButtonBase View { get; private set; } = null;

        /* ----------------------------------------------------------------- */
        ///
        /// Content
        /// 
        /// <summary>
        /// ボタンに表示する内容を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Content { get; set; } = string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// Surface
        /// 
        /// <summary>
        /// ボタンの基本となる外観を定義したオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ButtonStyleContainer Styles { get; } = new ButtonStyleContainer();

        /* ----------------------------------------------------------------- */
        ///
        /// IsChecked
        /// 
        /// <summary>
        /// ボタンがチェック状態かどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsChecked { get; protected set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// IsMouseDown
        /// 
        /// <summary>
        /// マウスがクリック状態かどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsMouseDown { get; protected set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// IsMouseOver
        /// 
        /// <summary>
        /// マウスポインタがボタンの境界範囲内に存在するかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsMouseOver { get; protected set; } = false;

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
            if (e == null || e.Graphics == null) return;

            var gs = e.Graphics;
            var client = View.ClientRectangle;
            var bounds = GetDrawBounds(client, View.Padding);
            gs.FillBackground(client, GetBackColor());
            gs.DrawImage(client, GetBackgroundImage(), View.BackgroundImageLayout);
            gs.DrawImage(bounds, GetImage(), View.ImageAlign);
            gs.DrawText(bounds, Content, View.Font, GetContentColor(), View.TextAlign);
            gs.DrawBorder(client, GetBorderColor(), GetBorderSize());
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
            => IsMouseOver = true;

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
            => IsMouseOver = false;

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
            => IsMouseDown = (e.Button == MouseButtons.Left);

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
            View.Invalidate();
        }

        #endregion

        #region Initialize methods

        /* ----------------------------------------------------------------- */
        ///
        /// InitializeSurface
        /// 
        /// <summary>
        /// 外観の描画に関して ButtonBase オブジェクトと競合するプロパティを
        /// 無効にします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void InitializeSurface()
        {
            var transparent = Color.FromArgb(0, 255, 255, 255);

            View.BackColor = transparent;
            View.ForeColor = transparent;
            View.BackgroundImage = null;
            View.Image = null;
            View.Text = string.Empty;
            View.FlatStyle = FlatStyle.Flat;
            View.FlatAppearance.BorderColor = transparent;
            View.FlatAppearance.BorderSize = 0;
            View.FlatAppearance.CheckedBackColor = transparent;
            View.FlatAppearance.MouseDownBackColor = transparent;
            View.FlatAppearance.MouseOverBackColor = transparent;
            View.UseVisualStyleBackColor = false;
        }

        #endregion

        #region Others

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
        private T Select<T>(T normal, T check, T over, T down, T ignore = default(T))
        {
            var x0 = !EqualityComparer<T>.Default.Equals(check, ignore) && IsChecked ? check : normal;
            var x1 = !EqualityComparer<T>.Default.Equals(over,  ignore) ? over : x0;
            var x2 = !EqualityComparer<T>.Default.Equals(down,  ignore) ? down : x1;

            return IsMouseDown ? x2 :
                   IsMouseOver ? x1 : x0;
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
        private Color GetBorderColor() => Select(
            Styles.Normal.BorderColor,
            Styles.Checked.BorderColor,
            Styles.MouseOver.BorderColor,
            Styles.MouseDown.BorderColor
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetBorderColor
        /// 
        /// <summary>
        /// 現在の境界線のサイズ (ピクセル単位) を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private int GetBorderSize() => Select(
            Styles.Normal.BorderSize,
            Styles.Checked.BorderSize,
            Styles.MouseOver.BorderSize,
            Styles.MouseDown.BorderSize,
            -1
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetBackColor
        /// 
        /// <summary>
        /// 現在の背景色を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Color GetBackColor() => Select(
            Styles.Normal.BackColor,
            Styles.Checked.BackColor,
            Styles.MouseOver.BackColor,
            Styles.MouseDown.BackColor
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetContentColor
        /// 
        /// <summary>
        /// 現在のテキスト色を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Color GetContentColor() => Select(
            Styles.Normal.ContentColor,
            Styles.Checked.ContentColor,
            Styles.MouseOver.ContentColor,
            Styles.MouseDown.ContentColor
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetImage
        /// 
        /// <summary>
        /// 現在のイメージを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Image GetImage() => Select(
            Styles.Normal.Image,
            Styles.Checked.Image,
            Styles.MouseOver.Image,
            Styles.MouseDown.Image
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetBackgroundImage
        /// 
        /// <summary>
        /// 現在の背景イメージを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Image GetBackgroundImage() => Select(
            Styles.Normal.BackgroundImage,
            Styles.Checked.BackgroundImage,
            Styles.MouseOver.BackgroundImage,
            Styles.MouseDown.BackgroundImage
        );

        /* ----------------------------------------------------------------- */
        ///
        /// GetDrawBounds
        /// 
        /// <summary>
        /// 描画領域を表すオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Rectangle GetDrawBounds(Rectangle client, Padding padding)
        {
            var x = client.Left + padding.Left;
            var y = client.Top + padding.Top;
            var width  = client.Right - padding.Right - x;
            var height = client.Bottom - padding.Bottom - y;

            return new Rectangle(x, y, width, height);
        }

        #endregion
    }
}
