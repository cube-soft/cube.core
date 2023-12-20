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
using System.Windows.Forms;

/* ------------------------------------------------------------------------- */
///
/// AdjustWindowBehavior
///
/// <summary>
/// Provides functionality to adjust the window so that it appears within
/// the desktop area when Shown event is fired.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public sealed class AdjustWindowBehavior : ShownEventBehavior
{
    /* --------------------------------------------------------------------- */
    ///
    /// AdjustWindowBehavior
    ///
    /// <summary>
    /// Initializes a new instance with the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Source view.</param>
    ///
    /* --------------------------------------------------------------------- */
    public AdjustWindowBehavior(Form src) : base(src, () =>
    {
        var screen = Screen.FromPoint(src.DesktopLocation) ?? Screen.PrimaryScreen;
        var x      = src.DesktopLocation.X;
        var y      = src.DesktopLocation.Y;

        var left   = screen.WorkingArea.Left;
        var top    = screen.WorkingArea.Top;
        var right  = screen.WorkingArea.Right;
        var bottom = screen.WorkingArea.Bottom;

        if (x >= left && x +  src.Width <=  right &&
            y >=  top && y + src.Height <= bottom) return;

        src.SetDesktopLocation(
            Math.Min(Math.Max(src.DesktopLocation.X, left), right - src.Width),
            Math.Min(Math.Max(src.DesktopLocation.Y, top), bottom - src.Height)
        );
    }) { }
}
