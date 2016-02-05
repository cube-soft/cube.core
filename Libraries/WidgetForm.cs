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
                cp.ClassStyle |= 0x00020000;
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

        #region Override methods

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
            if (result == Position.Client) result = Position.NoWhere;
            if (result == Position.NoWhere && IsCaption(e.Query)) result = Position.Caption;

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
                    var flag = (m.WParam.ToInt32() & 0xf030); // SC_MAXIMIZE
                    if (!Sizable && flag == 0xf030)
                    {
                        m.Result = IntPtr.Zero;
                        return;
                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
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

        #endregion
    }
}
