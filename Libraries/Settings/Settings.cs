/* ------------------------------------------------------------------------- */
///
/// Settings.cs
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
using log4net;
using Cube.Extensions;
using IoEx = System.IO;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Settings
    /// 
    /// <summary>
    /// アプリケーション and/or ユーザ設定を扱うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Settings
    {
        /* ----------------------------------------------------------------- */
        ///
        /// FileType
        /// 
        /// <summary>
        /// Settings クラスで読み込み、および保存可能なファイル形式一覧を
        /// 表した列挙型です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public enum FileType : int
        {
            Xml,
            Json,
            Unknown = -1
        }

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
        public static T Load<T>(RegistryKey root)
        {
            return (T)LoadRegistry(root, typeof(T));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        /// 
        /// <summary>
        /// 指定されたファイルから値を読み込み、オブジェクトに設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static T Load<T>(string path, FileType type = FileType.Xml)
        {
            using (var reader = new IoEx.StreamReader(path))
            {
                switch (type)
                {
                    case FileType.Xml:
                        return LoadXml<T>(reader.BaseStream);
                    case FileType.Json:
                        return LoadJson<T>(reader.BaseStream);
                    case FileType.Unknown:
                        return default(T);
                    default:
                        return default(T);
                }
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
        public static void Save<T>(T src, RegistryKey root)
        {
            SaveRegistry(src, typeof(T), root);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        /// 
        /// <summary>
        /// 指定されたファイルに、オブジェクトの値を保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void Save<T>(T src, string path, FileType type = FileType.Xml)
        {
            using (var writer = new IoEx.StreamWriter(path))
            {
                switch (type)
                {
                    case FileType.Xml:
                        SaveXml(src, writer.BaseStream);
                        break;
                    case FileType.Json:
                        SaveJson(src, writer.BaseStream);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Implementations

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
            if (root == null || dest == null) return null;

            foreach (var item in type.GetProperties())
            {
                try
                {
                    if (!Attribute.IsDefined(item, typeof(DataMemberAttribute))) continue;

                    var name = GetDataMemberName(item);
                    if (string.IsNullOrEmpty(name)) continue;

                    if (Type.GetTypeCode(item.PropertyType) != TypeCode.Object)
                    {
                        var value = root.GetValue(name, null);
                        if (value == null) continue;

                        var changed = ChangeType(value, item.PropertyType);
                        if (changed != null) item.SetValue(dest, changed, null);
                    }
                    else using (var subkey = root.OpenSubKey(name))
                    {
                        var obj = LoadRegistry(subkey, item.PropertyType);
                        if (obj != null) item.SetValue(dest, obj, null);
                    }
                }
                catch (Exception err) { Log(err); }
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
        private static T LoadXml<T>(IoEx.Stream src)
        {
            var serializer = new DataContractSerializer(typeof(T));
            var dest = (T)serializer.ReadObject(src);
            return dest;
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
        private static T LoadJson<T>(IoEx.Stream src)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var dest = (T)serializer.ReadObject(src);
            return dest;
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
        public static void SaveRegistry(object src, Type type, RegistryKey root)
        {
            if (src == null || root == null) return;

            foreach (var item in type.GetProperties())
            {
                try
                {
                    if (!Attribute.IsDefined(item, typeof(DataMemberAttribute))) continue;

                    var name = GetDataMemberName(item);
                    if (name == null) continue;

                    var value = item.GetValue(src, null);
                    var code = Type.GetTypeCode(item.PropertyType);

                    if (item.PropertyType.IsEnum) root.SetValue(name, (int)value);
                    else if (code == TypeCode.Boolean) root.SetValue(name, ((bool)value) ? 1 : 0);
                    else if (code == TypeCode.DateTime) root.SetValue(name, ((int)((DateTime)value).ToUnixTime()));
                    else if (code != TypeCode.Object) root.SetValue(name, value);
                    else using (var subkey = root.CreateSubKey(name)) SaveRegistry(value, item.PropertyType, subkey);
                }
                catch (Exception err) { Log(err); }
            }
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
        private static void SaveXml<T>(T src, IoEx.Stream dest)
        {
            var serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(dest, src);
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
        private static void SaveJson<T>(T src, IoEx.Stream dest)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(dest, src);
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
            var objects = info.GetCustomAttributes(typeof(DataMemberAttribute), false);
            if (objects == null || objects.Length == 0) return info.Name;

            var attr = objects[0] as DataMemberAttribute;
            return attr?.Name ?? info.Name;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ChangeType
        /// 
        /// <summary>
        /// 指定した型で、指定したオブジェクトと等しい値を持つ object を返します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static object ChangeType(object value, Type type)
        {
            if (type.IsEnum) return (int)value;
            if (Type.GetTypeCode(type) == TypeCode.DateTime) return ((int)value).ToDateTime();
            return Convert.ChangeType(value, type);            
        }

        /* ------------------------------------------------------------- */
        ///
        /// Log
        /// 
        /// <summary>
        /// エラー内容を記録します。
        /// </summary>
        ///
        /* ------------------------------------------------------------- */
        private static void Log(Exception err)
            => LogManager.GetLogger(typeof(Settings)).Error(err);

        #endregion
    }
}
