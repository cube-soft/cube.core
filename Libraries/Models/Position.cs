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
    /// コントロール中の位置を表すための列挙型です。
    /// </summary>
    ///
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms645618.aspx
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public enum Position
    {
        /// <summary>不明 (HTNOWHERE)</summary>
        NoWhere = 0x00,
        /// <summary>クライアント領域 (HTCLIENT)</summary>
        Client = 0x01,
        /// <summary>キャプション・ヘッダバー (HTCAPTION)</summary>
        Caption = 0x02,
        /// <summary>システムメニュー (HTSYSMENU)</summary>
        SystemMenu = 0x03,
        /// <summary>リサイズ用グリップ (HTSIZE or HTGROWBOX)</summary>
        Size = 0x04,
        /// <summary>メニュー (HTMENU)</summary>
        Menu = 0x05,
        /// <summary>水平スクロールバー (HTHSCROLL)</summary>
        HorizontalScroll = 0x06,
        /// <summary>垂直スクロールバー (HTVSCROLL)</summary>
        VerticalScroll = 0x07,
        /// <summary>最小化ボタン (HTMINBUTTON or HTREDUCE)</summary>
        Minimize = 0x08,
        /// <summary>最大化ボタン (HTMAXBUTTON or HTZOOM)</summary>
        Maximize = 0x09,
        /// <summary>左端 (HTLEFT)</summary>
        Left = 0x0a,
        /// <summary>右端 (HTRIGHT)</summary>
        Right = 0x0b,
        /// <summary>上端 (HTTOP)</summary>
        Top = 0x0c,
        /// <summary>左上 (HTTOPLEFT)</summary>
        TopLeft = 0x0d,
        /// <summary>右上 (HTTOPRIGHT)</summary>
        TopRight = 0x0e,
        /// <summary>下端 (HTBOTTOM)</summary>
        Bottom = 0x0f,
        /// <summary>左下 (HTBOTTOMLEFT)</summary>
        BottomLeft = 0x10,
        /// <summary>右下 (HTBOTTOMRIGHT)</summary>
        BottomRight = 0x11,
        /// <summary>枠線上 (HTBORDER)</summary>
        Border = 0x12,
        /// <summary>閉じるボタン (HTCLOSE)</summary>
        Close = 0x14,
        /// <summary>ヘルプボタン (HTHELP)</summary>
        Help = 0x15,
        /// <summary>透過領域 (HTTRANSPARENT)</summary>
        Transparent = -1,
        /// <summary>エラー (HTERROR)</summary>
        Error = -2,
    }

    /* --------------------------------------------------------------------- */
    ///
    /// PositionConversions
    ///
    /// <summary>
    /// Position の拡張用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class PositionConversions
    {
        #region Methods

        /* --------------------------------------------------------------------- */
        ///
        /// ToCursor
        ///
        /// <summary>
        /// Position に対応するカーソルを取得します。
        /// </summary>
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
