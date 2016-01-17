/* ------------------------------------------------------------------------- */
///
/// ControlEx.cs
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
using System.Reflection;
using System.Windows.Forms;

namespace Cube.Forms.Extensions
{
    /* --------------------------------------------------------------------- */
    ///
    /// ControlEx
    /// 
    /// <summary>
    /// System.Windows.Forms.Control の拡張メソッドを定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class ControlEx
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
