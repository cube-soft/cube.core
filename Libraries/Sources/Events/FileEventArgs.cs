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
using System.Collections.Generic;
using System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileSystemEventArgs
    ///
    /// <summary>
    /// ファイル選択用ダイアログまたはディレクトリ選択用ダイアログに
    /// 表示する情報を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class FileSystemEventArgs : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// FileSystemEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected FileSystemEventArgs() { }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        ///
        /// <summary>
        /// タイトルを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// FileName
        ///
        /// <summary>
        /// 選択されたパスを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string FileName { get; set; }

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
    /// FileEventArgs
    ///
    /// <summary>
    /// ファイルダイアログに表示する情報を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class FileEventArgs : FileSystemEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// FileEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected FileEventArgs() { }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// CheckPathExists
        ///
        /// <summary>
        /// 指定されたファイルの存在チェックを実行するかどうかを示す値を
        /// 取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool CheckPathExists { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// InitialDirectory
        ///
        /// <summary>
        /// ディレクトリの初期設定を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string InitialDirectory { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Filter
        ///
        /// <summary>
        /// フィルタを表す文字列を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Filter { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// FilterIndex
        ///
        /// <summary>
        /// 選択されたフィルタを表す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int FilterIndex { get; set; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// OpenFileEventArgs
    ///
    /// <summary>
    /// OpenFileDialog に表示する情報を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class OpenFileEventArgs : FileEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// OpenFileEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public OpenFileEventArgs()
        {
            CheckPathExists = true;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Multiselect
        ///
        /// <summary>
        /// 複数選択可能かどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Multiselect { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// FileNames
        ///
        /// <summary>
        /// 選択されたファイルのパス一覧を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> FileNames { get; set; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileEventArgs
    ///
    /// <summary>
    /// SaveFileDialog に表示する情報を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SaveFileEventArgs : FileEventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SaveFileEventArgs
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SaveFileEventArgs()
        {
            CheckPathExists = false;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// OverwritePrompt
        ///
        /// <summary>
        /// 上書き確認ダイアログを表示するかどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool OverwritePrompt { get; set; } = true;

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// OpenFileEventHandler
    ///
    /// <summary>
    /// OpenFileDialog を表示するための delegate です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void OpenFileEventHandler(object sender, OpenFileEventArgs e);

    /* --------------------------------------------------------------------- */
    ///
    /// SaveFileEventHandler
    ///
    /// <summary>
    /// SaveFileDialog を表示するための delegate です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Serializable]
    public delegate void SaveFileEventHandler(object sender, SaveFileEventArgs e);
}
