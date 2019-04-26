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
using System.Collections.Generic;

namespace Cube.Collections
{
    /* --------------------------------------------------------------------- */
    ///
    /// Subscription
    ///
    /// <summary>
    /// Provides functionality to add or remove subscribers.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Subscription<T> : EnumerableBase<T>
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
        public int Count => Subscribers.Count;

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribers
        ///
        /// <summary>
        /// Gets the collection of subscribers.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected ConcurrentDictionary<Guid, T> Subscribers { get; } =
            new ConcurrentDictionary<Guid, T>();

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Add the specified callback to the subscription.
        /// </summary>
        ///
        /// <param name="callback">
        /// Callback to be executed when published.
        /// </param>
        ///
        /// <returns>
        /// Object to remove the registered callback.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe(T callback)
        {
            var key = Guid.NewGuid();
            Subscribers.TryAdd(key, callback);
            return Disposable.Create(() => Subscribers.TryRemove(key, out _));
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
        public override IEnumerator<T> GetEnumerator() => Subscribers.Values.GetEnumerator();

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the SubscriptionReader
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
            if (disposing) Subscribers.Clear();
        }

        #endregion
    }
}
