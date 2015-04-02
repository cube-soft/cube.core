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
using System.ComponentModel;
using System.Threading.Tasks;

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
            IsBusy = false;
            base.Text = string.Empty;
            CloseButton.Click += (s, e) => Hide();
            TitleButton.Click += (s, e) => RaiseTitleClickEvent();
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

        #region Hiding properties

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Don't use this property, use Title")]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #endregion

        #endregion

        #region Events

        /* --------------------------------------------------------------------- */
        ///
        /// TitleClick
        /// 
        /// <summary>
        /// タイトル部分がクリックされた時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event Action<object, NotifyEventArgs> TitleClick;

        /* --------------------------------------------------------------------- */
        ///
        /// ImageClick
        /// 
        /// <summary>
        /// イメージ部分がクリックされた時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event Action<object, NotifyEventArgs> ImageClick;

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
        /// OnTitleClick
        /// 
        /// <summary>
        /// タイトル部分がクリックされた時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnTitleClick(NotifyEventArgs e)
        {
            if (TitleClick != null) TitleClick(this, e);
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
            SetLocation();
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
                await Cube.Tasks.Task.Delay(msec, source.Token);
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
        /// RaiseTitleClickEvent
        /// 
        /// <summary>
        /// TitleClick イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void RaiseTitleClickEvent()
        {
            OnTitleClick(new NotifyEventArgs(Level, Title, Image, Tag));
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
            OnImageClick(new NotifyEventArgs(Level, Title, Image, Tag));
        }

        #endregion
    }
}
