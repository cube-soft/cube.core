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
using Microsoft.Xaml.Behaviors;

/* ------------------------------------------------------------------------- */
///
/// ClosedToDispose
///
/// <summary>
/// Provides functionality to dispose the DataContext when the
/// Closed event is fired.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class ClosedToDispose : Behavior<Window>
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
        AssociatedObject.Closed -= OnClosed;
        AssociatedObject.Closed += OnClosed;
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
        AssociatedObject.Closed -= OnClosed;
        base.OnDetaching();
    }

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// OnClosed
    ///
    /// <summary>
    /// Occurs when the Closed event is fired.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void OnClosed(object s, EventArgs e)
    {
        if (AssociatedObject?.DataContext is IDisposable dc) dc.Dispose();
    }

    #endregion
}
