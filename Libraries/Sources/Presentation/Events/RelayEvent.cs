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

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// RelayEvent
    ///
    /// <summary>
    /// Provides functionality to relay events.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class RelayEvent
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Publish
        ///
        /// <summary>
        /// Publishes an event to subscribers.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Publish()
        {
            foreach (var callback in _subscription) callback();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Subscribes the event with the specified callback.
        /// </summary>
        ///
        /// <param name="callback">Callback function.</param>
        ///
        /// <returns>Object to release.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe(Action callback) => _subscription.Subscribe(callback);

        #endregion

        #region Fields
        private readonly Subscription<Action> _subscription = new Subscription<Action>();
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// RelayEvent(T)
    ///
    /// <summary>
    /// Provides functionality to relay events.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class RelayEvent<T>
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Publish
        ///
        /// <summary>
        /// Publishes an event to subscribers.
        /// </summary>
        /// 
        /// <param name="payload">Payload data.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Publish(T payload)
        {
            foreach (var callback in _subscription) callback(payload);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Subscribes the event with the specified callback.
        /// </summary>
        ///
        /// <param name="callback">Callback function.</param>
        ///
        /// <returns>Object to release.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IDisposable Subscribe(Action<T> callback) => _subscription.Subscribe(callback);

        #endregion

        #region Fields
        private readonly Subscription<Action<T>> _subscription = new Subscription<Action<T>>();
        #endregion
    }
}
