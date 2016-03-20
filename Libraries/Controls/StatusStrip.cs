/* ------------------------------------------------------------------------- */
///
/// StatusStrip.cs
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

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// StatusStrip
    /// 
    /// <summary>
    /// System.Windows.Forms.StatusStrip を拡張したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class StatusStrip : System.Windows.Forms.StatusStrip
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// StatusStrip
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public StatusStrip() : base() { }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// NcHitTest
        ///
        /// <summary>
        /// マウスのヒットテスト時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler<QueryEventArgs<Point, Position>> NcHitTest;

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnNcHitTest
        ///
        /// <summary>
        /// NcHitTest イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnNcHitTest(QueryEventArgs<Point, Position> e)
        {
            NcHitTest?.Invoke(this, e);

            if (!e.Cancel || FindForm() == null) return;
            if (IsNormalWindow() && IsSizingGrip(PointToClient(e.Query)))
            {
                e.Result = Position.Client;
                e.Cancel = false;
            }
        }

        #endregion

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseMove
        /// 
        /// <summary>
        /// マウス移動時に実行されます。
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
        /// マウスのボタンを押下時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (IsNormalWindow() && IsSizingGrip(e.Location))
            {
                User32.ReleaseCapture();
                User32.SendMessage(FindForm().Handle, 0xa1 /* WM_NCLBUTTONDOWN */, (IntPtr)Position.BottomRight, IntPtr.Zero);
            }
            else base.OnMouseDown(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WndProc
        ///
        /// <summary>
        /// ウィンドウメッセージを処理します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case 0x0084: // WM_NCHITTEST
                    var x = (int)m.LParam & 0xffff;
                    var y = (int)m.LParam >> 16 & 0xffff;
                    var e = new QueryEventArgs<Point, Position>(new Point(x, y), true);
                    OnNcHitTest(e);
                    var result = e.Cancel ? Position.Transparent : e.Result;
                    if (DesignMode && result == Position.Transparent) break;
                    m.Result = (IntPtr)result;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// IsSizingGrip
        ///
        /// <summary>
        /// リサイズ用グリップ部分かどうかを判別します。
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
        /// FindForm で見つかるフォームが通常状態かどうかを判別します。
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
