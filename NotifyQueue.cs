/* ------------------------------------------------------------------------- */
///
/// NotifyQueue.cs
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
using System.Collections.Generic;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.NotifyQueue
    /// 
    /// <summary>
    /// 通知オブジェクトを格納するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NotifyQueue
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// NotifyQueue
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NotifyQueue() { }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Queued
        ///
        /// <summary>
        /// 新しい要素が追加された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public EventHandler Queued;

        /* ----------------------------------------------------------------- */
        ///
        /// Removed
        ///
        /// <summary>
        /// 要素が削除された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public EventHandler Removed;

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Count
        ///
        /// <summary>
        /// 要素数を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Count
        {
            get { return _data.Count; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RawData
        ///
        /// <summary>
        /// 要素管理のために使用しているオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected IList<NotifyItem> RawData
        {
            get { return _data; }
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Contains
        ///
        /// <summary>
        /// 指定された要素が存在するかどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Contains(NotifyItem item)
        {
            return RawData.Contains(item);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Clear
        ///
        /// <summary>
        /// 全ての要素を消去します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Clear()
        {
            _data.Clear();
            OnRemoved(new EventArgs());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Enqueue
        ///
        /// <summary>
        /// 新しい要素を末尾に追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Enqueue(NotifyItem item)
        {
            _data.Add(item);
            OnQueued(new EventArgs());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dequeue
        ///
        /// <summary>
        /// 先頭の要素を取り出します。
        /// </summary>
        /// 
        /// <remarks>
        /// TODO: Dequeue() 実行時に Removed イベントを発生させるべきか
        ///       どうかを検討する。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public NotifyItem Dequeue()
        {
            if (RawData.Count <= 0) return null;
            var dest = RawData[0];
            RawData.RemoveAt(0);
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Peek
        ///
        /// <summary>
        /// 先頭の要素を取り出さずに取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NotifyItem Peek()
        {
            return RawData.Count > 0 ? RawData[0] : null;
        }

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnQueued
        ///
        /// <summary>
        /// 新しい要素が追加された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnQueued(EventArgs e)
        {
            if (Queued != null) Queued(this, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnRemoved
        ///
        /// <summary>
        /// 要素が削除された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnRemoved(EventArgs e)
        {
            if (Removed != null) Removed(this, e);
        }

        #endregion

        #region Fields
        private IList<NotifyItem> _data = new List<NotifyItem>();
        #endregion
    }
}
