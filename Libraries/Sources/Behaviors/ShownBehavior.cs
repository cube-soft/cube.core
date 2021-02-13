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
using System.Windows.Forms;

namespace Cube.Forms.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// ShownBehavior
    ///
    /// <summary>
    /// Represents the behavior that a Form object is shown.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ShownBehavior : DisposableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ShownBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the ShownBehavior class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="src">Source view.</param>
        /// <param name="action">Action to be shown.</param>
        ///
        /* ----------------------------------------------------------------- */
        public ShownBehavior(Form src, Action action)
        {
            void invoke(object s, EventArgs e) => action();
            src.Shown += invoke;
            _subscriber = Disposable.Create(() => src.Shown -= invoke);
        }

        #endregion

        #region Implementations

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
