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

namespace Cube.Forms.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// SubscribeBehavior(T)
    ///
    /// <summary>
    /// Represents the behavior that communicates with a presentable
    /// object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class SubscribeBehavior<T> : DisposableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SubscribeBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the SubscribeBehavior class
        /// with the specified presentable object.
        /// </summary>
        ///
        /// <param name="src">Presentable object.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected SubscribeBehavior(IPresentable src)
        {
            _subscriber = src.Subscribe<T>(Invoke);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the user action.
        /// </summary>
        ///
        /// <param name="e">Parameter object.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected abstract void Invoke(T e);

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the object and
        /// optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing)
        {
            if (disposing) _subscriber.Dispose();
        }

        #endregion

        #region Fields
        private readonly IDisposable _subscriber;
        #endregion
    }
}
