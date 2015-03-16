/* ------------------------------------------------------------------------- */
///
/// WidgetForm.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

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
            AutoScaleMode = AutoScaleMode.None;
            DoubleBuffered = true;
            Font = new Font(Font.Name, 12, Font.Style, GraphicsUnit.Pixel);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
        }

        #endregion

        #region Hiding properties

        [Browsable(false)]
        public new AutoScaleMode AutoScaleMode
        {
            get { return base.AutoScaleMode; }
            set { base.AutoScaleMode = value; }
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
        /// マウスクリック時に発生するイベントです。
        /// </summary>
        /// 
        /// <remarks>
        /// ドラッグ中のマウス移動にフォームを追随させるうにカスタマイズ
        /// します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Win32Api.ReleaseCapture();
                Win32Api.SendMessage(Handle, Win32Api.WM_NCLBUTTONDOWN,
                    (IntPtr)Win32Api.HT_CAPTION, IntPtr.Zero);
            }
            base.OnMouseDown(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnControlAdded
        /// 
        /// <summary>
        /// コントロールが追加された時に発生するイベントです。
        /// </summary>
        /// 
        /// <remarks>
        /// 追加されたコントロールに対しても、ドラッグ中のマウス移動に
        /// フォームを追随させるためのイベントハンドラを設定します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnControlAdded(ControlEventArgs e)
        {
            AddMouseDown(e.Control);
            base.OnControlAdded(e);
        }


        #endregion

        #region Implementations

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
        /// <remarks>
        /// TODO: MouseEnter, MouseHover, MouseLeave, MouseDown, MouseUp,
        ///       MouseClick, MouseDoubleclick イベントに対して、
        ///       既にハンドラが設定されている場合には false を返す。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private bool MouseDownAvailable(Control control)
        {
            var has =
                HasEventHandler(control, "MouseEnter") ||
                HasEventHandler(control, "MouseHover") ||
                HasEventHandler(control, "MouseLeave") ||
                HasEventHandler(control, "MouseDown") ||
                HasEventHandler(control, "MouseUp") ||
                HasEventHandler(control, "MouseClick") ||
                HasEventHandler(control, "MouseDoubleclick");
            return !has;
        }

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

        #region HasEventHandler

        public static bool HasEventHandler(object obj, string eventName)
        {
            EventHandlerList ehl = GetEvents(obj);
            if (ehl == null) return false;

            object key = GetEventKey(obj, eventName);
            if (key == null) return false;

            return ehl[key] != null;
        }

        private delegate MethodInfo delGetEventsMethod(Type objType, delGetEventsMethod callback);
        private static EventHandlerList GetEvents(object obj)
        {
            delGetEventsMethod GetEventsMethod = delegate(Type objtype, delGetEventsMethod callback)
            {
                MethodInfo mi = objtype.GetMethod("get_Events", All);
                if ((mi == null) & (objtype.BaseType != null))
                    mi = callback(objtype.BaseType, callback);
                return mi;
            };

            MethodInfo methodInfo = GetEventsMethod(obj.GetType(), GetEventsMethod);
            if (methodInfo == null) return null;
            return (EventHandlerList)methodInfo.Invoke(obj, new object[] { });
        }

        private delegate FieldInfo delGetKeyField(Type objType, string eventName, delGetKeyField callback);
        private static object GetEventKey(object obj, string eventName)
        {
            delGetKeyField GetKeyField = delegate(Type objtype, string eventname, delGetKeyField callback)
            {
                FieldInfo fi = objtype.GetField("Event" + eventName, All);
                if ((fi == null) & (objtype.BaseType != null))
                    fi = callback(objtype.BaseType, eventName, callback);
                return fi;
            };

            FieldInfo fieldInfo = GetKeyField(obj.GetType(), eventName, GetKeyField);
            if (fieldInfo == null) return null;
            return fieldInfo.GetValue(obj);
        }

        private static BindingFlags All
        {
            get
            {
                return
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance |
                    BindingFlags.IgnoreCase |
                    BindingFlags.Static;
            }
        }

        #endregion

        #endregion
    }
}
