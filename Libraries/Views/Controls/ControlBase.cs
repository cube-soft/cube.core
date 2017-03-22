/* ------------------------------------------------------------------------- */
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
using System.ComponentModel;
using System.Drawing;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// ControlBase
    /// 
    /// <summary>
    /// 各種コントロールの基底となるクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// System.Windows.Forms.UserControl をベースに実装されています。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class ControlBase : System.Windows.Forms.UserControl, IControl
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// UserControl
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ControlBase()
            : base()
        {
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            DoubleBuffered = true;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// EventAggregator
        /// 
        /// <summary>
        /// イベントを集約するためのオブジェクトを取得または設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// Controls に登録されている ControlBase オブジェクトに対して、
        /// 再帰的に設定します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEventAggregator EventAggregator
        {
            get { return _events; }
            set
            {
                if (_events == value) return;
                _events = value;
                foreach (var obj in Controls)
                {
                    var control = obj as ControlBase;
                    if (control == null) continue;
                    control.EventAggregator = value;
                }
            }
        }

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
        public event QueryEventHandler<Point, Position> NcHitTest;

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
            => NcHitTest?.Invoke(this, e);

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

        #region Fields
        private IEventAggregator _events;
        #endregion
    }
}
