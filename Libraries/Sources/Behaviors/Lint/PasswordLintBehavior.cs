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
using System.Windows.Forms;
using Cube.Mixin.String;

namespace Cube.Forms.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// PasswordLintBehavior
    ///
    /// <summary>
    /// Represents the behavior of a textbox for entering a password.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PasswordLintBehavior : DisposableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// PasswordLintBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the PasswordLintBehavior class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="master">Textbox to input a password.</param>
        /// <param name="confirm">Textbox to confirm the password.</param>
        ///
        /* ----------------------------------------------------------------- */
        public PasswordLintBehavior(TextBox master, TextBox confirm) : this(master, confirm, null) { }

        /* ----------------------------------------------------------------- */
        ///
        /// PasswordLintBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the PasswordLintBehavior class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="master">Textbox to input a password.</param>
        /// <param name="confirm">Textbox to confirm the password.</param>
        /// <param name="showPassword">
        /// Value indicating whether to show the entered password.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public PasswordLintBehavior(TextBox master, TextBox confirm, CheckBox showPassword)
        {
            _master  = master  ?? throw new ArgumentNullException();
            _confirm = confirm ?? throw new ArgumentNullException();
            _show    = showPassword;

            _master.PasswordChar  = (char)0;
            _master.TextChanged  += WhenPasswordChanged;
            _confirm.TextChanged += WhenConfirmChanged;

            if (_show != null)
            {
                _master.UseSystemPasswordChar = !_show.Checked;
                _show.CheckedChanged += WhenShowPasswordChanged;
            }

            UpdateConfirmTextBox();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Valid
        ///
        /// <summary>
        /// Gets a value indicating whether the entered password is valid.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Valid =>
            _master.TextLength > 0 &&
            (!UseSystemPasswordChar || _master.Text.Equals(_confirm.Text));

        /* ----------------------------------------------------------------- */
        ///
        /// DefaultColor
        ///
        /// <summary>
        /// Gets or sets the default background color.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Color DefaultColor
        {
            get => _default;
            set
            {
                if (_default == value) return;
                _default = value;
                UpdateConfirmTextBox();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WarningColor
        ///
        /// <summary>
        /// Gets or sets the background color that represents a warning.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Color WarningColor
        {
            get => _warning;
            set
            {
                if (_warning == value) return;
                _warning = value;
                UpdateConfirmTextBox();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DisabledColor
        ///
        /// <summary>
        /// Gets or sets the background color when the textbox is disabled.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Color DisabledColor
        {
            get => _disabled;
            set
            {
                if (_disabled == value) return;
                _disabled = value;
                UpdateConfirmTextBox();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UseSystemPasswordChar
        ///
        /// <summary>
        /// Gets or sets a value indicating whether to show special symbols
        /// in place of the entered password.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool UseSystemPasswordChar
        {
            get => _master.UseSystemPasswordChar;
            set
            {
                if (_master.UseSystemPasswordChar == value) return;
                _master.UseSystemPasswordChar = value;
                if (_confirm.TextLength > 0) _confirm.Text = string.Empty;
                UpdateConfirmTextBox();
            }
        }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// Updated
        ///
        /// <summary>
        /// Occurs when the state of related objects is updated.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Updated;

        /* ----------------------------------------------------------------- */
        ///
        /// OnUpdated
        ///
        /// <summary>
        /// Raises the Updated event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnUpdated(EventArgs e) => Updated?.Invoke(this, e);

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
            if (disposing)
            {
                if (_master  != null) _master.TextChanged  -= WhenPasswordChanged;
                if (_confirm != null) _confirm.TextChanged -= WhenConfirmChanged;
                if (_show    != null) _show.CheckedChanged -= WhenShowPasswordChanged;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateConfirmTextBox
        ///
        /// <summary>
        /// Updates the state of the textbox for confirming password.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateConfirmTextBox()
        {
            var show  = !UseSystemPasswordChar;
            var error = _confirm.TextLength > 0 && !_master.Text.Equals(_confirm.Text);
            var color = show  ? DisabledColor :
                        error ? WarningColor  : DefaultColor;

            _confirm.Enabled               = !show;
            _confirm.UseSystemPasswordChar = !show;
            _confirm.BackColor             = color;

            OnUpdated(EventArgs.Empty);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenPasswordChanged
        ///
        /// <summary>
        /// Occurs when the value of the textbox is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenPasswordChanged(object sender, EventArgs e)
        {
            if (_confirm.Text.HasValue()) _confirm.Text = string.Empty;
            else OnUpdated(EventArgs.Empty);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenConfirmChanged
        ///
        /// <summary>
        /// Occurs when the value of the textbox is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenConfirmChanged(object sender, EventArgs e) =>
            UpdateConfirmTextBox();

        /* ----------------------------------------------------------------- */
        ///
        /// WhenShowPasswordChanged
        ///
        /// <summary>
        /// Occurs when the value of UseSystemPasswordChar property is
        /// changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenShowPasswordChanged(object sender, EventArgs e) =>
           UseSystemPasswordChar = !_show.Checked;

        #endregion

        #region Fields
        private readonly TextBox _master;
        private readonly TextBox _confirm;
        private readonly CheckBox _show;
        private Color _warning = Color.FromArgb(255, 102, 102);
        private Color _default = SystemColors.Window;
        private Color _disabled = SystemColors.Control;
        #endregion
    }
}
