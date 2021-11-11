﻿/* ------------------------------------------------------------------------- */
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
using System.Runtime.InteropServices;

namespace Cube.Forms.Gdi32
{
    /* --------------------------------------------------------------------- */
    ///
    /// Gdi32.NativeMethods
    ///
    /// <summary>
    /// Provides functions defined in user32.dll.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class NativeMethods
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// CreateRoundRectRgn
        ///
        /// <summary>
        /// https://msdn.microsoft.com/ja-jp/library/windows/desktop/dd183516.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        #endregion

        #region Fields
        const string LibName = "Gdi32.dll";
        #endregion
    }
}
