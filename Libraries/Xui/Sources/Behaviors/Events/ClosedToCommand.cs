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
namespace Cube.Xui.Behaviors;

using System;
using System.Windows;
using Cube.Xui.Commands.Extensions;

/* ------------------------------------------------------------------------- */
///
/// ClosedToCommand
///
/// <summary>
/// Represents the behavior when the Closed event is fired.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class ClosedToCommand : CommandBehavior<Window>
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// OnAttached
    ///
    /// <summary>
    /// Occurs when the instance is attached to the Window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Closed -= WhenClosed;
        AssociatedObject.Closed += WhenClosed;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// OnDetaching
    ///
    /// <summary>
    /// Occurs when the instance is detaching from the Window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected override void OnDetaching()
    {
        AssociatedObject.Closed -= WhenClosed;
        base.OnDetaching();
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// WhenClosed
    ///
    /// <summary>
    /// Occurs when the Close event is fired.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void WhenClosed(object s, EventArgs e)
    {
        if (Command?.CanExecute() ?? false) Command.Execute();
    }

    #endregion
}
