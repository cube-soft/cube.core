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
using Cube.Log;
using Microsoft.Win32;
using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Cube.Settings
{
    /* --------------------------------------------------------------------- */
    ///
    /// RegistrySettings
    ///
    /// <summary>
    /// ユーザ設定をレジストリ上で管理するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class RegistrySettings
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下に、オブジェクトの値を保存
        /// します。
        /// </summary>
        ///
        /// <param name="dest">レジストリ・サブキー</param>
        /// <param name="src">保存オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Save<T>(RegistryKey dest, T src) => Save(src, typeof(T), dest);

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下に、オブジェクトの値を保存
        /// します。
        /// </summary>
        ///
        /// <param name="dest">レジストリ・サブキー名</param>
        /// <param name="src">保存オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Save<T>(string dest, T src)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(dest))
            {
                Save(key, src);
            }
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下に、オブジェクトの値を保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void Save(object src, Type type, RegistryKey root)
        {
            if (src == null || root == null) return;
            foreach (var info in type.GetProperties()) Log(() =>Save(src, info, root));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下に、オブジェクトの値を保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void Save(object src, PropertyInfo info, RegistryKey root)
        {
            var name = GetDataMemberName(info);
            var value = info.GetValue(src, null);
            if (name == null || value == null) return;

            var type = GetType(info);
            if (type.IsEnum) root.SetValue(name, (int)value);
            else switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    root.SetValue(name, ((bool)value) ? 1 : 0);
                    break;
                case TypeCode.DateTime:
                    root.SetValue(name, Convert((DateTime)value));
                    break;
                case TypeCode.Object:
                    using (var k = root.CreateSubKey(name)) Save(value, type, k);
                    break;
                default:
                    root.SetValue(name, value);
                    break;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetType
        ///
        /// <summary>
        /// PropertyInfo オブジェクトの型を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static Type GetType(PropertyInfo src)
        {
            var dest = src.PropertyType;
            return dest.IsGenericType &&
                   dest.GetGenericTypeDefinition() == typeof(Nullable<>) ?
                   dest.GetGenericArguments()[0] :
                   dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetDataMemberName
        ///
        /// <summary>
        /// DataMember 属性の名前を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static string GetDataMemberName(PropertyInfo info)
        {
            if (!Attribute.IsDefined(info, typeof(DataMemberAttribute))) return null;

            var obj = info.GetCustomAttributes(typeof(DataMemberAttribute), false);
            if (obj == null || obj.Length == 0) return info.Name;

            var attr = obj[0] as DataMemberAttribute;
            return attr?.Name ?? info.Name;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Convert
        ///
        /// <summary>
        /// DateTime オブジェクトをレジストリに保存する形式に変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static object Convert(DateTime e) => e.ToUniversalTime().ToString("o");

        /* ----------------------------------------------------------------- */
        ///
        /// Log
        ///
        /// <summary>
        /// 例外発生時にエラー内容をログに出力します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void Log(Action action)
        {
            try { action(); }
            catch (Exception err) { LogOperator.Warn(typeof(RegistrySettings), err.ToString()); }
        }

        #endregion
    }
}
