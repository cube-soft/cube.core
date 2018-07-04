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
using System.Reflection;
using System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// MessageEventArgs
    ///
    /// <summary>
    /// メッセージボックスに表示する情報を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class MessageEventArgs : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// MessageEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="message">メッセージ内容</param>
        /// <param name="assembly">アセンブリ情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public MessageEventArgs(string message, Assembly assembly) :
            this(message, assembly.GetReader().Title) { }

        /* ----------------------------------------------------------------- */
        ///
        /// MessageEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="message">メッセージ内容</param>
        /// <param name="title">メッセージボックスのタイトル</param>
        ///
        /* ----------------------------------------------------------------- */
        public MessageEventArgs(string message, string title) :
            this(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error) { }

        /* ----------------------------------------------------------------- */
        ///
        /// MessageEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="message">メッセージ内容</param>
        /// <param name="assembly">アセンブリ情報</param>
        /// <param name="buttons">表示ボタン</param>
        /// <param name="icon">表示アイコン</param>
        ///
        /* ----------------------------------------------------------------- */
        public MessageEventArgs(string message, Assembly assembly,
            MessageBoxButtons buttons, MessageBoxIcon icon) :
            this(message, assembly.GetReader().Title, buttons, icon) { }

        /* ----------------------------------------------------------------- */
        ///
        /// MessageEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="message">メッセージ内容</param>
        /// <param name="title">メッセージボックスのタイトル</param>
        /// <param name="buttons">表示ボタン</param>
        /// <param name="icon">表示アイコン</param>
        ///
        /* ----------------------------------------------------------------- */
        public MessageEventArgs(string message, string title,
            MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            Message = message;
            Title   = title;
            Buttons = buttons;
            Icon    = icon;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        ///
        /// <summary>
        /// メッセージボックスのタイトルを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Message
        ///
        /// <summary>
        /// メッセージを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Message { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Icon
        ///
        /// <summary>
        /// メッセージボックスに表示するアイコンを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public MessageBoxIcon Icon { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Buttons
        ///
        /// <summary>
        /// メッセージボックスに表示するボタンを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public MessageBoxButtons Buttons { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Result
        ///
        /// <summary>
        /// 実行結果を示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DialogResult Result { get; set; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// MessageEventHandler
    ///
    /// <summary>
    /// メッセージボックスを表示するための delegate です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void MessageEventHandler(object sender, MessageEventArgs e);
}
