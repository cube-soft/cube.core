/* ------------------------------------------------------------------------- */
///
/// NotifyQueue.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
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
