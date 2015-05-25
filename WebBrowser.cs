/* ------------------------------------------------------------------------- */
///
/// WebBrowser.cs
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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.WebBrowser
    /// 
    /// <summary>
    /// Web ページを表示するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class WebBrowser : System.Windows.Forms.WebBrowser
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// WebBrowser
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public WebBrowser() : base() { }

        #endregion

        #region Events

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
        /// NavigatingError
        /// 
        /// <summary>
        /// ページ遷移中にエラーが生じた際に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler<NavigatingErrorEventArgs> NavigatingError;

        #endregion

        #region Virtual methods

        /* --------------------------------------------------------------------- */
        ///
        /// OnBeforeNavigating
        /// 
        /// <summary>
        /// ページ遷移が発生する直前に実行されます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnBeforeNavigating(NavigatingEventArgs e)
        {
            if (BeforeNavigating != null) BeforeNavigating(this, e);
        }

        /* --------------------------------------------------------------------- */
        ///
        /// OnBeforeNewWindow
        /// 
        /// <summary>
        /// 新しいウィンドウでページを開く直前に実行されます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnBeforeNewWindow(NavigatingEventArgs e)
        {
            if (BeforeNewWindow != null) BeforeNewWindow(this, e);
        }

        /* --------------------------------------------------------------------- */
        ///
        /// OnNavigatingError
        /// 
        /// <summary>
        /// ページ遷移中にエラーが生じた際に実行されます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnNavigatingError(NavigatingErrorEventArgs e)
        {
            if (NavigatingError != null) NavigatingError(this, e);
        }

        #endregion

        #region Override methods

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
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0210: /* WM_PARENTNOTIFY */
                    /* WM_DESTROY (2) */
                    if (m.WParam.ToInt32() == 2 && !DesignMode) CloseForm();
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }

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
            _events = new ActiveXControlEvents(this);
            _cookie = new AxHost.ConnectionPointCookie(ActiveXInstance, _events, typeof(DWebBrowserEvents2));
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

        #endregion

        #region Implementations

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

        /* --------------------------------------------------------------------- */
        ///
        /// RaiseBeforeNavigating
        /// 
        /// <summary>
        /// ページ遷移が発生する直前に実行されます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void RaiseBeforeNavigating(string url, string frame, out bool cancel)
        {
            var e = new NavigatingEventArgs(url, frame);
            OnBeforeNavigating(e);
            cancel = e.Cancel;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// RaiseBeforeNewWindow
        /// 
        /// <summary>
        /// 新しいウィンドウでページを開く直前に実行されます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void RaiseBeforeNewWindow(string url, out bool cancel)
        {
            var e = new NavigatingEventArgs(url, string.Empty);
            OnBeforeNewWindow(e);
            cancel = e.Cancel;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// RaiseNavigatingError
        /// 
        /// <summary>
        /// ページ遷移中にエラーが生じた際に実行されます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void RaiseNavigatingError(string url, string frame, int code, out bool cancel)
        {
            var e = new NavigatingErrorEventArgs(url, frame, code);
            OnNavigatingError(e);
            cancel = e.Cancel;
        }

        #endregion

        #region Internal classes for events

        /* --------------------------------------------------------------------- */
        ///
        /// ActiveXControlEvents
        ///
        /// <summary>
        /// ActiveX コントロールで発生するイベントを WebBrowser に伝播させる
        /// ためのクラスです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        internal class ActiveXControlEvents
            : StandardOleMarshalObject, DWebBrowserEvents2
        {
            /* ----------------------------------------------------------------- */
            ///
            /// ActiveXControlEvents
            /// 
            /// <summary>
            /// オブジェクトを初期化します。
            /// </summary>
            ///
            /* ----------------------------------------------------------------- */
            public ActiveXControlEvents(WebBrowser target)
            {
                Target = target;
            }

            /* ----------------------------------------------------------------- */
            ///
            /// WebBrowser
            /// 
            /// <summary>
            /// 関連付けられた WebBrowser オブジェクトを取得します。
            /// </summary>
            ///
            /* ----------------------------------------------------------------- */
            public WebBrowser Target { get; private set; }

            /* ----------------------------------------------------------------- */
            ///
            /// BeforeNavigate2
            /// 
            /// <summary>
            /// ページ遷移が発生する直前に実行されます。
            /// </summary>
            ///
            /* ----------------------------------------------------------------- */
            public void BeforeNavigate2(object pDisp, ref object URL, ref object flags,
                ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
            {
                Target.RaiseBeforeNavigating((string)URL, (string)targetFrameName, out cancel);
            }

            /* ----------------------------------------------------------------- */
            ///
            /// NewWindow3
            /// 
            /// <summary>
            /// 新しいウィンドウが開く直前に実行されます。
            /// </summary>
            ///
            /* ----------------------------------------------------------------- */
            public void NewWindow3(object pDisp, ref bool cancel, ref object flags,
                ref object URLContext, ref object URL)
            {
                Target.RaiseBeforeNewWindow((string)URL, out cancel);
            }

            /* ----------------------------------------------------------------- */
            ///
            /// NavigateError
            /// 
            /// <summary>
            /// ページ遷移時にエラーが発生した時に実行されます。
            /// </summary>
            ///
            /* ----------------------------------------------------------------- */
            public void NavigateError(object pDisp, ref object URL, ref object targetFrameName,
                ref object statusCode, ref bool cancel)
            {
                Target.RaiseNavigatingError((string)URL, (string)targetFrameName, (int)statusCode, out cancel);
            }
        }

        #endregion

        #region COM Interfaces and/or Win32 APIs

        /* --------------------------------------------------------------------- */
        ///
        /// DWebBrowserEvents2
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/aa768283.aspx
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [System.Runtime.InteropServices.ComImport(),
         System.Runtime.InteropServices.Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D"),
         System.Runtime.InteropServices.InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIDispatch),
         System.Runtime.InteropServices.TypeLibType(System.Runtime.InteropServices.TypeLibTypeFlags.FHidden)]
        internal interface DWebBrowserEvents2
        {
            [System.Runtime.InteropServices.DispId(250)]
            void BeforeNavigate2(
                [System.Runtime.InteropServices.In,
                System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)] object pDisp,
                [System.Runtime.InteropServices.In] ref object URL,
                [System.Runtime.InteropServices.In] ref object flags,
                [System.Runtime.InteropServices.In] ref object targetFrameName, [System.Runtime.InteropServices.In] ref object postData,
                [System.Runtime.InteropServices.In] ref object headers,
                [System.Runtime.InteropServices.In,
                System.Runtime.InteropServices.Out] ref bool cancel);

            [System.Runtime.InteropServices.DispId(273)]
            void NewWindow3(
                [System.Runtime.InteropServices.In,
                System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)] object pDisp,
                [System.Runtime.InteropServices.In, System.Runtime.InteropServices.Out] ref bool cancel,
                [System.Runtime.InteropServices.In] ref object flags,
                [System.Runtime.InteropServices.In] ref object URLContext,
                [System.Runtime.InteropServices.In] ref object URL);

            [System.Runtime.InteropServices.DispId(271)]
            void NavigateError(
                [System.Runtime.InteropServices.In, System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)] object pDisp,
                [System.Runtime.InteropServices.In] ref object URL, [System.Runtime.InteropServices.In] ref object frame,
                [System.Runtime.InteropServices.In] ref object statusCode, [System.Runtime.InteropServices.In, System.Runtime.InteropServices.Out] ref bool cancel);
        }

        #endregion

        #region Fields
        private System.Windows.Forms.AxHost.ConnectionPointCookie _cookie = null;
        private ActiveXControlEvents _events = null;
        #endregion
    }
}
