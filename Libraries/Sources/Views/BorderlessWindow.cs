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
using System.Runtime.InteropServices;
using Cube.Forms.Controls;
using Microsoft.Win32;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// BorderlessWindow
    ///
    /// <summary>
    /// Represents an Windows form with no title bar and borders.
    /// </summary>
    ///
    /// <remarks>
    /// 既存の Form クラスに対して、タイトルバーや枠線等を全てクライアント
    /// 領域と見なすように修正されています。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class BorderlessWindow : Window
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// BorderlessForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public BorderlessWindow()
        {
            SystemEvents.DisplaySettingsChanged += (s, e) => UpdateMaximumSize();
            SystemEvents.UserPreferenceChanged  += (s, e) => UpdateMaximumSize();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// MaximizeBox
        ///
        /// <summary>
        /// 最大化ボタンを有効にするかどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        ///
        /// <remarks>
        /// 変更内容を Caption に反映させるため、元々の MaximizeBox
        /// プロパティを隠ぺいしています。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public new bool MaximizeBox
        {
            get => base.MaximizeBox;
            set
            {
                if (base.MaximizeBox == value) return;
                base.MaximizeBox = value;
                Attach(Caption);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MinimizeBox
        ///
        /// <summary>
        /// 最小化ボタンを有効にするかどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        ///
        /// <remarks>
        /// 変更内容を Caption に反映させるため、元々の MinimizeBox
        /// プロパティを隠ぺいしています。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public new bool MinimizeBox
        {
            get => base.MinimizeBox;
            set
            {
                if (base.MinimizeBox == value) return;
                base.MinimizeBox = value;
                Attach(Caption);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DropShadow
        ///
        /// <summary>
        /// フォームの外部に陰影を描画するかどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool DropShadow { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// Sizable
        ///
        /// <summary>
        /// サイズ変更を可能にするかどうかを示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool Sizable
        {
            get => _sizable;
            set
            {
                if (_sizable == value) return;
                _sizable = value;
                if (!_sizable && MaximizeBox) MaximizeBox = false;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SizeGrip
        ///
        /// <summary>
        /// サイズを変更するためのグリップ幅を取得または設定します。
        /// このプロパティは Sizable が無効の場合は無視されます。
        /// </summary>
        ///
        /// <remarks>
        /// フォームの上下左右から指定されたピクセル分の領域をサイズ変更の
        /// ためのグリップとして利用します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(6)]
        public int SizeGrip { get; set; } = 6;

        /* ----------------------------------------------------------------- */
        ///
        /// CornerRadius
        ///
        /// <summary>
        /// 四隅の角の丸みを表す値を取得または設定します。
        /// </summary>
        ///
        /// <remarks>
        /// TODO: ShowInTaskbar の値を変更した時に Region がリセット
        /// されてしまうので、該当部分の処理を要検討。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(0)]
        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                if (_cornerRadius == value) return;
                _cornerRadius = value;
                Region = CreateNewRegion();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SystemMenu
        ///
        /// <summary>
        /// システムメニューを表示するかどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        ///
        /// <remarks>
        /// システムメニューの有無は FormBorderStyle の値を変更する事で
        /// 対応します。 SystemMenu を false に設定した場合は、
        /// システムメニューの非表示に加えて最小化時のアニメーション等、
        /// いくつかのシステムによる動作が無効化されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool SystemMenu
        {
            get => base.FormBorderStyle != System.Windows.Forms.FormBorderStyle.None;
            set
            {
                var dest = value ?
                           System.Windows.Forms.FormBorderStyle.Sizable :
                           System.Windows.Forms.FormBorderStyle.None;
                if (base.FormBorderStyle == dest) return;
                base.FormBorderStyle = dest;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Caption
        ///
        /// <summary>
        /// キャプション（タイトルバー）を表すコントロールを取得または
        /// 設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CaptionControl Caption
        {
            get => _caption;
            set
            {
                if (_caption == value) return;
                Detach(_caption);
                _caption = value;
                Detach(_caption);
                Attach(_caption);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CaptionMonitoring
        ///
        /// <summary>
        /// キャプションから発生するイベントを監視するかどうかを示す値を
        /// 取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool CaptionMonitoring { get; set; } = true;

        /* ----------------------------------------------------------------- */
        ///
        /// CreateParams
        ///
        /// <summary>
        /// コントロールの作成時に必要な情報をカプセル化します。
        /// </summary>
        ///
        /// <remarks>
        /// いくつかのメソッド (メッセージ) では、カスタマイズされた
        /// 非クライアント領域に関する不都合が存在します。
        /// そこで、CreateParams から一時的に WS_THICKFRAME 等の値を除去
        /// する事によって、この不都合を回避します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                if (DropShadow) cp.ClassStyle |= 0x00020000; // CS_DROPSHADOW
                if (_fakeMode)
                {
                    cp.Style &= ~(
                        0x00800000 // WS_BORDER
                      | 0x00C00000 // WS_CAPTION
                      | 0x00400000 // WS_DLGFRAME
                      | 0x00040000 // WS_THICKFRAME
                    );
                }
                return cp;
            }
        }

        #region Hiding properties

        /* ----------------------------------------------------------------- */
        ///
        /// FormBorderStyle
        ///
        /// <summary>
        /// 枠線の表示方法を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.FormBorderStyle FormBorderStyle
        {
            get => base.FormBorderStyle;
            set => base.FormBorderStyle = value;
        }

        #endregion

        #endregion

        #region Implementations

        #region Overrides

        /* ----------------------------------------------------------------- */
        ///
        /// OnLoad
        ///
        /// <summary>
        /// フォームのロード時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateMaximumSize();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnActivated
        ///
        /// <summary>
        /// フォームがアクティブ化された時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (Caption == null || !CaptionMonitoring) return;
            Caption.Active = true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnDeactivate
        ///
        /// <summary>
        /// フォームが非アクティブ化された時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            if (Caption == null || !CaptionMonitoring) return;
            Caption.Active = false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnNcHitTest
        ///
        /// <summary>
        /// マウスのヒットテスト発生時に実行されます。
        /// </summary>
        ///
        /// <remarks>
        /// サイズ変更用のマウスカーソルを描画するかどうかを決定します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnNcHitTest(QueryMessage<Point, Position> e)
        {
            base.OnNcHitTest(e);
            if (!e.Cancel) return;

            var result = this.HitTest(PointToClient(e.Source), Sizable ? SizeGrip : 0);
            var others = result == Position.NoWhere || result == Position.Client;
            if (others && IsCaption(e.Source)) result = Position.Caption;

            e.Value  = result;
            e.Cancel = e.Value == Position.Caption ? false :
                       e.Value == Position.NoWhere ? true :
                       WindowState != System.Windows.Forms.FormWindowState.Normal;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnResize
        ///
        /// <summary>
        /// リサイズ時に実行されます。
        /// </summary>
        ///
        /// <remarks>
        /// サイズ変更用のマウスカーソルを描画するかどうかを決定します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Region != null) Region = CreateNewRegion();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetClientSizeCore
        ///
        /// <summary>
        /// コントロールのクライアント領域のサイズを設定します。
        /// </summary>
        ///
        /// <remarks>
        /// 処理内容の詳細については、CreateParams の remarks を参照下さい。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected override void SetClientSizeCore(int x, int y)
        {
            try {
                _fakeMode = true;
                base.SetClientSizeCore(x, y);
            }
            finally { _fakeMode = false; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WndProc
        ///
        /// <summary>
        /// ウィンドウメッセージを処理します。
        /// </summary>
        ///
        /// <remarks>
        /// TODO: WM_CREATE (0x0001) および WM_NCCREATE (0x0081) で
        /// 設定されているサイズは、デザイナ (InitializeComponents) 等で
        /// 設定された Size に非クライアント領域のサイズが加算されている。
        /// 現状では WM_CREATE で Result に Zero を設定した後（Zero は
        /// ウィンドウ生成を意味する）システム側の処理をスキップさせている。
        /// 確認した限りではうまく機能しているが、何かに影響が及んで
        /// いないか要検討。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case 0x0001: // WM_CREATE (see remarks)
                    if (SystemMenu)
                    {
                        m.Result = IntPtr.Zero;
                        return;
                    }
                    break;
                case 0x0024: // WM_GETMINMAXINFO
                    if (WhenGetMinMaxInfo(ref m)) return;
                    break;
                case 0x0083: // WM_NCCALCSIZE
                    m.Result = IntPtr.Zero;
                    return;
                case 0x0085: // WM_NCPAINT
                    m.Result = new IntPtr(1);
                    break;
                case 0x0086: // WM_NCACTIVE
                    if (WhenNcActive(ref m)) return;
                    break;
                case 0x00a3: // WM_NCLBUTTONDBLCLK:
                    if (WhenSystemMaximize(ref m)) return;
                    break;
                case 0x00a5: // WM_NCRBUTTONUP
                    if (WhenSystemMenu(ref m)) return;
                    break;
                case 0x0047: // WM_WINDOWPOSCHANGED
                    try
                    { // see remarks of CreateParams
                        _fakeMode = true;
                        base.WndProc(ref m);
                    }
                    finally { _fakeMode = false; }
                    return;
                default:
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion

        #region Handlers

        /* ----------------------------------------------------------------- */
        ///
        /// WhenNcActive
        ///
        /// <summary>
        /// 非クライアント領域のアクティブ状態が変更された時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool WhenNcActive(ref System.Windows.Forms.Message m)
        {
            if (WindowState != System.Windows.Forms.FormWindowState.Minimized)
            {
                m.Result = new IntPtr(1);
                return true;
            }
            return false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenSystemMenu
        ///
        /// <summary>
        /// システムメニューの表示コマンドを受信した時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool WhenSystemMenu(ref System.Windows.Forms.Message m)
        {
            var point = new Point(
                (int)m.LParam & 0xffff,
                (int)m.LParam >> 16 & 0xffff);
            if (!SystemMenu || !IsCaption(point)) return false;

            PopupSystemMenu(point);
            m.Result = IntPtr.Zero;
            return true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenSystemMaximize
        ///
        /// <summary>
        /// 最大化ボタンのクリック以外で最大化要求が発生した時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool WhenSystemMaximize(ref System.Windows.Forms.Message m)
        {
            var point = new Point(
                (int)m.LParam & 0xffff,
                (int)m.LParam >> 16 & 0xffff);
            if (!Sizable || !MaximizeBox || !IsCaption(point)) return false;

            DoMaximize();
            m.Result = IntPtr.Zero;
            return true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenGetMinMaxInfo
        ///
        /// <summary>
        /// 最小値・最大値を決定する時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool WhenGetMinMaxInfo(ref System.Windows.Forms.Message m)
        {
            if (MaximumSize.Width <= 0 || MaximumSize.Height <= 0) return false;

            var screen = System.Windows.Forms.Screen.FromControl(this);
            var info = (MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(MINMAXINFO));
            info.ptMaxPosition.x = screen.WorkingArea.X - screen.Bounds.X;
            info.ptMaxPosition.y = screen.WorkingArea.Y - screen.Bounds.Y;
            info.ptMaxSize.x = screen.WorkingArea.Width;
            info.ptMaxSize.y = screen.WorkingArea.Height;
            info.ptMaxTrackSize.x = screen.WorkingArea.Width;
            info.ptMaxTrackSize.y = screen.WorkingArea.Height - 1;
            info.ptMinTrackSize.x = MinimumSize.Width;
            info.ptMinTrackSize.y = MinimumSize.Height;
            Marshal.StructureToPtr(info, m.LParam, true);

            m.Result = IntPtr.Zero;
            return true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenMaximizeRequested
        ///
        /// <summary>
        /// 最大化要求時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenMaximizeRequested(object sender, EventArgs e) => DoMaximize();

        /* ----------------------------------------------------------------- */
        ///
        /// WhenMinimizeRequested
        ///
        /// <summary>
        /// 最小化要求時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenMinimizeRequested(object sender, EventArgs e) => DoMinimize();

        /* ----------------------------------------------------------------- */
        ///
        /// WhenCloseRequested
        ///
        /// <summary>
        /// 画面を閉じる操作が要求された時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenCloseRequested(object sender, EventArgs e) => Close();

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// DoMaximize
        ///
        /// <summary>
        /// 最大化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void DoMaximize()
        {
            if (!Sizable || !MaximizeBox) return;

            WindowState = WindowState == System.Windows.Forms.FormWindowState.Normal ?
                          System.Windows.Forms.FormWindowState.Maximized :
                          System.Windows.Forms.FormWindowState.Normal;

            if (Caption != null) Caption.WindowState = WindowState;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DoMinimize
        ///
        /// <summary>
        /// 最小化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void DoMinimize()
        {
            var state = System.Windows.Forms.FormWindowState.Minimized;
            if (WindowState == state) return;
            WindowState = state;
            if (Caption != null) Caption.WindowState = WindowState;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PopupSystemMenu
        ///
        /// <summary>
        /// システムメニューを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void PopupSystemMenu(Point absolute)
        {
            var menu = User32.NativeMethods.GetSystemMenu(Handle, false);
            if (menu == IntPtr.Zero) return;

            var enabled = 0x0000u; // MF_ENABLED
            var grayed  = 0x0001u; // MF_GRAYED
            var normal  = (WindowState == System.Windows.Forms.FormWindowState.Normal);
            var sizable = (Sizable && normal) ? enabled : grayed;
            var movable = (Caption != null && normal) ? enabled : grayed;

            User32.NativeMethods.EnableMenuItem(menu, 0xf000 /* SC_SIZE */, sizable);
            User32.NativeMethods.EnableMenuItem(menu, 0xf010 /* SC_MOVE */, movable);

            var command = User32.NativeMethods.TrackPopupMenuEx(menu, 0x100 /* TPM_RETURNCMD */,
                absolute.X, absolute.Y, Handle, IntPtr.Zero);
            if (command == 0) return;

            User32.NativeMethods.PostMessage(Handle, 0x0112 /* WM_SYSCOMMAND */, new IntPtr(command), IntPtr.Zero);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsCaption
        ///
        /// <summary>
        /// Position.Caption を表す領域かどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsCaption(Point absolute)
        {
            if (Caption == null) return false;
            var p = Caption.PointToClient(absolute);
            return p.X >= 0 && p.X <= Caption.ClientSize.Width &&
                   p.Y >= 0 && p.Y <= Caption.ClientSize.Height;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateMaximumSize
        ///
        /// <summary>
        /// フォームの最大サイズを更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateMaximumSize()
        {
            if (DesignMode) return;

            var size = System.Windows.Forms.Screen.FromControl(this).WorkingArea.Size;
            if (MaximumSize == size) return;
            MaximumSize = size;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Attach
        ///
        /// <summary>
        /// CaptionControl オブジェクトにイベントハンドラを設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Attach(CaptionControl caption)
        {
            if (caption == null) return;
            if (caption.MaximizeControl != null) caption.MaximizeControl.Enabled = MaximizeBox;
            if (caption.MinimizeControl != null) caption.MinimizeControl.Enabled = MinimizeBox;
            if (CaptionMonitoring)
            {
                caption.MaximizeRequested += WhenMaximizeRequested;
                caption.MinimizeRequested += WhenMinimizeRequested;
                caption.CloseRequested    += WhenCloseRequested;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Detach
        ///
        /// <summary>
        /// CaptionBase オブジェクトからイベントハンドラを削除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Detach(CaptionControl caption)
        {
            if (caption == null) return;

            caption.MaximizeRequested -= WhenMaximizeRequested;
            caption.MinimizeRequested -= WhenMinimizeRequested;
            caption.CloseRequested    -= WhenCloseRequested;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateNewRegion
        ///
        /// <summary>
        /// Region オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Region CreateNewRegion() =>
            Region.FromHrgn(Gdi32.NativeMethods.CreateRoundRectRgn(
                0, 0, Width, Height, CornerRadius, CornerRadius
            ));

        #endregion

        #endregion

        #region Fields
        private bool _sizable = true;
        private bool _fakeMode = false;
        private int _cornerRadius = 0;
        private CaptionControl _caption;
        #endregion
    }
}
