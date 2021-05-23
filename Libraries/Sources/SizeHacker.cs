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
using System.Windows.Forms;
using Cube.Mixin.Forms.Controls;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// SizeHacker
    ///
    /// <summary>
    /// Provides functionality to resize a Control object.
    /// </summary>
    ///
    /// <remarks>
    /// This class provides the same resizing method as the resize border
    /// when the resize border is not drawn with FormBorderStyle = None.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class SizeHacker : DisposableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SizeHacker
        ///
        /// <summary>
        /// Initializes a new instance of the SizeHacker with the specified
        /// arguments.
        /// </summary>
        ///
        /// <param name="control">Source control.</param>
        /// <param name="grip">Grip size.</param>
        ///
        /* ----------------------------------------------------------------- */
        public SizeHacker(Control control, int grip)
        {
            SizeGrip = grip;
            Root     = control;
            StartMonitor(Root);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Root
        ///
        /// <summary>
        /// Get the root control for the group of controls to be monitored.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Control Root { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// SizeGrip
        ///
        /// <summary>
        /// Get the width to use for the grip for resizing.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int SizeGrip { get; }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
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
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EndMonitor(Root);
                _cursors.Clear();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenControlAdded
        ///
        /// <summary>
        /// Occurs when a control is added.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenControlAdded(object s, ControlEventArgs e) => StartMonitor(e.Control);

        /* ----------------------------------------------------------------- */
        ///
        /// WhenControlRemoved
        ///
        /// <summary>
        /// Occurs when a control is removed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenControlRemoved(object s, ControlEventArgs e) => EndMonitor(e.Control);

        /* ----------------------------------------------------------------- */
        ///
        /// WhenMouseMove
        ///
        /// <summary>
        /// Occurs when the mouse is moved.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenMouseMove(object s, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.None || s is not Control control) return;

            var form = control.FindForm();
            if (form == null || form.WindowState != FormWindowState.Normal) return;

            var point  = form.PointToClient(control.PointToScreen(e.Location));
            var cursor = form.HitTest(point, SizeGrip).ToCursor();
            if (cursor != Cursors.Default) Stash(control, cursor);
            else Pop(control);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenMouseDown
        ///
        /// <summary>
        /// Occurs when the mouse is clicked.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenMouseDown(object s, MouseEventArgs e)
        {
            if (s is not Control control) return;

            var form = control.FindForm();
            if (form == null || form.WindowState != FormWindowState.Normal) return;

            var point  = form.PointToClient(control.PointToScreen(e.Location));
            var result = form.HitTest(point, SizeGrip);
            if (result != Position.Left       && result != Position.Right    &&
                result != Position.Top        && result != Position.Bottom   &&
                result != Position.TopLeft    && result != Position.TopRight &&
                result != Position.BottomLeft && result != Position.BottomRight) return;

            _ = User32.NativeMethods.ReleaseCapture();
            _ = User32.NativeMethods.SendMessage(form.Handle, 0xa1 /* WM_NCLBUTTONDOWN */, (IntPtr)result, IntPtr.Zero);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// StartMonitor
        ///
        /// <summary>
        /// Starts monitoring the MouseMove/MouseDown events for the
        /// specified control and all controls included in the Controls
        /// property.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void StartMonitor(Control control)
        {
            foreach (Control child in control.Controls) StartMonitor(child);

            Push(control);

            control.ControlAdded   -= WhenControlAdded;
            control.ControlAdded   += WhenControlAdded;
            control.ControlRemoved -= WhenControlRemoved;
            control.ControlRemoved += WhenControlRemoved;
            control.MouseMove      -= WhenMouseMove;
            control.MouseMove      += WhenMouseMove;
            control.MouseDown      -= WhenMouseDown;
            control.MouseDown      += WhenMouseDown;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// EndMonitor
        ///
        /// <summary>
        /// Terminates monitoring the MouseMove/MouseDown events of all
        /// controls.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void EndMonitor(Control control)
        {
            control.ControlAdded   -= WhenControlAdded;
            control.ControlRemoved -= WhenControlRemoved;
            control.MouseMove      -= WhenMouseMove;
            control.MouseDown      -= WhenMouseDown;

            Pop(control);

            foreach (Control child in control.Controls) EndMonitor(child);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Stash
        ///
        /// <summary>
        /// Stashes the cursor originally held by the specified control and
        /// set it to the specified cursor.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Stash(Control control, Cursor cursor)
        {
            Push(control);
            control.Cursor = cursor;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Push
        ///
        /// <summary>
        /// Remembers the cursor originally held by the specified control.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Push(Control control)
        {
            if (!_cursors.ContainsKey(control)) _cursors.Add(control, control.Cursor);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Pop
        ///
        /// <summary>
        /// Restores the cursor originally held by the specified control.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Pop(Control control)
        {
            if (!_cursors.ContainsKey(control)) return;

            control.Cursor = _cursors[control];
            _ = _cursors.Remove(control);
        }

        #endregion

        #region Fields
        private readonly Dictionary<Control, Cursor> _cursors = new();
        #endregion
    }
}
