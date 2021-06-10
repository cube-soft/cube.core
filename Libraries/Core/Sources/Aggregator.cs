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
using System.Collections;
using Cube.Collections;

namespace Cube
{
    #region IAggregator

    /* --------------------------------------------------------------------- */
    ///
    /// IAggregator
    ///
    /// <summary>
    /// Represents the interface of the aggregator.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
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

    /* --------------------------------------------------------------------- */
    ///
    /// Aggregator
    ///
    /// <summary>
    /// Represents the type based message aggregator.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public sealed class Aggregator : IAggregator
    {
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
            if (message == null) throw new ArgumentNullException(nameof(message));
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
                    _subscription[key] = new Subscription<Action<object>>();
                }
            }
            return Get(key);
        }

        #endregion

        #region Fields
        private readonly Hashtable _subscription = new();
        #endregion
    }

    #endregion
}
