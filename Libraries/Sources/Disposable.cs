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

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Disposable
    ///
    /// <summary>
    /// Provides functionality to create a IDisposable object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Disposable
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Creates a IDisposable object from the specified action.
        /// </summary>
        ///
        /// <param name="dispose">Invoke when disposed.</param>
        ///
        /// <returns>IDisposable object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IDisposable Create(Action dispose)
        {
            if (dispose == null) throw new ArgumentException(nameof(dispose));
            return new AnonymousDisposable(dispose);
        }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// AnonymousDisposable
    ///
    /// <summary>
    /// Provides functionality to convert from an action to the instance
    /// of IDisposable implemented class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal sealed class AnonymousDisposable : IDisposable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// AnonymousDisposable
        ///
        /// <summary>
        /// Initializes a new instance of the AnonymousDisposable class
        /// with the specified action.
        /// </summary>
        ///
        /// <param name="dispose">Invoke when disposed.</param>
        ///
        /* ----------------------------------------------------------------- */
        public AnonymousDisposable(Action dispose)
        {
            _dispose = new OnceAction(dispose);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Executes the provided action.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose() => _dispose.Invoke();

        #endregion

        #region Fields
        private OnceAction _dispose;
        #endregion
    }
}
