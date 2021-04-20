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
using System.Threading;
using Cube.Collections;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Locale
    ///
    /// <summary>
    /// Provides the event trigger to changed the locale.
    /// </summary>
    ///
    /// <seealso href="https://msdn.microsoft.com/ja-jp/library/cc392381.aspx" />
    ///
    /* --------------------------------------------------------------------- */
    public static class Locale
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Locale
        ///
        /// <summary>
        /// Initializes static fields.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        static Locale()
        {
            _default  = new(Language.Auto);
            _accessor = _default;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Language
        ///
        /// <summary>
        /// Gets the current language.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Language Language => _accessor.Get();

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Set
        ///
        /// <summary>
        /// Sets the specified language as the current locale.
        /// </summary>
        ///
        /// <param name="value">Language.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Set(Language value)
        {
            if (!_accessor.Set(value)) return;
            foreach (var callback in _subscription) callback(value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Subscribe
        ///
        /// <summary>
        /// Adds the specified callback to the subscription.
        /// </summary>
        ///
        /// <param name="callback">
        /// Callback action when the locale changes.
        /// </param>
        ///
        /// <returns>
        /// Object to remove the registered callback.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IDisposable Subscribe(Action<Language> callback) =>
            _subscription.Subscribe(callback);

        /* ----------------------------------------------------------------- */
        ///
        /// Configure
        ///
        /// <summary>
        /// Resets the setter function.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void Configure() => Configure(_default);

        /* ----------------------------------------------------------------- */
        ///
        /// Configure
        ///
        /// <summary>
        /// Updates the accessor of the language.
        /// </summary>
        ///
        /// <param name="accessor">Accessor object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Configure(Accessor<Language> accessor) =>
            Interlocked.Exchange(ref _accessor, accessor);

        #endregion

        #region Fields
        private static Accessor<Language> _accessor;
        private static readonly Accessor<Language> _default;
        private static readonly Subscription<Action<Language>> _subscription = new();
        #endregion
    }
}
