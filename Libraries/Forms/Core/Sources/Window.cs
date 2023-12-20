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
namespace Cube.Forms;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

/* ------------------------------------------------------------------------- */
///
/// Window
///
/// <summary>
/// Represents the base class of WinForms-based IBindable implementation.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class Window : Form, IBinder
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// Window
    ///
    /// <summary>
    /// Initializes a new instance of the Window class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public Window() => DoubleBuffered = true;

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Bindable
    ///
    /// <summary>
    /// Gets the bindable object.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected IBindable Bindable { get; private set; }

    /* --------------------------------------------------------------------- */
    ///
    /// Behaviors
    ///
    /// <summary>
    /// Gets the collection of registered behaviors.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected DisposableContainer Behaviors { get; } = new();

    /* --------------------------------------------------------------------- */
    ///
    /// ShortcutKeys
    ///
    /// <summary>
    /// Gets the collection of behaviors for shortcut keys.
    /// </summary>
    ///
    /// <remarks>
    /// The action registered in the property will be executed when
    /// the ProcessCmdKey(Message, Keys) method is invoked.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    protected IDictionary<Keys, Action> ShortcutKeys { get; } = new Dictionary<Keys, Action>();

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Bind
    ///
    /// <summary>
    /// Binds the window to the specified object.
    /// If the Bindable property is already set, the specified object
    /// is ignored.
    /// </summary>
    ///
    /// <param name="src">Object to bind.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void Bind(IBindable src)
    {
        if (Bindable is not null) return;
        Bindable = src;
        OnBind(src);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// OnBind
    ///
    /// <summary>
    /// Binds the window to the specified object.
    /// </summary>
    ///
    /// <param name="src">Object to bind.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected virtual void OnBind(IBindable src) { }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Dispose
    ///
    /// <summary>
    /// Releases the unmanaged resources used by the StandardForm
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
            if (_disposed) return;
            _disposed = true;
            if (!disposing) return;

            ShortcutKeys.Clear();
            Behaviors.Dispose();
            Bindable?.Dispose();
            Bindable = null;
        }
        finally { base.Dispose(disposing); }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ProcessCmdKey
    ///
    /// <summary>
    /// Processes the specified shortcut keys.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override bool ProcessCmdKey(ref Message msg, Keys keys)
    {
        if (ShortcutKeys.TryGetValue(keys, out var action))
        {
            action();
            return true;
        }
        return base.ProcessCmdKey(ref msg, keys);
    }

    #endregion

    #region Fields
    private bool _disposed = false;
    #endregion
}
