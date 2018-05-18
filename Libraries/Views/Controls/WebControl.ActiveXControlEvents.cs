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
using System.Runtime.InteropServices;

namespace Cube.Forms
{
    partial class WebControl
    {
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
        protected class ActiveXControlEvents : StandardOleMarshalObject, DWebBrowserEvents2
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
            public ActiveXControlEvents(WebControl host)
            {
                Host = host;
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
            public WebControl Host { get; private set; }

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
                ref object postData, ref object headers, ref bool cancel)
            {
                var e = new NavigatingEventArgs((string)URL, (string)targetFrameName);
                Host.OnBeforeNavigating(e);
                cancel = e.Cancel;

            }

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
                ref object flags, ref object URLContext, ref object URL)
            {
                var e = new NavigatingEventArgs((string)URL, string.Empty);
                Host.OnBeforeNewWindow(e);
                cancel = e.Cancel;

            }

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
                ref object targetFrameName, ref object statusCode, ref bool cancel)
            {
                var e = new NavigatingErrorEventArgs((string)URL, (string)targetFrameName, (int)statusCode);
                Host.OnNavigatingError(e);
                cancel = e.Cancel;
            }
        }
    }
}
