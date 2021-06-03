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
using System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// WindowBase
    ///
    /// <summary>
    /// Represents the base class of WinForms-based IBindable implementation.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class WindowBase : Form, IBinder
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Bindable
        ///
        /// <summary>
        /// Gets the bindable object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected IBindable Bindable { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Behaviors
        ///
        /// <summary>
        /// Gets the collection of registered behaviors.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected DisposableContainer Behaviors { get; } = new();

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Bind
        ///
        /// <summary>
        /// Binds the window to the specified object.
        /// If the Bindable property is already set, the specified object
        /// is ignored.
        /// </summary>
        ///
        /// <param name="src">Object to bind.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Bind(IBindable src)
        {
            if (Bindable != null) return;
            Bindable = src;
            OnBind(src);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnBind
        ///
        /// <summary>
        /// Binds the window to the specified object.
        /// </summary>
        ///
        /// <param name="src">Object to bind.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnBind(IBindable src) { }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the StandardForm
        /// and optionally releases the managed resources.
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
            try
            {
                if (_disposed) return;
                _disposed = true;
                if (!disposing) return;

                Behaviors.Dispose();
                Bindable?.Dispose();
                Bindable = null;
            }
            finally { base.Dispose(disposing); }
        }

        #endregion

        #region Fields
        private bool _disposed = false;
        #endregion
    }
}
