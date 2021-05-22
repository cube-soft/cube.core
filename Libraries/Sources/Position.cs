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
using System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Position
    ///
    /// <summary>
    /// Specifies a position in a control.
    /// </summary>
    ///
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms645618.aspx
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public enum Position
    {
        /// <summary>Unknown. (HTNOWHERE)</summary>
        NoWhere = 0x00,
        /// <summary>Client area. (HTCLIENT)</summary>
        Client = 0x01,
        /// <summary>Caption, aka header bar. (HTCAPTION)</summary>
        Caption = 0x02,
        /// <summary>System menu. (HTSYSMENU)</summary>
        SystemMenu = 0x03,
        /// <summary>Grips for resizing. (HTSIZE or HTGROWBOX)</summary>
        Size = 0x04,
        /// <summary>Menu. (HTMENU)</summary>
        Menu = 0x05,
        /// <summary>Horizontal scroll bar. (HTHSCROLL)</summary>
        HorizontalScroll = 0x06,
        /// <summary>Vertical scroll bar. (HTVSCROLL)</summary>
        VerticalScroll = 0x07,
        /// <summary>Minimize button. (HTMINBUTTON or HTREDUCE)</summary>
        Minimize = 0x08,
        /// <summary>Maximize button. (HTMAXBUTTON or HTZOOM)</summary>
        Maximize = 0x09,
        /// <summary>Left edge. (HTLEFT)</summary>
        Left = 0x0a,
        /// <summary>Right edge. (HTRIGHT)</summary>
        Right = 0x0b,
        /// <summary>Top edge. (HTTOP)</summary>
        Top = 0x0c,
        /// <summary>Top left corner. (HTTOPLEFT)</summary>
        TopLeft = 0x0d,
        /// <summary>Top right corner. (HTTOPRIGHT)</summary>
        TopRight = 0x0e,
        /// <summary>Bottom edge. (HTBOTTOM)</summary>
        Bottom = 0x0f,
        /// <summary>Bottom left corner. (HTBOTTOMLEFT)</summary>
        BottomLeft = 0x10,
        /// <summary>Bottom right corner. (HTBOTTOMRIGHT)</summary>
        BottomRight = 0x11,
        /// <summary>On the border. (HTBORDER)</summary>
        Border = 0x12,
        /// <summary>Close button. (HTCLOSE)</summary>
        Close = 0x14,
        /// <summary>Help button. (HTHELP)</summary>
        Help = 0x15,
        /// <summary>Transparent area. (HTTRANSPARENT)</summary>
        Transparent = -1,
        /// <summary>Error. (HTERROR)</summary>
        Error = -2,
    }

    /* --------------------------------------------------------------------- */
    ///
    /// PositionExtension
    ///
    /// <summary>
    /// Provides extended methods of the Position enum.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class PositionExtension
    {
        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// ToCursor
        ///
        /// <summary>
        /// Get the Cursor object corresponding to the specified Position object.
        /// </summary>
        ///
        /// <param name="position">Source position.</param>
        ///
        /// <returns>Cursor object.</returns>
        ///
        /* --------------------------------------------------------------------- */
        public static Cursor ToCursor(this Position position)
        {
            switch (position)
            {
                case Position.TopLeft:     return Cursors.SizeNWSE;
                case Position.TopRight:    return Cursors.SizeNESW;
                case Position.BottomLeft:  return Cursors.SizeNESW;
                case Position.BottomRight: return Cursors.SizeNWSE;
                case Position.Top:         return Cursors.SizeNS;
                case Position.Bottom:      return Cursors.SizeNS;
                case Position.Left:        return Cursors.SizeWE;
                case Position.Right:       return Cursors.SizeWE;
                default: break;
            }
            return Cursors.Default;
        }

        #endregion
    }
}
