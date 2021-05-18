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
        public NoticeWindow() : this(SystemIcons.Information) { }

        /* --------------------------------------------------------------------- */
        ///
        /// NoticeWindow
        ///
        /// <summary>
        /// Initializes a new instance of the NoticeWindow class with the
        /// specified icon.
        /// </summary>
        ///
        /// <param name="icon">Icon object.</param>
        ///
        /* --------------------------------------------------------------------- */
        public NoticeWindow(Icon icon)
        {
            Icon = icon;
            InitializeLayout();
        }

        #endregion

        #region Properties

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
        [Obsolete("Use SetTopMost extended method, instead of this.")]
        public new bool TopMost
        {
            get => base.TopMost;
            set => base.TopMost = value;
        }

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
        public event ValueEventHandler<NoticeResult> Selected;

        /* ----------------------------------------------------------------- */
        ///
        /// OnSelected
        ///
        /// <summary>
        /// Raises the Selected event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnSelected(ValueEventArgs<NoticeResult> e) => Selected?.Invoke(this, e);

        #endregion

        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// Set
        ///
        /// <summary>
        /// Sets the message.
        /// </summary>
        ///
        /// <param name="text">Message to show.</param>
        /// <param name="title">Title of the window.</param>
        ///
        /* --------------------------------------------------------------------- */
        public void Set(string text, string title)
        {
            _text.Content  = text;
            _title.Content = title;
            Text = title;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Set
        ///
        /// <summary>
        /// Sets the location corresponding to the specified value.
        /// </summary>
        ///
        /// <param name="src">Location to show.</param>
        ///
        /* --------------------------------------------------------------------- */
        public void Set(NoticeLocation src)
        {
            var area = WinForms.Screen.GetWorkingArea(Location);
            var x = src == NoticeLocation.TopLeft || src == NoticeLocation.BottomLeft ?
                    0 :
                    area.Width - Width - 1;
            var y = src == NoticeLocation.TopLeft || src == NoticeLocation.TopRight ?
                    0 :
                    area.Height - Height - 1;
            SetDesktopLocation(x, y);
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Set
        ///
        /// <summary>
        /// Applies the specified style to the window.
        /// </summary>
        ///
        /// <param name="src">User defined style.</param>
        ///
        /* --------------------------------------------------------------------- */
        public void Set(NoticeStyle src)
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

            if (src.Text != null)
            {
                if (src.Text.Color != Color.Empty) _text.Style.Default.ContentColor = src.Text.Color;
                if (src.Text.Font != null) _text.Font = src.Text.Font;
            }

            if (src.Color != Color.Empty)
            {
                BackColor                      = src.Color;
                _panel.BackColor               = src.Color;
                _close.Style.Default.BackColor = src.Color;
                _title.Style.Default.BackColor = src.Color;
                _text.Style.Default.BackColor  = src.Color;
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
            _image.Click += (s, e) => OnSelected(ValueEventArgs.Create(NoticeResult.Image));
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
            _title.Click += (s, e) => OnSelected(ValueEventArgs.Create(NoticeResult.Title));
            _title.ResumeLayout(false);

            _text.SuspendLayout();
            _text.AutoEllipsis = true;
            _text.Content = string.Empty;
            _text.Cursor = WinForms.Cursors.Hand;
            _text.Dock = WinForms.DockStyle.Fill;
            _text.Margin = new(0);
            _text.Padding = new(3, 0, 3, 3);
            _text.TextAlign = ContentAlignment.TopLeft;
            _text.Style.Default.BackColor = SystemColors.Window;
            _text.Style.Default.BorderSize = 0;
            _text.Click += (s, e) => OnSelected(ValueEventArgs.Create(NoticeResult.Text));
            _text.ResumeLayout(false);

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
            _close.Click += (s, e) => OnSelected(ValueEventArgs.Create(NoticeResult.Close));
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
            _panel.Controls.Add(_text,  1, 1);
            _panel.Controls.Add(_close, 2, 0);
            _panel.SetRowSpan(_image, 2);
            _panel.ResumeLayout(false);

            AutoScaleMode = WinForms.AutoScaleMode.None;
            BackColor     = SystemColors.Window;
            Font          = FontFactory.Create(12, FontStyle.Regular, GraphicsUnit.Pixel);
            MaximizeBox   = false;
            MinimizeBox   = false;
            ShowInTaskbar = false;
            ClientSize    = new(350, 88);
            Sizable       = false;

            Controls.Add(_panel);
            ResumeLayout(false);
        }

        #endregion

        #region Fields
        private readonly TableLayoutPanel _panel = new();
        private readonly FlatButton _image = new();
        private readonly FlatButton _title = new();
        private readonly FlatButton _text = new();
        private readonly FlatButton _close = new();
        #endregion
    }
}
