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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// SettingsControl
    ///
    /// <summary>
    /// 設定フォームを補助するためのコントロールです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SettingsControl : Panel
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SettingsControl
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsControl() { }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingsControl
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="ok">OK ボタン</param>
        /// <param name="cancel">キャンセルボタン</param>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsControl(
            System.Windows.Forms.Control ok,
            System.Windows.Forms.Control cancel
        ) : this(ok, cancel, null) { }

        /* ----------------------------------------------------------------- */
        ///
        /// SettingsControl
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="ok">OK ボタン</param>
        /// <param name="cancel">キャンセルボタン</param>
        /// <param name="apply">適用ボタン</param>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsControl(
            System.Windows.Forms.Control ok,
            System.Windows.Forms.Control cancel,
            System.Windows.Forms.Control apply)
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
        /// OK ボタンを表すコントロールを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.Control OKButton
        {
            get => _ok;
            set
            {
                if (_ok == value) return;
                if (_ok != null) _ok.Click -= WhenOK;
                _ok = value;
                if (_ok != null)
                {
                    _ok.Click -= WhenOK;
                    _ok.Click += WhenOK;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ApplyButton
        ///
        /// <summary>
        /// 適用ボタンを表すコントロールを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.Control ApplyButton
        {
            get => _apply;
            set
            {
                if (_apply == value) return;
                if (_apply != null) _apply.Click -= WhenApply;
                _apply = value;
                if (_apply != null)
                {
                    _apply.Click -= WhenApply;
                    _apply.Click += WhenApply;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CancelButton
        ///
        /// <summary>
        /// キャンセルボタンを表すコントロールを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.Control CancelButton
        {
            get => _cancel;
            set
            {
                if (_cancel == value) return;
                if (_cancel != null) _cancel.Click -= WhenCancel;
                _cancel = value;
                if (_cancel != null)
                {
                    _cancel.Click -= WhenCancel;
                    _cancel.Click += WhenCancel;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MonitoredControls
        ///
        /// <summary>
        /// 監視されているコントロールの一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<System.Windows.Forms.Control> MonitoredControls => _controls;

        /* ----------------------------------------------------------------- */
        ///
        /// CustomColors
        ///
        /// <summary>
        /// 全ての ColorButton で共有する CustomColors オブジェクトを
        /// 取得します。
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
        /// 全ての ColorButton で CustomColors オブジェクトを共有するか
        /// どうかを示す値を取得します。
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
        /// OK ボタン、または適用ボタンが押下された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Apply;

        /* ----------------------------------------------------------------- */
        ///
        /// OnApply
        ///
        /// <summary>
        /// Apply イベントを発生させます。
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
        /// キャンセルボタンが押下された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Cancel;

        /* ----------------------------------------------------------------- */
        ///
        /// OnCancel
        ///
        /// <summary>
        /// Cancel イベントを発生させます。
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
        /// 監視中のコントロールの状態が変化した時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler ControlChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnControlChanged
        ///
        /// <summary>
        /// PropertyChanged イベントを発生させます。
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
        /// コントロールが生成された時に実行されます。
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
        /// コントロールが追加された時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnControlAdded(System.Windows.Forms.ControlEventArgs e)
        {
            base.OnControlAdded(e);
            Attach(e.Control);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnControlRemoved
        ///
        /// <summary>
        /// コントロールが削除された時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            Detach(e.Control);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenOK
        ///
        /// <summary>
        /// OK ボタンのクリック時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenOK(object sender, EventArgs e)
        {
            if (ApplyButton == null || ApplyButton.Enabled) OnApply(e);
            FindForm()?.Close();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenApply
        ///
        /// <summary>
        /// 適用ボタンのクリック時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenApply(object sender, EventArgs e)
        {
            var control = sender as System.Windows.Forms.Control;
            if (control == null) return;

            OnApply(e);
            control.Enabled = false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenCancel
        ///
        /// <summary>
        /// キャンセルボタンのクリック時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenCancel(object sender, EventArgs e)
        {
            OnCancel(e);
            FindForm()?.Close();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenAdded
        ///
        /// <summary>
        /// 子コントロールが追加された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenAdded(object sender, System.Windows.Forms.ControlEventArgs e) =>
            Attach(e.Control);

        /* ----------------------------------------------------------------- */
        ///
        /// WhenRemoved
        ///
        /// <summary>
        /// 子コントロールが削除された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenRemoved(object sender, System.Windows.Forms.ControlEventArgs e) =>
            Detach(e.Control);

        /* ----------------------------------------------------------------- */
        ///
        /// Attach
        ///
        /// <summary>
        /// 指定されたコントロール以下の全てのコントロールに対して
        /// 必要なイベントを関連付けます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Attach(System.Windows.Forms.Control control)
        {
            control.ControlAdded   -= WhenAdded;
            control.ControlAdded   += WhenAdded;
            control.ControlRemoved -= WhenRemoved;
            control.ControlRemoved += WhenRemoved;

            switch (control.GetType().Name)
            {
                case nameof(ColorButton):
                    var color = control as ColorButton;
                    color.BackColorChanged -= OnControlChanged;
                    color.BackColorChanged += OnControlChanged;
                    if (UseCustomColors && CustomColors != null) color.CustomColors = CustomColors;
                    break;
                case nameof(FontButton):
                    var font = control as FontButton;
                    font.FontChanged -= OnControlChanged;
                    font.FontChanged += OnControlChanged;
                    break;
                case nameof(System.Windows.Forms.CheckBox):
                    var check = control as System.Windows.Forms.CheckBox;
                    check.CheckedChanged -= OnControlChanged;
                    check.CheckedChanged += OnControlChanged;
                    break;
                case nameof(System.Windows.Forms.ComboBox):
                    var combo = control as System.Windows.Forms.ComboBox;
                    combo.SelectedIndexChanged -= OnControlChanged;
                    combo.SelectedIndexChanged += OnControlChanged;
                    break;
                case nameof(System.Windows.Forms.NumericUpDown):
                    var num = control as System.Windows.Forms.NumericUpDown;
                    num.ValueChanged -= OnControlChanged;
                    num.ValueChanged += OnControlChanged;
                    break;
                case nameof(System.Windows.Forms.RadioButton):
                    var radio = control as System.Windows.Forms.RadioButton;
                    radio.CheckedChanged -= OnControlChanged;
                    radio.CheckedChanged += OnControlChanged;
                    break;
                case nameof(System.Windows.Forms.TextBox):
                    var text = control as System.Windows.Forms.TextBox;
                    text.TextChanged -= OnControlChanged;
                    text.TextChanged += OnControlChanged;
                    break;
                default:
                    return;
            }

            _controls.Add(control);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Detach
        ///
        /// <summary>
        /// 指定されたコントロール以下の全てのコントロールから関連付けた
        /// イベントを解除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Detach(System.Windows.Forms.Control control)
        {
            _controls.Remove(control);

            control.ControlAdded   -= WhenAdded;
            control.ControlRemoved -= WhenRemoved;

            switch (control.GetType().Name)
            {
                case nameof(ColorButton):
                    var color = control as ColorButton;
                    color.BackColorChanged -= OnControlChanged;
                    if (CustomColors == color.CustomColors) color.CustomColors = null;
                    break;
                case nameof(FontButton):
                    var font = control as FontButton;
                    font.FontChanged -= OnControlChanged;
                    break;
                case nameof(System.Windows.Forms.CheckBox):
                    var check = control as System.Windows.Forms.CheckBox;
                    check.CheckedChanged -= OnControlChanged;
                    break;
                case nameof(System.Windows.Forms.ComboBox):
                    var combo = control as System.Windows.Forms.ComboBox;
                    combo.SelectedIndexChanged -= OnControlChanged;
                    break;
                case nameof(System.Windows.Forms.NumericUpDown):
                    var num = control as System.Windows.Forms.NumericUpDown;
                    num.ValueChanged -= OnControlChanged;
                    break;
                case nameof(System.Windows.Forms.RadioButton):
                    var radio = control as System.Windows.Forms.RadioButton;
                    radio.CheckedChanged -= OnControlChanged;
                    break;
                case nameof(System.Windows.Forms.TextBox):
                    var text = control as System.Windows.Forms.TextBox;
                    text.TextChanged -= OnControlChanged;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Fields
        private System.Windows.Forms.Control _ok;
        private System.Windows.Forms.Control _cancel;
        private System.Windows.Forms.Control _apply;
        private readonly IList<System.Windows.Forms.Control> _controls = new List<System.Windows.Forms.Control>();
        #endregion
    }
}
