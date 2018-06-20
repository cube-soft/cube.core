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
using System.Linq;
using System.Windows.Forms;

namespace Cube.Forms.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// PathBehavior
    ///
    /// <summary>
    /// パス入力用テキストボックスの挙動を定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PathBehavior : IDisposable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// PathBehavior
        ///
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /// <param name="src">パス入力用テキストボックス</param>
        ///
        /* ----------------------------------------------------------------- */
        public PathBehavior(TextBox src) : this(src, new ToolTip
        {
            ToolTipTitle = Properties.Resources.MessageInvalidPath,
            IsBalloon    = false,
            InitialDelay =  500,
            ReshowDelay  =  100,
            AutoPopDelay = 1000,
        }) { }

        /* ----------------------------------------------------------------- */
        ///
        /// PathBehavior
        ///
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /// <param name="src">パス入力用テキストボックス</param>
        /// <param name="tips">ツールチップ表示用コントロール</param>
        ///
        /* ----------------------------------------------------------------- */
        public PathBehavior(TextBox src, ToolTip tips)
        {
            _dispose = new OnceAction<bool>(Dispose);
            _chars   = new[] { '/', '*', '"', '<', '>', '|', '?', ':' };
            _message = _chars.Select(e => e.ToString()).Aggregate((x, y) => $"{x} {y}");
            _tips    = tips;
            _source  = src;

            _source.TextChanged -= WhenTextChanged;
            _source.TextChanged += WhenTextChanged;
            _source.Click       -= WhenClick;
            _source.Click       += WhenClick;
            _source.Leave       -= WhenLeave;
            _source.Leave       += WhenLeave;
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ~PathBehavior
        ///
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~PathBehavior() { _dispose.Invoke(false); }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// リソースを開放します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            _dispose.Invoke(true);
            GC.SuppressFinalize(this);
        }

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
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _source.TextChanged -= WhenTextChanged;
                _source.Click       -= WhenClick;
                _source.Leave       -= WhenLeave;
            }
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// WhenTextChanged
        ///
        /// <summary>
        /// テキスト変更時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenTextChanged(object s, EventArgs e)
        {
            _tips.Hide(_source);

            var index = _source.Text.IndexOfAny(_chars);
            if (index == 1 && _source.Text[1] == ':') index = _source.Text.IndexOfAny(_chars, 2);
            if (index >= 0)
            {
                var pos = _source.SelectionStart;
                _source.Text = _source.Text.Remove(index, 1);
                _source.SelectionStart = Math.Max(pos - 1, 0);
                _tips.Show(_message, _source, 3000);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenClick
        ///
        /// <summary>
        /// テキストボックスのクリック時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenClick(object s, EventArgs e)
        {
            if (!_focus && _source.TextLength > 0)
            {
                var str = _source.Text;
                var pos = str.LastIndexOf('\\') + 1;
                if (pos == str.Length) _source.SelectionStart = pos;
                else _source.Select(pos, str.Length - pos);
            }
            _focus = true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenLeave
        ///
        /// <summary>
        /// テキストボックスがフォーカスを失った時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenLeave(object s, EventArgs e) => _focus = false;

        #endregion

        #region Fields
        private readonly OnceAction<bool> _dispose;
        private readonly TextBox _source;
        private readonly ToolTip _tips;
        private readonly char[]  _chars;
        private readonly string  _message;
        private bool _focus = false;
        #endregion
    }
}
