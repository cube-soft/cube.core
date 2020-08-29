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
    /// Represents the behavior of a textbox to entering a path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PathBehavior : DisposableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// PathBehavior
        ///
        /// <summary>
        /// Initializes a new instance of the PathBehavior class with the
        /// specified control.
        /// </summary>
        ///
        /// <param name="src">Textbox control.</param>
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
        /// Initializes a new instance of the PathBehavior class with the
        /// specified controls.
        /// </summary>
        ///
        /// <param name="src">Textbox control.</param>
        /// <param name="tips">Tooltip control.</param>
        ///
        /* ----------------------------------------------------------------- */
        public PathBehavior(TextBox src, ToolTip tips)
        {
            _chars   = new[] { '/', '*', '"', '<', '>', '|', '?', ':' };
            _tips    = tips;
            _source  = src;
            _message = _chars.Select(e => e.ToString()).Aggregate((x, y) => $"{x} {y}");

            _source.TextChanged -= WhenTextChanged;
            _source.TextChanged += WhenTextChanged;
            _source.Click       -= WhenClick;
            _source.Click       += WhenClick;
            _source.Leave       -= WhenLeave;
            _source.Leave       += WhenLeave;
        }

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
                _source.TextChanged -= WhenTextChanged;
                _source.Click -= WhenClick;
                _source.Leave -= WhenLeave;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenTextChanged
        ///
        /// <summary>
        /// Occurs when the value of the provided textbox is changed.
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
        /// Occurs when the provided textbox is clicked.
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
        /// Occurs when the focus of the provided textbox is lost.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenLeave(object s, EventArgs e) => _focus = false;

        #endregion

        #region Fields
        private readonly TextBox _source;
        private readonly ToolTip _tips;
        private readonly char[]  _chars;
        private readonly string  _message;
        private bool _focus = false;
        #endregion
    }
}
