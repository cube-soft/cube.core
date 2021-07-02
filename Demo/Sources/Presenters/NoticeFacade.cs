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
using System.Threading;
using Cube.Collections;
using Cube.Logging;

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// NoticeFacade
    ///
    /// <summary>
    /// Represents the facade for notice functions.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public sealed class NoticeFacade
    {
        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Notify
        ///
        /// <summary>
        /// Gets the action to take when notified of a new message.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public Action<NoticeMessage> Notify { get; set; }

        #endregion

        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// Enqueue
        ///
        /// <summary>
        /// Adds a new message with the specified callback.
        /// </summary>
        ///
        /// <param name="callback">Callback action.</param>
        ///
        /* --------------------------------------------------------------------- */
        public void Enqueue(NoticeCallback callback) => Enqueue(Create(callback));

        /* --------------------------------------------------------------------- */
        ///
        /// Enqueue
        ///
        /// <summary>
        /// Adds the specified message.
        /// </summary>
        ///
        /// <param name="src">New message.</param>
        ///
        /* --------------------------------------------------------------------- */
        public void Enqueue(NoticeMessage src)
        {
            _queue.Enqueue(src);
            Consume(false);
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the operation corresponding to the user's choice.
        /// </summary>
        ///
        /// <param name="src">Notice message.</param>
        /// <param name="result">User action or timeout.</param>
        ///
        /* --------------------------------------------------------------------- */
        public void Invoke(NoticeMessage src, NoticeResult result)
        {
            try { GetType().LogDebug($"Result:{result}", $"Value:{src.Value}"); }
            finally
            {
                if (!_queue.Empty) Consume(true);
                else _ = Interlocked.Exchange(ref _busy, default);
            }
        }

        #endregion

        #region Implementations

        /* --------------------------------------------------------------------- */
        ///
        /// Consume
        ///
        /// <summary>
        /// Dequeues a message and invokes the provided action.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private void Consume(bool continuous)
        {
            if (_queue.Empty) return;
            if (!continuous && _busy != null) return;
            if (!continuous && Interlocked.Exchange(ref _busy, new()) != null) return;
            Notify?.Invoke(_queue.Dequeue());
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Creates a new message.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        private NoticeMessage Create(NoticeCallback callback) => new()
        {
            Title        = "Notice Demo",
            Text         = $"This is a sample message ({++_count})",
            Value        = "DummyData",
            DisplayTime  = TimeSpan.FromSeconds(60),
            InitialDelay = TimeSpan.FromSeconds(1),
            Priority     = NoticePriority.Normal,
            Location     = (NoticeLocation)(_count % Enum.GetValues(typeof(NoticeLocation)).Length),
            Callback     = callback,
        };


        #endregion

        #region Fields
        private readonly NoticeQueue _queue = new();
        private object _busy;
        private int _count = 0;
        #endregion
    }
}
