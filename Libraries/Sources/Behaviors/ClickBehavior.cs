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
    /// ClickBehavior
    ///
    /// <summary>
    /// Represents the behavior that a Control object is clicked.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ClickBehavior : DisposableProxy
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ClickBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the ClickBehavior class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="src">Source view.</param>
        /// <param name="action">Action to click.</param>
        ///
        /* ----------------------------------------------------------------- */
        public ClickBehavior(Control src, Action action) : base(() =>
        {
            void invoke(object s, EventArgs e) => action();
            src.Click += invoke;
            return Disposable.Create(() => src.Click -= invoke);
        }) { }
    }
}
