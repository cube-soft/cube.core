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
using System.Windows.Forms;

namespace Cube.Forms
{
    partial class WebControl
    {
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
            #region Methods

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
                var mb = GetButtons(type & 0x0f);
                var mi = GetIcon(type & 0xf0);
                var e  = new MessageEventArgs(text, caption, mb, mi);
                Host.OnMessageShowing(e);
                result = (int)e.Result;
                return (e.Result != DialogResult.None) ? 1 : 0;
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

            #endregion

            #region Implementations

            /* ------------------------------------------------------------- */
            ///
            /// GetButtons
            ///
            /// <summary>
            /// 表示ボタンを示すオブジェクトを取得します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            private MessageBoxButtons GetButtons(int src)
            {
                var buttons = Enum.GetValues(typeof(MessageBoxButtons));
                foreach (MessageBoxButtons mb in buttons)
                {
                    if (src == (int)mb) return mb;
                }

                if (src == 0x06) return MessageBoxButtons.AbortRetryIgnore;
                else return MessageBoxButtons.OK;
            }

            /* ------------------------------------------------------------- */
            ///
            /// GetIcon
            ///
            /// <summary>
            /// 表示アイコンを示すオブジェクトを取得します。
            /// </summary>
            ///
            /* ------------------------------------------------------------- */
            private MessageBoxIcon GetIcon(int src)
            {
                var icons = Enum.GetValues(typeof(MessageBoxIcon));
                foreach (MessageBoxIcon mi in icons)
                {
                    if (src == (int)mi) return mi;
                }
                return MessageBoxIcon.Error;
            }

            #endregion
        }
    }
}
