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
    /// DisposableOnceAction
    ///
    /// <summary>
    /// Provides functionality to invoke the provided action only once.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public sealed class DisposableOnceAction : DisposableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DisposableOnceAction
        ///
        /// <summary>
        /// Initializes a new instance of the DisposableOnceAction
        /// with the specified actions.
        /// </summary>
        ///
        /// <param name="action">Action to be invoked.</param>
        /// <param name="dispose">Action to be invoked when disposing.</param>
        ///
        /* ----------------------------------------------------------------- */
        public DisposableOnceAction(Action action, Action<bool> dispose)
        {
            _dispose = dispose;
            _action  = new OnceAction(action);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Invoked
        ///
        /// <summary>
        /// Gets a value indicating whether the provided action has been
        /// already invoked.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Invoked => _action.Invoked;

        /* ----------------------------------------------------------------- */
        ///
        /// IgnoreTwice
        ///
        /// <summary>
        /// Gets or sets a value indicating whether to ignore the second
        /// or later call. If set to false, TwiceException will be thrown
        /// on the second or later.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IgnoreTwice
        {
            get => _action.IgnoreTwice;
            set => _action.IgnoreTwice = value;
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the provided action.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Invoke()
        {
            if (!Disposed) _action.Invoke();
            else throw new ObjectDisposedException(GetType().Name);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the class
        /// and optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing) => _dispose?.Invoke(disposing);

        #endregion

        #region Fields
        private readonly OnceAction _action;
        private readonly Action<bool> _dispose;
        #endregion
    }
}
