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
using System.Drawing;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// StatusStrip
    ///
    /// <summary>
    /// Represents the customized version of the WinForms StatusStrip class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class StatusStrip : System.Windows.Forms.StatusStrip
    {
        #region Events

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
        protected virtual void OnNcHitTest(QueryMessage<Point, Position> e) =>
            NcHitTest?.Invoke(this, e);

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseMove
        ///
        /// <summary>
        /// Occurs when the mouse is moved.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.None &&
                IsNormalWindow() && IsSizingGrip(e.Location))
            {
                Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            }
            else base.OnMouseMove(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseDown
        ///
        /// <summary>
        /// Occurs when any mouse button is pressed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (IsNormalWindow() && IsSizingGrip(e.Location))
            {
                User32.NativeMethods.ReleaseCapture();

                var form = FindForm();
                if (form == null) return;

                User32.NativeMethods.SendMessage(
                    form.Handle,
                    0xa1 /* WM_NCLBUTTONDOWN */,
                    (IntPtr)Position.BottomRight,
                    IntPtr.Zero
                );
            }
            else base.OnMouseDown(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WndProc
        ///
        /// <summary>
        /// Processes the specified window message.
        /// </summary>
        ///
        /// <param name="m">Window message.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0084) // WM_NCHITTEST
            {
                var x = (int)m.LParam & 0xffff;
                var y = (int)m.LParam >> 16 & 0xffff;
                var e = new QueryMessage<Point, Position>
                {
                    Source = new Point(x, y),
                    Cancel = true,
                };
                OnNcHitTest(e);
                var result = e.Cancel ? Position.Transparent : e.Value;
                if (DesignMode && result == Position.Transparent) return;
                m.Result = (IntPtr)result;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsSizingGrip
        ///
        /// <summary>
        /// Determines if the control at the specified point is a resize grip.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsSizingGrip(Point point)
        {
            var grip = Height;
            return SizingGrip &&
                   point.X >= Width  - grip && point.X <= Width &&
                   point.Y >= Height - grip && point.Y <= Height;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsNormalWindow
        ///
        /// <summary>
        /// Determines if the value returned from the FindForm method is
        /// the normal state.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsNormalWindow()
        {
            var form = FindForm();
            return form != null &&
                   form.WindowState == System.Windows.Forms.FormWindowState.Normal;
        }

        #endregion
    }
}
