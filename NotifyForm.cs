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
            CloseButton.Click += (s, e) => Hide();
            TitleButton.Click += (s, e) => RaiseTitleClickEvent();
            ImageButton.Click += (s, e) => RaiseImageClickEvent();
        }

        #endregion

        #region Properties

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
        /// ShowPending
        /// 
        /// <summary>
        /// 表示処理が遅延されているかどうかを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public bool ShowPending { get; private set; }

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

        /* --------------------------------------------------------------------- */
        ///
        /// OnShowing
        /// 
        /// <summary>
        /// フォームが表示される直前に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected override void OnShowing(CancelEventArgs e)
        {
            if (InitialDelay > 0 && !_pend4impl)
            {
                ShowPending = true;
                _pend4impl = true;
                e.Cancel = true;

                DelayInvoke(InitialDelay, () => {
                    SetLocation();
                    ShowPending = false;
                    Show();
                    _pend4impl = false;
                });
            }
            base.OnShowing(e);
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
            worker.DoWork += (s, e) => Thread.Sleep(msec);
            worker.RunWorkerCompleted += (s, e) => {
                if (InvokeRequired) Invoke(action);
                else action();
            };
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
        /// RaiseTitleClickEvent
        /// 
        /// <summary>
        /// TitleClick イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void RaiseTitleClickEvent()
        {
            OnTitleClick(new NotifyEventArgs(Title, Image));
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
            OnImageClick(new NotifyEventArgs(Title, Image));
        }

        #endregion

        #region Fields
        private bool _pend4impl = false;
        #endregion
    }
}
