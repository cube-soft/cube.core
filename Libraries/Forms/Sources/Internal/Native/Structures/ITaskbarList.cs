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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// ITaskbarList3
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb774652.aspx
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb774638.aspx
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/dd391692.aspx
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [ComImport()]
    [Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ITaskbarList3
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// HrInit
        ///
        /// <summary>
        /// Initializes the taskbar list object. This method must be called
        /// before any other ITaskbarList methods can be called.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [PreserveSig]
        void HrInit();

        /* ----------------------------------------------------------------- */
        ///
        /// AddTab
        ///
        /// <summary>
        /// Adds an item to the taskbar.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [PreserveSig]
        void AddTab(IntPtr hwnd);

        /* ----------------------------------------------------------------- */
        ///
        /// DeleteTab
        ///
        /// <summary>
        /// Deletes an item from the taskbar.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [PreserveSig]
        void DeleteTab(IntPtr hwnd);

        /* ----------------------------------------------------------------- */
        ///
        /// ActivateTab
        ///
        /// <summary>
        /// Activates an item on the taskbar. The window is not actually
        /// activated; the window's item on the taskbar is merely displayed
        /// as active.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [PreserveSig]
        void ActivateTab(IntPtr hwnd);

        /* ----------------------------------------------------------------- */
        ///
        /// SetActiveAlt
        ///
        /// <summary>
        /// Marks a taskbar item as active but does not visually activate it.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [PreserveSig]
        void SetActiveAlt(IntPtr hwnd);

        /* ----------------------------------------------------------------- */
        ///
        /// MarkFullscreenWindow
        ///
        /// <summary>
        /// Marks a window as full-screen.
        /// </summary>
        ///
        /// <remarks>
        /// ITaskbarList2 interface
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [PreserveSig]
        void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

        /* ----------------------------------------------------------------- */
        ///
        /// SetProgressValue
        ///
        /// <summary>
        /// Displays or updates a progress bar hosted in a taskbar button
        /// to show the specific percentage completed of the full operation.
        /// </summary>
        ///
        /// <remarks>
        /// ITaskbarList3 interface
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [PreserveSig]
        void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);

        /* ----------------------------------------------------------------- */
        ///
        /// SetProgressState
        ///
        /// <summary>
        /// Sets the type and state of the progress indicator displayed
        /// on a taskbar button.
        /// </summary>
        ///
        /// <remarks>
        /// ITaskbarList3 interface
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [PreserveSig]
        void SetProgressState(IntPtr hwnd, TaskbarProgressState state);

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// TaskbarListInstance
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb774652.aspx
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [ComImport()]
    [Guid("56fdf344-fd6d-11d0-958a-006097c9a090")]
    [ClassInterface(ClassInterfaceType.None)]
    internal class TaskbarListInstance { }
}
