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
using System.Collections.Concurrent;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Aggregator
    ///
    /// <summary>
    /// Represents the type based message aggregator.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public sealed class Aggregator
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
        /// <remarks>
        /// Type of the specified object is used for selecting the subscriber.
        /// </remarks>
        ///
        /* --------------------------------------------------------------------- */
        public void Publish(object message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (_subscription.TryGetValue(message.GetType(), out var dest))
            {
                foreach (var e in dest) e(message);
            }
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
        public IDisposable Subscribe<T>(Action<T> callback) => _subscription
            .GetOrAdd(typeof(T), e => new Subscription<Action<object>>())
            .Subscribe(e => callback((T)e));

        #endregion

        #region Fields
        private readonly ConcurrentDictionary<Type, Subscription<Action<object>>> _subscription =
            new ConcurrentDictionary<Type, Subscription<Action<object>>>();
        #endregion
    }
}
