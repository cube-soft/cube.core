/* ------------------------------------------------------------------------- */
///
/// WidgetForm.cs
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
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Cube.Forms.Controls;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// WidgetForm
    /// 
    /// <summary>
    /// Widget アプリケーション用のフォームを作成するためのクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// 既存の Form クラスに対して、タイトルバーや枠線等を全てクライアント領域と
    /// 見なすように修正されています。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class WidgetForm : Form
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// WidgetForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public WidgetForm()
            : base()
        {
            SystemEvents.DisplaySettingsChanged += (s, e) => UpdateMaximumSize();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// DropShadow
        /// 
        /// <summary>
        /// フォームの外部に陰影を描画するかどうかを示す値を取得または設定します。
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
            get { return _sizable; }
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
        /// SystemMenu
        /// 
        /// <summary>
        /// システムメニューを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// システムメニューの有無は FormBorderStyle の値を変更する事で対応します。
        /// SystemMenu を false に設定した場合は、システムメニューの非表示に加えて
        /// 最小化時のアニメーション等、いくつかのシステムによる動作が無効化されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool SystemMenu
        {
            get { return base.FormBorderStyle != System.Windows.Forms.FormBorderStyle.None; }
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
        /// タイトルバーを表すコントロールを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public UserControl Caption { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateParams
        /// 
        /// <summary>
        /// コントロールの作成時に必要な情報をカプセル化します。
        /// </summary>
        /// 
        /// <remarks>
        /// いくつかのメソッド (メッセージ) では、カスタマイズされた非クライアント
        /// 領域に関する不都合が存在します。そこで、CreateParams から一時的に
        /// WS_THICKFRAME 等の値を除去する事によって、この不都合を回避します。
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

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.AutoScaleMode AutoScaleMode
        {
            get { return base.AutoScaleMode; }
            set { base.AutoScaleMode = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = value; }
        }

        #endregion

        #endregion

        #region Non-virtual protected methods

        /* ----------------------------------------------------------------- */
        ///
        /// Maximize
        ///
        /// <summary>
        /// 最大化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected void Maximize()
        {
            if (!Sizable || !MaximizeBox) return;

            WindowState  = WindowState == System.Windows.Forms.FormWindowState.Normal ?
                           System.Windows.Forms.FormWindowState.Maximized :
                           System.Windows.Forms.FormWindowState.Normal;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Minimize
        ///
        /// <summary>
        /// 最小化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected void Minimize()
        {
            var state = System.Windows.Forms.FormWindowState.Minimized;
            if (WindowState == state) return;
            WindowState = state;
        }

        #endregion

        #region Override methods

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
        protected override void OnNcHitTest(QueryEventArgs<Point, Position> e)
        {
            base.OnNcHitTest(e);
            if (!e.Cancel) return;

            var normal = WindowState == System.Windows.Forms.FormWindowState.Normal;
            var result = this.HitTest(PointToClient(e.Query), SizeGrip);
            var others = result == Position.NoWhere || result == Position.Client;
            if (others && IsCaption(e.Query)) result = Position.Caption;

            e.Result = result;
            e.Cancel = e.Result == Position.Caption ? false :
                       e.Result == Position.NoWhere ? true  :
                       (!Sizable || !normal);
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
        /// TODO: WM_CREATE (0x0001) および WM_NCCREATE (0x0081) で設定されて
        /// いるサイズは、デザイナ (InitializeComponents) 等で設定された Size に
        /// 非クライアント領域のサイズが加算されている。現状では WM_CREATE で
        /// Result に Zero を設定した後（Zero はウィンドウ生成を意味する）、
        /// システム側の処理をスキップさせている。確認した限りではうまく機能して
        /// いるが、何かに影響が及んでいないか要検討。
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
                    if (OnGetMinMaxInfo(ref m)) return;
                    break;
                case 0x0083: // WM_NCCALCSIZE
                    m.Result = IntPtr.Zero;
                    return;
                case 0x00a5: // WM_NCRBUTTONUP
                    if (OnSystemMenu(ref m)) return;
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

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// OnSystemMenu
        ///
        /// <summary>
        /// システムメニューの表示コマンドを受信した時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool OnSystemMenu(ref System.Windows.Forms.Message m)
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
        /// OnGetMinMaxInfo
        ///
        /// <summary>
        /// 最小値・最大値を決定する時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool OnGetMinMaxInfo(ref System.Windows.Forms.Message m)
        {
            if (MaximumSize.Width <= 0 || MaximumSize.Height <= 0) return false;

            var info = (MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(MINMAXINFO));
            info.ptMaxPosition.x  = 1;
            info.ptMaxPosition.y  = 1;
            info.ptMaxSize.x      = MaximumSize.Width;
            info.ptMaxSize.y      = MaximumSize.Height;
            info.ptMaxTrackSize.x = MaximumSize.Width;
            info.ptMaxTrackSize.y = MaximumSize.Height;
            info.ptMinTrackSize.x = MinimumSize.Width;
            info.ptMinTrackSize.y = MinimumSize.Height;
            Marshal.StructureToPtr(info, m.LParam, true);

            m.Result = IntPtr.Zero;
            return true;
        }

        #endregion

        #region Others

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
            var size = System.Windows.Forms.Screen.FromControl(this).WorkingArea.Size;
            if (MaximumSize == size) return;
            MaximumSize = size;
        }

        #endregion

        #region Fields
        private bool _sizable = true;
        private bool _fakeMode = false;
        #endregion
    }
}
