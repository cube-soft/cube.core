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
            CloseButton.Click += (s, e) => Close();
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
            get { return ImageButton.Image; }
            set { ImageButton.Image = value; }
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
        /// <remarks>
        /// TODO: 以下の処理を実装
        ///       1. InitialDelay だけ表示するのを遅延させる
        ///       2. NotifyForm を 右下に表示 (SetLocation で位置は調整可能)
        ///       3. msec (ミリ秒)後、フォームを閉じる。
        ///       
        /// TODO: また、Show() メソッド（引数無し）についても、1, 2 の処理を
        ///       適用できないか検討する。
        /// </remarks>
        ///
        /* --------------------------------------------------------------------- */
        public void Show(int msec)
        {
            base.Show();
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

        #region Implementations

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
    }
}
