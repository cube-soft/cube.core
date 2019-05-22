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
using Cube.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// NotifyPriority
    ///
    /// <summary>
    /// 通知項目の優先度を示す値を定義した列挙体です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum NotifyPriority
    {
        /// <summary>最高</summary>
        Highest = 40,
        /// <summary>高い</summary>
        High = 30,
        /// <summary>通常</summary>
        Normal = 20,
        /// <summary>低い</summary>
        Low = 10,
        /// <summary>最低</summary>
        Lowest = 0,
    }

    /* --------------------------------------------------------------------- */
    ///
    /// NotifyItem
    ///
    /// <summary>
    /// 通知内容を保持するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NotifyItem
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Priority
        ///
        /// <summary>
        /// 通知内容の優先度を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public NotifyPriority Priority { get; set; } = NotifyPriority.Normal;

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        ///
        /// <summary>
        /// 通知内容のタイトルを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title { get; set; }


        /* ----------------------------------------------------------------- */
        ///
        /// Description
        ///
        /// <summary>
        /// 通知内容の本文を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Description { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// DisplayTime
        ///
        /// <summary>
        /// 通知内容の表示時間を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSpan DisplayTime { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// InitialDelay
        ///
        /// <summary>
        /// 通知内容の表示を遅延させる時間を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSpan InitialDelay { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// ユーザデータを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public object Value { get; set; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// NotifyQueue
    ///
    /// <summary>
    /// NotifyItem をキュー管理するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NotifyQueue : IEnumerable<NotifyItem>, INotifyCollectionChanged
    {
        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Count
        ///
        /// <summary>
        /// 要素数を取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public int Count => _inner.Values.Aggregate(0, (n, q) => n + q.Count);

        #endregion

        #region Events

        /* --------------------------------------------------------------------- */
        ///
        /// CollectionChanged
        ///
        /// <summary>
        /// コレクションの内容が変化した時に発生するイベントです。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /* --------------------------------------------------------------------- */
        ///
        /// OnCollectionChanged
        ///
        /// <summary>
        /// CollectionChanged イベントを発生させます。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) =>
            CollectionChanged?.Invoke(this, e);

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
        public void Enqueue(NotifyItem item)
        {
            lock (_lock)
            {
                var key = item.Priority;
                if (!_inner.ContainsKey(key)) _inner.Add(key, new Queue<NotifyItem>());
                _inner[key].Enqueue(item);
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, item));
        }

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
            if (_inner.Count <= 0) return null;

            var dest = DequeueCore();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Remove, dest));
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
        public NotifyItem Peek() =>
            _inner.Count > 0 ? _inner.First().Value.Peek() : null;

        /* --------------------------------------------------------------------- */
        ///
        /// Clear
        ///
        /// <summary>
        /// コレクションの要素をすべて削除します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void Clear()
        {
            lock (_lock) _inner.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Reset));
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Clear
        ///
        /// <summary>
        /// 指定された優先度に設定されている通知をすべて削除します。
        /// </summary>
        ///
        /// <param name="priority">優先度</param>
        ///
        /* --------------------------------------------------------------------- */
        public void Clear(NotifyPriority priority)
        {
            var result = false;
            lock (_lock) result = _inner.Remove(priority);
            if (result)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset)
                );
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// GetEnumerator
        ///
        /// <summary>
        /// 反復子を取得します。
        /// </summary>
        ///
        /// <returns>反復子</returns>
        ///
        /* --------------------------------------------------------------------- */
        public IEnumerator<NotifyItem> GetEnumerator()
        {
            foreach (var queue in _inner.Values)
            foreach (var value in queue)
            {
                yield return value;
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// GetEnumerator
        ///
        /// <summary>
        /// 反復子を取得します。
        /// </summary>
        ///
        /// <returns>反復子</returns>
        ///
        /* --------------------------------------------------------------------- */
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Implementations

        /* --------------------------------------------------------------------- */
        ///
        /// DequeueCore
        ///
        /// <summary>
        /// 先頭のオブジェクトを取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private NotifyItem DequeueCore()
        {
            lock (_lock)
            {
                var pair = _inner.First();
                var dest = pair.Value.Dequeue();
                if (pair.Value.Count <= 0) _inner.Remove(pair.Key);
                return dest;
            }
        }

        #endregion

        #region Fields
        private readonly object _lock = new object();
        private readonly SortedDictionary<NotifyPriority, Queue<NotifyItem>> _inner =
            new SortedDictionary<NotifyPriority, Queue<NotifyItem>>(
              new LambdaComparer<NotifyPriority>((x, y) => y.CompareTo(x))
            );
        #endregion
    }
}
