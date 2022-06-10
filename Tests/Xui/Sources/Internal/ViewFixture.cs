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
namespace Cube.Xui.Tests;

using System.Windows;
using Microsoft.Xaml.Behaviors;

/* ------------------------------------------------------------------------- */
///
/// ViewFixture
///
/// <summary>
/// Provides functionality to test with a mock window.
/// </summary>
///
/* ------------------------------------------------------------------------- */
abstract class ViewFixture
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Attach
    ///
    /// <summary>
    /// Attaches the specified view and behavior object.
    /// </summary>
    ///
    /// <param name="src">Window to be attached.</param>
    /// <param name="behavior">Source behavior.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected T Attach<T>(Window src, T behavior) where T : Behavior
    {
        behavior.Attach(src);
        return behavior;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Hack
    ///
    /// <summary>
    /// Sets properties of the Window class for testing.
    /// </summary>
    ///
    /// <param name="src">Window to be hacked.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected T Hack<T>(T src) where T : Window
    {
        src.Top = SystemParameters.PrimaryScreenHeight + 10;
        src.ShowInTaskbar = false;
        return src;
    }

    #endregion
}
