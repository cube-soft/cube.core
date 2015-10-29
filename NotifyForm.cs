/* ------------------------------------------------------------------------- */
///
/// NotifyForm.cs
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TaskEx = System.Threading.Tasks.Task;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.NotifyForm
    /// 
    /// <summary>
    /// 通知用フォームを作成するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class NotifyForm : WidgetForm
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
            InitializeComponent();
            InitializeStyles();
            SetTopMost();
            IsBusy = false;
            base.Text = string.Empty;
            HideButton.Click += (s, e) => OnHideClick(e);
            TitleButton.Click += (s, e) => RaiseTextClickEvent();
            DescriptionButton.Click += (s, e) => RaiseTextClickEvent();
            ImageButton.Click += (s, e) => RaiseImageClickEvent();
        }

        #endregion

        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Level
        /// 
        /// <summary>
        /// 重要度を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public NotifyLevel Level { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Title
        /// 
        /// <summary>
        /// タイトルを取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public string Title
        {
            get { return TitleButton.Text; }
            set
            {
                TitleButton.Text = value;
                base.Text = value;
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Description
        /// 
        /// <summary>
        /// 本文を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public string Description
        {
            get { return DescriptionButton.Text; }
            set { DescriptionButton.Text = value; }
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
            get { return DescriptionButton.AutoEllipsis; }
            set { DescriptionButton.AutoEllipsis = value; }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// DefaultStyle
        /// 
        /// <summary>
        /// 規定スタイルを取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NotifyStyle DefaultStyle { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Image
        /// 
        /// <summary>
        /// イメージを取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public System.Drawing.Image Image
        {
            get { return ImageButton.Surface.Image; }
            set { ImageButton.Surface.Image = value; }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// InitialDelay
        /// 
        /// <summary>
        /// 表示する際の遅延時間 (ミリ秒) を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(true)]
        public int InitialDelay { get; set; }

        /* --------------------------------------------------------------------- */
        ///
        /// IsBusy
        /// 
        /// <summary>
        /// 実行中かどうかを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public bool IsBusy { get; private set; }

        /* --------------------------------------------------------------------- */
        ///
        /// Styles
        /// 
        /// <summary>
        /// 通知レベル毎のスタイルを取得または設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// Styles に存在しない通知レベルが指定された場合、DefaultStyle が
        /// 適用されます。
        /// </remarks>
        ///
        /* --------------------------------------------------------------------- */
        public IDictionary<NotifyLevel, NotifyStyle> Styles
        {
            get { return _styles; }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// ShowWithoutActivation
        /// 
        /// <summary>
        /// フォーカスを奪わずに表示させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

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

        /* --------------------------------------------------------------------- */
        ///
        /// TextClick
        /// 
        /// <summary>
        /// テキスト部分（タイトルおよび本文）がクリックされた時に発生する
        /// イベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler<NotifyEventArgs> TextClick;

        /* --------------------------------------------------------------------- */
        ///
        /// ImageClick
        /// 
        /// <summary>
        /// イメージ部分がクリックされた時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler<NotifyEventArgs> ImageClick;

        /* --------------------------------------------------------------------- */
        ///
        /// HideClick
        /// 
        /// <summary>
        /// Hide ボタンがクリックされた時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler HideClick;

        #endregion

        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// Show
        /// 
        /// <summary>
        /// 指定された時間だけフォームを表示します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void Show(int msec)
        {
            Show();
            var _ = RunAsync(() => Hide(), InitialDelay + msec); // suppress warning
        }

        #endregion

        #region Vritual methods

        /* --------------------------------------------------------------------- */
        ///
        /// OnTextClick
        /// 
        /// <summary>
        /// テキスト部分（タイトルおよび本文）がクリックされた時に発生する
        /// イベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnTextClick(NotifyEventArgs e)
        {
            if (TextClick != null) TextClick(this, e);
        }

        /* --------------------------------------------------------------------- */
        ///
        /// OnImageClick
        /// 
        /// <summary>
        /// イメージ部分がクリックされた時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnImageClick(NotifyEventArgs e)
        {
            if (ImageClick != null) ImageClick(this, e);
        }

        /* --------------------------------------------------------------------- */
        ///
        /// HideClick
        /// 
        /// <summary>
        /// Hide ボタンがクリックされた時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnHideClick(EventArgs e)
        {
            if (HideClick != null) HideClick(this, e);
            Hide();
        }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// SetVisibleCore
        /// 
        /// <summary>
        /// コントロールを指定した表示状態に設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void SetVisibleCore(bool value)
        {
            var current = Visible;
            var showing = value && !current;
            if (showing) IsBusy = true;
            if (showing && InitialDelay > 0)
            {
                var _ = RunAsync(() => base.SetVisibleCore(value), InitialDelay); // suppress warning
                base.SetVisibleCore(current);
            }
            else base.SetVisibleCore(value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnShowing
        /// 
        /// <summary>
        /// フォームが表示される直前に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnShowing(CancelEventArgs e)
        {
            Logger.DebugFormat("Title:{0}\tDescription:{1}", Title, Description);
            SetLocation();
            SetStyle();
            base.OnShowing(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnHidden
        /// 
        /// <summary>
        /// フォームが非表示になった直後に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnHidden(EventArgs e)
        {
            IsBusy = false;
            base.OnHidden(e);
        }

        #endregion

        #region Style definitions

        /* --------------------------------------------------------------------- */
        ///
        /// InitializeStyles
        /// 
        /// <summary>
        /// スタイルを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void InitializeStyles()
        {
            DefaultStyle = new NotifyStyle
            {
                BackColor        = System.Drawing.Color.FromArgb(242, 242, 242),
                BorderColor      = System.Drawing.Color.FromArgb(230, 230, 230),
                Title            = new System.Drawing.Font(Font, System.Drawing.FontStyle.Bold),
                TitleColor       = System.Drawing.Color.DimGray,
                Description      = Font,
                DescriptionColor = System.Drawing.SystemColors.ControlText
            };

            Styles.Add(NotifyLevel.None, new NotifyStyle
            {
                BackColor        = System.Drawing.Color.Empty,
                BorderColor      = System.Drawing.Color.Empty,
                Title            = Font,
                TitleColor       = System.Drawing.SystemColors.ControlText,
                Description      = Font,
                DescriptionColor = System.Drawing.SystemColors.ControlText
            });

            Styles.Add(NotifyLevel.Information, new NotifyStyle
            {
                BackColor        = DefaultStyle.BackColor,
                BorderColor      = DefaultStyle.BorderColor,
                Title            = DefaultStyle.Title,
                TitleColor       = DefaultStyle.TitleColor,
                Description      = DefaultStyle.Description,
                DescriptionColor = DefaultStyle.DescriptionColor
            });

            Styles.Add(NotifyLevel.Recommended, new NotifyStyle
            {
                BackColor        = DefaultStyle.BackColor,
                BorderColor      = DefaultStyle.BorderColor,
                Title            = DefaultStyle.Title,
                TitleColor       = DefaultStyle.TitleColor,
                Description      = DefaultStyle.Description,
                DescriptionColor = System.Drawing.Color.FromArgb(192, 0, 0)
            });

            Styles.Add(NotifyLevel.Important, new NotifyStyle
            {
                BackColor        = DefaultStyle.BackColor,
                BorderColor      = DefaultStyle.BorderColor,
                Title            = DefaultStyle.Title,
                TitleColor       = DefaultStyle.TitleColor,
                Description      = DefaultStyle.Description,
                DescriptionColor = System.Drawing.Color.FromArgb(192, 0, 0)
            });

            Styles.Add(NotifyLevel.Warning, new NotifyStyle
            {
                BackColor        = DefaultStyle.BackColor,
                BorderColor      = DefaultStyle.BorderColor,
                Title            = DefaultStyle.Title,
                TitleColor       = System.Drawing.SystemColors.ControlText,
                Description      = DefaultStyle.Description,
                DescriptionColor = System.Drawing.Color.FromArgb(192, 0, 0)
            });

            Styles.Add(NotifyLevel.Error, new NotifyStyle
            {
                BackColor        = DefaultStyle.BackColor,
                BorderColor      = DefaultStyle.BorderColor,
                Title            = DefaultStyle.Title,
                TitleColor       = System.Drawing.SystemColors.ControlText,
                Description      = DefaultStyle.Description,
                DescriptionColor = System.Drawing.Color.Red
            });
        }

        #endregion

        #region Implementations

        /* --------------------------------------------------------------------- */
        ///
        /// RunAsync
        /// 
        /// <summary>
        /// 指定された時間 (ミリ秒) だけ処理を遅延させて実行します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private async Task RunAsync(Action action, int msec)
        {
            var source = new System.Threading.CancellationTokenSource();
            EventHandler m = (s, e) => source.Cancel();
            Hidden += m;

            try
            {
                await TaskEx.Delay(msec, source.Token);
                action();
            }
            catch (TaskCanceledException /* err */) { /* ignore user's cancel */ }
            catch (OperationCanceledException /* err */) { /* ignore user's cancel */ }
            catch (Exception err) { System.Diagnostics.Trace.TraceError(err.ToString()); }
            finally { Hidden -= m; }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// SetLocation
        /// 
        /// <summary>
        /// 表示位置を設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void SetLocation()
        {
            var screen = System.Windows.Forms.Screen.GetWorkingArea(this);
            SetDesktopLocation(screen.Width - Width - 10, screen.Height - Height - 10);
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

            SetWindowPos(
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
        /// SetStyle
        /// 
        /// <summary>
        /// スタイルを設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void SetStyle()
        {
            var style = Styles.ContainsKey(Level) ? Styles[Level] : DefaultStyle;
            ImageButton.Surface.BackColor = style.BackColor;
            Separator.BackColor = style.BorderColor;
            TitleButton.Font = style.Title;
            TitleButton.Surface.TextColor = style.TitleColor;
            DescriptionButton.Font = style.Description;
            DescriptionButton.Surface.TextColor = style.DescriptionColor;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseChangingVisibleEvent
        /// 
        /// <summary>
        /// 表示状態の変更に関するイベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void RaiseChangingVisibleEvent(bool current, bool ahead, CancelEventArgs e)
        {
            if (!current && ahead) OnShowing(e);
            else if (current && !ahead) OnHiding(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseVisibleChangedEvent
        /// 
        /// <summary>
        /// 表示状態が変更された事を通知するイベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void RaiseVisibleChangedEvent(bool current, bool behind, EventArgs e)
        {
            if (!current && behind) OnHidden(e);
        }

        /* --------------------------------------------------------------------- */
        ///
        /// RaiseTextClickEvent
        /// 
        /// <summary>
        /// TextClick イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void RaiseTextClickEvent()
        {
            OnTextClick(new NotifyEventArgs(Level, Title, Description, Image, Tag));
        }

        /* --------------------------------------------------------------------- */
        ///
        /// RaiseImageClickEvent
        /// 
        /// <summary>
        /// ImageClick イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void RaiseImageClickEvent()
        {
            OnImageClick(new NotifyEventArgs(Level, Title, Description, Image, Tag));
        }

        #endregion

        #region Win32 APIs

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        #endregion

        #region Fields
        private Dictionary<NotifyLevel, NotifyStyle> _styles = new Dictionary<NotifyLevel, NotifyStyle>();
        #endregion
    }
}
