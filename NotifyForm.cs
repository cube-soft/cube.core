/* ------------------------------------------------------------------------- */
///
/// NotifyForm.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.ComponentModel;
using System.Threading;

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
            DelayInvoke(InitialDelay + msec, () => Hide());
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
                DelayInvoke(InitialDelay, () => base.SetVisibleCore(value));
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
            SetBackColor();
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
        /// DelayInvoke
        /// 
        /// <summary>
        /// 指定された時間 (ミリ秒) だけ処理を遅延させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void DelayInvoke(int msec, Action action)
        {
            var worker = new System.ComponentModel.BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            EventHandler method = (s, e) => worker.CancelAsync();
            
            worker.DoWork += (s, e) => {
                var expired = DateTime.Now + TimeSpan.FromMilliseconds(msec);
                while (DateTime.Now < expired)
                {
                    if (worker.CancellationPending) break;
                    var delta = expired - DateTime.Now;
                    var sleep = (int)Math.Min(delta.TotalMilliseconds, 100);
                    if (sleep > 0) Thread.Sleep(sleep);
                }
                if (worker.CancellationPending) e.Cancel = true;
            };

            worker.RunWorkerCompleted += (s, e) => {
                Hidden -= method;
                if (e.Cancelled) return;
                if (InvokeRequired) Invoke(action);
                else action();
            };

            Hidden += method;
            worker.RunWorkerAsync();
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
        /// SetBackColor
        /// 
        /// <summary>
        /// 背景色を設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void SetBackColor()
        {
            var color = System.Drawing.Color.Empty;
            switch (Level)
            {
                case NotifyLevel.None:
                    break;
                case NotifyLevel.Information:
                    color = System.Drawing.Color.Cyan;
                    break;
                case NotifyLevel.Recommended:
                    color = System.Drawing.Color.Lime;
                    break;
                case NotifyLevel.Important:
                    color = System.Drawing.Color.Yellow;
                    break;
                case NotifyLevel.Warning:
                    color = System.Drawing.Color.Orange;
                    break;
                case NotifyLevel.Error:
                    color = System.Drawing.Color.Red;
                    break;
                default:
                    break;
            }
            ImageButton.Surface.BackColor = color;
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
