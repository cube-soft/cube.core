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
using System.Windows.Forms;
using Cube.Forms.Controls;
using Cube.Mixin.Forms.Controls;
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
    /// All title bars and borders are changed to be considered as client
    /// areas against the WinForms Form class.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class BorderlessWindow : Window
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// BorderlessWindow
        ///
        /// <summary>
        /// Initializes a new instance of the BorderlessWindow class.
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
        /// Gets or sets a value indicating whether the Maximize button is
        /// displayed in the caption bar of the window.
        /// </summary>
        ///
        /// <remarks>
        /// In order to reflect the changes in Caption, the original
        /// MaximizeBox property is hidden.
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
        /// Gets or sets a value indicating whether the Minimize button is
        /// displayed in the caption bar of the form.
        /// </summary>
        ///
        /// <remarks>
        /// In order to reflect the changes in Caption, the original
        /// MinimizeBox property is hidden.
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
        /// Gets or sets a value indicating whether to draw shading outside
        /// the window.
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
        /// Gets or sets a value indicating whether the windows is resizable.
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
        /// Gets or sets the grip width for resizing. The property is ignored
        /// when the Sizable property is set to false.
        /// </summary>
        ///
        /// <remarks>
        /// The class uses the specified pixels from the top, bottom, left,
        /// and right of the form as a grip for resizing.
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
        /// Gets or sets a value indicating whether to display the system
        /// menu.
        /// </summary>
        ///
        /// <remarks>
        /// The presence of the system menu is handled by changing the value
        /// of FormBorderStyle. If SystemMenu is set to false, in addition
        /// to hiding the system menu, some system behaviors such as
        /// minimization animation will be disabled.
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(true)]
        [DefaultValue(true)]
        public bool SystemMenu
        {
            get => base.FormBorderStyle != FormBorderStyle.None;
            set
            {
                var dest = value ? FormBorderStyle.Sizable : FormBorderStyle.None;
                if (base.FormBorderStyle == dest) return;
                base.FormBorderStyle = dest;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Caption
        ///
        /// <summary>
        /// Get or set the control that represents the caption, aka title bar.
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
        /// Gets or sets a value indicating whether to monitor events
        /// generated by the caption control.
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
        /// Gets the required creation parameters when the control handle
        /// is created.
        /// </summary>
        ///
        /// <remarks>
        /// In some methods (messages), there are some inconveniences
        /// related to customized non-client areas. We avoid this
        /// inconvenience by temporarily removing values such as
        /// WS_THICKFRAME from CreateParams property.
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected override CreateParams CreateParams
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
        /// Gets or sets the border style of the window.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new FormBorderStyle FormBorderStyle
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
        /// Occurs when the Load event is fired.
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
        /// Occurs when the Activated event is fired.
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
        /// Occurs when the Deactivate event is fired.
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
        /// Occurs when the NcHitTest event is fired.
        /// </summary>
        ///
        /// <remarks>
        /// Determines whether to draw the mouse cursor for resizing.
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
                       WindowState != FormWindowState.Normal;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetClientSizeCore
        ///
        /// <summary>
        /// Sets the client size of the form. This will adjust the bounds
        /// of the form to make the client size the requested size.
        /// </summary>
        ///
        /// <see cref="CreateParams" />
        ///
        /* ----------------------------------------------------------------- */
        protected override void SetClientSizeCore(int x, int y)
        {
            try
            {
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
        /// Processes Windows messages.
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
        protected override void WndProc(ref Message m)
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
        /// Occurs when the active state of a non-client area is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool WhenNcActive(ref Message m)
        {
            if (WindowState != FormWindowState.Minimized)
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
        /// Occurs when the system menu display command is received.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool WhenSystemMenu(ref Message m)
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
        /// Occurs when maximization is requested by means other than
        /// clicking the maximize button.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool WhenSystemMaximize(ref Message m)
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
        /// Occurs when determining the minimum and maximum values.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool WhenGetMinMaxInfo(ref Message m)
        {
            if (MaximumSize.Width <= 0 || MaximumSize.Height <= 0) return false;

            var screen = Screen.FromControl(this);
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
        /// Occurs when the maximization is requested.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenMaximizeRequested(object sender, EventArgs e) => DoMaximize();

        /* ----------------------------------------------------------------- */
        ///
        /// WhenMinimizeRequested
        ///
        /// <summary>
        /// Occurs when the minimization is requested.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenMinimizeRequested(object sender, EventArgs e) => DoMinimize();

        /* ----------------------------------------------------------------- */
        ///
        /// WhenCloseRequested
        ///
        /// <summary>
        /// Occurs when the close action is requested.
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
        /// Invokes the miximization.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void DoMaximize()
        {
            if (!Sizable || !MaximizeBox) return;

            WindowState = WindowState == FormWindowState.Normal ?
                          FormWindowState.Maximized :
                          FormWindowState.Normal;

            if (Caption != null) Caption.WindowState = WindowState;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DoMinimize
        ///
        /// <summary>
        /// Invokes the minimization.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void DoMinimize()
        {
            var state = FormWindowState.Minimized;
            if (WindowState == state) return;
            WindowState = state;
            if (Caption != null) Caption.WindowState = WindowState;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PopupSystemMenu
        ///
        /// <summary>
        /// Shows the system menu.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void PopupSystemMenu(Point absolute)
        {
            var menu = User32.NativeMethods.GetSystemMenu(Handle, false);
            if (menu == IntPtr.Zero) return;

            var enabled = 0x0000u; // MF_ENABLED
            var grayed  = 0x0001u; // MF_GRAYED
            var normal  = (WindowState == FormWindowState.Normal);
            var sizable = (Sizable && normal) ? enabled : grayed;
            var movable = (Caption != null && normal) ? enabled : grayed;

            _ = User32.NativeMethods.EnableMenuItem(menu, 0xf000 /* SC_SIZE */, sizable);
            _ = User32.NativeMethods.EnableMenuItem(menu, 0xf010 /* SC_MOVE */, movable);

            var command = User32.NativeMethods.TrackPopupMenuEx(menu, 0x100 /* TPM_RETURNCMD */,
                absolute.X, absolute.Y, Handle, IntPtr.Zero);
            if (command == 0) return;

            _ = User32.NativeMethods.PostMessage(Handle, 0x0112 /* WM_SYSCOMMAND */, new IntPtr(command), IntPtr.Zero);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsCaption
        ///
        /// <summary>
        /// Determines whether the specified point is in the Caption area.
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
        /// Updates the maximum size of the window.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateMaximumSize()
        {
            if (DesignMode) return;

            var size = Screen.FromControl(this).WorkingArea.Size;
            if (MaximumSize == size) return;
            MaximumSize = size;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Attach
        ///
        /// <summary>
        /// Sets event handlers to the specified CaptionControl object.
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
        /// Remove event handlers from the specified CaptionBase object.
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

        #endregion

        #endregion

        #region Fields
        private bool _sizable = true;
        private bool _fakeMode = false;
        private CaptionControl _caption;
        #endregion
    }
}
