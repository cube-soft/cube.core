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
using Cube.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
    public class SizeHacker : DisposableBase
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

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// リソースを解放します。
        /// </summary>
        ///
        /// <param name="disposing">
        /// マネージリソースを解放するかどうか
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
        /// コントロールが追加された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenControlAdded(object s, ControlEventArgs e) => StartMonitor(e.Control);

        /* ----------------------------------------------------------------- */
        ///
        /// WhenControlRemoved
        ///
        /// <summary>
        /// コントロールが削除された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenControlRemoved(object s, ControlEventArgs e) => EndMonitor(e.Control);

        /* ----------------------------------------------------------------- */
        ///
        /// WhenMouseMove
        ///
        /// <summary>
        /// マウスが移動した時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenMouseMove(object s, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.None || !(s is Control control)) return;

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
        /// マウスのボタンが押下された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenMouseDown(object s, MouseEventArgs e)
        {
            if (!(s is Control control)) return;

            var form = control.FindForm();
            if (form == null || form.WindowState != FormWindowState.Normal) return;

            var point  = form.PointToClient(control.PointToScreen(e.Location));
            var result = form.HitTest(point, SizeGrip);
            if (result != Position.Left       && result != Position.Right    &&
                result != Position.Top        && result != Position.Bottom   &&
                result != Position.TopLeft    && result != Position.TopRight &&
                result != Position.BottomLeft && result != Position.BottomRight) return;

            User32.NativeMethods.ReleaseCapture();
            User32.NativeMethods.SendMessage(form.Handle, 0xa1 /* WM_NCLBUTTONDOWN */, (IntPtr)result, IntPtr.Zero);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// StartMonitor
        ///
        /// <summary>
        /// 指定されたコントロールおよび Controls に含まれる全ての
        /// コントロールの MouseMove/MouseDown イベントを監視します。
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
        /// 指定されたコントロールおよび Controls に含まれる全ての
        /// コントロールの監視を終了します。
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
        /// コントロールが元々保持していたカーソルを退避し、新たなカーソルに
        /// 設定します。
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
        /// コントロールが元々保持していたカーソルを記憶します。
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
        /// コントロールが元々保持していたカーソルを復元します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Pop(Control control)
        {
            if (!_cursors.ContainsKey(control)) return;

            control.Cursor = _cursors[control];
            _cursors.Remove(control);
        }

        #endregion

        #region Fields
        private readonly IDictionary<Control, Cursor> _cursors = new Dictionary<Control, Cursor>();
        #endregion
    }
}
