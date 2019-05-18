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
using System.Diagnostics;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Aggregator
    ///
    /// <summary>
    /// Represents type based message aggregator.
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
        /// <param name="message">Message to be publishd.</param>
        ///
        /* --------------------------------------------------------------------- */
        public void Publish<T>(T message)
        {
            if (_subscription.TryGetValue(typeof(T), out var dest))
            {
                foreach (var obj in dest)
                {
                    Debug.Assert(obj is Action<T>);
                    ((Action<T>)obj)(message);
                }
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
        public IDisposable Subscribe<T>(Action<T> callback) =>
            _subscription.GetOrAdd(typeof(T), e => new Subscription<Delegate>()).Subscribe(callback);

        #endregion

        #region Fields
        private readonly ConcurrentDictionary<Type, Subscription<Delegate>> _subscription =
            new ConcurrentDictionary<Type, Subscription<Delegate>>();
        #endregion
    }
}
