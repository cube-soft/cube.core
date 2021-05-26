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
using System.Collections.Generic;
using System.ComponentModel;
using WF = System.Windows.Forms;

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// SettingControl
    ///
    /// <summary>
    /// Provides functionality to assist the user configuration.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SettingControl : Panel
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SettingControl
        ///
        /// <summary>
        /// Initializes a new instance of the SettingControl class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SettingControl() { }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingControl
        ///
        /// <summary>
        /// Initializes a new instance of the SettingControl class with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="ok">OK button.</param>
        /// <param name="cancel">Cancel button.</param>
        ///
        /* ----------------------------------------------------------------- */
        public SettingControl(WF.Control ok, WF.Control cancel) : this(ok, cancel, null) { }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingControl
        ///
        /// <summary>
        /// Initializes a new instance of the SettingControl class with the
        /// specified arguments.
        /// </summary>
        ///
        /// <param name="ok">OK button.</param>
        /// <param name="cancel">Cancel button.</param>
        /// <param name="apply">Apply button.</param>
        ///
        /* ----------------------------------------------------------------- */
        public SettingControl(WF.Control ok, WF.Control cancel, WF.Control apply)
        {
            OKButton     = ok;
            CancelButton = cancel;
            ApplyButton  = apply;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// OKButton
        ///
        /// <summary>
        /// Gets or sets the control that represents the OK button.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WF.Control OKButton
        {
            get => _ok;
            set
            {
                void handler(object sender, EventArgs e)
                {
                    if (ApplyButton == null || ApplyButton.Enabled) OnApply(e);
                    FindForm()?.Close();
                }

                if (_ok == value) return;
                if (_ok != null) _ok.Click -= handler;
                _ok = value;
                if (_ok != null) _ok.Click += handler;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ApplyButton
        ///
        /// <summary>
        /// Gets or sets the control that represents the apply button.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WF.Control ApplyButton
        {
            get => _apply;
            set
            {
                void handler(object s, EventArgs e)
                {
                    if (s is not WF.Control src) return;
                    OnApply(e);
                    src.Enabled = false;
                }

                if (_apply == value) return;
                if (_apply != null) _apply.Click -= handler;
                _apply = value;
                if (_apply != null) _apply.Click += handler;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CancelButton
        ///
        /// <summary>
        /// Gets or sets the control that represents the cancel button.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WF.Control CancelButton
        {
            get => _cancel;
            set
            {
                void handler(object sender, EventArgs e)
                {
                    OnCancel(e);
                    FindForm()?.Close();
                }

                if (_cancel == value) return;
                if (_cancel != null) _cancel.Click -= handler;
                _cancel = value;
                if (_cancel != null) _cancel.Click += handler;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MonitoredControls
        ///
        /// <summary>
        /// Get the list of monitored controls.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<WF.Control> MonitoredControls => _controls;

        /* ----------------------------------------------------------------- */
        ///
        /// CustomColors
        ///
        /// <summary>
        /// Gets the custom color object shared by all ColorButton controls.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<int> CustomColors { get; } = new List<int>();

        /* ----------------------------------------------------------------- */
        ///
        /// UseCustomColors
        ///
        /// <summary>
        /// Gets or sets a value indicating whether the CustomColors object
        /// is shared by all ColorButtons.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool UseCustomColors { get; set; } = true;

        #endregion

        #region Events

        #region Apply

        /* ----------------------------------------------------------------- */
        ///
        /// Apply
        ///
        /// <summary>
        /// Occurs when the apply button is pressed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Apply;

        /* ----------------------------------------------------------------- */
        ///
        /// OnApply
        ///
        /// <summary>
        /// Raises the Apply event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnApply(EventArgs e) => Apply?.Invoke(this, e);

        #endregion

        #region Cancel

        /* ----------------------------------------------------------------- */
        ///
        /// Cancel
        ///
        /// <summary>
        /// Occurs when the cancel button is pressed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Cancel;

        /* ----------------------------------------------------------------- */
        ///
        /// OnCancel
        ///
        /// <summary>
        /// Raises the Cancel event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnCancel(EventArgs e) => Cancel?.Invoke(this, e);

        #endregion

        #region ControlChanged

        /* ----------------------------------------------------------------- */
        ///
        /// ControlChanged
        ///
        /// <summary>
        /// Occurs when the state of the monitored control changes.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler ControlChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnControlChanged
        ///
        /// <summary>
        /// Raises the ControlChanged event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnControlChanged(object control, EventArgs e)
        {
            ControlChanged?.Invoke(control, e);
            if (ApplyButton != null) ApplyButton.Enabled = true;
        }

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnCreateControl
        ///
        /// <summary>
        /// Occurs when the control is created.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Attach(this);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnControlAdded
        ///
        /// <summary>
        /// Occurs when a new control is added to the control.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnControlAdded(WF.ControlEventArgs e)
        {
            base.OnControlAdded(e);
            Attach(e.Control);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnControlRemoved
        ///
        /// <summary>
        /// Occurs when a control is removed from the control.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnControlRemoved(WF.ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            Detach(e.Control);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenAdded
        ///
        /// <summary>
        /// Invokes when a new child control is added.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenAdded(object s, WF.ControlEventArgs e) => Attach(e.Control);

        /* ----------------------------------------------------------------- */
        ///
        /// WhenRemoved
        ///
        /// <summary>
        /// Invokes when a child control is removed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenRemoved(object s, WF.ControlEventArgs e) => Detach(e.Control);

        /* ----------------------------------------------------------------- */
        ///
        /// Attach
        ///
        /// <summary>
        /// Associates the necessary events for all controls below the
        /// specified control.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Attach(WF.Control src)
        {
            src.ControlAdded   -= WhenAdded;
            src.ControlAdded   += WhenAdded;
            src.ControlRemoved -= WhenRemoved;
            src.ControlRemoved += WhenRemoved;

            switch (src.GetType().Name)
            {
                case nameof(ColorButton):
                    var color = src as ColorButton;
                    color.BackColorChanged -= OnControlChanged;
                    color.BackColorChanged += OnControlChanged;
                    if (UseCustomColors && CustomColors != null) color.CustomColors = CustomColors;
                    break;
                case nameof(FontButton):
                    var font = src as FontButton;
                    font.FontChanged -= OnControlChanged;
                    font.FontChanged += OnControlChanged;
                    break;
                case nameof(WF.CheckBox):
                    var check = src as WF.CheckBox;
                    check.CheckedChanged -= OnControlChanged;
                    check.CheckedChanged += OnControlChanged;
                    break;
                case nameof(WF.ComboBox):
                    var combo = src as WF.ComboBox;
                    combo.SelectedIndexChanged -= OnControlChanged;
                    combo.SelectedIndexChanged += OnControlChanged;
                    break;
                case nameof(WF.NumericUpDown):
                    var num = src as WF.NumericUpDown;
                    num.ValueChanged -= OnControlChanged;
                    num.ValueChanged += OnControlChanged;
                    break;
                case nameof(WF.RadioButton):
                    var radio = src as WF.RadioButton;
                    radio.CheckedChanged -= OnControlChanged;
                    radio.CheckedChanged += OnControlChanged;
                    break;
                case nameof(WF.TextBox):
                    var text = src as WF.TextBox;
                    text.TextChanged -= OnControlChanged;
                    text.TextChanged += OnControlChanged;
                    break;
                default:
                    return;
            }

            _controls.Add(src);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Detach
        ///
        /// <summary>
        /// Removes the associated event from all controls below the
        /// specified control.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Detach(WF.Control src)
        {
            _ = _controls.Remove(src);

            src.ControlAdded   -= WhenAdded;
            src.ControlRemoved -= WhenRemoved;

            switch (src.GetType().Name)
            {
                case nameof(ColorButton):
                    var color = src as ColorButton;
                    color.BackColorChanged -= OnControlChanged;
                    if (CustomColors == color.CustomColors) color.CustomColors = null;
                    break;
                case nameof(FontButton):
                    var font = src as FontButton;
                    font.FontChanged -= OnControlChanged;
                    break;
                case nameof(WF.CheckBox):
                    var check = src as WF.CheckBox;
                    check.CheckedChanged -= OnControlChanged;
                    break;
                case nameof(WF.ComboBox):
                    var combo = src as WF.ComboBox;
                    combo.SelectedIndexChanged -= OnControlChanged;
                    break;
                case nameof(WF.NumericUpDown):
                    var num = src as WF.NumericUpDown;
                    num.ValueChanged -= OnControlChanged;
                    break;
                case nameof(WF.RadioButton):
                    var radio = src as WF.RadioButton;
                    radio.CheckedChanged -= OnControlChanged;
                    break;
                case nameof(WF.TextBox):
                    var text = src as WF.TextBox;
                    text.TextChanged -= OnControlChanged;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Fields
        private WF.Control _ok;
        private WF.Control _cancel;
        private WF.Control _apply;
        private readonly List<WF.Control> _controls = new();
        #endregion
    }
}
