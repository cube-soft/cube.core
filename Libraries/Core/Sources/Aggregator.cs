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
namespace Cube;

using System;
using System.Collections;
using Cube.Collections;

#region IAggregator

/* ------------------------------------------------------------------------- */
///
/// IAggregator
///
/// <summary>
/// Represents the interface of the aggregator.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public interface IAggregator
{
    /* --------------------------------------------------------------------- */
    ///
    /// Subscribe
    ///
    /// <summary>
    /// Subscribes the message of type T.
    /// </summary>
    ///
    /// <typeparam name="T">Message type.</typeparam>
    ///
    /// <param name="callback">
    /// Callback function for the message of type T.
    /// </param>
    ///
    /// <returns>Object to clear the subscription.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public IDisposable Subscribe<T>(Action<T> callback);
}

#endregion

#region Aggregator

/* ------------------------------------------------------------------------- */
///
/// Aggregator
///
/// <summary>
/// Represents the type based message aggregator.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public sealed class Aggregator : IAggregator
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// Aggregator
    ///
    /// <summary>
    /// Initializes a new instance of the Aggregator class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public Aggregator() : this(0) { }

    /* --------------------------------------------------------------------- */
    ///
    /// Aggregator
    ///
    /// <summary>
    /// Initializes a new instance of the Aggregator class with the specified
    /// capacity.
    /// </summary>
    ///
    /// <param name="capacity">
    /// Capacity of the internal hash table. If zero is specified,
    /// the initial capacity of the Hashtable class will be used.
    /// </param>
    ///
    /// <remarks>
    /// Due to the specification of the Hashtable class, the actual
    /// capacity will be as follows:
    ///
    /// [0,   3] to  2.16 ( 3 * 0.72),
    /// [4,   7] to  5.04 ( 7 * 0.72),
    /// [8,  11] to  7.92 (11 * 0.72),
    /// [12, 17] to 12.24 (17 * 0.72),
    /// [18, 23] to 16.56 (23 * 0.72),
    /// [24, 29] to 20.88 (29 * 0.72),
    /// [30, 37] to 26.64 (37 * 0.72).
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public Aggregator(int capacity) => _subscription = new(capacity);

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Publish
    ///
    /// <summary>
    /// Publishes the specified message.
    /// </summary>
    ///
    /// <param name="message">Message to be published.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void Publish<T>(T message)
    {
        if (message is null) throw new ArgumentNullException(nameof(message));
        var dest = Get(message.GetType());
        if (dest is null) return;
        foreach (var e in dest) e(message);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Subscribe
    ///
    /// <summary>
    /// Subscribes the message of type T.
    /// </summary>
    ///
    /// <typeparam name="T">Message type.</typeparam>
    ///
    /// <param name="callback">
    /// Callback function for the message of type T.
    /// </param>
    ///
    /// <returns>Object to clear the subscription.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public IDisposable Subscribe<T>(Action<T> callback) =>
        GetOrAdd(typeof(T)).Subscribe(e => callback((T)e));

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Gets the object of the specified key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private Subscription<Action<object>> Get(Type key) =>
        _subscription[key] as Subscription<Action<object>>;

    /* --------------------------------------------------------------------- */
    ///
    /// GetOrAdd
    ///
    /// <summary>
    /// Gets the object of the specified key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private Subscription<Action<object>> GetOrAdd(Type key)
    {
        if (!_subscription.ContainsKey(key))
        {
            lock (_subscription.SyncRoot)
            {
                if (!_subscription.ContainsKey(key))
                {
                    _subscription[key] = new Subscription<Action<object>>();
                }
            }
        }
        return Get(key);
    }

    #endregion

    #region Fields
    private readonly Hashtable _subscription;
    #endregion
}

#endregion
