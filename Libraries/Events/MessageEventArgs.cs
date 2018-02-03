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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// MessageEventArgs
    ///
    /// <summary>
    /// ダイアログで表示されるメッセージを保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class MessageEventArgs : EventArgs
    {
        /* ----------------------------------------------------------------- */
        ///
        /// MessageEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public MessageEventArgs(string text, string caption, int type, string file, int context)
        {
            Text        = text;
            Caption     = caption;
            Type        = type;
            HelpFile    = file;
            HelpContext = context;
        }

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Handled
        ///
        /// <summary>
        /// イベントを受け取ったオブジェクトが必要な処理を行ったかどうかを
        /// 示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Handled { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Result
        ///
        /// <summary>
        /// 実行結果を表す値を取得または設定します。
        /// </summary>
        ///
        /// <remarks>
        /// この値は Handled が true の場合のみ有効となります。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public int Result { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Type
        ///
        /// <summary>
        /// メッセージの種類を示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Type { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// HelpContext
        ///
        /// <summary>
        /// ヘルプコンテキストを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int HelpContext { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Text
        ///
        /// <summary>
        /// メッセージ本文を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Text { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Caption
        ///
        /// <summary>
        /// タイトルを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Caption { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// HelpFile
        ///
        /// <summary>
        /// ヘルプ用ファイルへのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string HelpFile { get; }

        #endregion
    }
}
