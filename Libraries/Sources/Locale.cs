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
using Cube.Mixin.Tasks;
using System;
using System.Threading;

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
            var current = Language.Auto;
            bool setter(Language e)
            {
                var result = (e != current);
                if (result) current = e;
                return result;
            }

            _default = setter;
            _setter  = setter;
        }

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
            if (_setter(value)) _subscription.Publish(value).Forget();
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
        /// Updates the setter function of the language.
        /// </summary>
        ///
        /// <param name="setter">Setter function.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Configure(Setter<Language> setter) =>
            Interlocked.Exchange(ref _setter, setter);

        #endregion

        #region Fields
        private static Setter<Language> _setter;
        private static readonly Setter<Language> _default;
        private static readonly Subscription<Language> _subscription = new Subscription<Language>();
        #endregion
    }
}
