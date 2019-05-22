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
using System.ComponentModel;
using System.Drawing;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Panel
    ///
    /// <summary>
    /// System.Windows.Forms.Panel を拡張したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Panel : System.Windows.Forms.Panel, IControl
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Dpi
        ///
        /// <summary>
        /// 現在の Dpi の値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double Dpi
        {
            get => _dpi;
            set
            {
                if (_dpi == value) return;
                var old = _dpi;
                _dpi = value;
                OnDpiChanged(ValueEventArgs.Create(old, value));
            }
        }

        #endregion

        #region Events

        #region DpiChanged

        /* ----------------------------------------------------------------- */
        ///
        /// DpiChanged
        ///
        /// <summary>
        /// DPI の値が変化した時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event ValueChangedEventHandler<double> DpiChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnDpiChanged
        ///
        /// <summary>
        /// DpiChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnDpiChanged(ValueChangedEventArgs<double> e)
        {
            this.UpdateDpi(e.OldValue, e.NewValue);
            DpiChanged?.Invoke(this, e);
        }

        #endregion

        #region NcHitTest

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
        protected virtual void OnNcHitTest(QueryMessage<Point, Position> e) =>
            NcHitTest?.Invoke(this, e);

        #endregion

        #endregion

        #region Implementations

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

            if (m.Msg == 0x0084) // WM_NCHITTEST
            {
                var x = (int)m.LParam & 0xffff;
                var y = (int)m.LParam >> 16 & 0xffff;
                var e = new QueryMessage<Point, Position>
                {
                    Query  = new Point(x, y),
                    Cancel = true,
                };
                OnNcHitTest(e);
                var result = e.Cancel ? Position.Transparent : e.Value;
                if (DesignMode && result == Position.Transparent) return;
                m.Result = (IntPtr)result;
            }
        }

        #endregion

        #region Fields
        private double _dpi = Window.BaseDpi;
        #endregion
    }
}
