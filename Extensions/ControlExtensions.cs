/* ------------------------------------------------------------------------- */
///
/// ControlExtensions.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace Cube.Extensions.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Extensions.Forms.ControlExtensions
    /// 
    /// <summary>
    /// System.Windows.Forms.Control の拡張メソッドを定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    static class ControlExtensions
    {
        #region Extension methods

        /* ----------------------------------------------------------------- */
        ///
        /// HasEventHandler
        /// 
        /// <summary>
        /// 指定されたイベントに対して、イベントハンドラが設定されているか
        /// どうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static bool HasEventHandler(this Control control, string name)
        {
            var handler = GetEventHandlerList(control);
            var key = GetEventKey(control, name);
            if (handler == null || key == null) return false;
            return handler[key] != null;
        }

        #endregion

        #region Private methods

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
            if (info == null) return null;
            return info.Invoke(obj, new object[] { }) as EventHandlerList;
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
                var fi = t.GetField("Event" + n, GetAllFlags());
                if (fi == null && t.BaseType != null) fi = method(t.BaseType, n);
                return fi;
            };

            var info = method(obj.GetType(), name);
            return (info != null) ? info.GetValue(obj) : null;
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
