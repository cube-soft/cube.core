/* ------------------------------------------------------------------------- */
///
/// SizeHacker.cs
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
using System.Windows.Forms;
using Cube.Forms.Extensions;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// SizeHacker
    /// 
    /// <summary>
    /// フォームのリサイズを補助するクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// FormBorderStyle = None 等でリサイズ用の枠線が描画されていない場合に
    /// リサイズ用の枠線と同様のリサイズ方法を提供します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class SizeHacker
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// SizeHacker
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SizeHacker(Control control, int grip)
        {
            SizeGrip = grip;
            Root = control;
            Monitor(Root);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Root
        /// 
        /// <summary>
        /// 監視するコントロール群の基底となるコントロールを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Control Root { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// SizeGrip
        /// 
        /// <summary>
        /// リサイズ用のグリップに使用する幅を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int SizeGrip { get; }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// Contro_MouseMove
        /// 
        /// <summary>
        /// コントロールが追加された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Control_ControlAdded(object sender, ControlEventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;

            Monitor(control);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Contro_MouseMove
        /// 
        /// <summary>
        /// マウスが移動した時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.None) return;

            var control = sender as Control;
            if (control == null) return;

            var form = control.FindForm();
            if (form == null || form.WindowState != FormWindowState.Normal) return;

            var point = form.PointToClient(control.PointToScreen(e.Location));
            form.Cursor = form.HitTest(point, SizeGrip).ToCursor();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Control_MouseDown
        /// 
        /// <summary>
        /// マウスのボタンが押下された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;

            var form = control.FindForm();
            if (form == null || form.WindowState != FormWindowState.Normal) return;
            
            var point  = form.PointToClient(control.PointToScreen(e.Location));
            var result = form.HitTest(point, SizeGrip);
            if (result != Position.Left       && result != Position.Right    &&
                result != Position.Top        && result != Position.Bottom   &&
                result != Position.TopLeft    && result != Position.TopRight &&
                result != Position.BottomLeft && result != Position.BottomRight) return;

            User32.ReleaseCapture();
            User32.SendMessage(form.Handle, 0xa1 /* WM_NCLBUTTONDOWN */, (IntPtr)result, IntPtr.Zero);
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Monitor
        /// 
        /// <summary>
        /// 指定されたコントロールおよび Controls に含まれる全ての
        /// コントロールの MouseMove/MouseDown イベントを監視します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Monitor(Control root)
        {
            foreach (Control child in root.Controls) Monitor(child);

            root.ControlAdded -= Control_ControlAdded;
            root.ControlAdded += Control_ControlAdded;
            root.MouseMove    -= Control_MouseMove;
            root.MouseMove    += Control_MouseMove;
            root.MouseDown    -= Control_MouseDown;
            root.MouseDown    += Control_MouseDown;
        }

        #endregion
    }
}
