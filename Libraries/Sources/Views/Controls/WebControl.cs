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
using System.Text;
using Cube.Mixin.Logging;
using Cube.Mixin.String;

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// WebControl
    ///
    /// <summary>
    /// Web ページを表示するためのコントロールです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class WebControl : System.Windows.Forms.WebBrowser
    {
        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// UserAgent
        ///
        /// <summary>
        /// UserAgent を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public string UserAgent
        {
            get
            {
                if (!_agent.HasValue()) _agent = GetUserAgent();
                return _agent;
            }

            set
            {
                if (_agent == value) return;
                SetUserAgent(ref _agent, value);
            }
        }

        #endregion

        #region Events

        #region BeforeNavigating

        /* --------------------------------------------------------------------- */
        ///
        /// BeforeNavigating
        ///
        /// <summary>
        /// ページ遷移が発生する直前に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler<NavigatingEventArgs> BeforeNavigating;

        /* --------------------------------------------------------------------- */
        ///
        /// OnBeforeNavigating
        ///
        /// <summary>
        /// BeforeNavigating を発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnBeforeNavigating(NavigatingEventArgs e) =>
            BeforeNavigating?.Invoke(this, e);

        #endregion

        #region BeforeNewWindow

        /* --------------------------------------------------------------------- */
        ///
        /// BeforeNewWindow
        ///
        /// <summary>
        /// 新しいウィンドウでページを開く直前に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler<NavigatingEventArgs> BeforeNewWindow;

        /* --------------------------------------------------------------------- */
        ///
        /// OnBeforeNewWindow
        ///
        /// <summary>
        /// BeforeNewWindow を発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnBeforeNewWindow(NavigatingEventArgs e) =>
            BeforeNewWindow?.Invoke(this, e);

        #endregion

        #region NavigatingError

        /* --------------------------------------------------------------------- */
        ///
        /// NavigatingError
        ///
        /// <summary>
        /// ページ遷移中にエラーが生じた際に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler<NavigatingErrorEventArgs> NavigatingError;

        /* --------------------------------------------------------------------- */
        ///
        /// OnNavigatingError
        ///
        /// <summary>
        /// ページ遷移中にエラーが生じた際に実行されます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnNavigatingError(NavigatingErrorEventArgs e) =>
            NavigatingError?.Invoke(this, e);

        #endregion

        #region MessageShowing

        /* --------------------------------------------------------------------- */
        ///
        /// MessageShowing
        ///
        /// <summary>
        /// メッセージボックスが表示される直前に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler<DialogMessage> MessageShowing;

        /* --------------------------------------------------------------------- */
        ///
        /// OnMessageShowing
        ///
        /// <summary>
        /// メッセージボックスが表示される直前に実行されます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnMessageShowing(DialogMessage e) =>
            MessageShowing?.Invoke(this, e);

        #endregion

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Start
        ///
        /// <summary>
        /// URL で示されたコンテンツの表示を開始します。
        /// </summary>
        ///
        /// <param name="uri">URL</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Start(Uri uri)
        {
            if (IsBusy) Stop();
            Navigate(uri);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Start
        ///
        /// <summary>
        /// HTML で示されたコンテンツの表示を開始します。
        /// </summary>
        ///
        /// <param name="html">HTML</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Start(string html)
        {
            if (IsBusy) Stop();
            DocumentText = html;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Start
        ///
        /// <summary>
        /// HTML で示されたコンテンツの表示を開始します。
        /// </summary>
        ///
        /// <param name="stream">HTML コンテンツを含むストリーム</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Start(System.IO.Stream stream)
        {
            if (IsBusy) Stop();
            DocumentStream = stream;
        }

        #endregion

        #region Implementations

        /* --------------------------------------------------------------------- */
        ///
        /// WndProc
        ///
        /// <summary>
        /// ウィンドウメッセージを受信します。
        /// </summary>
        ///
        /// <remarks>
        /// JavaScript の window.close() が実行された場合への対応。
        /// TODO: WM_DESTROY をキャンセルする方法があるかどうか要調査
        /// </remarks>
        ///
        /* --------------------------------------------------------------------- */
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x0210) // WM_PARENTNOTIFY
            {
                if (m.WParam.ToInt32() == 2 && !DesignMode) CloseForm(); // WM_DESTROY (2)
            }
            base.WndProc(ref m);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateWebBrowserSiteBase
        ///
        /// <summary>
        /// ActiveX コントロールを使用した機能拡張を行うためのオブジェクトを
        /// 生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override System.Windows.Forms.WebBrowserSiteBase CreateWebBrowserSiteBase() =>
            new ShowUIWebBrowserSite(this);

        /* --------------------------------------------------------------------- */
        ///
        /// CreateSink
        ///
        /// <summary>
        /// コントロール イベントを処理できるクライアントに、基になる ActiveX
        /// コントロールを関連付けます。
        /// </summary>
        ///
        /// <remarks>
        /// System.Windows.Forms.WebBrowser から継承されます。
        /// </remarks>
        ///
        /* --------------------------------------------------------------------- */
        protected override void CreateSink()
        {
            base.CreateSink();
            _ax = new ActiveXControlEvents(this);
            _cookie = new System.Windows.Forms.AxHost.ConnectionPointCookie(ActiveXInstance, _ax, typeof(DWebBrowserEvents2));
        }

        /* --------------------------------------------------------------------- */
        ///
        /// DetachSink
        ///
        /// <summary>
        /// 基になる ActiveX コントロールの CreateSink メソッドでアタッチされた
        /// イベント処理クライアントを解放します。
        /// </summary>
        ///
        /// <remarks>
        /// System.Windows.Forms.WebBrowser から継承されます。
        /// </remarks>
        ///
        /* --------------------------------------------------------------------- */
        protected override void DetachSink()
        {
            if (_cookie != null)
            {
                _cookie.Disconnect();
                _cookie = null;
            }
            base.DetachSink();
        }

        /* --------------------------------------------------------------------- */
        ///
        /// GetUserAgent
        ///
        /// <summary>
        /// UserAgent を取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private string GetUserAgent()
        {
            var sb     = new StringBuilder(2048);
            var size   = 0;
            var result = UrlMon.NativeMethods.UrlMkGetSessionOption(
                0x10000001, // URLMON_OPTION_USERAGENT,
                sb, sb.Capacity, ref size, 0
            );

            if (result != 0) this.LogWarn($"UrlMkGetSessionOption:{result}");
            return result == 0 ? sb.ToString() : string.Empty;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// SetUserAgent
        ///
        /// <summary>
        /// UserAgent を設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void SetUserAgent(ref string dest, string value)
        {
            var result = UrlMon.NativeMethods.UrlMkSetSessionOption(
                0x10000001, // URLMON_OPTION_USERAGENT,
                value, value.Length, 0
            );

            if (result == 0) dest = value;
            else this.LogWarn($"UrlMkSetSessionOption:{result}");
        }

        /* --------------------------------------------------------------------- */
        ///
        /// CloseForm
        ///
        /// <summary>
        /// コンポーネントが関連付られているフォームを閉じます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void CloseForm()
        {
            var form = FindForm();
            if (form != null && !form.IsDisposed) form.Close();
        }

        #endregion

        #region Fields
        private string _agent = string.Empty;
        private System.Windows.Forms.AxHost.ConnectionPointCookie _cookie;
        private ActiveXControlEvents _ax;
        #endregion
    }
}
