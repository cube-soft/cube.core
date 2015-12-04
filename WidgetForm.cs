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
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Cube.Forms.Extensions;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.WidgetForm
    /// 
    /// <summary>
    /// Widget アプリケーション用のフォームを作成するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class WidgetForm : NtsForm
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
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// AllowDragMove
        /// 
        /// <summary>
        /// マウスのドラッグ操作でフォームを移動可能にするかどうかを取得
        /// または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowDragMove { get; set; } = true;

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
        [DefaultValue(5)]
        public int SizeGrip { get; set; } = 5;

        #region Hiding properties

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new AutoScaleMode AutoScaleMode
        {
            get { return base.AutoScaleMode; }
            set { base.AutoScaleMode = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new SizeGripStyle SizeGripStyle
        {
            get { return base.SizeGripStyle; }
            set { base.SizeGripStyle = value; }
        }

        #endregion

        #endregion

        #region Events

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
        /// Hidden
        /// 
        /// <summary>
        /// フォームが非表示なった直後に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler Hidden;

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnShowing
        /// 
        /// <summary>
        /// フォームが表示される直前に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnShowing(CancelEventArgs e)
        {
            if (Showing != null) Showing(this, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnHiding
        /// 
        /// <summary>
        /// フォームが非表示になる直前に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnHiding(CancelEventArgs e)
        {
            if (Hiding != null) Hiding(this, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnHidden
        /// 
        /// <summary>
        /// フォームが非表示なった直後に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnHidden(EventArgs e)
        {
            if (Hidden != null) Hidden(this, e);
        }

        #endregion

        #region Override properties and methods

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
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000;
                return cp;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseDown
        /// 
        /// <summary>
        /// マウスが押下された時に実行されるハンドラです。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Left) return;

                var kind = GetMousePoint(e.Location, Size);
                if (kind != MousePoint.Others) BeginDragResize(kind, e.Location);
                else if (AllowDragMove) DoDragMove();
            }
            finally { base.OnMouseDown(e); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseUp
        /// 
        /// <summary>
        /// マウスのボタンが離れた時に実行されるハンドラです。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        protected override void OnMouseUp(MouseEventArgs e)
        {
            EndDragResize();
            base.OnMouseUp(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseMove
        /// 
        /// <summary>
        /// マウスが移動した時に実行されるハンドラです。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                if (!Sizable) return;

                switch (e.Button)
                {
                    case MouseButtons.None:
                        UpdateCursor(e.Location);
                        break;
                    case MouseButtons.Left:
                        UpdateSize(e.Location);
                        break;
                    default:
                        break;
                }
            }
            finally { base.OnMouseMove(e); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseLeave
        /// 
        /// <summary>
        /// マウスポインタがフォームから離れた時に実行されるハンドラです。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        protected override void OnMouseLeave(EventArgs e)
        {
            try
            {
                if (!Sizable) return;
                Cursor = _userCursor;
            }
            finally { base.OnMouseLeave(e); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnLoad
        /// 
        /// <summary>
        /// フォームがロードされた時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnLoad(EventArgs e)
        {
            AddMouseDown(this);
            base.OnLoad(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnShown
        /// 
        /// <summary>
        /// フォームが表示された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnShown(EventArgs e)
        {
            _userCursor = Cursor;
            base.OnShown(e);
        }

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
            RaiseVisibleChangedEvent(value, prev, new EventArgs());
        }

        #endregion

        #region Raise event methods

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
        ///       模様。Showing イベント等との整合性をどうするか検討する。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void RaiseVisibleChangedEvent(bool current, bool behind, EventArgs e)
        {
            if (!current && behind) OnHidden(e);
        }

        #endregion

        #region Moving methods

        /* ----------------------------------------------------------------- */
        ///
        /// DoDragMove
        /// 
        /// <summary>
        /// フォームのドラッグ移動を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void DoDragMove()
        {
            Win32Api.ReleaseCapture();
            Win32Api.SendMessage(Handle, Win32Api.WM_NCLBUTTONDOWN,
                (IntPtr)Win32Api.HT_CAPTION, IntPtr.Zero);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddMouseDown
        /// 
        /// <summary>
        /// コントロールに対して、ドラッグ中のマウス移動に
        /// フォームを追随させるためのイベントハンドラを設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AddMouseDown(Control control)
        {
            foreach (Control child in control.Controls) AddMouseDown(child);
            if (MouseDownAvailable(control))
            {
                control.MouseDown += (s, e) => OnMouseDown(e);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDownAvailable
        /// 
        /// <summary>
        /// MouseDown イベントに対して、ドラッグ中のマウス移動にフォームを
        /// 追随させるためのハンドラを追加して良いかどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool MouseDownAvailable(Control control)
        {
            var reserved = control.HasEventHandler("MouseEnter") ||
                           control.HasEventHandler("MouseHover") ||
                           control.HasEventHandler("MouseLeave") ||
                           control.HasEventHandler("MouseDown") ||
                           control.HasEventHandler("MouseUp") ||
                           control.HasEventHandler("MouseClick") ||
                           control.HasEventHandler("MouseDoubleclick");
            return IsContainerComponent(control) && !reserved;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsContainerComponent
        /// 
        /// <summary>
        /// MouseDown イベントを奪っても良いコンポーネントかどうかを
        /// 判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsContainerComponent(Control control)
        {
            if (control is SplitContainer ||
                control is Panel ||
                control is GroupBox ||
                control is TabControl ||
                control is Label ||
                control is PictureBox) return true;
            return false;
        }

        #endregion

        #region Resizing methods

        /* ----------------------------------------------------------------- */
        ///
        /// BeginDragResize
        /// 
        /// <summary>
        /// マウスドラッグによるサイズ変更操作を開始します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void BeginDragResize(MousePoint kind, Point point)
        {
            _startKind  = kind;
            _startPoint = PointToScreen(point);
            _startSize  = Size;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// EndDragResize
        /// 
        /// <summary>
        /// マウスドラッグによるサイズ変更操作を終了します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void EndDragResize()
        {
            _startKind  = MousePoint.Others;
            _startPoint = Point.Empty;
            _startSize  = Size.Empty;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateSize
        /// 
        /// <summary>
        /// フォームのサイズを変更します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateSize(Point point)
        {
            var converted = PointToScreen(point);
            UpdateWidth(converted);
            UpdateHeight(converted);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateWidth
        /// 
        /// <summary>
        /// マウス座標を基にフォームの幅を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateWidth(Point current)
        {
            if ((_startKind & (MousePoint.Left | MousePoint.Right)) == 0) return;

            var offset = current.X - _startPoint.X;
            if ((_startKind & MousePoint.Left) != 0)
            {
                var value = _startSize.Width - offset;
                if (value == Width) return;

                Width = value;
                Left = _startPoint.X + offset;
            }
            else
            {
                var value = _startSize.Width + offset;
                if (Width != value) Width = value;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateHeight
        /// 
        /// <summary>
        /// マウス座標を基にフォームの高さを更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateHeight(Point current)
        {
            if ((_startKind & (MousePoint.Top | MousePoint.Bottom)) == 0) return;

            var offset = current.Y - _startPoint.Y;
            if ((_startKind & MousePoint.Top) != 0)
            {
                var value = _startSize.Height - offset;
                if (value == Height) return;

                Height = value;
                Top = _startPoint.Y + offset;
            }
            else
            {
                var value = _startSize.Height + offset;
                if (value != Height) Height = value;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateCursor
        /// 
        /// <summary>
        /// カーソルの外観を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateCursor(Point point)
        {
            var kind = GetMousePoint(point, Size);
            var dest = kind.ToCursor();
            Cursor = (dest != Cursors.Default) ? dest : _userCursor;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetMousePoint
        /// 
        /// <summary>
        /// マウスポインタが存在する位置を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private MousePoint GetMousePoint(Point origin, Size size)
        {
            var x = origin.X;
            var y = origin.Y;
            var w = size.Width;
            var h = size.Height;

            var left   = (x >= 0 && x <= SizeGrip);
            var top    = (y >= 0 && y <= SizeGrip);
            var right  = (x <= w && x >= w - SizeGrip);
            var bottom = (y <= h && y >= h - SizeGrip);

            return top && left     ? MousePoint.TopLeft     :
                   top && right    ? MousePoint.TopRight    :
                   bottom && left  ? MousePoint.BottomLeft  :
                   bottom && right ? MousePoint.BottomRight :
                   top             ? MousePoint.Top         :
                   bottom          ? MousePoint.Bottom      :
                   left            ? MousePoint.Left        :
                   right           ? MousePoint.Right       :
                                     MousePoint.Others      ;
        }

        #endregion

        #region Win32 APIs

        internal class Win32Api
        {
            public const int WM_NCLBUTTONDOWN = 0xA1;
            public const int HT_CAPTION = 0x2;

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern bool ReleaseCapture();
        }

        #endregion

        #region Fields
        private Cursor _userCursor = Cursors.Default;
        private MousePoint _startKind = MousePoint.Others;
        private Point _startPoint = Point.Empty;
        private Size _startSize = Size.Empty;
        #endregion
    }
}
