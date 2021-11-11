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
    /// Provides functionality to draw the appearance of the button.
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
        /// Initializes a new instance of the ButtonPainter class with the
        /// specified view.
        /// </summary>
        ///
        /// <param name="view">View object.</param>
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
        /// Get the button object to draw.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public System.Windows.Forms.ButtonBase View { get; private set; } = null;

        /* ----------------------------------------------------------------- */
        ///
        /// Content
        ///
        /// <summary>
        /// Gets or sets the content to be displayed on the button.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Content { get; set; } = string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// Style
        ///
        /// <summary>
        /// Get the object that defines the basic appearance of the button.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ButtonStyle Style { get; } = new ButtonStyle();

        /* ----------------------------------------------------------------- */
        ///
        /// Checked
        ///
        /// <summary>
        /// Gets a value indicating whether the button is in the checked
        /// state.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Checked { get; protected set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDown
        ///
        /// <summary>
        /// Gets a value indicating whether the mouse is in the click state.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool MouseDown { get; protected set; } = false;

        /* ----------------------------------------------------------------- */
        ///
        /// MouseOver
        ///
        /// <summary>
        /// Gets a value indicating whether the mouse pointer is within
        /// the boundary of the button.
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
        /// Occurs when painting a button.
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
        /// Occurs when the mouse is within the boundary of the button.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnMouseEnter(EventArgs e) => MouseOver = true;

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseLeave
        ///
        /// <summary>
        /// Occurs when the mouse leaves the boundary of the button.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnMouseLeave(EventArgs e) => MouseOver = false;

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseDown
        ///
        /// <summary>
        /// Occurs when the mouse is down.
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
        /// Occurs when the mouse is up.
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
        /// Disables the properties related to drawing the system's appearance.
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
        /// Selects the object to be used for drawing
        /// </summary>
        ///
        /// <param name="normal">Normal state.</param>
        /// <param name="check">Checked state.</param>
        /// <param name="over">Mouse over state.</param>
        /// <param name="down">Mouse down state.</param>
        /// <param name="ignore">Ignored state.</param>
        ///
        /// <remarks>
        /// The order of preference for using objects is as follows:
        ///
        ///   1. MouseDown object (down)
        ///   2. MouseOver object (over)
        ///   3. Checked object (check)
        ///   4. Normal (normal)
        ///
        /// For example, when the mouse is clicked (IsMouseDown == true),
        /// the selection method is down if the object has down enabled,
        /// and over if the object has down disabled and over enabled.
        /// If down is disabled and over is enabled, use over.
        /// And if both are disabled, use the more appropriate object
        /// between check and normal.
        ///
        /// As for whether to use the check or normal object,
        /// if the button is in the checked state (IsChecked == true) and
        /// check is a valid object, then use check, otherwise use normal.
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
        /// Gets the border color.
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
        /// GetBorderSize
        ///
        /// <summary>
        /// Gets the border size in pixels.
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
        /// Gets the background color.
        /// </summary>
        ///
        /// <remarks>
        /// If the background color is not drawn, there is a possibility that
        /// something unintended, such as FocusCue, will be drawn, so we try
        /// to return a value other than Color.Empty whenever possible.
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
        /// Gets the content color.
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
        /// Gets the image.
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
        /// Gets the background image.
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
        /// Gets the drawing area.
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
    /// Provides functionality to draw the appearance of the radio button.
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
        /// Initializes a new instance of the RadioButtonPainter class
        /// with the specified view.
        /// </summary>
        ///
        /// <param name="view">View object.</param>
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
        /// Occurs when the checked state of the button is changed.
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
    /// Provides functionality to draw the appearance of the toggle button.
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
        /// Initializes a new instance of the ToggleButtonPainter class
        /// with the specified view.
        /// </summary>
        ///
        /// <param name="view">View object.</param>
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
        /// Occurs when the checked state of the button is changed.
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
