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
using System.Threading.Tasks;
using Cube.Forms.Controls;
using Cube.Mixin.Logging;
using Cube.Mixin.Tasks;

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
        /// Value
        ///
        /// <summary>
        /// Gets or sets the notice.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Notice Value
        {
            get => _value;
            private set
            {
                if (_value == value) return;
                _value = value;

                _title.Content = value?.Title ?? string.Empty;
                _message.Content  = value?.Message ?? string.Empty;
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Busy
        ///
        /// <summary>
        /// Gets or sets a value whether the window is busy.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Busy { get; private set; } = false;

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

        #region Selected

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
        protected virtual void OnSelected(ValueEventArgs<NoticeComponent> e)
        {
            Selected?.Invoke(this, e);
            Hide();
        }

        #endregion

        #region Completed

        /* ----------------------------------------------------------------- */
        ///
        /// Completed
        ///
        /// <summary>
        /// Occurs when the notice is completed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Completed;

        /* ----------------------------------------------------------------- */
        ///
        /// OnCompleted
        ///
        /// <summary>
        /// Raises the Completed event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnCompleted(EventArgs e)
        {
            Busy = false;
            Completed?.Invoke(this, e);
        }

        #endregion

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Show
        ///
        /// <summary>
        /// Shows the window.
        /// </summary>
        ///
        /// <param name="item">Notice to show.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Show(Notice item) => Show(item, null);

        /* ----------------------------------------------------------------- */
        ///
        /// Show
        ///
        /// <summary>
        /// Shows the window.
        /// </summary>
        ///
        /// <param name="item">Notice to show.</param>
        /// <param name="style">Displayed style.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Show(Notice item, NoticeStyle style)
        {
            Value = item;
            SetStyle(style);
            ShowAsync(item.DisplayTime, item.InitialDelay).Forget();
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Show
        ///
        /// <summary>
        /// Uses this method only in the class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private new void Show()
        {
            Size = _size;
            var screen = System.Windows.Forms.Screen.GetWorkingArea(Location);
            SetDesktopLocation(screen.Width - Width - 1, screen.Height - Height - 1);
            base.Show();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ShowDialog
        ///
        /// <summary>
        /// Don't use this method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private new System.Windows.Forms.DialogResult ShowDialog() => base.ShowDialog();

        /* ----------------------------------------------------------------- */
        ///
        /// ShowAsync
        ///
        /// <summary>
        /// Shows the window with the specified time.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private async Task ShowAsync(TimeSpan time, TimeSpan delay)
        {
            var source = new System.Threading.CancellationTokenSource();
            void m(object s, EventArgs e) { if (!Visible) source.Cancel(); }
            VisibleChanged += m;

            try
            {
                Busy = true;

                this.SetTopMost(false);
                if (delay > TimeSpan.Zero) await Task.Delay(delay).ConfigureAwait(false);
                if (InvokeRequired) Invoke((Action)(() => Show()));
                else Show();
                if (time > TimeSpan.Zero) await Task.Delay(time, source.Token).ConfigureAwait(false);
                if (InvokeRequired) Invoke((Action)(() => Hide()));
                else Hide();
            }
            catch (TaskCanceledException) { /* Ignore user cancel */ }
            catch (OperationCanceledException) { /* Ignore user cancel */ }
            catch (Exception err) { this.LogWarn(err); }
            finally
            {
                VisibleChanged -= m;
                OnCompleted(EventArgs.Empty);
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// SetStyle
        ///
        /// <summary>
        /// Applies the specified style to the window.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void SetStyle(NoticeStyle style)
        {
            if (style == null) return;

            if (style.Image != null)
            {
                if (style.Image.Color != Color.Empty) _image.Styles.Default.BackColor = style.Image.Color;
                if (style.Image.Value != null) _image.Styles.Default.Image = style.Image.Value;
            }

            if (style.Title != null)
            {
                if (style.Title.Font != null) _title.Font = style.Title.Font;
                if (style.Title.Color != Color.Empty) _title.Styles.Default.ContentColor = style.Title.Color;
            }

            if (style.Message != null)
            {
                if (style.Message.Font != null) _message.Font = style.Message.Font;
                if (style.Message.Color != Color.Empty) _message.Styles.Default.ContentColor = style.Message.Color;
            }

            if (style.Color != Color.Empty)
            {
                BackColor                         = style.Color;
                _panel.BackColor                  = style.Color;
                _close.Styles.Default.BackColor   = style.Color;
                _title.Styles.Default.BackColor   = style.Color;
                _message.Styles.Default.BackColor = style.Color;
            }
        }

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

            _image.Content = string.Empty;
            _image.Dock = System.Windows.Forms.DockStyle.Fill;
            _image.Margin = new System.Windows.Forms.Padding(0);
            _image.Styles.Default.BorderSize = 0;
            _image.Styles.Default.BackColor = Color.FromArgb(230, 230, 230);
            _image.Styles.Default.Image = Properties.Resources.LogoLarge;
            _image.Click += (s, e) => OnSelected(ValueEventArgs.Create(NoticeComponent.Image));

            _title.Content = string.Empty;
            _title.Dock = System.Windows.Forms.DockStyle.Fill;
            _title.Font = FontFactory.Create(12, FontStyle.Bold, GraphicsUnit.Pixel);
            _title.Margin = new System.Windows.Forms.Padding(0);
            _title.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            _title.TextAlign = ContentAlignment.MiddleLeft;
            _title.Styles.Default.BackColor = SystemColors.Window;
            _title.Styles.Default.BorderSize = 0;
            _title.Styles.Default.ContentColor = Color.DimGray;
            _title.Click += (s, e) => OnSelected(ValueEventArgs.Create(NoticeComponent.Title));

            _message.AutoEllipsis = true;
            _message.Content = string.Empty;
            _message.Cursor = System.Windows.Forms.Cursors.Hand;
            _message.Dock = System.Windows.Forms.DockStyle.Fill;
            _message.Margin = new System.Windows.Forms.Padding(0);
            _message.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            _message.TextAlign = ContentAlignment.TopLeft;
            _message.Styles.Default.BackColor = SystemColors.Window;
            _message.Styles.Default.BorderSize = 0;
            _message.Click += (s, e) => OnSelected(ValueEventArgs.Create(NoticeComponent.Description));

            _close.Content = string.Empty;
            _close.Dock = System.Windows.Forms.DockStyle.Fill;
            _close.Margin = new System.Windows.Forms.Padding(0);
            _close.Styles.Default.BackColor = SystemColors.Window;
            _close.Styles.Default.BorderSize = 0;
            _close.Styles.Default.Image = Properties.Resources.CloseButton;
            _close.Styles.MouseOver.BackColor = Color.FromArgb(240, 240, 240);
            _close.Styles.MouseOver.BorderColor = Color.FromArgb(230, 230, 230);
            _close.Styles.MouseOver.BorderSize = 1;
            _close.Styles.MouseDown.BackColor = Color.FromArgb(236, 236, 236);
            _close.Click += (s, e) => OnSelected(ValueEventArgs.Create(NoticeComponent.Others));

            _panel.SuspendLayout();
            _panel.Dock = System.Windows.Forms.DockStyle.Fill;
            _panel.ColumnCount = 3;
            _panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            _panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            _panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            _panel.RowCount = 2;
            _panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            _panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            _panel.Controls.Add(_image, 0, 0);
            _panel.Controls.Add(_title, 1, 0);
            _panel.Controls.Add(_message,  1, 1);
            _panel.Controls.Add(_close, 2, 0);
            _panel.SetRowSpan(_image, 2);

            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor     = SystemColors.Window;
            Size          = _size;
            Font          = FontFactory.Create(12, FontStyle.Regular, GraphicsUnit.Pixel);
            Busy          = false;
            Location      = new Point(0, 0);
            MaximizeBox   = false;
            MinimizeBox   = false;
            ShowInTaskbar = false;
            Sizable       = false;

            Controls.Add(_panel);

            _panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        #region Fields
        private Notice _value;
        private readonly Size _size = new Size(350, 88);
        private readonly TableLayoutPanel _panel = new TableLayoutPanel();
        private readonly FlatButton _image = new FlatButton();
        private readonly FlatButton _title = new FlatButton();
        private readonly FlatButton _message = new FlatButton();
        private readonly FlatButton _close = new FlatButton();
        #endregion
    }
}
