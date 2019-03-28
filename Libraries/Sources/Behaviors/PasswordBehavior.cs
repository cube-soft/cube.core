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
using Cube.Generics;
using System;
using System.Drawing;

namespace Cube.Forms.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// PasswordBehavior
    ///
    /// <summary>
    /// パスワード入力用テキストボックスの挙動を定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PasswordBehavior : DisposableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// PasswordBehavior
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="master">パスワード入力欄</param>
        /// <param name="confirm">確認用パスワード入力欄</param>
        ///
        /* ----------------------------------------------------------------- */
        public PasswordBehavior(System.Windows.Forms.TextBox master,
            System.Windows.Forms.TextBox confirm) :
            this(master, confirm, null) { }

        /* ----------------------------------------------------------------- */
        ///
        /// PasswordBehavior
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="master">パスワード入力欄</param>
        /// <param name="confirm">確認用パスワード入力欄</param>
        /// <param name="showPassword">
        /// パスワードを表示するかどうかを示すチェック項目
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public PasswordBehavior(System.Windows.Forms.TextBox master,
            System.Windows.Forms.TextBox confirm,
            System.Windows.Forms.CheckBox showPassword)
        {
            _master  = master  ?? throw new ArgumentNullException();
            _confirm = confirm ?? throw new ArgumentNullException();
            _show = showPassword;

            _master.PasswordChar = (char)0;
            _master.TextChanged += WhenPasswordChanged;
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
        /// IsValid
        ///
        /// <summary>
        /// 入力内容が正しいかどうかを示す値です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsValid =>
            _master.TextLength > 0 &&
            (!UseSystemPasswordChar || _master.Text.Equals(_confirm.Text));

        /* ----------------------------------------------------------------- */
        ///
        /// DefaultColor
        ///
        /// <summary>
        /// パスワード確認欄の既定の背景色を取得または設定します。
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
        /// パスワード確認欄の警告時の背景色を取得または設定します。
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
        /// パスワード確認欄の無効時の背景色を取得または設定します。
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
        /// パスワード入力欄の文字を隠すかどうかを示す値を取得または設定
        /// します。
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
        /// 関連オブジェクトの状態が更新された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Updated;

        /* ----------------------------------------------------------------- */
        ///
        /// OnUpdated
        ///
        /// <summary>
        /// Updated イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnUpdated(EventArgs e) =>
            Updated?.Invoke(this, e);

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// リソースを開放します。
        /// </summary>
        ///
        /// <param name="disposing">
        /// マネージオブジェクトを開放するかどうか
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
        /// パスワード確認欄の状態を更新します。
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
        /// パスワード入力欄の文字列が変更された時に実行される
        /// ハンドラです。
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
        /// パスワード確認欄の文字列が変更された時に実行される
        /// ハンドラです。
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
        /// パスワードを表示するかどうかの値が変化した時に実行される
        /// ハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenShowPasswordChanged(object sender, EventArgs e) =>
           UseSystemPasswordChar = !_show.Checked;

        #endregion

        #region Fields
        private readonly System.Windows.Forms.TextBox _master;
        private readonly System.Windows.Forms.TextBox _confirm;
        private readonly System.Windows.Forms.CheckBox _show;
        private Color _warning = Color.FromArgb(255, 102, 102);
        private Color _default = SystemColors.Window;
        private Color _disabled = SystemColors.Control;
        #endregion
    }
}
