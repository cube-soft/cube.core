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
namespace Cube.Xui;

using System;
using System.Windows.Input;
using Cube.Globalization;

/* ------------------------------------------------------------------------- */
///
/// BindableElement
///
/// <summary>
/// Represents a bindable element that has text and command.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class BindableElement : BindableBase, IElement
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// BindableElement
    ///
    /// <summary>
    /// Initializes a new instance of the BindableElement
    /// class with the specified arguments.
    /// </summary>
    ///
    /// <param name="gettext">Function to get text.</param>
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public BindableElement(Getter<string> gettext, Dispatcher dispatcher) :
        this(gettext, default, dispatcher) { }

    /* --------------------------------------------------------------------- */
    ///
    /// BindableElement
    ///
    /// <summary>
    /// Initializes a new instance of the BindableElement
    /// class with the specified arguments.
    /// </summary>
    ///
    /// <param name="gettext">Function to get text.</param>
    /// <param name="command">Command object.</param>
    /// <param name="dispatcher">Dispatcher object.</param>
    ///
    /* --------------------------------------------------------------------- */
    public BindableElement(Getter<string> gettext, ICommand command, Dispatcher dispatcher) : base(dispatcher)
    {
        _gettext = gettext;
        _locale  = Locale.Subscribe(_ => React());
        if (command is not null) Command = command;
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Text
    ///
    /// <summary>
    /// Gets a text to be displayed in the View.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Text => _gettext();

    /* --------------------------------------------------------------------- */
    ///
    /// Command
    ///
    /// <summary>
    /// Gets or sets a command to be executed by the View.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public ICommand Command
    {
        get => Get<ICommand>();
        set => Set(value);
    }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// React
    ///
    /// <summary>
    /// Invokes when any states are changed.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void React() => Refresh(nameof(Text));

    /* --------------------------------------------------------------------- */
    ///
    /// Dispose
    ///
    /// <summary>
    /// Releases the unmanaged resources used by <c>BindableElement</c>
    /// and optionally releases the managed resources.
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
        try
        {
            if (!disposing) return;
            if (Command is IDisposable dc) dc.Dispose();
            _locale.Dispose();
        }
        finally { base.Dispose(disposing); }
    }

    #endregion

    #region Fields
    private readonly Getter<string> _gettext;
    private readonly IDisposable _locale;
    #endregion
}
