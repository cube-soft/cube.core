﻿/* ------------------------------------------------------------------------- */
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
using System.ComponentModel;
using System.Drawing;
using Cube.Forms.Controls;
using WinForms = System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// NoticeWindow
    ///
    /// <summary>
    /// Represents the notice window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NoticeWindow : BorderlessWindow
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// NoticeWindow
        ///
        /// <summary>
        /// Initializes a new instance of the NoticeWindow class.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public NoticeWindow() { InitializeLayout(); }

        #endregion

        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Title
        ///
        /// <summary>
        /// Gets or sets the title of the window.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public string Title
        {
            get => _title.Content;
            set => _title.Content = value;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Message
        ///
        /// <summary>
        /// Gets or sets the message of the window.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public string Message
        {
            get => _message.Content;
            set => _message.Content = value;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// ShowWithoutActivation
        ///
        /// <summary>
        /// Gets a value indicating whether the window will be activated when
        /// it is shown.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected override bool ShowWithoutActivation => true;

        #region Hiding properties

        /* --------------------------------------------------------------------- */
        ///
        /// Text
        ///
        /// <summary>
        /// Don't use this property, use Title.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Don't use this property, use Title")]
        public new string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// TopMost
        ///
        /// <summary>
        /// Don't use this property.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Don't use this property")]
        public new bool TopMost
        {
            get => base.TopMost;
            set => base.TopMost = value;
        }

        #endregion

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Selected
        ///
        /// <summary>
        /// Occurs when a component is selected by user.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event ValueEventHandler<NoticeComponent> Selected;

        /* ----------------------------------------------------------------- */
        ///
        /// OnSelected
        ///
        /// <summary>
        /// Raises the Selected event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnSelected(ValueEventArgs<NoticeComponent> e) => Selected?.Invoke(this, e);

        #endregion

        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// SetLocation
        ///
        /// <summary>
        /// Sets the location corresponding to the specified value.
        /// </summary>
        ///
        /// <param name="src">Location to show.</param>
        ///
        /* --------------------------------------------------------------------- */
        public void SetLocation(NoticeLocation src)
        {
            var screen = WinForms.Screen.GetWorkingArea(Location);
            var x = src == NoticeLocation.TopLeft || src == NoticeLocation.BottomLeft ?
                    0 :
                    screen.Width - Width - 1;
            var y = src == NoticeLocation.TopLeft || src == NoticeLocation.TopRight ?
                    0 :
                    screen.Height - Height - 1;
            SetDesktopLocation(x, y);
        }

        /* --------------------------------------------------------------------- */
        ///
        /// SetStyle
        ///
        /// <summary>
        /// Applies the specified style to the window.
        /// </summary>
        ///
        /// <param name="src">User defined style.</param>
        ///
        /* --------------------------------------------------------------------- */
        public void SetStyle(NoticeStyle src)
        {
            if (src == null) return;

            if (src.Image != null)
            {
                if (src.Image.Color != Color.Empty) _image.Style.Default.BackColor = src.Image.Color;
                if (src.Image.Value != null) _image.Style.Default.Image = src.Image.Value;
            }

            if (src.Title != null)
            {
                if (src.Title.Color != Color.Empty) _title.Style.Default.ContentColor = src.Title.Color;
                if (src.Title.Font != null) _title.Font = src.Title.Font;
            }

            if (src.Message != null)
            {
                if (src.Message.Color != Color.Empty) _message.Style.Default.ContentColor = src.Message.Color;
                if (src.Message.Font != null) _message.Font = src.Message.Font;
            }

            if (src.Color != Color.Empty)
            {
                BackColor                        = src.Color;
                _panel.BackColor                 = src.Color;
                _close.Style.Default.BackColor   = src.Color;
                _title.Style.Default.BackColor   = src.Color;
                _message.Style.Default.BackColor = src.Color;
            }
        }

        #endregion

        #region Implementations

        /* --------------------------------------------------------------------- */
        ///
        /// InitializeLayout
        ///
        /// <summary>
        /// Initializes the layout of the window.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void InitializeLayout()
        {
            SuspendLayout();

            _image.SuspendLayout();
            _image.Content = string.Empty;
            _image.Dock = WinForms.DockStyle.Fill;
            _image.Margin = new(0);
            _image.Style.Default.BorderSize = 0;
            _image.Style.Default.BackColor = Color.FromArgb(230, 230, 230);
            _image.Style.Default.Image = Properties.Resources.LogoLarge;
            _image.Click += (s, e) => OnSelected(ValueEventArgs.Create(NoticeComponent.Image));
            _image.ResumeLayout(false);

            _title.SuspendLayout();
            _title.Content = string.Empty;
            _title.Dock = WinForms.DockStyle.Fill;
            _title.Font = FontFactory.Create(12, FontStyle.Bold, GraphicsUnit.Pixel);
            _title.Margin = new(0);
            _title.Padding = new(3, 0, 3, 0);
            _title.TextAlign = ContentAlignment.MiddleLeft;
            _title.Style.Default.BackColor = SystemColors.Window;
            _title.Style.Default.BorderSize = 0;
            _title.Style.Default.ContentColor = Color.DimGray;
            _title.Click += (s, e) => OnSelected(ValueEventArgs.Create(NoticeComponent.Title));
            _title.ResumeLayout(false);

            _message.SuspendLayout();
            _message.AutoEllipsis = true;
            _message.Content = string.Empty;
            _message.Cursor = WinForms.Cursors.Hand;
            _message.Dock = WinForms.DockStyle.Fill;
            _message.Margin = new(0);
            _message.Padding = new(3, 0, 3, 3);
            _message.TextAlign = ContentAlignment.TopLeft;
            _message.Style.Default.BackColor = SystemColors.Window;
            _message.Style.Default.BorderSize = 0;
            _message.Click += (s, e) => OnSelected(ValueEventArgs.Create(NoticeComponent.Description));
            _message.ResumeLayout(false);

            _close.SuspendLayout();
            _close.Content = string.Empty;
            _close.Dock = WinForms.DockStyle.Fill;
            _close.Margin = new(0);
            _close.Style.Default.BackColor = SystemColors.Window;
            _close.Style.Default.BorderSize = 0;
            _close.Style.Default.Image = Properties.Resources.CloseButton;
            _close.Style.MouseOver.BackColor = Color.FromArgb(240, 240, 240);
            _close.Style.MouseOver.BorderColor = Color.FromArgb(230, 230, 230);
            _close.Style.MouseOver.BorderSize = 1;
            _close.Style.MouseDown.BackColor = Color.FromArgb(236, 236, 236);
            _close.Click += (s, e) => OnSelected(ValueEventArgs.Create(NoticeComponent.Others));
            _close.ResumeLayout(false);

            _panel.SuspendLayout();
            _panel.Dock = WinForms.DockStyle.Fill;
            _panel.ColumnCount = 3;
            _ = _panel.ColumnStyles.Add(new(WinForms.SizeType.Absolute, 64F));
            _ = _panel.ColumnStyles.Add(new(WinForms.SizeType.Percent, 100F));
            _ = _panel.ColumnStyles.Add(new(WinForms.SizeType.Absolute, 22F));
            _panel.RowCount = 2;
            _ = _panel.RowStyles.Add(new(WinForms.SizeType.Absolute, 22F));
            _ = _panel.RowStyles.Add(new(WinForms.SizeType.Percent, 100F));
            _panel.Controls.Add(_image, 0, 0);
            _panel.Controls.Add(_title, 1, 0);
            _panel.Controls.Add(_message,  1, 1);
            _panel.Controls.Add(_close, 2, 0);
            _panel.SetRowSpan(_image, 2);
            _panel.ResumeLayout(false);

            AutoScaleMode = WinForms.AutoScaleMode.None;
            BackColor     = SystemColors.Window;
            Font          = FontFactory.Create(12, FontStyle.Regular, GraphicsUnit.Pixel);
            Location      = new(0, 0);
            Size          = new(350, 88);
            MaximizeBox   = false;
            MinimizeBox   = false;
            ShowInTaskbar = false;
            Sizable       = false;

            Controls.Add(_panel);
            ResumeLayout(false);
        }

        #endregion

        #region Fields
        private readonly TableLayoutPanel _panel = new();
        private readonly FlatButton _image = new();
        private readonly FlatButton _title = new();
        private readonly FlatButton _message = new();
        private readonly FlatButton _close = new();
        #endregion
    }
}
