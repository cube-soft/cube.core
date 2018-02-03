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
using System.Reflection;
using System.Drawing;

namespace Cube.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// ControlOperator
    ///
    /// <summary>
    /// System.Windows.Forms.Control の拡張メソッドを定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ControlOperator
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// HasEventHandler
        ///
        /// <summary>
        /// 指定されたイベントに対して、イベントハンドラが設定されているか
        /// どうかを判別します。
        /// </summary>
        ///
        /// <param name="control">判別するコントロール</param>
        /// <param name="name">イベント名</param>
        ///
        /// <returns>
        /// イベントハンドラが設定されているかどうかを示す値
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool HasEventHandler(this System.Windows.Forms.Control control, string name)
        {
            var handler = GetEventHandlerList(control);
            var key = GetEventKey(control, name);
            if (handler == null || key == null) return false;
            return handler[key] != null;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// HitTest
        ///
        /// <summary>
        /// コントロール中のどの位置にいるのかヒットテストを行います。
        /// </summary>
        ///
        /// <param name="control">コントロール</param>
        /// <param name="point">コントロールを基準とした座標</param>
        /// <param name="grip">グリップサイズ</param>
        ///
        /// <returns>コントロール中の位置</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Position HitTest(this System.Windows.Forms.Control control, Point point, int grip)
        {
            var x = point.X;
            var y = point.Y;
            var w = control.ClientSize.Width;
            var h = control.ClientSize.Height;

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
        /// UpdateText
        ///
        /// <summary>
        /// フォームのタイトルを "message - ProductName" と言う表記で
        /// 更新します。
        /// </summary>
        ///
        /// <param name="form">フォーム</param>
        /// <param name="message">タイトルに表示するメッセージ</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateText(this System.Windows.Forms.Form form, string message) =>
            UpdateText(form, message, AssemblyReader.Default);

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateText
        ///
        /// <summary>
        /// フォームのタイトルを "message - ProductName" と言う表記で
        /// 更新します。
        /// </summary>
        ///
        /// <param name="form">フォーム</param>
        /// <param name="message">タイトルに表示するメッセージ</param>
        /// <param name="assembly">アセンブリ情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateText(this System.Windows.Forms.Form form,
            string message, Assembly assembly) =>
            UpdateText(form, message, new AssemblyReader(assembly));

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateText
        ///
        /// <summary>
        /// フォームのタイトルを "message - ProductName" と言う表記で
        /// 更新します。
        /// </summary>
        ///
        /// <param name="form">フォーム</param>
        /// <param name="message">タイトルに表示するメッセージ</param>
        /// <param name="assembly">アセンブリ情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateText(this System.Windows.Forms.Form form,
            string message, AssemblyReader assembly)
        {
            var ss = new System.Text.StringBuilder();
            ss.Append(message);
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(assembly.Product)) ss.Append(" - ");
            ss.Append(assembly.Product);

            form.Text = ss.ToString();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateControl
        ///
        /// <summary>
        /// DPI の変更に応じてレイアウトを更新します。
        /// </summary>
        ///
        /// <param name="src">更新対象となるコントロール</param>
        /// <param name="olddpi">変更前の DPI</param>
        /// <param name="newdpi">変更後の DPI</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateControl(this IDpiAwarable src, double olddpi, double newdpi)
        {
            if (olddpi > 1.0 && src is IControl control)
            {
                var ratio = newdpi / olddpi;
                var x = (int)(control.Location.X * ratio);
                var y = (int)(control.Location.Y * ratio);
                control.Location = new Point(x, y);
            }
            UpdateLayout(src, olddpi, newdpi);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateForm
        ///
        /// <summary>
        /// DPI の変更に応じてレイアウトを更新します。
        /// </summary>
        ///
        /// <param name="form">更新対象となるフォーム</param>
        /// <param name="olddpi">変更前の DPI</param>
        /// <param name="newdpi">変更後の DPI</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateForm(this IForm form, double olddpi, double newdpi) =>
            UpdateLayout(form, olddpi, newdpi);

        /* ----------------------------------------------------------------- */
        ///
        /// BringToFront
        ///
        /// <summary>
        /// フォームを最前面に表示します。
        /// </summary>
        ///
        /// <param name="form">フォーム</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void BringToFront(this System.Windows.Forms.Form form)
        {
            form.ResetTopMost();
            form.Activate();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ResetTopMost
        ///
        /// <summary>
        /// TopMost の値をリセットします。
        /// </summary>
        ///
        /// <param name="form">フォーム</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void ResetTopMost(this System.Windows.Forms.Form form)
        {
            var tmp = form.TopMost;
            form.TopMost = false;
            form.TopMost = true;
            form.TopMost = tmp;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetTopMost
        ///
        /// <summary>
        /// フォームを最前面に表示します。
        /// </summary>
        ///
        /// <param name="form">フォーム</param>
        /// <param name="active">アクティブ状態にするかどうか</param>
        ///
        /// <remarks>
        /// SetTopMost は主に、フォーカスを奪わずに最前面に表示する時に
        /// 使用します。この場合、最前面に表示された状態でも TopMost
        /// プロパティは false となります。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static void SetTopMost(this System.Windows.Forms.Form form, bool active)
        {
            if (active)
            {
                form.TopMost = true;
                form.Activate();
            }
            else
            {
                const uint SWP_NOSIZE         = 0x0001;
                const uint SWP_NOMOVE         = 0x0002;
                const uint SWP_NOACTIVATE     = 0x0010;
                const uint SWP_NOSENDCHANGING = 0x0400;

                User32.NativeMethods.SetWindowPos(form.Handle,
                    (IntPtr)(-1), /* HWND_TOPMOST */
                    0, 0, 0, 0,
                    SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSENDCHANGING | SWP_NOSIZE
                );
            }
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateLayout
        ///
        /// <summary>
        /// DPI の変更に応じてレイアウトを更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void UpdateLayout(IDpiAwarable src, double olddpi, double newdpi)
        {
            var control = src as IControl;
            if (control != null && olddpi > 1.0)
            {
                var ratio = newdpi / olddpi;

                var w = (int)(control.Size.Width * ratio);
                var h = (int)(control.Size.Height * ratio);
                control.Size = new Size(w, h);

                var ml = (int)(control.Margin.Left * ratio);
                var mt = (int)(control.Margin.Top * ratio);
                var mr = (int)(control.Margin.Right * ratio);
                var mb = (int)(control.Margin.Bottom * ratio);
                control.Margin = new System.Windows.Forms.Padding(ml, mt, mr, mb);

                var pl = (int)(control.Padding.Left * ratio);
                var pt = (int)(control.Padding.Top * ratio);
                var pr = (int)(control.Padding.Right * ratio);
                var pb = (int)(control.Padding.Bottom * ratio);
                control.Padding = new System.Windows.Forms.Padding(pl, pt, pr, pb);

                if (control.Font != null)
                {
                    var name = control.Font.FontFamily.Name;
                    var size = (float)(control.Font.Size * ratio);
                    var unit = control.Font.Unit;
                    var style = control.Font.Style;
                    control.Font = new Font(name, size, style, unit);
                }
            }

            if (control is System.Windows.Forms.Control native)
            {
                foreach (var c in native.Controls)
                {
                    if (c is IDpiAwarable dac) dac.Dpi = control.Dpi;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetEventHandlerList
        ///
        /// <summary>
        /// 指定されたオブジェクトに設定されているイベントハンドラの一覧を
        /// 取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static EventHandlerList GetEventHandlerList(object obj)
        {
            Func<Type, MethodInfo> method = null;
            method = (t) => {
                var mi = t.GetMethod("get_Events", GetAllFlags());
                if (mi == null && t.BaseType != null) mi = method(t.BaseType);
                return mi;
            };

            var info = method(obj.GetType());
            return info?.Invoke(obj, new object[] { }) as EventHandlerList;
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
            Func<Type, string, FieldInfo> method = null;
            method = (t, n) => {
                var fi = t.GetField($"Event{n}", GetAllFlags());
                if (fi == null && t.BaseType != null) fi = method(t.BaseType, n);
                return fi;
            };

            var info = method(obj.GetType(), name);
            return info?.GetValue(obj);
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
        private static BindingFlags GetAllFlags()
        {
            return BindingFlags.Public |
                   BindingFlags.NonPublic |
                   BindingFlags.Instance |
                   BindingFlags.IgnoreCase |
                   BindingFlags.Static;
        }

        #endregion
    }
}
