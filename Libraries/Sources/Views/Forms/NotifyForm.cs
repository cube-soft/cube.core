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
using Cube.Forms.Controls;
using Cube.Mixin.Logger;
using Cube.Mixin.Tasks;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;

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
    public class NotifyForm : BorderlessForm, INotifyForm
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
        public NotifyForm()
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
            get => _value;
            private set
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
            get => _text.AutoEllipsis;
            set => _text.AutoEllipsis = value;
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsBusy { get; private set; } = false;

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

        /* --------------------------------------------------------------------- */
        ///
        /// Text
        ///
        /// <summary>
        /// 表示テキストを取得または設定します。
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
        /// 最前面に表示するかどうかを示す値を取得または設定します。
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
        /// 通知完了時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Completed;

        /* ----------------------------------------------------------------- */
        ///
        /// OnCompleted
        ///
        /// <summary>
        /// Completed イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnCompleted(EventArgs e)
        {
            IsBusy = false;
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
            ShowAsync(item.DisplayTime, item.InitialDelay).Forget();
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Show
        ///
        /// <summary>
        /// 画面を表示します。
        /// </summary>
        ///
        /// <remarks>
        /// 外部から実行できないように private で再定義します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private new void Show() => base.Show();

        /* ----------------------------------------------------------------- */
        ///
        /// ShowDialog
        ///
        /// <summary>
        /// 画面を表示します。
        /// </summary>
        ///
        /// <remarks>
        /// 外部から実行できないように private で再定義します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private new System.Windows.Forms.DialogResult ShowDialog() => base.ShowDialog();

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
            void m(object s, EventArgs e) { if (!Visible) source.Cancel(); }
            VisibleChanged += m;

            try
            {
                IsBusy = true;

                var screen = System.Windows.Forms.Screen.GetWorkingArea(Location);
                SetDesktopLocation(screen.Width - Width - 1, screen.Height - Height - 1);

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
        /// 表示スタイルを適用します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void SetStyle(NotifyStyle style)
        {
            if (style == null) return;
            if (style.ImageColor != Color.Empty) _image.Styles.NormalStyle.BackColor = style.ImageColor;
            if (style.Image != null) _image.Styles.NormalStyle.Image = style.Image;
            if (style.Title != null) _title.Font = style.Title;
            if (style.TitleColor != Color.Empty) _title.Styles.NormalStyle.ContentColor = style.TitleColor;
            if (style.Description != null) _text.Font = style.Description;
            if (style.DescriptionColor != Color.Empty) _text.Styles.NormalStyle.ContentColor = style.DescriptionColor;
            if (style.BackColor != Color.Empty)
            {
                BackColor                           = style.BackColor;
                _panel.BackColor                    = style.BackColor;
                _close.Styles.NormalStyle.BackColor = style.BackColor;
                _title.Styles.NormalStyle.BackColor = style.BackColor;
                _text.Styles.NormalStyle.BackColor  = style.BackColor;
            }
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

            _image.Content = string.Empty;
            _image.Dock = System.Windows.Forms.DockStyle.Fill;
            _image.Margin = new System.Windows.Forms.Padding(0);
            _image.Styles.NormalStyle.BorderSize = 0;
            _image.Styles.NormalStyle.BackColor = Color.FromArgb(230, 230, 230);
            _image.Styles.NormalStyle.Image = Properties.Resources.LogoLarge;
            _image.Click += (s, e) => OnSelected(ValueEventArgs.Create(NotifyComponents.Image));

            _title.Content = string.Empty;
            _title.Dock = System.Windows.Forms.DockStyle.Fill;
            _title.Font = FontFactory.Create(12, FontStyle.Bold, GraphicsUnit.Pixel);
            _title.Margin = new System.Windows.Forms.Padding(0);
            _title.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            _title.TextAlign = ContentAlignment.MiddleLeft;
            _title.Styles.NormalStyle.BackColor = SystemColors.Window;
            _title.Styles.NormalStyle.BorderSize = 0;
            _title.Styles.NormalStyle.ContentColor = Color.DimGray;
            _title.Click += (s, e) => OnSelected(ValueEventArgs.Create(NotifyComponents.Title));

            _text.AutoEllipsis = true;
            _text.Content = string.Empty;
            _text.Cursor = System.Windows.Forms.Cursors.Hand;
            _text.Dock = System.Windows.Forms.DockStyle.Fill;
            _text.Margin = new System.Windows.Forms.Padding(0);
            _text.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            _text.TextAlign = ContentAlignment.TopLeft;
            _text.Styles.NormalStyle.BackColor = SystemColors.Window;
            _text.Styles.NormalStyle.BorderSize = 0;
            _text.Click += (s, e) => OnSelected(ValueEventArgs.Create(NotifyComponents.Description));

            _close.Content = string.Empty;
            _close.Dock = System.Windows.Forms.DockStyle.Fill;
            _close.Margin = new System.Windows.Forms.Padding(0);
            _close.Styles.NormalStyle.BackColor = SystemColors.Window;
            _close.Styles.NormalStyle.BorderSize = 0;
            _close.Styles.NormalStyle.Image = Properties.Resources.CloseButton;
            _close.Styles.MouseOverStyle.BackColor = Color.FromArgb(240, 240, 240);
            _close.Styles.MouseOverStyle.BorderColor = Color.FromArgb(230, 230, 230);
            _close.Styles.MouseOverStyle.BorderSize = 1;
            _close.Styles.MouseDownStyle.BackColor = Color.FromArgb(236, 236, 236);
            _close.Click += (s, e) => OnSelected(ValueEventArgs.Create(NotifyComponents.Others));

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
            BackColor     = SystemColors.Window;
            Size          = new Size(350, 80);
            Font          = FontFactory.Create(12, FontStyle.Regular, GraphicsUnit.Pixel);
            IsBusy        = false;
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
        private NotifyItem _value;
        private readonly TableLayoutPanel _panel = new TableLayoutPanel();
        private readonly FlatButton _image = new FlatButton();
        private readonly FlatButton _title = new FlatButton();
        private readonly FlatButton _text = new FlatButton();
        private readonly FlatButton _close = new FlatButton();
        #endregion
    }
}
