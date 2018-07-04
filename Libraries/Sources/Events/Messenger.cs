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
namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Messenger
    ///
    /// <summary>
    /// ViewModel から View を操作するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Messenger : IAggregator
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Close
        ///
        /// <summary>
        /// 画面を閉じるイベントを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent Close { get; } = new RelayEvent();

        /* ----------------------------------------------------------------- */
        ///
        /// MessageBox
        ///
        /// <summary>
        /// MessageBox を表示するイベントを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent<MessageEventArgs> MessageBox { get; } =
            new RelayEvent<MessageEventArgs>();

        /* ----------------------------------------------------------------- */
        ///
        /// OpenFileDialog
        ///
        /// <summary>
        /// OpenFileDialog を表示するイベントを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent<OpenFileEventArgs> OpenFileDialog { get; } =
            new RelayEvent<OpenFileEventArgs>();

        /* ----------------------------------------------------------------- */
        ///
        /// SaveFileDialog
        ///
        /// <summary>
        /// SaveFileDialog を表示するイベントを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent<SaveFileEventArgs> SaveFileDialog { get; } =
            new RelayEvent<SaveFileEventArgs>();

        #endregion
    }
}
