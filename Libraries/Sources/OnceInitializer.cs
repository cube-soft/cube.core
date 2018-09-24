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
    /// OnceInitializer
    ///
    /// <summary>
    /// Provides functionality to initialize and destroy an object
    /// only once.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public sealed class OnceInitializer : IDisposable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// OnceInitializer
        ///
        /// <summary>
        /// Initializes a new instance of the <c>OnceInitializer</c>
        /// with the specified actions.
        /// </summary>
        ///
        /// <param name="initialize">
        /// Action when the Invoke method is invoked.
        /// </param>
        ///
        /// <param name="dispose">
        /// Action when the Dispose method is invoked.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public OnceInitializer(Action initialize, Action dispose)
        {
            _initializer = new OnceAction(initialize);
            _disposer    = new OnceAction(dispose);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Invoked
        ///
        /// <summary>
        /// Gets the value indicating whether the Invoke method has been
        /// already invoked.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Invoked => _initializer.Invoked;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnceInitializer
        ///
        /// <summary>
        /// Invokes the action that is specified at construction.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Invoke()
        {
            if (!_disposer.Invoked) _initializer.Invoke();
            else throw new ObjectDisposedException(GetType().Name);
        }

        #endregion

        #region IDisposable

        /* ----------------------------------------------------------------- */
        ///
        /// ~OnceInitializer
        ///
        /// <summary>
        /// Finalizes the instance of the <c>OnceInitializer</c> class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~OnceInitializer() { Dispose(false); }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases all resources used by the class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
        void Dispose(bool disposing) => _disposer.Invoke();

        #endregion

        #region Fields
        private readonly OnceAction _initializer;
        private readonly OnceAction _disposer;
        #endregion
    }
}
