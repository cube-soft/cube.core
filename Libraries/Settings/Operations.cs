/* ------------------------------------------------------------------------- */
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
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.Win32;

namespace Cube.Settings
{
    /* --------------------------------------------------------------------- */
    ///
    /// Settings.Operations
    /// 
    /// <summary>
    /// ユーザ設定を扱うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Operations
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
        /* ----------------------------------------------------------------- */
        public static T Load<T>(this RegistryKey src)
            => (T)LoadRegistry(src, typeof(T));

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        /// 
        /// <summary>
        /// 指定されたファイルから値を読み込み、オブジェクトに設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static T Load<T>(this SettingsType type, string src)
        {
            switch (type)
            {
                case SettingsType.Registry:
                    using (var k = Registry.CurrentUser.OpenSubKey(src, false)) return k.Load<T>();
                case SettingsType.Xml:
                    return LoadXml<T>(src);
                case SettingsType.Json:
                    return LoadJson<T>(src);
                default:
                    return default(T);
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
        public static void Save<T>(this RegistryKey dest, T src)
            => SaveRegistry(src, typeof(T), dest);

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        /// 
        /// <summary>
        /// 指定されたファイルに、オブジェクトの値を保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void Save<T>(this SettingsType type, string dest, T src)
        {
            switch (type)
            {
                case SettingsType.Registry:
                    using (var k = Registry.CurrentUser.CreateSubKey(dest)) k.Save(src);
                    break;
                case SettingsType.Xml:
                    SaveXml(src, dest);
                    break;
                case SettingsType.Json:
                    SaveJson(src, dest);
                    break;
                default:
                    throw new ArgumentException($"{type}:Unknown SettingsType");
            }
        }

        #endregion

        #region Implementations

        #region Load methods

        /* ----------------------------------------------------------------- */
        ///
        /// LoadRegistry
        /// 
        /// <summary>
        /// 指定されたレジストリ・サブキー下に存在する値を読み込み、
        /// オブジェクトに設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static object LoadRegistry(RegistryKey root, Type type)
        {
            var dest = Activator.CreateInstance(type);
            if (root == null) return dest;

            foreach (var info in type.GetProperties())
            {
                var name = GetDataMemberName(info);
                if (string.IsNullOrEmpty(name)) continue;

                if (Type.GetTypeCode(info.PropertyType) != TypeCode.Object)
                {
                    var value = Convert(root.GetValue(name, null), info.PropertyType);
                    if (value != null) info.SetValue(dest, value, null);
                }
                else using (var subkey = root.OpenSubKey(name))
                {
                    if (subkey == null) continue;
                    var value = LoadRegistry(subkey, info.PropertyType);
                    if (value != null) info.SetValue(dest, value, null);
                }
            }

            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadXml
        /// 
        /// <summary>
        /// XML 形式のストリームから値を読み込み、オブジェクトに設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static T LoadXml<T>(string src)
        {
            using (var reader = new System.IO.StreamReader(src))
            {
                var serializer = new DataContractSerializer(typeof(T));
                var dest = (T)serializer.ReadObject(reader.BaseStream);
                return dest;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadJson
        /// 
        /// <summary>
        /// JSON 形式のストリームから値を読み込み、オブジェクトに設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static T LoadJson<T>(string src)
        {
            using (var reader = new System.IO.StreamReader(src))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                var dest = (T)serializer.ReadObject(reader.BaseStream);
                return dest;
            }
        }

        #endregion

        #region Save methods

        /* ----------------------------------------------------------------- */
        ///
        /// SaveRegistry
        /// 
        /// <summary>
        /// 指定されたレジストリ・サブキー下に、オブジェクトの値を保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SaveRegistry(object src, Type type, RegistryKey root)
        {
            if (src == null || root == null) return;
            foreach (var info in type.GetProperties()) SaveRegistry(src, info, root);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveRegistry
        /// 
        /// <summary>
        /// 指定されたレジストリ・サブキー下に、オブジェクトの値を保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SaveRegistry(object src, PropertyInfo info, RegistryKey root)
        {
            try
            {
                var name  = GetDataMemberName(info);
                var value = info.GetValue(src, null);
                if (name == null || value == null) return;

                if (info.PropertyType.IsEnum) root.SetValue(name, (int)value);
                else switch (Type.GetTypeCode(info.PropertyType))
                {
                    case TypeCode.Boolean:
                        root.SetValue(name, ((bool)value) ? 1 : 0);
                        break;
                    case TypeCode.DateTime:
                        root.SetValue(name, (((DateTime)value).ToUniversalTime().ToString("o")));
                        break;
                    case TypeCode.Object:
                        using (var subkey = root.CreateSubKey(name))
                        {
                            SaveRegistry(value, info.PropertyType, subkey);
                        }
                        break;
                    default:
                        root.SetValue(name, value);
                        break;
                }
            }
            catch (Exception err) { Cube.Log.Operations.Warn(typeof(Operations), err.ToString()); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveXml
        /// 
        /// <summary>
        /// ストリームに XML 形式で保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SaveXml<T>(T src, string dest)
        {
            using (var writer = new System.IO.StreamWriter(dest))
            {
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(writer.BaseStream, src);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveJson
        /// 
        /// <summary>
        /// ストリームに JSON 形式で保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SaveJson<T>(T src, string dest)
        {
            using (var writer = new System.IO.StreamWriter(dest))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(writer.BaseStream, src);
            }
        }

        #endregion

        #region Others

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
        /// 指定した型で、指定したオブジェクトと同じ内容を表すを持つオブジェクトを
        /// 返します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static object Convert(object value, Type type)
        {
            try
            {
                if (value == null) return null;
                if (type.IsEnum) return (int)value;
                else switch(Type.GetTypeCode(type))
                {
                    case TypeCode.DateTime:
                        return DateTime.Parse(value as string).ToLocalTime();
                    default:
                        return System.Convert.ChangeType(value, type);
                }
            }
            catch (Exception err) { Cube.Log.Operations.Warn(typeof(Operations), err.ToString()); }

            return null;
        }

        #endregion

        #endregion
    }
}
