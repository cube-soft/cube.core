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
using Cube.Logging;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// TaskbarProgress
    ///
    /// <summary>
    /// Provides functionality to display the progress status on the taskbar.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class TaskbarProgress : ObservableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// TaskbarProgress
        ///
        /// <summary>
        /// Initializes a new instance of the TaskbarProgress class with
        /// the specified window object.
        /// </summary>
        ///
        /// <param name="window">Window object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public TaskbarProgress(IWin32Window window) : this(window.Handle) { }

        /* ----------------------------------------------------------------- */
        ///
        /// TaskbarProgress
        ///
        /// <summary>
        /// Initializes a new instance of the TaskbarProgress class with
        /// the specified handle.
        /// </summary>
        ///
        /// <param name="handle">Window handle.</param>
        ///
        /* ----------------------------------------------------------------- */
        public TaskbarProgress(IntPtr handle) { _handle = handle; }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// State
        ///
        /// <summary>
        /// Gets or sets a value representing the progress status.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TaskbarProgressState State
        {
            get => Get(() => TaskbarProgressState.None);
            set { if (Set(value)) Refresh(); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// Gets or sets a value representing the current progress.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Value
        {
            get => Get(() => 0);
            set { if (Set(value)) Refresh(); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Maximum
        ///
        /// <summary>
        /// Gets or sets the maximum value of the progress.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Maximum
        {
            get => Get(() => 100);
            set { if (Set(value)) Refresh(); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsSupported
        ///
        /// <summary>
        /// Gets a value indicating whether the function is supported.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsSupported { get; private set; } = Environment.OSVersion.Version >= new Version(6, 1);

        /* ----------------------------------------------------------------- */
        ///
        /// Core
        ///
        /// <summary>
        /// Gets the core object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private ITaskbarList3 Core
        {
            get
            {
                if (IsSupported && _core == null)
                {
                    try { _core = (ITaskbarList3)new TaskbarListInstance(); }
                    catch (Exception err)
                    {
                        GetType().LogWarn(err);
                        IsSupported = false;
                    }
                }
                return _core;
            }
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        ///
        /// <summary>
        /// Updates on progress.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Refresh()
        {
            var cvt = Math.Min(Value, Maximum);
            Core?.SetProgressValue(_handle, (ulong)cvt, (ulong)Maximum);
            Core?.SetProgressState(_handle, State);
        }

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
        protected override void Dispose(bool disposing) { }

        #endregion

        #region Fields
        private readonly IntPtr _handle;
        private ITaskbarList3 _core;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// TaskbarProgressState
    ///
    /// <summary>
    /// Specifies the progress status.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum TaskbarProgressState
    {
        /// <summary>No progress indicator.</summary>
        None = 0,
        /// <summary>Progress unknown.</summary>
        Indeterminate = 0x1,
        /// <summary>Normal.</summary>
        Normal = 0x2,
        /// <summary>Error.</summary>
        Error = 0x4,
        /// <summary>Paused.</summary>
        Paused = 0x8
    }
}
