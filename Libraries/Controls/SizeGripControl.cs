/* ------------------------------------------------------------------------- */
///
/// SizeGripControl.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// SizeGripControl
    /// 
    /// <summary>
    /// フォーム右下に表示されるリサイズ用グリップの機能を提供する
    /// コントロールクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class SizeGripControl : System.Windows.Forms.PictureBox
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SizeGripControl
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SizeGripControl() : base() { }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnCreateControl
        /// 
        /// <summary>
        /// コントロール生成時に実行されます。
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
        /// 背景色の変更時に実行されます。
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
        /// コントロール自体のサイズ変更時に実行されます。
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
        /// マウス移動時に実行されます。
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
        /// マウスのボタンを押下時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnMouseDown(MouseEventArgs e)
        {
            var form = FindForm();
            if (form != null || form.WindowState == FormWindowState.Normal)
            {
                User32.ReleaseCapture();
                User32.SendMessage(form.Handle, 0xa1 /* WM_NCLBUTTONDOWN */, (IntPtr)Position.BottomRight, IntPtr.Zero);
            }
            else base.OnMouseDown(e);
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// DrawSizeGrip
        /// 
        /// <summary>
        /// リサイズ用グリップの外観を描画します。
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
