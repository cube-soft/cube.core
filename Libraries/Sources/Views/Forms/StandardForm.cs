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
using Cube.Mixin.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// StandardForm
    ///
    /// <summary>
    /// Windows 標準のフォームを表すクラスです。
    /// </summary>
    ///
    /// <remarks>
    /// System.Windows.Forms.Form をベースに実装されています。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class StandardForm : System.Windows.Forms.Form, IForm
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// StandardForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public StandardForm()
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
        /// Activator
        ///
        /// <summary>
        /// プロセス間通信を介した起動およびアクティブ化を制御するための
        /// オブジェクトを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Cube.Ipc.IMessenger<IEnumerable<string>> Activator
        {
            get => _activator;
            set
            {
                if (_activator == value) return;

                _remover?.Dispose();
                _activator = value;
                if (_activator != null) _remover = _activator.Subscribe(WhenActivated);
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
                System.Diagnostics.Debug.Assert(value > 1.0);

                if (_dpi == value) return;
                var old = _dpi;
                _dpi = value;
                OnDpiChanged(ValueEventArgs.Create(old, value));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// BaseDpi
        ///
        /// <summary>
        /// 基準となる Dpi の値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static double BaseDpi { get; } = 96.0;

        /* ----------------------------------------------------------------- */
        ///
        /// Aggregator
        ///
        /// <summary>
        /// イベント集約用オブジェクトを取得または設定します。
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
        public IAggregator Aggregator
        {
            get => _aggregator;
            set
            {
                if (_aggregator == value) return;
                _aggregator = value;
                foreach (var obj in Controls)
                {
                    if (obj is IControl c) c.Aggregator = value;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ShortcutKeys
        ///
        /// <summary>
        /// ショートカットキーの一覧を取得します。
        /// </summary>
        ///
        /// <remarks>
        /// ShortcutKeys に登録された動作が ProcessCmdKey(Message, Keys)
        /// にて実行されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDictionary<System.Windows.Forms.Keys, Action> ShortcutKeys { get; } =
            new Dictionary<System.Windows.Forms.Keys, Action>();

        /* ----------------------------------------------------------------- */
        ///
        /// ProductPlatform
        ///
        /// <summary>
        /// 実行中のプロセスのプラットフォームを表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ProductPlatform => IntPtr.Size == 4 ? "x86" : "x64";

        #endregion

        #region Events

        #region VisibleChanging

        /* ----------------------------------------------------------------- */
        ///
        /// VisibleChanging
        ///
        /// <summary>
        /// Visible プロパティの値が変更される直前に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event CancelEventHandler VisibleChanging;

        /* ----------------------------------------------------------------- */
        ///
        /// OnVisibleChanging
        ///
        /// <summary>
        /// VisibleChanging イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnVisibleChanging(CancelEventArgs e)
        {
            if (Visible && !DesignMode) AdjustDesktopLocation();
            VisibleChanging?.Invoke(this, e);
        }

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
        public event CollectionEventHandler<string> Received;

        /* ----------------------------------------------------------------- */
        ///
        /// OnReceived
        ///
        /// <summary>
        /// Received イベントを発生させます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnReceived(CollectionEventArgs<string> e) =>
            Received?.Invoke(this, e);

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
            var need = AutoScaleMode == System.Windows.Forms.AutoScaleMode.Dpi;
            if (need) this.UpdateDpi(e.OldValue, e.NewValue);
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
            var x      = DesktopLocation.X;
            var y      = DesktopLocation.Y;

            var left   = screen.WorkingArea.Left;
            var top    = screen.WorkingArea.Top;
            var right  = screen.WorkingArea.Right;
            var bottom = screen.WorkingArea.Bottom;

            if (x >= left && x +  Width <=  right &&
                y >=  top && y + Height <= bottom) return;

            SetDesktopLocation(
                Math.Min(Math.Max(DesktopLocation.X, left), right - Width),
                Math.Min(Math.Max(DesktopLocation.Y, top), bottom - Height)
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DoShortcutKeys
        ///
        /// <summary>
        /// ショートカットキーを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected bool DoShortcutKeys(System.Windows.Forms.Keys keys)
        {
            if (!ShortcutKeys.ContainsKey(keys)) return false;
            ShortcutKeys[keys]();
            return true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the StandardForm
        /// and optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (_disposed) return;
                _disposed = true;
                if (disposing) _activator?.Dispose();
            }
            finally { base.Dispose(disposing); }
        }

        #endregion

        #region Implementations

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
            var args = new CancelEventArgs();
            OnVisibleChanging(args);
            base.SetVisibleCore(args.Cancel ? prev : value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ProcessCmdKey
        ///
        /// <summary>
        /// ショートカットキーを処理します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg,
            System.Windows.Forms.Keys keys)
        {
            if (ShortcutKeys.ContainsKey(keys))
            {
                ShortcutKeys[keys]();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keys);
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

        /* ----------------------------------------------------------------- */
        ///
        /// WhenActivated
        ///
        /// <summary>
        /// 他プロセスからメッセージを受信（アクティブ化）した時に実行
        /// されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenActivated(IEnumerable<string> args)
        {
            if (InvokeRequired) Invoke(new Action(() => WhenActivated(args)));
            else
            {
                if (!Visible) Show();
                if (WindowState == System.Windows.Forms.FormWindowState.Minimized)
                {
                    WindowState = System.Windows.Forms.FormWindowState.Normal;
                }

                Activate();
                BringToFront();
                OnReceived(CollectionEventArgs.Create(args));
            }
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
        private double _dpi = BaseDpi;
        private Cube.Ipc.IMessenger<IEnumerable<string>> _activator;
        private IAggregator _aggregator;
        private IDisposable _remover;
        private bool _disposed = false;
        #endregion
    }
}
