﻿/* ------------------------------------------------------------------------- */
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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Cube.Collections
{
    /* --------------------------------------------------------------------- */
    ///
    /// NoticeQueue
    ///
    /// <summary>
    /// Represents the collection of notice items.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class NoticeQueue : IEnumerable<NoticeMessage>, INotifyCollectionChanged
    {
        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Count
        ///
        /// <summary>
        /// Gets the number of notice items.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public int Count => _inner.Values.Aggregate(0, (n, q) => n + q.Count);

        /* --------------------------------------------------------------------- */
        ///
        /// Empty
        ///
        /// <summary>
        /// Gets a value indicating whether the queue has no items.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public bool Empty => _inner.Count == 0;

        #endregion

        #region Events

        /* --------------------------------------------------------------------- */
        ///
        /// CollectionChanged
        ///
        /// <summary>
        /// Occurs when the collection changes.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /* --------------------------------------------------------------------- */
        ///
        /// OnCollectionChanged
        ///
        /// <summary>
        /// Raises the CollectionChanged event.
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
        /// Adds the specified notice to the end of the queue.
        /// </summary>
        ///
        /// <param name="item">Notice item.</param>
        ///
        /* --------------------------------------------------------------------- */
        public void Enqueue(NoticeMessage item)
        {
            lock (_lock)
            {
                var key = item.Priority;
                if (!_inner.ContainsKey(key)) _inner.Add(key, new());
                _inner[key].Enqueue(item);
            }

            OnCollectionChanged(new(NotifyCollectionChangedAction.Add, item));
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Dequeue
        ///
        /// <summary>
        /// Gets the first notice item and remove it from the queue.
        /// </summary>
        ///
        /// <returns>null if empty, others notice item.</returns>
        ///
        /* --------------------------------------------------------------------- */
        public NoticeMessage Dequeue()
        {
            if (_inner.Count <= 0) return null;

            var dest = DequeueCore();
            OnCollectionChanged(new(NotifyCollectionChangedAction.Remove, dest));
            return dest;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Peek
        ///
        /// <summary>
        /// Gets the first notice item without removing.
        /// </summary>
        ///
        /// <returns>null if empty, others notice item.</returns>
        ///
        /* --------------------------------------------------------------------- */
        public NoticeMessage Peek() =>
            _inner.Count > 0 ? _inner.First().Value.Peek() : null;

        /* --------------------------------------------------------------------- */
        ///
        /// Clear
        ///
        /// <summary>
        /// Removes all items in the queue.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public void Clear()
        {
            lock (_lock) _inner.Clear();
            OnCollectionChanged(new(NotifyCollectionChangedAction.Reset));
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Clear
        ///
        /// <summary>
        /// Removes all items with the specified priority in the queue.
        /// </summary>
        ///
        /// <param name="priority">Priority.</param>
        ///
        /* --------------------------------------------------------------------- */
        public void Clear(NoticePriority priority)
        {
            var result = false;
            lock (_lock) result = _inner.Remove(priority);
            if (result)
            {
                OnCollectionChanged(new(NotifyCollectionChangedAction.Reset));
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// GetEnumerator
        ///
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        ///
        /// <returns>
        /// Enumerator that can be used to iterate through the collection.
        /// </returns>
        ///
        /* --------------------------------------------------------------------- */
        public IEnumerator<NoticeMessage> GetEnumerator()
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
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        ///
        /// <returns>
        /// IEnumerator object that can be used to iterate through the
        /// collection.
        /// </returns>
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
        /// Gets the first notice item and remove it from the queue.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private NoticeMessage DequeueCore()
        {
            lock (_lock)
            {
                var pair = _inner.First();
                var dest = pair.Value.Dequeue();
                if (pair.Value.Count <= 0) _ = _inner.Remove(pair.Key);
                return dest;
            }
        }

        #endregion

        #region Fields
        private readonly object _lock = new();
        private readonly SortedDictionary<NoticePriority, Queue<NoticeMessage>> _inner =
            new(new LambdaComparer<NoticePriority>((x, y) => y.CompareTo(x)));
        #endregion
    }
}
