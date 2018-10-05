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
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cube
{
    #region Subscription

    /* --------------------------------------------------------------------- */
    ///
    /// Subscription
    ///
    /// <summary>
    /// Provides functionality to publish and subscribe operations.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Subscription : DisposableBase
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Count
        ///
        /// <summary>
        /// Gets the number of registered callbacks.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Count => _inner.Count;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// PublishAsync
        ///
        /// <summary>
        /// Executes all registered callbacks.
        /// </summary>
        ///
        /// <returns>Task object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public async Task Publish()
        {
            foreach (var kv in _inner) await kv.Value().ConfigureAwait(false);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Add the specified callback to the subscription.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe(Action callback) => SubscribeAsync(() =>
        {
            callback();
            return Task.FromResult(0);
        });

        /* ----------------------------------------------------------------- */
        ///
        /// SubscribeAsync
        ///
        /// <summary>
        /// Add the specified callback to the subscription.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable SubscribeAsync(Func<Task> callback)
        {
            var key = Guid.NewGuid();
            var ok  = _inner.TryAdd(key, callback);

            Debug.Assert(ok);
            return Disposable.Create(() => _inner.TryRemove(key, out var _));
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the Subscription
        /// and optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing)
        {
            _inner.Clear();
        }

        #endregion

        #region Fields
        private readonly ConcurrentDictionary<Guid, Func<Task>> _inner = new ConcurrentDictionary<Guid, Func<Task>>();
        #endregion
    }

    #endregion

    #region Subscription<T>

    /* --------------------------------------------------------------------- */
    ///
    /// Subscription(T)
    ///
    /// <summary>
    /// Provides functionality to publish and subscribe operations.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Subscription<T> : DisposableBase
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Count
        ///
        /// <summary>
        /// Gets the number of registered callbacks.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Count => _inner.Count;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Publish
        ///
        /// <summary>
        /// Executes all registered callbacks with the specified value.
        /// </summary>
        ///
        /// <param name="src">
        /// Arguments that is specified in each callback.
        /// </param>
        ///
        /// <returns>Task object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public async Task Publish(T src)
        {
            foreach (var kv in _inner) await kv.Value(src).ConfigureAwait(false);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Add the specified callback to the subscription.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe(Action<T> callback) => SubscribeAsync(e =>
        {
            callback(e);
            return Task.FromResult(0);
        });

        /* ----------------------------------------------------------------- */
        ///
        /// SubscribeAsync
        ///
        /// <summary>
        /// Add the specified callback to the subscription.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable SubscribeAsync(Func<T, Task> callback)
        {
            var key = Guid.NewGuid();
            var ok  = _inner.TryAdd(key, callback);

            Debug.Assert(ok);
            return Disposable.Create(() => _inner.TryRemove(key, out var _));
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the Subscription
        /// and optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing)
        {
            _inner.Clear();
        }

        #endregion

        #region Fields
        private readonly ConcurrentDictionary<Guid, Func<T, Task>> _inner = new ConcurrentDictionary<Guid, Func<T, Task>>();
        #endregion
    }

    #endregion
}
