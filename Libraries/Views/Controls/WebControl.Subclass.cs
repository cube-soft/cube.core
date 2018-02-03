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
using System.Runtime.InteropServices;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// WebBrowser
    ///
    /// <summary>
    /// Web ページを表示するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    partial class WebControl
    {
        #region ActiveXControlEvents

        /* ----------------------------------------------------------------- */
        ///
        /// ActiveXControlEvents
        ///
        /// <summary>
        /// ActiveX コントロールで発生するイベントを WebBrowser に
        /// 伝播させるためのクラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        internal class ActiveXControlEvents : StandardOleMarshalObject, DWebBrowserEvents2
        {
            /* ------------------------------------------------------------- */
            ///
            /// ActiveXControlEvents
            ///
            /// <summary>
            /// オブジェクトを初期化します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public ActiveXControlEvents(WebControl target)
            {
                Target = target;
            }

            /* ------------------------------------------------------------- */
            ///
            /// WebBrowser
            ///
            /// <summary>
            /// 関連付けられた WebBrowser オブジェクトを取得します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public WebControl Target { get; private set; }

            /* ------------------------------------------------------------- */
            ///
            /// BeforeNavigate2
            ///
            /// <summary>
            /// ページ遷移が発生する直前に実行されます。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public void BeforeNavigate2(object pDisp, ref object URL,
                ref object flags, ref object targetFrameName,
                ref object postData, ref object headers, ref bool cancel) =>
                Target.RaiseBeforeNavigating((string)URL, (string)targetFrameName, out cancel);

            /* ------------------------------------------------------------- */
            ///
            /// NewWindow3
            ///
            /// <summary>
            /// 新しいウィンドウが開く直前に実行されます。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public void NewWindow3(object pDisp, ref bool cancel,
                ref object flags, ref object URLContext, ref object URL) =>
                Target.RaiseBeforeNewWindow((string)URL, out cancel);

            /* ------------------------------------------------------------- */
            ///
            /// NavigateError
            ///
            /// <summary>
            /// ページ遷移時にエラーが発生した時に実行されます。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public void NavigateError(object pDisp, ref object URL,
                ref object targetFrameName, ref object statusCode, ref bool cancel) =>
                Target.RaiseNavigatingError(
                    (string)URL, (string)targetFrameName,
                    (int)statusCode, out cancel
                );
        }

        #endregion

        #region ShowUIWebBrowserSite

        /* ----------------------------------------------------------------- */
        ///
        /// ShowUIWebBrowserSite
        ///
        /// <summary>
        /// WebBrowser 上で表示されるメッセージダイアログ等を処理する
        /// ためのクラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected class ShowUIWebBrowserSite : WebBrowserSite, IDocHostShowUI
        {
            /* ------------------------------------------------------------- */
            ///
            /// ShowUIWebBrowserSite
            ///
            /// <summary>
            /// オブジェクトを初期化します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public ShowUIWebBrowserSite(WebControl host) : base(host)
            {
                Host = host;
            }

            /* ------------------------------------------------------------- */
            ///
            /// Host
            ///
            /// <summary>
            /// 関連付ける WebBrowser オブジェクトを取得します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public WebControl Host { get; private set; }

            /* ------------------------------------------------------------- */
            ///
            /// ShowMessage
            ///
            /// <summary>
            /// メッセージを表示します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            public int ShowMessage(IntPtr hwnd, string text, string caption,
                int type, string file, int context, out int result)
            {
                Host.RaiseMessageShowing(text, caption, type, file, context, out result);
                return (result != -1) ? 0 : 1;
            }

            /* ------------------------------------------------------------- */
            ///
            /// ShowHelp
            ///
            /// <summary>
            /// ヘルプを表示します。
            /// </summary>
            ///
            /// <remarks>
            /// 現在は常にキャンセルされます。
            /// </remarks>
            ///
            /* ------------------------------------------------------------- */
            public int ShowHelp(IntPtr hwnd, string file, int command, int data,
                IntPtr /* POINT */ mouse, object hit) => 1;
        }

        #endregion

        #region DWebBrowserEvents2

        /* ----------------------------------------------------------------- */
        ///
        /// DWebBrowserEvents2
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/aa768283.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [ComImport,
         Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D"),
         InterfaceType(ComInterfaceType.InterfaceIsIDispatch),
         TypeLibType(TypeLibTypeFlags.FHidden)]
        internal interface DWebBrowserEvents2
        {
            [DispId(250)]
            void BeforeNavigate2(
                [In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
                [In] ref object URL,
                [In] ref object flags,
                [In] ref object targetFrameName,
                [In] ref object postData,
                [In] ref object headers,
                [In, Out] ref bool cancel
            );

            [DispId(273)]
            void NewWindow3(
                [In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
                [In, Out] ref bool cancel,
                [In] ref object flags,
                [In] ref object URLContext,
                [In] ref object URL
            );

            [DispId(271)]
            void NavigateError(
                [In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
                [In] ref object URL,
                [In] ref object frame,
                [In] ref object statusCode,
                [In, Out] ref bool cancel
            );
        }

        #endregion

        #region IDocHostShowUI

        /* ----------------------------------------------------------------- */
        ///
        /// IDocHostShowUI
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/aa753269.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [ComImport,
         Guid("C4D244B0-D43E-11CF-893B-00AA00BDCE1A"),
         InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        protected interface IDocHostShowUI
        {
            /// <summary>メッセージを表示します。</summary>
            [return: MarshalAs(UnmanagedType.U4)]
            [PreserveSig]
            int ShowMessage(IntPtr hwnd,
                [MarshalAs(UnmanagedType.LPWStr)] string lpstrText,
                [MarshalAs(UnmanagedType.LPWStr)] string lpstrCaption,
                int dwType,
                [MarshalAs(UnmanagedType.LPWStr)] string lpstrHelpFile,
                int dwHelpContext,
                out int lpResult
            );

            /// <summary>ヘルプを表示します。</summary>
            [return: MarshalAs(UnmanagedType.U4)]
            [PreserveSig]
            int ShowHelp(
                IntPtr hwnd,
                [MarshalAs(UnmanagedType.LPWStr)] string pszHelpFile,
                int uCommand,
                int dwData,
                IntPtr ptMouse, // POINT
                [MarshalAs(UnmanagedType.IDispatch)] object pDispatchObjectHit
            );
        }

        #endregion
    }
}
