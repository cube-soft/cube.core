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
using System.Diagnostics;

namespace Cube.Mixin.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// ProcessExtension
    ///
    /// <summary>
    /// Provides extended methods of the Process class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ProcessExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Activate
        ///
        /// <summary>
        /// Activates the main window of the specified process.
        /// </summary>
        ///
        /// <param name="src">Source process.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Activate(this Process src)
        {
            var h = src?.MainWindowHandle ?? IntPtr.Zero;
            if (h == IntPtr.Zero) return;
            if (Cube.Forms.User32.NativeMethods.IsIconic(h))
            {
                _ = Cube.Forms.User32.NativeMethods.ShowWindowAsync(h, 9); // SW_RESTORE
            }
            _ = Cube.Forms.User32.NativeMethods.SetForegroundWindow(h);
        }

        #endregion
    }
}
