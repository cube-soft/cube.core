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
using System.Reflection;
using WinForms = System.Windows.Forms;

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// ControlExtension
    ///
    /// <summary>
    /// System.Windows.Forms.Control の拡張用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ControlExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateDpi
        ///
        /// <summary>
        /// DPI の変更に応じてレイアウトを更新します。
        /// </summary>
        ///
        /// <param name="src">更新対象となるコントロール</param>
        /// <param name="before">変更前の DPI</param>
        /// <param name="after">変更後の DPI</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateDpi(this IDpiAwarable src, double before, double after)
        {
            if (before > 1.0 && src is WinForms.Control control)
            {
                var ratio = after / before;
                if (!(control is WinForms.Form)) UpdateLocation(control, ratio);
                UpdateLayout(control, ratio);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// HitTest
        ///
        /// <summary>
        /// コントロール中のどの位置にいるのかヒットテストを行います。
        /// </summary>
        ///
        /// <param name="src">コントロール</param>
        /// <param name="point">コントロールを基準とした座標</param>
        /// <param name="grip">グリップサイズ</param>
        ///
        /// <returns>コントロール中の位置</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Position HitTest(this WinForms.Control src, Point point, int grip)
        {
            var x = point.X;
            var y = point.Y;
            var w = src.ClientSize.Width;
            var h = src.ClientSize.Height;

            var client = (x > grip && x < w - grip && y > grip && y < h - grip);
            var left   = (x >= 0 && x <= grip);
            var top    = (y >= 0 && y <= grip);
            var right  = (x <= w && x >= w - grip);
            var bottom = (y <= h && y >= h - grip);

            return client          ? Position.Client      :
                   top && left     ? Position.TopLeft     :
                   top && right    ? Position.TopRight    :
                   bottom && left  ? Position.BottomLeft  :
                   bottom && right ? Position.BottomRight :
                   top             ? Position.Top         :
                   bottom          ? Position.Bottom      :
                   left            ? Position.Left        :
                   right           ? Position.Right       :
                                     Position.NoWhere     ;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// HasEventHandler
        ///
        /// <summary>
        /// 指定されたイベントに対して、イベントハンドラが設定されているか
        /// どうかを判別します。
        /// </summary>
        ///
        /// <param name="src">判別するコントロール</param>
        /// <param name="name">イベント名</param>
        ///
        /// <returns>
        /// イベントハンドラが設定されているかどうかを示す値
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool HasEventHandler(this WinForms.Control src, string name)
        {
            var key = GetEventKey(src, name);
            var map = GetEventHandlers(src);
            return key != null && map?[key] != null;
        }

        #endregion

        #region Implementations

        #region UpdateLayout

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateLayout
        ///
        /// <summary>
        /// DPI の変更に応じてレイアウトを更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateLayout(WinForms.Control src, double ratio)
        {
            UpdateSize(src, ratio);
            UpdateFont(src, ratio);

            if (src is IDpiAwarable dac)
            {
                foreach (var c in src.Controls)
                {
                    if (c is IDpiAwarable dest) dest.Dpi = dac.Dpi;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateLocation
        ///
        /// <summary>
        /// コントロールの位置を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void UpdateLocation(WinForms.Control src, double ratio) =>
            src.Location = new Point(
                (int)(src.Location.X * ratio),
                (int)(src.Location.Y * ratio)
            );

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateLayout
        ///
        /// <summary>
        /// コントロールの大きさや余白などを更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void UpdateSize(WinForms.Control src, double ratio)
        {
            src.Size = new Size(
                (int)(src.Size.Width  * ratio),
                (int)(src.Size.Height * ratio)
            );

            src.Margin = new WinForms.Padding(
                (int)(src.Margin.Left   * ratio),
                (int)(src.Margin.Top    * ratio),
                (int)(src.Margin.Right  * ratio),
                (int)(src.Margin.Bottom * ratio)
            );

            src.Padding = new WinForms.Padding(
                (int)(src.Padding.Left   * ratio),
                (int)(src.Padding.Top    * ratio),
                (int)(src.Padding.Right  * ratio),
                (int)(src.Padding.Bottom * ratio)
            );
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateFont
        ///
        /// <summary>
        /// フォントサイズを更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void UpdateFont(WinForms.Control src, double ratio)
        {
            if (src.Font == null) return;

            var name  = src.Font.FontFamily.Name;
            var size  = (float)(src.Font.Size * ratio);
            var unit  = src.Font.Unit;
            var style = src.Font.Style;

            src.Font = new Font(name, size, style, unit);
        }

        #endregion

        #region GetEvent

        /* ----------------------------------------------------------------- */
        ///
        /// GetEventHandlers
        ///
        /// <summary>
        /// 指定されたオブジェクトに設定されているイベントハンドラの
        /// 一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static EventHandlerList GetEventHandlers(object obj)
        {
            MethodInfo method(Type t)
            {
                var mi = t.GetMethod("get_Events", GetAllFlags());
                if (mi == null && t.BaseType != null) mi = method(t.BaseType);
                return mi;
            }
            return method(obj.GetType())?.Invoke(obj, new object[0]) as EventHandlerList;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetEventKey
        ///
        /// <summary>
        /// 指定されたイベント名に対応するオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static object GetEventKey(object obj, string name)
        {
            FieldInfo method(Type t, string n)
            {
                var fi = t.GetField($"Event{n}", GetAllFlags());
                if (fi == null && t.BaseType != null) fi = method(t.BaseType, n);
                return fi;
            }
            return method(obj.GetType(), name)?.GetValue(obj);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetAllFlags
        ///
        /// <summary>
        /// 全ての属性が有効になった BindingFlags を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static BindingFlags GetAllFlags() =>
            BindingFlags.Public     |
            BindingFlags.NonPublic  |
            BindingFlags.Instance   |
            BindingFlags.IgnoreCase |
            BindingFlags.Static     ;

        #endregion

        #endregion
    }
}
