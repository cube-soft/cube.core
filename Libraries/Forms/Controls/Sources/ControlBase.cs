﻿/* ------------------------------------------------------------------------- */
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
namespace Cube.Forms.Controls;

using System;
using System.Drawing;

/* ------------------------------------------------------------------------- */
///
/// ControlBase
///
/// <summary>
/// Represents the base class of controls.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class ControlBase : System.Windows.Forms.UserControl
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// ControlBase
    ///
    /// <summary>
    /// Initializes a new instance of the ControlBase class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected ControlBase() => DoubleBuffered = true;

    #endregion

    #region Events

    #region NcHitTest

    /* --------------------------------------------------------------------- */
    ///
    /// NcHitTest
    ///
    /// <summary>
    /// Occurs when the hit test of the non-client area.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public event QueryEventHandler<Point, Position> NcHitTest;

    /* --------------------------------------------------------------------- */
    ///
    /// OnNcHitTest
    ///
    /// <summary>
    /// Raises the NcHitTest event.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    protected virtual void OnNcHitTest(QueryMessage<Point, Position> e) =>
        NcHitTest?.Invoke(this, e);

    #endregion

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// WndProc
    ///
    /// <summary>
    /// Processes the specified window message.
    /// </summary>
    ///
    /// <param name="m">Window message.</param>
    ///
    /* --------------------------------------------------------------------- */
    protected override void WndProc(ref System.Windows.Forms.Message m)
    {
        base.WndProc(ref m);

        if (m.Msg == 0x0084) // WM_NCHITTEST
        {
            var x = (int)m.LParam & 0xffff;
            var y = (int)m.LParam >> 16 & 0xffff;
            var e = new QueryMessage<Point, Position>(new(x, y)) { Cancel = true };

            OnNcHitTest(e);
            var result = e.Cancel ? Position.Transparent : e.Value;
            if (DesignMode && result == Position.Transparent) return;
            m.Result = (IntPtr)result;
        }
    }

    #endregion
}
