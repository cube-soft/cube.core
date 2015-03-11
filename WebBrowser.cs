/* ------------------------------------------------------------------------- */
///
/// WebBrowser.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;

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
    public class WebBrowser : System.Windows.Forms.WebBrowser
    {
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
        public event Action<object, NavigatingEventArgs> BeforeNavigating;

        /* --------------------------------------------------------------------- */
        ///
        /// BeforeNewWindow
        /// 
        /// <summary>
        /// 新しいウィンドウでページを開く直前に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event Action<object, NavigatingEventArgs> BeforeNewWindow;

        /* --------------------------------------------------------------------- */
        ///
        /// NavigatingError
        /// 
        /// <summary>
        /// ページ遷移中にエラーが生じた際に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event Action<object, NavigatingErrorEventArgs> NavigatingError;

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
            _cookie = new System.Windows.Forms.AxHost.ConnectionPointCookie(ActiveXInstance, _events, typeof(DWebBrowserEvents2));
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
        internal class ActiveXControlEvents : System.Runtime.InteropServices.StandardOleMarshalObject, DWebBrowserEvents2
        {
            public ActiveXControlEvents(WebBrowser browser) { _browser = browser; }

            public void BeforeNavigate2(object pDisp, ref object URL, ref object flags,
                ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
            {
                _browser.RaiseBeforeNavigating((string)URL, (string)targetFrameName, out cancel);
            }

            public void NewWindow3(object pDisp, ref bool cancel, ref object flags,
                ref object URLContext, ref object URL)
            {
                _browser.RaiseBeforeNewWindow((string)URL, out cancel);
            }

            public void NavigateError(object pDisp, ref object URL, ref object targetFrameName,
                ref object statusCode, ref bool cancel)
            {
                _browser.RaiseNavigatingError((string)URL, (string)targetFrameName, (int)statusCode, out cancel);
            }

            private WebBrowser _browser = null;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// DWebBrowserEvents2
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/aa768283%28v=vs.85%29.aspx
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
