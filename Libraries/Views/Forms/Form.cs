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
using Cube.Log;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Form
    /// 
    /// <summary>
    /// System.Windows.Forms.Form の拡張クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Form : System.Windows.Forms.Form
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Form
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Form() : base()
        {
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            DoubleBuffered = true;
            Font = FontFactory.Create(Font);
            using (var gs = CreateGraphics())
            {
                Dpi = gs.DpiX;
                if (gs.DpiX != gs.DpiY) this.LogWarn($"DpiX:{gs.DpiX}\tDpiY:{gs.DpiY}");
            }
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Dpi
        /// 
        /// <summary>
        /// 現在の Dpi の値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double Dpi
        {
            get { return _dpi; }
            private set
            {
                if (_dpi == value) return;
                _dpi = value;
                OnDpiChanged(ValueEventArgs.Create(value));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Bootstrap
        /// 
        /// <summary>
        /// プロセス間通信を介した起動およびアクティブ化を制御するための
        /// オブジェクトを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Cube.Processes.Bootstrap Bootstrap
        {
            get { return _bootstrap; }
            set
            {
                if (_bootstrap == value) return;
                if (_bootstrap != null) _bootstrap.Received -= Bootstrap_Received;
                _bootstrap = value;
                if (_bootstrap != null)
                {
                    _bootstrap.Received -= Bootstrap_Received;
                    _bootstrap.Received += Bootstrap_Received;
                }
            }
        }

        #endregion

        #region Events

        #region Showing

        /* ----------------------------------------------------------------- */
        ///
        /// Showing
        /// 
        /// <summary>
        /// フォームが表示される直前に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event CancelEventHandler Showing;

        /* ----------------------------------------------------------------- */
        ///
        /// OnShowing
        /// 
        /// <summary>
        /// Showing イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnShowing(CancelEventArgs e) => Showing?.Invoke(this, e);

        #endregion

        #region Hiding

        /* ----------------------------------------------------------------- */
        ///
        /// Hiding
        /// 
        /// <summary>
        /// フォームが非表示になる直前に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event CancelEventHandler Hiding;

        /* ----------------------------------------------------------------- */
        ///
        /// OnHiding
        /// 
        /// <summary>
        /// Hiding イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnHiding(CancelEventArgs e) => Hiding?.Invoke(this, e);

        #endregion

        #region Hidden

        /* ----------------------------------------------------------------- */
        ///
        /// Hidden
        /// 
        /// <summary>
        /// フォームが非表示なった直後に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Hidden;

        /* ----------------------------------------------------------------- */
        ///
        /// OnHidden
        /// 
        /// <summary>
        /// Hidden イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnHidden(EventArgs e) => Hidden?.Invoke(this, e);

        #endregion

        #region Received

        /* ----------------------------------------------------------------- */
        ///
        /// Received
        ///
        /// <summary>
        /// 他のプロセスからデータを受信した時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler<ValueEventArgs<object>> Received;

        /* ----------------------------------------------------------------- */
        ///
        /// OnReceived
        ///
        /// <summary>
        /// Received イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnReceived(ValueEventArgs<object> e) => Received?.Invoke(this, e);

        #endregion

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
        public event EventHandler<ValueEventArgs<double>> DpiChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnDpiChanged
        ///
        /// <summary>
        /// DpiChanged イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnDpiChanged(ValueEventArgs<double> e) => DpiChanged?.Invoke(this, e);

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
        public event EventHandler<QueryEventArgs<Point, Position>> NcHitTest;

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
            if (NcHitTest != null) NcHitTest(this, e);
            else e.Cancel = true;
        }

        #endregion

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// AdjustDesktopLocation
        /// 
        /// <summary>
        /// スクリーンからはみ出さないように表示位置を調整します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void AdjustDesktopLocation()
        {
            var screen = System.Windows.Forms.Screen.FromPoint(DesktopLocation) ??
                         System.Windows.Forms.Screen.PrimaryScreen;
            var left   = screen.WorkingArea.Left;
            var top    = screen.WorkingArea.Top;
            var right  = screen.WorkingArea.Right;
            var bottom = screen.WorkingArea.Bottom;

            SetDesktopLocation(
                Math.Min(Math.Max(DesktopLocation.X, left), right - Width),
                Math.Min(Math.Max(DesktopLocation.Y, top), bottom - Height)
            );
        }

        #endregion

        #region Implementations

        #region Override methods

        /* ----------------------------------------------------------------- */
        ///
        /// SetVisibleCore
        /// 
        /// <summary>
        /// コントロールを指定した表示状態に設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void SetVisibleCore(bool value)
        {
            var prev = Visible;
            var ev = new CancelEventArgs();
            RaiseChangingVisibleEvent(prev, value, ev);
            base.SetVisibleCore(ev.Cancel ? prev : value);
            if (prev == value || ev.Cancel) return;
            RaiseVisibleChangedEvent(value, prev, EventArgs.Empty);
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
                case 0x02e0: // WM_DPICHANGED                    
                    Dpi = (short)(m.WParam.ToInt32() & 0x0000ffff);
                    break;
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

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// Bootstrap_Received
        /// 
        /// <summary>
        /// 他プロセスからメッセージを受信（アクティブ化）した時に実行
        /// されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Bootstrap_Received(object sender, ValueEventArgs<object> e)
        {
            if (InvokeRequired) Invoke(new Action(() => Bootstrap_Received(sender, e)));
            else
            {
                Show();

                var tmp = TopMost;
                TopMost = true;
                TopMost = tmp;

                OnReceived(e);
            }
        }

        #endregion

        #region Other private methods

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseChangingVisibleEvent
        /// 
        /// <summary>
        /// 表示状態の変更に関するイベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void RaiseChangingVisibleEvent(bool current, bool ahead, CancelEventArgs e)
        {
            if (!current && ahead) OnShowing(e);
            else if (current && !ahead) OnHiding(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseVisibleChangedEvent
        /// 
        /// <summary>
        /// 表示状態が変更された事を通知するイベントを発生させます。
        /// </summary>
        /// 
        /// <remarks>
        /// TODO: システムによる Shown イベントは最初の 1 度しか発生しない
        /// 模様。Showing イベント等との整合性をどうするか検討する。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void RaiseVisibleChangedEvent(bool current, bool behind, EventArgs e)
        {
            if (!current && behind) OnHidden(e);
        }

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
            int x = (short)(lparam.ToInt32() & 0x0000ffff);
            int y = (short)((lparam.ToInt32() & 0xffff0000) >> 16);
            return new Point(x, y);
        }

        #endregion

        #region Fields
        private double _dpi = 0.0;
        private Cube.Processes.Bootstrap _bootstrap = null;
        #endregion

        #endregion
    }
}
