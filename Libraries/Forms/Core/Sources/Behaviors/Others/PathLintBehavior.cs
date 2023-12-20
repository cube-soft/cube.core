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
namespace Cube.Forms.Behaviors;

using System;
using System.Linq;
using System.Windows.Forms;

/* ------------------------------------------------------------------------- */
///
/// PathLintBehavior
///
/// <summary>
/// Represents the behavior to detect invalid characters from the
/// entered path.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class PathLintBehavior : DisposableBase
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// PathLintBehavior
    ///
    /// <summary>
    /// Initializes a new instance of the PathLintBehavior class with
    /// the specified control.
    /// </summary>
    ///
    /// <param name="src">TextBox control.</param>
    ///
    /* --------------------------------------------------------------------- */
    public PathLintBehavior(TextBox src) : this(src, new()
    {
        ToolTipTitle = "Path cannot contain any of the following characters",
        IsBalloon    = false,
        InitialDelay = 100,
        ReshowDelay  = 100,
        AutoPopDelay = 1000,
    }) { }

    /* --------------------------------------------------------------------- */
    ///
    /// PathLintBehavior
    ///
    /// <summary>
    /// Initializes a new instance of the PathLintBehavior class with
    /// the specified controls.
    /// </summary>
    ///
    /// <param name="src">TextBox control.</param>
    /// <param name="tips">ToolTip control.</param>
    ///
    /* --------------------------------------------------------------------- */
    public PathLintBehavior(TextBox src, ToolTip tips)
    {
        _source = src;
        _tips   = tips;

        _source.TextChanged -= WhenTextChanged;
        _source.TextChanged += WhenTextChanged;
        _source.Click -= WhenClick;
        _source.Click += WhenClick;
        _source.Leave -= WhenLeave;
        _source.Leave += WhenLeave;
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Text
    ///
    /// <summary>
    /// Get or set the message when entering an invalid character.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Text
    {
        get => _tips.ToolTipTitle;
        set => _tips.ToolTipTitle = value;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// InvalidChars
    ///
    /// <summary>
    /// Get a list of characters that cannot be included in a path.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public char[] InvalidChars { get; } = new[] { '/', '*', '"', '<', '>', '|', '?', ':' };

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _source.TextChanged -= WhenTextChanged;
            _source.Click -= WhenClick;
            _source.Leave -= WhenLeave;
        }
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// WhenTextChanged
    ///
    /// <summary>
    /// Occurs when the value of the provided textbox is changed.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void WhenTextChanged(object s, EventArgs e)
    {
        var index = _source.Text.IndexOfAny(InvalidChars);
        if (index == 1 && _source.Text[1] == ':') index = _source.Text.IndexOfAny(InvalidChars, 2);
        if (index < 0) return;

        var pos = _source.SelectionStart;
        var str = InvalidChars.Select(c => c.ToString()).Aggregate((x, y) => $"{x} {y}");
        _source.Text = _source.Text.Remove(index, 1);
        _source.SelectionStart = Math.Max(pos - 1, 0);
        _tips.Show(str, _source, 3000);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// WhenClick
    ///
    /// <summary>
    /// Occurs when the provided textbox is clicked.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
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

    /* --------------------------------------------------------------------- */
    ///
    /// WhenLeave
    ///
    /// <summary>
    /// Occurs when the focus of the provided textbox is lost.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void WhenLeave(object s, EventArgs e) => _focus = false;

    #endregion

    #region Fields
    private readonly TextBox _source;
    private readonly ToolTip _tips;
    private bool _focus = false;
    #endregion
}
