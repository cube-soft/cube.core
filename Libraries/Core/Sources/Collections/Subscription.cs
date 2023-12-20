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
namespace Cube.Collections;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

/* ------------------------------------------------------------------------- */
///
/// Subscription
///
/// <summary>
/// Provides functionality to add or remove subscribers.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public sealed class Subscription<T> : EnumerableBase<T>
{
    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Count
    ///
    /// <summary>
    /// Gets the number of registered callbacks.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public int Count => _subscribers.Count;

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Subscribe
    ///
    /// <summary>
    /// Add the specified subscriber to the subscription.
    /// </summary>
    ///
    /// <param name="subscriber">Subscriber object.</param>
    ///
    /// <returns>
    /// Object to remove the specified subscriber.
    /// </returns>
    ///
    /* --------------------------------------------------------------------- */
    public IDisposable Subscribe(T subscriber)
    {
        var key = Guid.NewGuid();
        return _subscribers.TryAdd(key, subscriber) ?
               Disposable.Create(() => _subscribers.TryRemove(key, out _)) :
               Subscribe(subscriber); // Retry due to GUID collision.
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
    public override IEnumerator<T> GetEnumerator() => _subscribers.Values.GetEnumerator();

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    protected override void Dispose(bool disposing)
    {
        if (disposing) _subscribers.Clear();
    }

    #endregion

    #region Fields
    private readonly ConcurrentDictionary<Guid, T> _subscribers = new();
    #endregion
}
