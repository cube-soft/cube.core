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
using System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// IWebControl
    /// 
    /// <summary>
    /// Web ページを表示するためのインターフェースです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public interface IWebControl : IControl
    {
        /* --------------------------------------------------------------------- */
        ///
        /// Document
        ///
        /// <summary>
        /// 画面に表示されている Web ページの HtmlDocument オブジェクトを
        /// 取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        HtmlDocument Document { get; }

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

        /* --------------------------------------------------------------------- */
        ///
        /// IsBusy
        ///
        /// <summary>
        /// 表示処理中かどうかを示す値を取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        bool IsBusy { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Start
        /// 
        /// <summary>
        /// URL で示されたコンテンツの表示を開始します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Start(Uri uri);

        /* ----------------------------------------------------------------- */
        ///
        /// Start
        /// 
        /// <summary>
        /// HTML で示されたコンテンツの表示を開始します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Start(string html);

        /* ----------------------------------------------------------------- */
        ///
        /// Stop
        /// 
        /// <summary>
        /// コンテンツの表示を中断します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        void Stop();

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

        /* --------------------------------------------------------------------- */
        ///
        /// DocumentCompleted
        /// 
        /// <summary>
        /// コンテンツの表示が完了した時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        event WebBrowserDocumentCompletedEventHandler DocumentCompleted;
    }
}
