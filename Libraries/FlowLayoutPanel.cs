/* ------------------------------------------------------------------------- */
///
/// FlowLayoutPanel.cs
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
using log4net;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// FlowLayoutPanel
    /// 
    /// <summary>
    /// System.Windows.Forms.FlowLayoutPanel を拡張したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class FlowLayoutPanel : System.Windows.Forms.FlowLayoutPanel
    {
        /* ----------------------------------------------------------------- */
        ///
        /// SplitContainer
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public FlowLayoutPanel() : base()
        {
            Logger = LogManager.GetLogger(GetType());
        }

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Logger
        ///
        /// <summary>
        /// ログ出力用オブジェクトを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected ILog Logger { get; }

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
            e.Cancel = true;
            if (NcHitTest != null) NcHitTest(this, e);
            if (!e.Cancel) return;

            e.Result = Position.Transparent;
            e.Cancel = false;
        }

        #endregion

        #region Override methods

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
                    var e = new QueryEventArgs<Point, Position>(CreatePoint(m.LParam));
                    OnNcHitTest(e);
                    if (!e.Cancel) m.Result = (IntPtr)e.Result;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// CreatePoint
        /// 
        /// <summary>
        /// lParam から Point オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Point CreatePoint(IntPtr lparam)
        {
            var x = (int)lparam & 0xffff;
            var y = (int)lparam >> 16 & 0xffff;
            return new Point(x, y);
        }

        #endregion
    }
}
