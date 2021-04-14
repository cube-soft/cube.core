/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Drawing;
using Cube.Forms.Drawing;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// ButtonPainter
    ///
    /// <summary>
    /// ボタンの外観を描画するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class ButtonPainter
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
        public ButtonPainter(System.Windows.Forms.ButtonBase view)
        {
            View    = view;
            Content = view?.GetType().Name;

            View.Paint      += (s, e) => OnPaint(e);
            View.MouseEnter += (s, e) => OnMouseEnter(e);
            View.MouseLeave += (s, e) => OnMouseLeave(e);
            View.MouseDown  += (s, e) => OnMouseDown(e);
            View.MouseUp    += (s, e) => OnMouseUp(e);

            DisableSystemStyle();
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
        public System.Windows.Forms.ButtonBase View { get; private set; } = null;

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
        /// Style
        ///
        /// <summary>
        /// ボタンの基本となる外観を定義したオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ButtonStyle Style { get; } = new ButtonStyle();

        /* ----------------------------------------------------------------- */
        ///
        /// Checked
        ///
        /// <summary>
        /// ボタンがチェック状態かどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Checked { get; protected set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDown
        ///
        /// <summary>
        /// マウスがクリック状態かどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool MouseDown { get; protected set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// MouseOver
        ///
        /// <summary>
        /// マウスポインタがボタンの境界範囲内に存在するかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool MouseOver { get; protected set; } = false;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnPaint
        ///
        /// <summary>
        /// 描画対象となるボタンの Paint イベントを捕捉するハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (e == null || e.Graphics == null) return;

            var gs = e.Graphics;
            var client = View.ClientRectangle;
            var bounds = GetDrawBounds(client, View.Padding);
            gs.FillBackground(GetBackColor());
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
        protected virtual void OnMouseEnter(EventArgs e) => MouseOver = true;

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseLeave
        ///
        /// <summary>
        /// 描画対象となるボタンの MouseLeave イベントを捕捉するハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnMouseLeave(EventArgs e) => MouseOver = false;

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseDown
        ///
        /// <summary>
        /// 描画対象となるボタンの MouseDown イベントを捕捉するハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnMouseDown(System.Windows.Forms.MouseEventArgs e) =>
            MouseDown = (e.Button == System.Windows.Forms.MouseButtons.Left);

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseUp
        ///
        /// <summary>
        /// 描画対象となるボタンの MouseUp イベントを捕捉するハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            MouseDown = false;
            View.Invalidate();
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// DisableSystemStyle
        ///
        /// <summary>
        /// 外観の描画に関して ButtonBase オブジェクトと競合するプロパティを
        /// 無効にします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void DisableSystemStyle()
        {
            var color = Color.Empty;

            View.BackColor = color;
            View.ForeColor = color;
            View.BackgroundImage = null;
            View.Image = null;
            View.Text = string.Empty;
            View.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            View.FlatAppearance.BorderColor = color;
            View.FlatAppearance.BorderSize = 0;
            View.FlatAppearance.CheckedBackColor = color;
            View.FlatAppearance.MouseDownBackColor = color;
            View.FlatAppearance.MouseOverBackColor = color;
            View.UseVisualStyleBackColor = false;
        }

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
        /// <param name="ignore">一致する時に無視する値</param>
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
        private T Select<T>(T normal, T check, T over, T down, T ignore = default)
        {
            var x0 = !EqualityComparer<T>.Default.Equals(check, ignore) && Checked ? check : normal;
            var x1 = !EqualityComparer<T>.Default.Equals(over,  ignore) ? over : x0;
            var x2 = !EqualityComparer<T>.Default.Equals(down,  ignore) ? down : x1;

            return MouseDown ? x2 :
                   MouseOver ? x1 : x0;
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
            Style.Default.BorderColor,
            Style.Checked.BorderColor,
            Style.MouseOver.BorderColor,
            Style.MouseDown.BorderColor
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
            Style.Default.BorderSize,
            Style.Checked.BorderSize,
            Style.MouseOver.BorderSize,
            Style.MouseDown.BorderSize,
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
        /// <remarks>
        /// 背景色の描画を行わない場合、FocusCue 等の意図しないものが描画
        /// される可能性があるため、可能な限り Color.Empty 以外の値を返す
        /// ようにしています。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private Color GetBackColor()
        {
            var dest = Select(
                Style.Default.BackColor,
                Style.Checked.BackColor,
                Style.MouseOver.BackColor,
                Style.MouseDown.BackColor
            );

            if (dest != Color.Empty) return dest;

            for (var c = View.Parent; c != null; c = c.Parent)
            {
                if (c.BackColor != Color.Empty) return c.BackColor;
            }
            return Color.Empty;
        }

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
            Style.Default.ContentColor,
            Style.Checked.ContentColor,
            Style.MouseOver.ContentColor,
            Style.MouseDown.ContentColor
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
            Style.Default.Image,
            Style.Checked.Image,
            Style.MouseOver.Image,
            Style.MouseDown.Image
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
            Style.Default.BackgroundImage,
            Style.Checked.BackgroundImage,
            Style.MouseOver.BackgroundImage,
            Style.MouseDown.BackgroundImage
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
        private Rectangle GetDrawBounds(Rectangle client, System.Windows.Forms.Padding padding)
        {
            var x = client.Left + padding.Left;
            var y = client.Top + padding.Top;
            var width  = client.Right - padding.Right - x;
            var height = client.Bottom - padding.Bottom - y;

            return new Rectangle(x, y, width, height);
        }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// RadioButtonPainter
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
        public RadioButtonPainter(System.Windows.Forms.RadioButton view) : base(view)
        {
            view.CheckedChanged += (s, e) => OnCheckedChanged(e);
            view.Appearance = System.Windows.Forms.Appearance.Button;
            view.TextAlign = ContentAlignment.MiddleCenter;
        }

        #endregion

        #region Methods

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
            if (View is not System.Windows.Forms.RadioButton control) return;
            Checked = control.Checked;
            control.Invalidate();
        }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ToggleButtonPainter
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
        public ToggleButtonPainter(System.Windows.Forms.CheckBox view) : base(view)
        {
            view.CheckedChanged += (s, e) => OnCheckedChanged(e);
            view.Appearance = System.Windows.Forms.Appearance.Button;
            view.TextAlign = ContentAlignment.MiddleCenter;
        }

        #endregion

        #region Methods

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
            if (View is not System.Windows.Forms.CheckBox control) return;
            Checked = control.Checked;
        }

        #endregion
    }
}
