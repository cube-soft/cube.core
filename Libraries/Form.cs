/* ------------------------------------------------------------------------- */
///
/// Form.cs
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
using log4net;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Form
    /// 
    /// <summary>
    /// System.Windows.Forms.Form の拡張クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Form : System.Windows.Forms.Form
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Form
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Form() : base()
        {
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            DoubleBuffered = true;
            Font = FontFactory.Create(12, Font.Style, GraphicsUnit.Pixel);
            Logger = LogManager.GetLogger(GetType());
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Bootstrap
        /// 
        /// <summary>
        /// プロセス間通信を介した起動およびアクティブ化を制御するための
        /// オブジェクトを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Bootstrap Bootstrap
        {
            get { return _bootstrap; }
            set
            {
                if (_bootstrap != value)
                {
                    if (_bootstrap != null) _bootstrap.Activated -= Bootstrap_Activated;
                    _bootstrap = value;
                    if (_bootstrap != null)
                    {
                        _bootstrap.Activated -= Bootstrap_Activated;
                        _bootstrap.Activated += Bootstrap_Activated;
                        _bootstrap.Register();
                    }
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Logger
        ///
        /// <summary>
        /// ログ出力用オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected ILog Logger { get; }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Showing
        /// 
        /// <summary>
        /// フォームが表示される直前に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event CancelEventHandler Showing;

        /* ----------------------------------------------------------------- */
        ///
        /// Hiding
        /// 
        /// <summary>
        /// フォームが非表示になる直前に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event CancelEventHandler Hiding;

        /* ----------------------------------------------------------------- */
        ///
        /// Hidden
        /// 
        /// <summary>
        /// フォームが非表示なった直後に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Hidden;

        /* ----------------------------------------------------------------- */
        ///
        /// Received
        ///
        /// <summary>
        /// 他のプロセスからデータを受信した時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public EventHandler<DataEventArgs<object>> Received;

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnShowing
        /// 
        /// <summary>
        /// フォームが表示される直前に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnShowing(CancelEventArgs e)
        {
            if (Showing != null) Showing(this, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnHiding
        /// 
        /// <summary>
        /// フォームが非表示になる直前に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnHiding(CancelEventArgs e)
        {
            if (Hiding != null) Hiding(this, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnHidden
        /// 
        /// <summary>
        /// フォームが非表示なった直後に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnHidden(EventArgs e)
        {
            if (Hidden != null) Hidden(this, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnReceived
        ///
        /// <summary>
        /// 他のプロセスからデータを受信した時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnReceived(DataEventArgs<object> e)
        {
            if (Received != null) Received(this, e);
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
            var prev = Visible;
            var ev = new CancelEventArgs();
            RaiseChangingVisibleEvent(prev, value, ev);
            base.SetVisibleCore(ev.Cancel ? prev : value);
            if (prev == value || ev.Cancel) return;
            RaiseVisibleChangedEvent(value, prev, new EventArgs());
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// Bootstrap
        /// 
        /// <summary>
        /// 他プロセスからアクティブ化時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Bootstrap_Activated(object sender, DataEventArgs<object> e)
        {
            if (InvokeRequired) Invoke(new Action(() => Bootstrap_Activated(sender, e)));
            else
            {
                if (e.Value != null) OnReceived(e);

                Show();
                var tmp = TopMost;
                TopMost = true;
                TopMost = tmp;
            }
        }

        #endregion

        #region Other private methods

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
        /// <remarks>
        /// TODO: システムによる Shown イベントは最初の 1 度しか発生しない
        ///       模様。Showing イベント等との整合性をどうするか検討する。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void RaiseVisibleChangedEvent(bool current, bool behind, EventArgs e)
        {
            if (!current && behind) OnHidden(e);
        }

        #endregion

        #region Fields
        private Bootstrap _bootstrap = null;
        #endregion
    }
}
