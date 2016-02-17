/* ------------------------------------------------------------------------- */
///
/// SettingsControl.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// SettingsControl
    /// 
    /// <summary>
    /// 設定フォームを補助するためのコントロールクラスです。
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
        public SettingsControl() : base() { }

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
        public System.Windows.Forms.Control OKButton
        {
            get { return _ok; }
            set
            {
                if (_ok == value) return;
                if (_ok != null) _ok.Click -= OKButton_Click;
                _ok = value;
                if (_ok != null)
                {
                    _ok.Click -= OKButton_Click;
                    _ok.Click += OKButton_Click;
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
        public System.Windows.Forms.Control ApplyButton
        {
            get { return _apply; }
            set
            {
                if (_apply == value) return;
                if (_apply != null) _apply.Click -= ApplyButton_Click;
                _apply = value;
                if (_apply != null)
                {
                    _apply.Click -= ApplyButton_Click;
                    _apply.Click += ApplyButton_Click;
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
        public System.Windows.Forms.Control CancelButton
        {
            get { return _cancel; }
            set
            {
                if (_cancel == value) return;
                if (_cancel != null) _cancel.Click -= CancelButton_Click;
                _cancel = value;
                if (_cancel != null)
                {
                    _cancel.Click -= CancelButton_Click;
                    _cancel.Click += CancelButton_Click;
                }
            }
        }

        #endregion

        #region Events

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
        /// PropertyChanged
        ///
        /// <summary>
        /// 各種コントロールの内容が変化した時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler<KeyValueEventArgs<string, object>> PropertyChanged;

        #endregion

        #region Virtual methods

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

        /* ----------------------------------------------------------------- */
        ///
        /// OnPropertyChanged
        ///
        /// <summary>
        /// PropertyChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPropertyChanged(KeyValueEventArgs<string, object> e)
            => PropertyChanged?.Invoke(this, e);

        /* ----------------------------------------------------------------- */
        ///
        /// CheckBoxChanged
        ///
        /// <summary>
        /// チェックボックスの状態が変化した時に実行されるハンドラです。
        /// </summary>
        /// 
        /// <remarks>
        /// Value には CheckBox.Checked が設定されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void CheckBoxChanged(object sender, EventArgs e)
        {
            var control = sender as System.Windows.Forms.CheckBox;
            if (control == null) return;
            RaisePropertyChanged(control, control.Checked);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ComboBoxChanged
        ///
        /// <summary>
        /// コンボボックスの状態が変化した時に実行されるハンドラです。
        /// </summary>
        /// 
        /// <remarks>
        /// Value には ComboBox.SelectedIndex が設定されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void ComboBoxChanged(object sender, EventArgs e)
        {
            var control = sender as System.Windows.Forms.ComboBox;
            if (control == null) return;
            RaisePropertyChanged(control, control.SelectedIndex);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// NumericUpDownChanged
        ///
        /// <summary>
        /// NumericUpDown の状態が変化した時に実行されるハンドラです。
        /// </summary>
        /// 
        /// <remarks>
        /// Value には NumericUpDown.Value が設定されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void NumericUpDownChanged(object sender, EventArgs e)
        {
            var control = sender as System.Windows.Forms.NumericUpDown;
            if (control == null) return;
            RaisePropertyChanged(control, control.Value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RadioButtonChanged
        ///
        /// <summary>
        /// ラジオボタンの状態が変化した時に実行されるハンドラです。
        /// </summary>
        /// 
        /// <remarks>
        /// Value には RadioButton.Checked が設定されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void RadioButtonChanged(object sender, EventArgs e)
        {
            var control = sender as System.Windows.Forms.RadioButton;
            if (control == null) return;
            RaisePropertyChanged(control, control.Checked);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TextBoxChanged
        ///
        /// <summary>
        /// テキストボックスの状態が変化した時に実行されるハンドラです。
        /// </summary>
        /// 
        /// <remarks>
        /// Value には TextBox.Text が設定されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void TextBoxChanged(object sender, EventArgs e)
        {
            var control = sender as System.Windows.Forms.TextBox;
            if (control == null) return;
            RaisePropertyChanged(control, control.Text);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ColorButtonChanged
        ///
        /// <summary>
        /// 色選択用ボタンの状態が変化した時に実行されるハンドラです。
        /// </summary>
        /// 
        /// <remarks>
        /// Value には TextBox.BackColor が設定されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void ColorButtonChanged(object sender, EventArgs e)
        {
            var control = sender as ColorButton;
            if (control == null) return;
            RaisePropertyChanged(control, control.BackColor);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FontButtonChanged
        ///
        /// <summary>
        /// フォント選択用ボタンの状態が変化した時に実行されるハンドラです。
        /// </summary>
        /// 
        /// <remarks>
        /// Value には TextBox.Font が設定されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void FontButtonChanged(object sender, EventArgs e)
        {
            var control = sender as FontButton;
            if (control == null) return;
            RaisePropertyChanged(control, control.Font);
        }

        #endregion

        #region Non-virtual protected methods

        /* ----------------------------------------------------------------- */
        ///
        /// RaisePropertyChanged
        ///
        /// <summary>
        /// PropertyChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected void RaisePropertyChanged(System.Windows.Forms.Control control, object value)
        {
            var name = control.Name.Replace(control.GetType().Name, string.Empty);
            if (string.IsNullOrEmpty(name)) return;

            OnPropertyChanged(new KeyValueEventArgs<string, object>(name, value));
            if (ApplyButton != null) ApplyButton.Enabled = true;
        }

        #endregion

        #region Override methods

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

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// OKButton_Click
        ///
        /// <summary>
        /// OK ボタンが押下された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OKButton_Click(object sender, EventArgs e) => OnApply(e);

        /* ----------------------------------------------------------------- */
        ///
        /// ApplyButton_Click
        ///
        /// <summary>
        /// 適用ボタンが押下された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            var control = sender as System.Windows.Forms.Control;
            if (control == null) return;

            OnApply(e);
            control.Enabled = false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CancelButton_Click
        ///
        /// <summary>
        /// キャンセルボタンが押下された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void CancelButton_Click(object sender, EventArgs e) => OnCancel(e);

        /* ----------------------------------------------------------------- */
        ///
        /// Control_ControlAdded
        ///
        /// <summary>
        /// コントロールに子コントロールが追加された時に実行される
        /// ハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Control_ControlAdded(object sender, System.Windows.Forms.ControlEventArgs e)
            => Attach(e.Control);

        /* ----------------------------------------------------------------- */
        ///
        /// Control_ControlRemoved
        ///
        /// <summary>
        /// コントロールから子コントロールが削除された時に実行される
        /// ハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Control_ControlRemoved(object sender, System.Windows.Forms.ControlEventArgs e)
            => Detach(e.Control);

        #endregion

        #region Others

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
            control.ControlAdded -= Control_ControlAdded;
            control.ControlAdded += Control_ControlAdded;
            control.ControlRemoved -= Control_ControlRemoved;
            control.ControlRemoved += Control_ControlRemoved;
            _controllist.Add(control);
            switch (control.GetType().Name)
            {
                case nameof(ColorButton):
                    var color = control as ColorButton;
                    color.BackColorChanged -= ColorButtonChanged;
                    color.BackColorChanged += ColorButtonChanged;
                    break;
                case nameof(FontButton):
                    var font = control as FontButton;
                    font.FontChanged -= FontButtonChanged;
                    font.FontChanged += FontButtonChanged;
                    break;
                case nameof(System.Windows.Forms.CheckBox):
                    var check = control as System.Windows.Forms.CheckBox;
                    check.CheckedChanged -= CheckBoxChanged;
                    check.CheckedChanged += CheckBoxChanged;
                    break;
                case nameof(System.Windows.Forms.ComboBox):
                    var combo = control as System.Windows.Forms.ComboBox;
                    combo.SelectedIndexChanged -= ComboBoxChanged;
                    combo.SelectedIndexChanged += ComboBoxChanged;
                    break;
                case nameof(System.Windows.Forms.NumericUpDown):
                    var num = control as System.Windows.Forms.NumericUpDown;
                    num.ValueChanged -= NumericUpDownChanged;
                    num.ValueChanged += NumericUpDownChanged;
                    break;
                case nameof(System.Windows.Forms.RadioButton):
                    var radio = control as System.Windows.Forms.RadioButton;
                    radio.CheckedChanged -= RadioButtonChanged;
                    radio.CheckedChanged += RadioButtonChanged;
                    break;
                case nameof(System.Windows.Forms.TextBox):
                    var text = control as System.Windows.Forms.TextBox;
                    text.TextChanged -= TextBoxChanged;
                    text.TextChanged += TextBoxChanged;
                    break;
                default:
                    _controllist.Remove(control);
                    break;
            }
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
            control.ControlAdded -= Control_ControlAdded;
            control.ControlRemoved -= Control_ControlRemoved;
            _controllist.Remove(control);
            switch (control.GetType().Name)
            {
                case nameof(ColorButton):
                    var color = control as ColorButton;
                    color.BackColorChanged -= ColorButtonChanged;
                    break;
                case nameof(FontButton):
                    var font = control as FontButton;
                    font.FontChanged -= FontButtonChanged;
                    break;
                case nameof(System.Windows.Forms.CheckBox):
                    var check = control as System.Windows.Forms.CheckBox;
                    check.CheckedChanged -= CheckBoxChanged;
                    break;
                case nameof(System.Windows.Forms.ComboBox):
                    var combo = control as System.Windows.Forms.ComboBox;
                    combo.SelectedIndexChanged -= ComboBoxChanged;
                    break;
                case nameof(System.Windows.Forms.NumericUpDown):
                    var num = control as System.Windows.Forms.NumericUpDown;
                    num.ValueChanged -= NumericUpDownChanged;
                    break;
                case nameof(System.Windows.Forms.RadioButton):
                    var radio = control as System.Windows.Forms.RadioButton;
                    radio.CheckedChanged -= RadioButtonChanged;
                    break;
                case nameof(System.Windows.Forms.TextBox):
                    var text = control as System.Windows.Forms.TextBox;
                    text.TextChanged -= TextBoxChanged;
                    break;
            }
        }

        public void UpdateFromSetting(object setting)
        {
            var type=setting.GetType();
            foreach(var control in _controllist)
            {
                var name = control.Name.Replace(control.GetType().Name, string.Empty);
                var value = type.GetProperty(name).GetValue(setting);
                if (value == null) return;
                CheckUpdateType(control, name, value);
            }
        }

        protected virtual void CheckUpdateType(System.Windows.Forms.Control control,string propertyname,object value)
        {
            switch (control.GetType().Name)
            {
                case nameof(ColorButton):
                    if (!(value is System.Drawing.Color)) break;
                    var color = control as ColorButton;
                    color.BackColor = (System.Drawing.Color)value;
                    color.ForeColor = (System.Drawing.Color)value;
                    break;
                case nameof(FontButton):
                    if (!(value is System.Drawing.Font)) break;
                    var font = control as FontButton;
                    font.Font = (System.Drawing.Font)value;
                    break;
                case nameof(System.Windows.Forms.CheckBox):
                    if (!(value is bool)) break;
                    var check = control as System.Windows.Forms.CheckBox;
                    check.Checked = (bool)value;
                    break;
                case nameof(System.Windows.Forms.ComboBox):
                    if (!(value is int)) break;
                    var combo = control as System.Windows.Forms.ComboBox;
                    combo.SelectedIndex = (int)value;
                    break;
                case nameof(System.Windows.Forms.NumericUpDown):
                    if (!(value is int)) break;
                    var num = control as System.Windows.Forms.NumericUpDown;
                    num.Value = (int)value;
                    break;
                case nameof(System.Windows.Forms.RadioButton):
                    if (!(value is bool)) break;
                    var radio = control as System.Windows.Forms.RadioButton;
                    radio.Checked = (bool)value;
                    break;
                case nameof(System.Windows.Forms.TextBox):
                    if (!(value is string)) break;
                    var text = control as System.Windows.Forms.TextBox;
                    text.Text = (string)value;
                    break;
            }
        }
        #endregion

        #region Fields
        private System.Windows.Forms.Control _ok=null;
        private System.Windows.Forms.Control _cancel=null;
        private System.Windows.Forms.Control _apply=null;
        private System.Collections.Generic.List<System.Windows.Forms.Control> _controllist= 
            new System.Collections.Generic.List<System.Windows.Forms.Control>();
        #endregion
    }
}
