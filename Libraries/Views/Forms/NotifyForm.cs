/* ------------------------------------------------------------------------- */
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
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using Cube.Log;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// NotifyForm
    /// 
    /// <summary>
    /// 通知用フォームを表すクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NotifyForm : BorderlessForm, INotifyView
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// NotifyForm
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public NotifyForm() : base()
        {
            InitializeLayout();
        }

        #endregion

        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Value
        /// 
        /// <summary>
        /// 通知内容を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NotifyItem Value
        {
            get { return _value; }
            set
            {
                if (_value == value) return;
                _value = value;

                _title.Content = value?.Title ?? string.Empty;
                _text.Content  = value?.Description ?? string.Empty;
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// AutoEllipsis
        /// 
        /// <summary>
        /// 枠に入りきらない本文を自動的に省略するかどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public bool AutoEllipsis
        {
            get { return _text.AutoEllipsis; }
            set { _text.AutoEllipsis = value; }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// IsBusy
        /// 
        /// <summary>
        /// 実行中かどうかを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        public bool IsBusy { get; private set; }

        /* --------------------------------------------------------------------- */
        ///
        /// ShowWithoutActivation
        /// 
        /// <summary>
        /// フォーカスを奪わずに表示させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected override bool ShowWithoutActivation => true;

        #region Hiding properties

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Don't use this property, use Title")]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Don't use this property")]
        public new bool TopMost
        {
            get { return base.TopMost; }
            set { base.TopMost = value; }
        }

        #endregion

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Selected
        /// 
        /// <summary>
        /// ユーザの選択時に発生します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event ValueEventHandler<NotifyComponents> Selected;

        /* ----------------------------------------------------------------- */
        ///
        /// OnSelected
        /// 
        /// <summary>
        /// Selected イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnSelected(ValueEventArgs<NotifyComponents> e)
            => Selected?.Invoke(this, e);

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Show
        /// 
        /// <summary>
        /// フォームを表示します。
        /// </summary>
        /// 
        /// <param name="item">表示内容</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Show(NotifyItem item) => Show(item, null);

        /* ----------------------------------------------------------------- */
        ///
        /// Show
        /// 
        /// <summary>
        /// フォームを表示します。
        /// </summary>
        /// 
        /// <param name="item">表示内容</param>
        /// <param name="style">表示スタイル</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Show(NotifyItem item, NotifyStyle style)
        {
            Value = item;

            SetStyle(style);
            Show(item.DisplayTime, item.InitialDelay);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Show
        /// 
        /// <summary>
        /// フォームを表示します。
        /// </summary>
        /// 
        /// <param name="time">表示時間</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Show(TimeSpan time) => Show(time, TimeSpan.Zero);

        /* ----------------------------------------------------------------- */
        ///
        /// Show
        /// 
        /// <summary>
        /// フォームを表示します。
        /// </summary>
        /// 
        /// <param name="time">表示時間</param>
        /// <param name="delay">表示されるまでの時間</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Show(TimeSpan time, TimeSpan delay)
        {
            var _ = ShowAsync(time, delay);
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnBackColorChanged
        /// 
        /// <summary>
        /// 背景色が変化した時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);

            _panel.BackColor                    =
            _close.Styles.NormalStyle.BackColor =
            _image.Styles.NormalStyle.BackColor =
            _title.Styles.NormalStyle.BackColor =
            _text.Styles.NormalStyle.BackColor  = BackColor;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ShowAsync
        /// 
        /// <summary>
        /// 指定時間フォームを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private async Task ShowAsync(TimeSpan time, TimeSpan delay)
        {
            var source = new System.Threading.CancellationTokenSource();
            EventHandler m = (s, e) => { if (!Visible) source.Cancel(); };
            VisibleChanged += m;
            
            try
            {
                IsBusy = true;

                var screen = System.Windows.Forms.Screen.GetWorkingArea(Location);
                SetDesktopLocation(screen.Width - Width - 1, screen.Height - Height - 1);

                if (delay > TimeSpan.Zero) await Task.Delay(delay);
                SetTopMost();
                Show();
                if (time > TimeSpan.Zero) await Task.Delay(time, source.Token);
                Hide();
            }
            catch (TaskCanceledException /* err */) { }
            catch (OperationCanceledException /* err */) { }
            catch (Exception err) { this.LogWarn(err.ToString()); }
            finally
            {
                VisibleChanged -= m;
                IsBusy = false;
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// SetStyle
        /// 
        /// <summary>
        /// 表示スタイルを適用します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void SetStyle(NotifyStyle style)
        {
            if (style == null) return;
            if (style.BackColor != Color.Empty) BackColor = style.BackColor;
            if (style.ImageColor != Color.Empty) _image.Styles.NormalStyle.BackColor = style.ImageColor;
            if (style.Image != null) _image.Styles.NormalStyle.Image = style.Image;
            if (style.Title != null) _title.Font = style.Title;
            if (style.TitleColor != Color.Empty) _title.Styles.NormalStyle.ContentColor = style.TitleColor;
            if (style.Description != null) _text.Font = style.Description;
            if (style.DescriptionColor != Color.Empty) _text.Styles.NormalStyle.ContentColor = style.DescriptionColor;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// SetTopMost
        /// 
        /// <summary>
        /// 最前面に表示するように設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void SetTopMost()
        {
            const uint SWP_NOSIZE         = 0x0001;
            const uint SWP_NOMOVE         = 0x0002;
            const uint SWP_NOACTIVATE     = 0x0010;
            const uint SWP_NOSENDCHANGING = 0x0400;

            User32.NativeMethods.SetWindowPos(
                Handle,
                (IntPtr)(-1), /* HWND_TOPMOST */
                0,
                0,
                0,
                0,
                SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSENDCHANGING | SWP_NOSIZE
            );
        }

        /* --------------------------------------------------------------------- */
        ///
        /// InitializeLayout
        /// 
        /// <summary>
        /// レイアウトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void InitializeLayout()
        {
            SuspendLayout();

            _image = new FlatButton();
            _image.Content = string.Empty;
            _image.Dock = System.Windows.Forms.DockStyle.Fill;
            _image.Margin = new System.Windows.Forms.Padding(0);
            _image.Styles.NormalStyle.BorderSize = 0;
            _image.Styles.NormalStyle.BackColor = Color.FromArgb(230, 230, 230);
            _image.Styles.NormalStyle.Image = Properties.Resources.LogoLarge;
            _image.Click += (s, e) => OnSelected(ValueEventArgs.Create(NotifyComponents.Image));

            _title = new FlatButton();
            _title.Content = string.Empty;
            _title.Dock = System.Windows.Forms.DockStyle.Fill;
            _title.Font = new Font(Font, FontStyle.Bold);
            _title.Margin = new System.Windows.Forms.Padding(0);
            _title.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            _title.TextAlign = ContentAlignment.MiddleLeft;
            _title.Styles.NormalStyle.BackColor = Color.White;
            _title.Styles.NormalStyle.BorderSize = 0;
            _title.Styles.NormalStyle.ContentColor = Color.DimGray;
            _title.Click += (s, e) => OnSelected(ValueEventArgs.Create(NotifyComponents.Title));

            _text = new FlatButton();
            _text.AutoEllipsis = true;
            _text.Content = string.Empty;
            _text.Cursor = System.Windows.Forms.Cursors.Hand;
            _text.Dock = System.Windows.Forms.DockStyle.Fill;
            _text.Margin = new System.Windows.Forms.Padding(0);
            _text.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            _text.TextAlign = ContentAlignment.TopLeft;
            _text.Styles.NormalStyle.BackColor = Color.White;
            _text.Styles.NormalStyle.BorderSize = 0;
            _text.Click += (s, e) => OnSelected(ValueEventArgs.Create(NotifyComponents.Description));

            _close = new FlatButton();
            _close.Content = string.Empty;
            _close.Dock = System.Windows.Forms.DockStyle.Fill;
            _close.Margin = new System.Windows.Forms.Padding(0);
            _close.Styles.NormalStyle.BackColor = Color.White;
            _close.Styles.NormalStyle.BorderSize = 0;
            _close.Styles.NormalStyle.Image = Properties.Resources.CloseButton;
            _close.Styles.MouseOverStyle.BackColor = Color.FromArgb(240, 240, 240);
            _close.Styles.MouseOverStyle.BorderColor = Color.FromArgb(230, 230, 230);
            _close.Styles.MouseOverStyle.BorderSize = 1;
            _close.Styles.MouseDownStyle.BackColor = Color.FromArgb(236, 236, 236);
            _close.Click += (s, e) =>
            {
                Hide();
                OnSelected(ValueEventArgs.Create(NotifyComponents.Others));
            };

            _panel = new TableLayoutPanel();
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
            _panel.Controls.Add(_text,  1, 1);
            _panel.Controls.Add(_close, 2, 0);
            _panel.SetRowSpan(_image, 2);

            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = SystemColors.Window;
            Size = new Size(350, 70);
            Font = FontFactory.Create(12, FontStyle.Regular, GraphicsUnit.Pixel);
            IsBusy = false;
            Location = new Point(0, 0);
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            Sizable = false;
            Controls.Add(_panel);

            _panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #region Fields
        private NotifyItem _value;
        private TableLayoutPanel _panel;
        private FlatButton _image;
        private FlatButton _title;
        private FlatButton _text;
        private FlatButton _close;
        #endregion

        #endregion
    }
}
