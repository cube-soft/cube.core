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
using System.Collections.ObjectModel;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// NotifyQueue
    /// 
    /// <summary>
    /// NotifyItem をキュー管理するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NotifyQueue : ObservableCollection<NotifyItem>
    {
        #region Constructor

        /* --------------------------------------------------------------------- */
        ///
        /// NotifyQueue
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public NotifyQueue() : base() { }

        #endregion

        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// Enqueue
        /// 
        /// <summary>
        /// オブジェクトを末尾に追加します。
        /// </summary>
        /// 
        /// <param name="item">追加するオブジェクト</param>
        ///
        /* --------------------------------------------------------------------- */
        public void Enqueue(NotifyItem item) => Add(item);

        /* --------------------------------------------------------------------- */
        ///
        /// Dequeue
        /// 
        /// <summary>
        /// 先頭のオブジェクトを取得します。
        /// </summary>
        /// 
        /// <returns>先頭のオブジェクト</returns>
        ///
        /* --------------------------------------------------------------------- */
        public NotifyItem Dequeue()
        {
            if (Count <= 0) return null;
            var dest = base[0];
            RemoveAt(0);
            return dest;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Peek
        /// 
        /// <summary>
        /// 先頭のオブジェクトを削除せずに取得します。
        /// </summary>
        ///
        /// <returns>先頭のオブジェクト</returns>
        ///
        /* --------------------------------------------------------------------- */
        public NotifyItem Peek() => Count > 0 ? base[0] : null;

        #endregion
    }
}
