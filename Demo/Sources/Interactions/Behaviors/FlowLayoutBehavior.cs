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
using Cube.Forms.Controls;

namespace Cube.Forms.Demo
{
    /* --------------------------------------------------------------------- */
    ///
    /// FlowLayoutBehavior
    ///
    /// <summary>
    /// Provides functionality to adjust the width of components in the
    /// provided control when resizing.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class FlowLayoutBehavior : DisposableBase
    {
        #region Constructors

        /* --------------------------------------------------------------------- */
        ///
        /// FlowLayoutHacker
        ///
        /// <summary>
        /// Initializes a new instance of the FlowLayoutBehavior class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="src">View object.</param>
        ///
        /* --------------------------------------------------------------------- */
        public FlowLayoutBehavior(FlowLayoutPanel src)
        {
            void resize(object s, EventArgs e)
            {
                if (src.Controls.Count <= 0) return;
                src.Controls[0].Width = src.ClientSize.Width - src.Padding.Left - src.Padding.Right;
            }

            src.Resize += resize;
            _disposables.Add(Disposable.Create(() => src.Resize -= resize));
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
            if (!disposing) return;
            foreach (var e in _disposables) e.Dispose();
        }

        #endregion

        #region Fields
        private readonly IList<IDisposable> _disposables = new List<IDisposable>();
        #endregion
    }
}
