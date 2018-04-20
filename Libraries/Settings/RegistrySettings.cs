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
        /// Load
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下に存在する値を読み込み、
        /// オブジェクトに設定します。
        /// </summary>
        ///
        /// <param name="src">レジストリ・サブキー</param>
        ///
        /// <returns>生成オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Load<T>(RegistryKey src) => (T)Load(src, typeof(T));

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// HKEY_CURRENT_USER 下の指定されたサブキーに存在する値を
        /// 読み込み、オブジェクトに設定します。
        /// </summary>
        ///
        /// <param name="src">レジストリ・サブキー名</param>
        ///
        /// <returns>生成オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Load<T>(string src)
        {
            using (var key = Registry.CurrentUser.OpenSubKey(src, false))
            {
                return Load<T>(key);
            }
        }

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
        /// Load
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下に存在する値を読み込み、
        /// オブジェクトに設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static object Load(RegistryKey root, Type type)
        {
            var dest = Activator.CreateInstance(type);
            if (root == null) return dest;
            foreach (var info in type.GetProperties()) Log(() => Load(dest, info, root));
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下に存在する値を読み込み、
        /// オブジェクトに設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void Load(object dest, PropertyInfo info, RegistryKey root)
        {
            var name = GetDataMemberName(info);
            if (string.IsNullOrEmpty(name)) return;

            var type = GetType(info);
            if (Type.GetTypeCode(type) == TypeCode.Object)
            {
                using (var subkey = root.OpenSubKey(name))
                {
                    if (subkey == null) return;
                    var value = Load(subkey, type);
                    if (value != null) info.SetValue(dest, value, null);
                }
            }
            else
            {
                var value = Convert(root.GetValue(name, null), type);
                if (value != null) info.SetValue(dest, value, null);
            }
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
        public static string GetDataMemberName(PropertyInfo info)
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
        /// 指定した型で、指定したオブジェクトと同じ内容を表すを持つ
        /// オブジェクトを返します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static object Convert(object value, Type type) =>
            value == null            ? null :
            type.IsEnum              ? (int)value :
            type == typeof(DateTime) ? DateTime.Parse(value as string).ToLocalTime() :
            System.Convert.ChangeType(value, type);

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
