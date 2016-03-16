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
using Cube.Forms.Extensions;

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
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// MinimizeAnimation
        /// 
        /// <summary>
        /// 最小化時のアニメーションを有効にするかどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool MinimizeAnimation { get; set; } = true;

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
        [DefaultValue(false)]
        public bool Sizable { get; set; } = false;

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
        /// WidgetForm クラスでは、フォームに陰影を付与するための
        /// パラメータをベースの値に追加しています。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.Style |= 0x20000; /* WS_MINIMIZEBOX*/
                cp.ClassStyle |= 0x00020000; /* CS_DROPSHADOW */
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
        public new System.Windows.Forms.SizeGripStyle SizeGripStyle
        {
            get { return base.SizeGripStyle; }
            set { base.SizeGripStyle = value; }
        }

        #endregion

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Maximize
        ///
        /// <summary>
        /// 最大化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Maximize()
        {
            if (!Sizable || !MaximizeBox) return;

            WindowState  = WindowState == System.Windows.Forms.FormWindowState.Normal ?
                           System.Windows.Forms.FormWindowState.Maximized :
                           System.Windows.Forms.FormWindowState.Normal;
            _windowState = WindowState;
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
        public void Minimize()
        {
            var minimized = System.Windows.Forms.FormWindowState.Minimized;
            var none  = System.Windows.Forms.FormBorderStyle.None;
            var style = System.Windows.Forms.FormBorderStyle.FixedSingle;

            if (WindowState == minimized) return;
            try { if (MinimizeAnimation && FormBorderStyle == none) FormBorderStyle = style; }
            catch { /* ignore erros */ }
            WindowState = minimized;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Restore
        ///
        /// <summary>
        /// 最小化される直前の WindowState に戻します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Restore()
        {
            var minimized = System.Windows.Forms.FormWindowState.Minimized;
            if (WindowState != minimized) return;
            WindowState = _windowState;
            try { if (FormBorderStyle != _borderStyle) FormBorderStyle = _borderStyle; }
            catch { /* ignore errors */ }
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
        /// OnShown
        /// 
        /// <summary>
        /// 初回表示時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _windowState = WindowState;
            _borderStyle = FormBorderStyle;
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
            var normal = WindowState == System.Windows.Forms.FormWindowState.Normal;
            var result = this.HitTest(PointToClient(e.Query), SizeGrip);
            var others = result == Position.NoWhere || result == Position.Client;
            if (others && IsCaption(e.Query)) result = Position.Caption;

            e.Result = result;
            e.Cancel = e.Result == Position.Caption ? false :
                       e.Result == Position.NoWhere ? true  :
                       (!Sizable || !normal);

            base.OnNcHitTest(e);
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
            switch (m.Msg)
            {
                case 0x0112: // WM_SYSCOMMAND
                    switch (m.WParam.ToInt32() & 0xfff0)
                    {
                        case 0xf020: // SC_MINIMIZE
                            OnSysMinimize(ref m);
                            break;
                        case 0xf030: // SC_MAXIMIZE
                            OnSysMaximize(ref m);
                            break;
                        case 0xf120: // SC_RESTORE
                            OnSysRestore(ref m);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// OnSysMinimize
        ///
        /// <summary>
        /// 最小化コマンドを受信した時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OnSysMinimize(ref System.Windows.Forms.Message m)
        {
            Minimize();
            m.Result = IntPtr.Zero;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnSysMaximize
        ///
        /// <summary>
        /// 最大化コマンドを受信した時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OnSysMaximize(ref System.Windows.Forms.Message m)
        {
            if (!Sizable) m.Result = IntPtr.Zero;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnSysRestore
        ///
        /// <summary>
        /// 元に戻すコマンドを受信した時に実行されます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OnSysRestore(ref System.Windows.Forms.Message m)
        {
            Restore();
            m.Result = IntPtr.Zero;
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// IsCaption
        /// 
        /// <summary>
        /// Position.Caption を表す領域かどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsCaption(Point origin)
        {
            if (Caption == null) return false;
            var p = Caption.PointToClient(origin);
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
            => MaximumSize = System.Windows.Forms.Screen.FromControl(this).WorkingArea.Size;

        #endregion

        #region Fields
        private System.Windows.Forms.FormWindowState _windowState;
        private System.Windows.Forms.FormBorderStyle _borderStyle;
        #endregion
    }
}
