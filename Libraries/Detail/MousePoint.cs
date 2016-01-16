/* ------------------------------------------------------------------------- */
///
/// MousePoint.cs
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
using System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.MousePoint
    /// 
    /// <summary>
    /// マウスポインタがコントロールのどの位置に存在するかを示すための
    /// 列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [Flags]
    internal enum MousePoint : int
    {
        Top         = 0x01,
        Bottom      = 0x02,
        Left        = 0x04,
        Right       = 0x08,
        TopLeft     = Top | Left,
        TopRight    = Top | Right,
        BottomLeft  = Bottom | Left,
        BottomRight = Bottom | Right,
        Others      = 0x00
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.MousePointExtensions
    /// 
    /// <summary>
    /// MousePoint に関する拡張メソッドを定義するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class MousePointExtensions
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ToCursor
        /// 
        /// <summary>
        /// マウスポインタの位置に対応するカーソルを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Cursor ToCursor(this MousePoint point)
        {
            switch (point)
            {
                case MousePoint.TopLeft:     return Cursors.SizeNWSE;
                case MousePoint.TopRight:    return Cursors.SizeNESW;
                case MousePoint.BottomLeft:  return Cursors.SizeNESW;
                case MousePoint.BottomRight: return Cursors.SizeNWSE;
                case MousePoint.Top:         return Cursors.SizeNS;
                case MousePoint.Bottom:      return Cursors.SizeNS;
                case MousePoint.Left:        return Cursors.SizeWE;
                case MousePoint.Right:       return Cursors.SizeWE;
                default: break;
            }
            return Cursors.Default;
        }
    }
}
