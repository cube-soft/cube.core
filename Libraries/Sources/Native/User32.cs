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
using System.Runtime.InteropServices;

namespace Cube.Forms.User32
{
    /* --------------------------------------------------------------------- */
    ///
    /// User32.NativeMethods
    ///
    /// <summary>
    /// user32.dll に定義された関数を宣言するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class NativeMethods
    {
        /* ----------------------------------------------------------------- */
        ///
        /// SendMessage
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms644950.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /* ----------------------------------------------------------------- */
        ///
        /// PostMessage
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms644944.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, CharSet = CharSet.Unicode)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /* ----------------------------------------------------------------- */
        ///
        /// ReleaseCapture
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms646261.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern bool ReleaseCapture();

        /* ----------------------------------------------------------------- */
        ///
        /// SetWindowPos
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms633545.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        /* ----------------------------------------------------------------- */
        ///
        /// GetSystemMenu
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms647985.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        /* ----------------------------------------------------------------- */
        ///
        /// EnableMenuItem
        ///
        /// <summary>
        /// https://msdn.microsoft.com/ja-jp/library/windows/desktop/ms647636.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern bool EnableMenuItem(IntPtr hMenu,
            uint uIDEnableItem, uint uEnable);

        /* ----------------------------------------------------------------- */
        ///
        /// TrackPopupMenuEx
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms648003.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags,
            int x, int y, IntPtr hwnd, IntPtr lptpm);

        /* ----------------------------------------------------------------- */
        ///
        /// SetForegroundWindow
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /* ----------------------------------------------------------------- */
        ///
        /// ShowWindowAsync
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms633549.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, SetLastError = true)]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        /* ----------------------------------------------------------------- */
        ///
        /// IsIconic
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms633527.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName)]
        public static extern bool IsIconic(IntPtr hWnd);

        #region Fields
        const string LibName = "user32.dll";
        #endregion
    }
}
