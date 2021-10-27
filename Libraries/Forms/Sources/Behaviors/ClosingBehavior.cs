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
    /// ClosingBehavior
    ///
    /// <summary>
    /// Represents the behavior that a Form object is closing.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ClosingBehavior : DisposableProxy
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ClosingBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the ClosingBehavior class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="src">Source view.</param>
        /// <param name="action">Action when the view is closing.</param>
        ///
        /* ----------------------------------------------------------------- */
        public ClosingBehavior(Form src, Action<FormClosingEventArgs> action) : base(() =>
        {
            void invoke(object s, FormClosingEventArgs e) => action(e);
            src.FormClosing += invoke;
            return Disposable.Create(() => src.FormClosing -= invoke);
        }) { }
    }
}
