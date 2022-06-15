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

/* ------------------------------------------------------------------------- */
///
/// DelegateCommand
///
/// <summary>
/// Represents an ICommand implementation that can be associated with
/// INotifyPropertyChanged objects.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class DelegateCommand : DelegateCommandBase
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// DelegateCommand
    ///
    /// <summary>
    /// Initializes a new instance of the BindableCommand class with
    /// the specified action.
    /// </summary>
    ///
    /// <param name="execute">Action to execute.</param>
    ///
    /* --------------------------------------------------------------------- */
    public DelegateCommand(Action execute) : this(execute, () => true) { }

    /* --------------------------------------------------------------------- */
    ///
    /// DelegateCommand
    ///
    /// <summary>
    /// Initializes a new instance of the BindableCommand class with
    /// the specified action.
    /// </summary>
    ///
    /// <param name="execute">Action to execute.</param>
    /// <param name="canExecute">
    /// Function to determine whether the command can be executed.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public DelegateCommand(Action execute, Func<bool> canExecute)
    {
        _execute    = execute    ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
    }

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// OnCanExecute
    ///
    /// <summary>
    /// Determines whether the command can execute in its current state.
    /// </summary>
    ///
    /// <param name="parameter">Not used parameter.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected override bool OnCanExecute(object parameter) => _canExecute?.Invoke() ?? false;

    /* --------------------------------------------------------------------- */
    ///
    /// OnExecute
    ///
    /// <summary>
    /// Executes the command.
    /// </summary>
    ///
    /// <param name="parameter">Not used parameter.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected override void OnExecute(object parameter) => _execute?.Invoke();

    /* --------------------------------------------------------------------- */
    ///
    /// Dispose
    ///
    /// <summary>
    /// Releases the unmanaged resources used by the object
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
            _execute    = null;
            _canExecute = null;
        }
        finally { base.Dispose(disposing); }
    }

    #endregion

    #region Fields
    private Action _execute;
    private Func<bool> _canExecute;
    #endregion
}
