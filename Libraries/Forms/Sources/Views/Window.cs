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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Window
    ///
    /// <summary>
    /// Represents a standard Windows form.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Window : WindowBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Window
        ///
        /// <summary>
        /// Initializes a new instance of the Window class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Window()
        {
            AutoScaleMode = AutoScaleMode.Dpi;
            DoubleBuffered = true;
            Font = FontFactory.Create(Font);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// ShortcutKeys
        ///
        /// <summary>
        /// Gets the collection of shortcut keys.
        /// </summary>
        ///
        /// <remarks>
        /// The action registered in the property will be executed when
        /// the ProcessCmdKey(Message, Keys) method is invoked.
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDictionary<Keys, Action> ShortcutKeys { get; } = new Dictionary<Keys, Action>();

        #endregion

        #region Events

        #region VisibleChanging

        /* ----------------------------------------------------------------- */
        ///
        /// VisibleChanging
        ///
        /// <summary>
        /// Occurs before the value of the Visible property changes.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event CancelEventHandler VisibleChanging;

        /* ----------------------------------------------------------------- */
        ///
        /// OnVisibleChanging
        ///
        /// <summary>
        /// Raises the VisibleChanging event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnVisibleChanging(CancelEventArgs e)
        {
            if (Visible && !DesignMode) AdjustDesktopLocation();
            VisibleChanging?.Invoke(this, e);
        }

        #endregion

        #region NcHitTest

        /* ----------------------------------------------------------------- */
        ///
        /// NcHitTest
        ///
        /// <summary>
        /// Occurs when the hit test of the non-client area.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event QueryEventHandler<Point, Position> NcHitTest;

        /* ----------------------------------------------------------------- */
        ///
        /// OnNcHitTest
        ///
        /// <summary>
        /// Raises the NcHitTest event.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnNcHitTest(QueryMessage<Point, Position> e)
        {
            if (NcHitTest != null) NcHitTest(this, e);
            else e.Cancel = true;
        }

        #endregion

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// AdjustDesktopLocation
        ///
        /// <summary>
        /// Adjusts the location of the window not to protrude from the
        /// screen.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void AdjustDesktopLocation()
        {
            var screen = Screen.FromPoint(DesktopLocation) ?? Screen.PrimaryScreen;
            var x      = DesktopLocation.X;
            var y      = DesktopLocation.Y;

            var left   = screen.WorkingArea.Left;
            var top    = screen.WorkingArea.Top;
            var right  = screen.WorkingArea.Right;
            var bottom = screen.WorkingArea.Bottom;

            if (x >= left && x +  Width <=  right &&
                y >=  top && y + Height <= bottom) return;

            SetDesktopLocation(
                Math.Min(Math.Max(DesktopLocation.X, left), right - Width),
                Math.Min(Math.Max(DesktopLocation.Y, top), bottom - Height)
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DoShortcutKeys
        ///
        /// <summary>
        /// Invokes the specified shortcut keys.
        /// </summary>
        ///
        /// <param name="keys">Shortcut keys.</param>
        ///
        /// <returns>true for executed.</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected bool DoShortcutKeys(Keys keys)
        {
            if (!ShortcutKeys.ContainsKey(keys)) return false;
            ShortcutKeys[keys]();
            return true;
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// SetVisibleCore
        ///
        /// <summary>
        /// Sets the control to the specified state.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void SetVisibleCore(bool value)
        {
            var prev = Visible;
            var args = new CancelEventArgs();
            OnVisibleChanging(args);
            base.SetVisibleCore(args.Cancel ? prev : value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ProcessCmdKey
        ///
        /// <summary>
        /// Processes the specified shortcut keys.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override bool ProcessCmdKey(ref Message msg, Keys keys)
        {
            if (ShortcutKeys.ContainsKey(keys))
            {
                ShortcutKeys[keys]();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keys);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WndProc
        ///
        /// <summary>
        /// Processes the specified window message.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case 0x0084: // WM_NCHITTEST
                    var e = Query.NewMessage<Point, Position>(CreatePoint(m.LParam));
                    OnNcHitTest(e);
                    if (!e.Cancel) m.Result = (IntPtr)e.Value;
                    break;
                default:
                    break;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreatePoint
        ///
        /// <summary>
        /// Creates a new instance of the Point class with the specified
        /// arguments.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Point CreatePoint(IntPtr lparam)
        {
            int x = (short)(lparam.ToInt32() & 0x0000ffff);
            int y = (short)((lparam.ToInt32() & 0xffff0000) >> 16);
            return new Point(x, y);
        }

        #endregion
    }
}
