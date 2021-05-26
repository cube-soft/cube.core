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
using System.Drawing;

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// Button
    ///
    /// <summary>
    /// Represents the button control.
    /// </summary>
    ///
    /// <remarks>
    /// The class solves some display problems in the System.Windows.Forms.
    /// If you want to define a more flexible appearance, use the FlatButton
    /// class.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class Button : System.Windows.Forms.Button
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// ShowFocusCues
        ///
        /// <summary>
        /// Gets the value indicating whether or not to display a border
        /// when focusing.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override bool ShowFocusCues => false;

        #endregion

        #region Events

        #region NcHitTest

        /* ----------------------------------------------------------------- */
        ///
        /// NcHitTest
        ///
        /// <summary>
        /// Occurs when the hit test of the non-client area.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event QueryEventHandler<Point, Position> NcHitTest;

        /* ----------------------------------------------------------------- */
        ///
        /// OnNcHitTest
        ///
        /// <summary>
        /// Raises the NcHitTest event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnNcHitTest(QueryMessage<Point, Position> e) =>
            NcHitTest?.Invoke(this, e);

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnEnabledChanged
        ///
        /// <summary>
        /// Occurs when the Enabled property is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnEnabledChanged(EventArgs e)
        {
            try
            {
                if (Enabled == _previous) return;

                if (Enabled) SetEnabledColor();
                else SetDisabledColor();
                _previous = Enabled;
            }
            finally { base.OnEnabledChanged(e); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetEnabledColor
        ///
        /// <summary>
        /// Sets the color of the button when it is enabled.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SetEnabledColor()
        {
            BackColor = _background;
            ForeColor = _foreground;
            FlatAppearance.BorderColor = _border;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetDisabledColor
        ///
        /// <summary>
        /// Sets the color of the button when it is disabled.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SetDisabledColor()
        {
            _background = BackColor;
            _foreground = ForeColor;
            _border = FlatAppearance.BorderColor;

            BackColor = Color.FromArgb(204, 204, 204);
            ForeColor = SystemColors.GrayText;
            FlatAppearance.BorderColor = Color.FromArgb(191, 191, 191);
        }

        #endregion

        #region Fields
        private bool _previous = true;
        private Color _background = Color.Empty;
        private Color _foreground = Color.Empty;
        private Color _border = Color.Empty;
        #endregion
    }
}
