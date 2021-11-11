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
using System.ComponentModel;

namespace Cube.Mixin.Observing
{
    /* --------------------------------------------------------------------- */
    ///
    /// Extension
    ///
    /// <summary>
    /// Provides extended methods of IObservePropertyChanged and its
    /// implemented classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Extension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Associates the specified callback to the PropertyChanged event.
        /// </summary>
        ///
        /// <param name="src">Observable source.</param>
        /// <param name="callback">
        /// Action to invoked when the PropertyChanged event is fired.
        /// </param>
        ///
        /// <returns>
        /// Object to remove the callback from the PropertyChanged event
        /// handler.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IDisposable Subscribe(this INotifyPropertyChanged src, Action<string> callback)
        {
            void handler(object s, PropertyChangedEventArgs e) => callback(e.PropertyName);
            src.PropertyChanged += handler;
            return Disposable.Create(() => src.PropertyChanged -= handler);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Hook
        ///
        /// <summary>
        /// Observes the specified observer and the specified object.
        /// </summary>
        ///
        /// <param name="src">Source observer.</param>
        /// <param name="obj">Object to be observed.</param>
        /// <param name="names">Target property names.</param>
        ///
        /// <returns>Source observer.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Hook<T>(this T src, INotifyPropertyChanged obj, params string[] names)
            where T : IObservePropertyChanged
        {
            src.Observe(obj, names);
            return src;
        }

        #endregion
    }
}
