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
using System.ComponentModel;

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// CaptionControl
    ///
    /// <summary>
    /// Represents the caption control, aka title bar.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class CaptionControl : ControlBase
    {
        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Active
        ///
        /// <summary>
        /// Gets or sets a value indicating whether the related window is
        /// active.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public bool Active
        {
            get => _active;
            set
            {
                if (_active == value) return;
                _active = value;

                if (value) OnActivated(EventArgs.Empty);
                else OnDeactivate(EventArgs.Empty);
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// WindowState
        ///
        /// <summary>
        /// Gets or sets the state of the window.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public System.Windows.Forms.FormWindowState WindowState
        {
            get => _state;
            set
            {
                if (_state == value) return;
                _state = value;

                switch (value)
                {
                    case System.Windows.Forms.FormWindowState.Maximized:
                        OnMaximized(EventArgs.Empty);
                        break;
                    case System.Windows.Forms.FormWindowState.Minimized:
                        OnMinimized(EventArgs.Empty);
                        break;
                    case System.Windows.Forms.FormWindowState.Normal:
                        OnNormalized(EventArgs.Empty);
                        break;
                    default:
                        break;
                }
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// MaximizeControl
        ///
        /// <summary>
        /// Gets the control that represents the maximize button.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.Control MaximizeControl
        {
            get => _maximize;
            protected set
            {
                if (_maximize == value) return;

                void handler(object s, EventArgs e) => OnMaximizeRequested(e);
                if (_maximize != null) _maximize.Click -= handler;
                _maximize = value;
                if (_maximize != null) _maximize.Click += handler;
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// MinimizeControl
        ///
        /// <summary>
        /// Gets the control that represents the minimize button.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.Control MinimizeControl
        {
            get => _minimize;
            protected set
            {
                if (_minimize == value) return;

                void handler(object s, EventArgs e) => OnMinmizeRequested(e);
                if (_minimize != null) _minimize.Click -= handler;
                _minimize = value;
                if (_minimize != null) _minimize.Click += handler;
            }
        }

        /* --------------------------------------------------------------------- */
        ///
        /// CloseControl
        ///
        /// <summary>
        /// Gets the control that represents the close button.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.Control CloseControl
        {
            get => _close;
            protected set
            {
                if (_close == value) return;

                void handler(object s, EventArgs e) => OnCloseRequested(e);
                if (_close != null) _close.Click -= handler;
                _close = value;
                if (_close != null) _close.Click += handler;
            }
        }

        #endregion

        #region Events

        #region CloseRequested

        /* --------------------------------------------------------------------- */
        ///
        /// CloseRequested
        ///
        /// <summary>
        /// Occurs when the close command is requested.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler CloseRequested;

        /* --------------------------------------------------------------------- */
        ///
        /// OnCloseRequested
        ///
        /// <summary>
        /// Raises the CloseRequested event.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnCloseRequested(EventArgs e) => CloseRequested?.Invoke(this, e);

        #endregion

        #region MaximizeRequested

        /* --------------------------------------------------------------------- */
        ///
        /// MaximizeRequested
        ///
        /// <summary>
        /// Occurs when the maximize command is requested.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler MaximizeRequested;

        /* --------------------------------------------------------------------- */
        ///
        /// OnMaximizeRequested
        ///
        /// <summary>
        /// Raises the MaximizeRequested event.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnMaximizeRequested(EventArgs e) => MaximizeRequested?.Invoke(this, e);

        #endregion

        #region Maximized

        /* --------------------------------------------------------------------- */
        ///
        /// Maximized
        ///
        /// <summary>
        /// Occurs when the windows is maximized.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler Maximized;

        /* --------------------------------------------------------------------- */
        ///
        /// OnMaximized
        ///
        /// <summary>
        /// Raises the Maximized event.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnMaximized(EventArgs e) => Maximized?.Invoke(this, e);

        #endregion

        #region MinimizeRequested

        /* --------------------------------------------------------------------- */
        ///
        /// MinimizeRequested
        ///
        /// <summary>
        /// Occurs when the minimize command is requested.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler MinimizeRequested;

        /* --------------------------------------------------------------------- */
        ///
        /// OnMinimizeRequested
        ///
        /// <summary>
        /// Raises the MinimizeRequested event.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnMinmizeRequested(EventArgs e) => MinimizeRequested?.Invoke(this, e);

        #endregion

        #region Minimized

        /* --------------------------------------------------------------------- */
        ///
        /// Minimized
        ///
        /// <summary>
        /// Occurs when the window is minimized.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler Minimized;

        /* --------------------------------------------------------------------- */
        ///
        /// OnMinimized
        ///
        /// <summary>
        /// Raises the Minimized event.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnMinimized(EventArgs e) => Minimized?.Invoke(this, e);

        #endregion

        #region Normalized

        /* --------------------------------------------------------------------- */
        ///
        /// Normalized
        ///
        /// <summary>
        /// Occurs when the window is normalized, i.e., unmaximized or
        /// unminimized.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler Normalized;

        /* --------------------------------------------------------------------- */
        ///
        /// OnNormalized
        ///
        /// <summary>
        /// Raises the Normalized event.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnNormalized(EventArgs e) => Normalized?.Invoke(this, e);

        #endregion

        #region Activated

        /* --------------------------------------------------------------------- */
        ///
        /// Activated
        ///
        /// <summary>
        /// Occurs when the window is active.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler Activated;

        /* --------------------------------------------------------------------- */
        ///
        /// OnActivated
        ///
        /// <summary>
        /// Raises the Activated event.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnActivated(EventArgs e) => Activated?.Invoke(this, e);

        #endregion

        #region Deactivate

        /* --------------------------------------------------------------------- */
        ///
        /// Deactivate
        ///
        /// <summary>
        /// Occurs when the window is deactive.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public event EventHandler Deactivate;

        /* --------------------------------------------------------------------- */
        ///
        /// OnDeactivate
        ///
        /// <summary>
        /// Raises the Deactivate event.
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        protected virtual void OnDeactivate(EventArgs e) => Deactivate?.Invoke(this, e);

        #endregion

        #endregion

        #region Fields
        private bool _active = true;
        private System.Windows.Forms.FormWindowState _state = System.Windows.Forms.FormWindowState.Normal;
        private System.Windows.Forms.Control _maximize;
        private System.Windows.Forms.Control _minimize;
        private System.Windows.Forms.Control _close;
        #endregion
    }
}
