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
using System.ComponentModel;
using System.Drawing;
using Cube.Forms.Controls;

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
    public class StatusStrip : System.Windows.Forms.StatusStrip, IControl
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

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// EventHub
        /// 
        /// <summary>
        /// イベントを集約するためのオブジェクトを取得または設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// Controls に登録されている IControl オブジェクトに対して、
        /// 再帰的に設定します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEventHub EventHub
        {
            get => _events;
            set
            {
                if (_events == value) return;
                _events = value;
                foreach (var obj in Controls)
                {
                    if (obj is IControl c) c.EventHub = value;
                }
            }
        }

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
                OnDpiChanged(ValueChangedEventArgs.Create(old, value));
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
            this.UpdateControl(e.OldValue, e.NewValue);
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
        protected virtual void OnNcHitTest(QueryEventArgs<Point, Position> e)
            => NcHitTest?.Invoke(this, e);

        #endregion

        #endregion

        #region Implementations

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
                User32.NativeMethods.ReleaseCapture();
                User32.NativeMethods.SendMessage(FindForm().Handle, 0xa1 /* WM_NCLBUTTONDOWN */, (IntPtr)Position.BottomRight, IntPtr.Zero);
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

        #region Fields
        private IEventHub _events;
        private double _dpi = StandardForm.BaseDpi;
        #endregion

        #endregion
    }
}
