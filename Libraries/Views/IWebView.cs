/* ------------------------------------------------------------------------- */
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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// IWebView
    /// 
    /// <summary>
    /// Web ページを表示するためのインターフェースです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IWebView : IControl
    {
        /* --------------------------------------------------------------------- */
        ///
        /// UserAgent
        ///
        /// <summary>
        /// UserAgent を取得または設定します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        string UserAgent { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Set
        /// 
        /// <summary>
        /// コントロールに表示する内容を URL で設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Set(Uri uri);

        /* ----------------------------------------------------------------- */
        ///
        /// Set
        /// 
        /// <summary>
        /// コントロールに表示する内容を HTML で設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Set(string html);

        /* --------------------------------------------------------------------- */
        ///
        /// BeforeNavigating
        /// 
        /// <summary>
        /// ページ遷移が発生する直前に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        event EventHandler<NavigatingEventArgs> BeforeNavigating;

        /* --------------------------------------------------------------------- */
        ///
        /// BeforeNewWindow
        /// 
        /// <summary>
        /// 新しいウィンドウでページを開く直前に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        event EventHandler<NavigatingEventArgs> BeforeNewWindow;
    }
}
