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
using System.Collections.Generic;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// ResourceCulture
    ///
    /// <summary>
    /// Provides functionality to notify the change of resource culture.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ResourceCulture
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Configure
        ///
        /// <summary>
        /// Sets the delegation to update the culture of the resource.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void Configure(Func<string, bool> setter) => _setter = setter;

        /* ----------------------------------------------------------------- */
        ///
        /// Set
        ///
        /// <summary>
        /// Sets the new culture.
        /// </summary>
        ///
        /// <param name="name">Name of culture.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Set(string name)
        {
            if (_setter?.Invoke(name) ?? false)
            {
                lock (_lock)
                {
                    foreach (var action in _subscriptions) action();
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Registers the user action that occurs when the culture of
        /// the resource is changed.
        /// </summary>
        ///
        /// <param name="action">
        /// User action that occurs when the culture of the resource is
        /// changed.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public static IDisposable Subscribe(Action action)
        {
            lock (_lock) _subscriptions.Add(action);
            return Disposable.Create(() =>
            {
                lock (_lock) _subscriptions.Remove(action);
            });
        }

        #endregion

        #region Fields
        private static readonly object _lock = new object();
        private static readonly List<Action> _subscriptions = new List<Action>();
        private static Func<string, bool> _setter;
        #endregion
    }
}
