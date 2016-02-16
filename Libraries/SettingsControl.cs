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
using System.ComponentModel;

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
    public class SettingsControl : UserControl
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
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

        #endregion

        #region RaiseXxxEvent methods

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
        protected void CheckBoxChanged(object sender, EventArgs e)
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
        protected void ComboBoxChanged(object sender, EventArgs e)
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
        protected void NumericUpDownChanged(object sender, EventArgs e)
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
        protected void RadioButtonChanged(object sender, EventArgs e)
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
        protected void TextBoxChanged(object sender, EventArgs e)
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
        protected void ColorButtonChanged(object sender, EventArgs e)
        {
            var control = sender as System.Windows.Forms.Control;
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
        protected void FontButtonChanged(object sender, EventArgs e)
        {
            var control = sender as System.Windows.Forms.Control;
            if (control == null) return;
            RaisePropertyChanged(control, control.Font);
        }

        #endregion

        #region Fields
        private System.Windows.Forms.Control _ok;
        private System.Windows.Forms.Control _cancel;
        private System.Windows.Forms.Control _apply;
        #endregion
    }
}
