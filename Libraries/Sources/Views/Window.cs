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
using Cube.Mixin.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using WinForms = System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Window
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
    public class Window : WindowBase, IDpiAwarable
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
        public Window()
        {
            AutoScaleMode = WinForms.AutoScaleMode.Dpi;
            DoubleBuffered = true;
            Font = FontFactory.Create(Font);

            using (var gs = CreateGraphics())
            {
                Dpi = gs.DpiX;
                if (gs.DpiX != gs.DpiY) this.LogWarn($"DpiX:{gs.DpiX}", $"DpiY:{gs.DpiY}");
            }
        }

        #endregion

        #region Properties

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
        public IDictionary<WinForms.Keys, Action> ShortcutKeys { get; } = new Dictionary<WinForms.Keys, Action>();

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
            var need = AutoScaleMode == WinForms.AutoScaleMode.Dpi;
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
        protected virtual void OnNcHitTest(QueryMessage<Point, Position> e)
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
            var screen = WinForms.Screen.FromPoint(DesktopLocation) ??
                         WinForms.Screen.PrimaryScreen;
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
        protected bool DoShortcutKeys(WinForms.Keys keys)
        {
            if (!ShortcutKeys.ContainsKey(keys)) return false;
            ShortcutKeys[keys]();
            return true;
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
        protected override bool ProcessCmdKey(ref WinForms.Message msg, WinForms.Keys keys)
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
        protected override void WndProc(ref WinForms.Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case 0x02e0: // WM_DPICHANGED
                    Dpi = (short)(m.WParam.ToInt32() & 0x0000ffff);
                    break;
                case 0x0084: // WM_NCHITTEST
                    var e = Query.NewMessage<Point, Position>(CreatePoint(m.LParam));
                    OnNcHitTest(e);
                    if (!e.Cancel) m.Result = (IntPtr)e.Value;
                    break;
                default:
                    break;
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
        #endregion
    }
}
