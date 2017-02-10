/* ------------------------------------------------------------------------- */
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
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Control = System.Windows.Forms.Control;

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

        /* ----------------------------------------------------------------- */
        ///
        /// SettingsControl
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsControl(Control ok, Control cancel, Control apply = null) : base()
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
        public Control OKButton
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
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control ApplyButton
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
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control CancelButton
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
        public IEnumerable<Control> MonitoredControls => _monitor;

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

        /* ----------------------------------------------------------------- */
        ///
        /// GetName
        ///
        /// <summary>
        /// Control オブジェクトから対応する Model の名前を返す関数を
        /// 取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<Control, string, string> GetName { get; set; } = (control, prefix) =>
        {
            var dest = control.Name.Replace(control.GetType().Name, string.Empty);
            if (string.IsNullOrEmpty(dest) || string.IsNullOrEmpty(prefix)) return dest;
            return dest.Contains(prefix) ? dest.Replace(prefix, string.Empty) : string.Empty;
        };

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

        /* ----------------------------------------------------------------- */
        ///
        /// ControlUpdate
        ///
        /// <summary>
        /// Update(object) メソッドを通じてコントロールの内容が更新された
        /// 時に発生するイベントです。
        /// </summary>
        /// 
        /// <remarks>
        /// Cancel を true に設定した場合、SettingsControl 側で行われる
        /// UpdateControl の処理をスキップします。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler<KeyValueCancelEventArgs<Control, object>> UpdateControl;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Update
        ///
        /// <summary>
        /// 指定されたオブジェクトのプロパティ値を用いて各種コントロールの
        /// 内容を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Update<T>(T model) => Update(model, string.Empty);
        public void Update<T>(T model, string prefix)
        {
            var type = model.GetType();
            var list = string.IsNullOrEmpty(prefix) ?
                       MonitoredControls :
                       MonitoredControls.Where(x => x.Name.Contains(prefix));

            foreach (var control in list)
            {
                var name = GetName?.Invoke(control, prefix);
                if (string.IsNullOrEmpty(name)) continue;

                var value = type.GetProperty(name)?.GetValue(model, null);
                if (value == null) continue;

                RaiseUpdateControl(control, value);
            }
        }

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
        /// OnUpdateControl
        ///
        /// <summary>
        /// Update(object) メソッドを通じてコントロールの内容が更新された
        /// 時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnUpdateControl(KeyValueCancelEventArgs<Control, object> e)
        {
            UpdateControl?.Invoke(this, e);
            if (e.Cancel) return;

            switch (e.Key.GetType().Name)
            {
                case nameof(ColorButton):
                    UpdateColorButton(e.Key as ColorButton, e.Value as Color?);
                    break;
                case nameof(FontButton):
                    UpdateFontButton(e.Key as FontButton, e.Value as Font);
                    break;
                case nameof(System.Windows.Forms.CheckBox):
                    UpdateCheckBox(e.Key as System.Windows.Forms.CheckBox, e.Value as bool?);
                    break;
                case nameof(System.Windows.Forms.ComboBox):
                    UpdateComboBox(e.Key as System.Windows.Forms.ComboBox, e.Value as int?);
                    break;
                case nameof(System.Windows.Forms.NumericUpDown):
                    UpdateNumericUpDown(e.Key as System.Windows.Forms.NumericUpDown, e.Value as int?);
                    break;
                case nameof(System.Windows.Forms.RadioButton):
                    UpdateRadioButton(e.Key as System.Windows.Forms.RadioButton, e.Value as bool?);
                    break;
                case nameof(System.Windows.Forms.TextBox):
                    UpdateTextBox(e.Key as System.Windows.Forms.TextBox, e.Value as string);
                    break;
                default:
                    break;
            }
        }

        #region Event handlers

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
        protected void RaisePropertyChanged(Control control, object value)
        {
            var name = GetName?.Invoke(control, string.Empty);
            if (string.IsNullOrEmpty(name)) return;

            OnPropertyChanged(KeyValueEventArgs.Create(name, value));
            if (ApplyButton != null) ApplyButton.Enabled = true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseUpdateControl
        ///
        /// <summary>
        /// UpdateControl イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected void RaiseUpdateControl(Control control, object value)
            => OnUpdateControl(KeyValueEventArgs.Create(control, value, false));

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

        #region Private event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// OKButton_Click
        ///
        /// <summary>
        /// OK ボタンが押下された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OKButton_Click(object sender, EventArgs e)
        {
            OnApply(e);
            FindForm()?.Close();
        }

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
            var control = sender as Control;
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
        private void CancelButton_Click(object sender, EventArgs e)
        {
            OnCancel(e);
            FindForm()?.Close();
        }

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

        #region Update control methods

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateColorButton
        ///
        /// <summary>
        /// ColorButton の内容を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateColorButton(ColorButton control, Color? value)
        {
            if (control == null || !value.HasValue) return;
            control.BackColor = value.Value;
            control.ForeColor = value.Value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateFontButton
        ///
        /// <summary>
        /// FontButton の内容を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateFontButton(FontButton control, Font value)
        {
            if (control == null || value == null) return;
            control.Font = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateCheckBox
        ///
        /// <summary>
        /// CheckBox の内容を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateCheckBox(System.Windows.Forms.CheckBox control, bool? value)
        {
            if (control == null || !value.HasValue) return;
            control.Checked = value.Value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateComboBox
        ///
        /// <summary>
        /// ComboBox の内容を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateComboBox(System.Windows.Forms.ComboBox control, int? value)
        {
            if (control == null || !value.HasValue) return;
            control.SelectedIndex = value.Value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateNumericUpDown
        ///
        /// <summary>
        /// NumericUpDown の内容を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateNumericUpDown(System.Windows.Forms.NumericUpDown control, int? value)
        {
            if (control == null || !value.HasValue) return;
            control.Value = value.Value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateRadioButton
        ///
        /// <summary>
        /// RadioButton の内容を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateRadioButton(System.Windows.Forms.RadioButton control, bool? value)
        {
            if (control == null || !value.HasValue) return;
            control.Checked = value.Value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateTextBox
        ///
        /// <summary>
        /// TextBox の内容を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateTextBox(System.Windows.Forms.TextBox control, string value)
        {
            if (control == null || value == null) return;
            control.Text = value;
        }

        #endregion

        #region Attach and detach methods

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
        private void Attach(Control control)
        {
            control.ControlAdded   -= Control_ControlAdded;
            control.ControlAdded   += Control_ControlAdded;
            control.ControlRemoved -= Control_ControlRemoved;
            control.ControlRemoved += Control_ControlRemoved;

            switch (control.GetType().Name)
            {
                case nameof(ColorButton):
                    var color = control as ColorButton;
                    color.BackColorChanged -= ColorButtonChanged;
                    color.BackColorChanged += ColorButtonChanged;
                    if (UseCustomColors && CustomColors != null) color.CustomColors = CustomColors;
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
                    return;
            }

            _monitor.Add(control);
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
        private void Detach(Control control)
        {
            _monitor.Remove(control);

            control.ControlAdded   -= Control_ControlAdded;
            control.ControlRemoved -= Control_ControlRemoved;

            switch (control.GetType().Name)
            {
                case nameof(ColorButton):
                    var color = control as ColorButton;
                    color.BackColorChanged -= ColorButtonChanged;
                    if (CustomColors == color.CustomColors) color.CustomColors = null;
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
                default:
                    break;
            }
        }

        #endregion

        #region Fields
        private Control _ok     = null;
        private Control _cancel = null;
        private Control _apply  = null;
        private IList<Control> _monitor = new List<Control>();
        #endregion
    }
}
