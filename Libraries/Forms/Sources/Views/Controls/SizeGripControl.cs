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
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// SizeGripControl
    ///
    /// <summary>
    /// Represents the resize grip control that appears in the right-bottom
    /// corner of the window.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SizeGripControl : System.Windows.Forms.PictureBox
    {
        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnCreateControl
        ///
        /// <summary>
        /// Occurs when the control is created.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            DrawSizeGrip();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnBackColorChanged
        ///
        /// <summary>
        /// Occurs when the background color is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            DrawSizeGrip();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnResize
        ///
        /// <summary>
        /// Occurs when the control size is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            DrawSizeGrip();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseMove
        ///
        /// <summary>
        /// Occurs when the mouse is moved.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                var form = FindForm();
                if (form != null && form.WindowState == FormWindowState.Normal) Cursor = Cursors.SizeNWSE;
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
        protected override void OnMouseDown(MouseEventArgs e)
        {
            var form = FindForm();
            if (form != null && form.WindowState == FormWindowState.Normal)
            {
                User32.NativeMethods.ReleaseCapture();
                User32.NativeMethods.SendMessage(form.Handle, 0xa1 /* WM_NCLBUTTONDOWN */, (IntPtr)Position.BottomRight, IntPtr.Zero);
            }
            else base.OnMouseDown(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DrawSizeGrip
        ///
        /// <summary>
        /// Draws the resize grip.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void DrawSizeGrip()
        {
            var bounds = new Rectangle(0, 0, Width, Height);
            var image = new Bitmap(bounds.Width, bounds.Height);

            using (var gs = Graphics.FromImage(image))
            {
                var element = VisualStyleElement.Status.Gripper.Normal;
                if (Application.RenderWithVisualStyles && VisualStyleRenderer.IsElementDefined(element))
                {
                    var renderer = new VisualStyleRenderer(element);
                    renderer.DrawBackground(gs, bounds);
                }
                else ControlPaint.DrawSizeGrip(gs, BackColor, bounds);
            }

            Image?.Dispose();
            Image = image;
        }

        #endregion
    }
}
