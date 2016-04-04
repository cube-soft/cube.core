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
using Cube.Conversions;
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
            => SaveRegistry(src, typeof(T), root);

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
            if (root == null || dest == null) return null;

            foreach (var info in type.GetProperties()) LogException(() =>
            {
                if (!Attribute.IsDefined(info, typeof(DataMemberAttribute))) return;

                var name = GetDataMemberName(info);
                if (string.IsNullOrEmpty(name)) return;

                if (Type.GetTypeCode(info.PropertyType) != TypeCode.Object)
                {
                    var value = root.GetValue(name, null);
                    if (value == null) return;

                    var changed = ChangeType(value, info.PropertyType);
                    if (changed != null) info.SetValue(dest, changed, null);
                }
                else using (var subkey = root.OpenSubKey(name))
                {
                    var obj = LoadRegistry(subkey, info.PropertyType);
                    if (obj != null) info.SetValue(dest, obj, null);
                }
            });

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
            => LogException(() =>
        {
            if (!Attribute.IsDefined(info, typeof(DataMemberAttribute))) return;

            var name = GetDataMemberName(info);
            if (name == null) return;

            var value = info.GetValue(src, null);
            var code = Type.GetTypeCode(info.PropertyType);

            if (info.PropertyType.IsEnum) root.SetValue(name, (int)value);
            else if (code == TypeCode.Boolean) root.SetValue(name, ((bool)value) ? 1 : 0);
            else if (code == TypeCode.DateTime) root.SetValue(name, ((int)((DateTime)value).ToUnixTime()));
            else if (code != TypeCode.Object) root.SetValue(name, value);
            else using (var subkey = root.CreateSubKey(name)) SaveRegistry(value, info.PropertyType, subkey);
        });

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

        /* ----------------------------------------------------------------- */
        ///
        /// LogError
        /// 
        /// <summary>
        /// 例外情報をログに出力します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void LogError(Exception err)
            => Cube.Log.Operations.Error(typeof(Settings), err.Message, err);

        /* ----------------------------------------------------------------- */
        ///
        /// LogException
        /// 
        /// <summary>
        /// 実行時に送出された例外をログに出力します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void LogException(Action action)
        {
            try { action(); }
            catch (Exception err) { LogError(err); }
        }

        #endregion
    }
}
