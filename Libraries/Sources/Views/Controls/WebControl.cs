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
using System.IO;
using System.Text;
using Cube.Logging;
using Cube.Mixin.String;
using WF = System.Windows.Forms;

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// WebControl
    ///
    /// <summary>
    /// Represents the control to show a Web page.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class WebControl : WF.WebBrowser
    {
        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// UserAgent
        ///
        /// <summary>
        /// Gets or sets the user agent.
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
        /// Occurs before navigating.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler<NavigatingEventArgs> BeforeNavigating;

        /* --------------------------------------------------------------------- */
        ///
        /// OnBeforeNavigating
        ///
        /// <summary>
        /// Raises the BeforeNavigating event.
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
        /// Occurs before opening the Web page in a new window.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler<NavigatingEventArgs> BeforeNewWindow;

        /* --------------------------------------------------------------------- */
        ///
        /// OnBeforeNewWindow
        ///
        /// <summary>
        /// Raises the BeforeNewWindow event.
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
        /// Occurs when an error detects during page transition.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler<NavigatingErrorEventArgs> NavigatingError;

        /* --------------------------------------------------------------------- */
        ///
        /// OnNavigatingError
        ///
        /// <summary>
        /// Raises the NavigatingError event.
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
        /// Occurs before showing a message box.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler<DialogMessage> MessageShowing;

        /* --------------------------------------------------------------------- */
        ///
        /// OnMessageShowing
        ///
        /// <summary>
        /// Raises the MessageShowing event.
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
        /// Starts displaying the content indicated by the URL.
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
        /// Starts displaying the content indicated by the HTML.
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
        /// Starts displaying the content indicated by the HTML.
        /// </summary>
        ///
        /// <param name="stream">Streams containing HTML content.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Start(Stream stream)
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
        /// Processes the specified window message.
        /// </summary>
        ///
        /// <remarks>
        /// JavaScript の window.close() が実行された場合への対応。
        /// TODO: WM_DESTROY をキャンセルする方法があるかどうか要調査
        /// </remarks>
        ///
        /* --------------------------------------------------------------------- */
        protected override void WndProc(ref WF.Message m)
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
        /// Generates an object to extend functionality using ActiveX
        /// controls.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override WF.WebBrowserSiteBase CreateWebBrowserSiteBase() => new ShowUIWebBrowserSite(this);

        /* --------------------------------------------------------------------- */
        ///
        /// CreateSink
        ///
        /// <summary>
        /// Associates the underlying ActiveX Control with a Client that can
        /// handle Control events.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected override void CreateSink()
        {
            base.CreateSink();
            _ax = new(this);
            _cookie = new(ActiveXInstance, _ax, typeof(DWebBrowserEvents2));
        }

        /* --------------------------------------------------------------------- */
        ///
        /// DetachSink
        ///
        /// <summary>
        /// Releases the event handling client attached by the CreateSink method
        /// of the underlying ActiveX control.
        /// </summary>
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
        /// Gets the UserAgent value.
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

            if (result != 0) GetType().LogWarn($"UrlMkGetSessionOption:{result}");
            return result == 0 ? sb.ToString() : string.Empty;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// SetUserAgent
        ///
        /// <summary>
        /// Sets the UserAgent value.
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
            else GetType().LogWarn($"UrlMkSetSessionOption:{result}");
        }

        /* --------------------------------------------------------------------- */
        ///
        /// CloseForm
        ///
        /// <summary>
        /// Closes the form to which the control is joined.
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
        private WF.AxHost.ConnectionPointCookie _cookie;
        private ActiveXControlEvents _ax;
        #endregion
    }
}
