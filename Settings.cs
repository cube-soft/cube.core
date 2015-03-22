/* ------------------------------------------------------------------------- */
///
/// Settings.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.Win32;
using Cube.Extensions;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Settings
    /// 
    /// <summary>
    /// アプリケーション and/or ユーザ設定を扱うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class Settings
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
            using (var reader = new System.IO.StreamReader(path))
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
        public static void Save<T>(T src, string path, FileType type = FileType.Xml) { }

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
            var dest = System.Activator.CreateInstance(type);
            if (root == null || dest == null) return null;

            foreach (var item in GetDataMembers(type))
            {
                try
                {
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
                catch (Exception err)
                {
                    System.Diagnostics.Trace.TraceError(err.ToString());
                    continue;
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
        private static T LoadXml<T>(System.IO.Stream src)
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
        private static T LoadJson<T>(System.IO.Stream src)
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
            try
            {
                foreach (var item in GetDataMembers(type))
                {
                    var name = GetDataMemberName(item);
                    var value = item.GetValue(src, null);
                    var code = Type.GetTypeCode(item.PropertyType);

                    if (code == TypeCode.DateTime) root.SetValue(name, ((DateTime)value).ToUnixTime());
                    else if (code != TypeCode.Object) root.SetValue(name, value);
                    else using (var subkey = root.CreateSubKey(name))
                    {
                        SaveRegistry(value, item.PropertyType, subkey);
                    }
                }
            }
            catch (Exception err) { System.Diagnostics.Trace.TraceError(err.ToString()); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetDataMembers
        /// 
        /// <summary>
        /// DataMember 属性のプロパティ一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<PropertyInfo> GetDataMembers(Type type)
        {
            return type.GetProperties().Where(item => {
                return Attribute.IsDefined(item, typeof(DataMemberAttribute));
            });
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
            return (attr == null || string.IsNullOrEmpty(attr.Name)) ? info.Name : attr.Name;
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
            if (Type.GetTypeCode(type) == TypeCode.DateTime) return ((int)value).ToDateTime();
            return Convert.ChangeType(value, type);            
        }

        #endregion
    }
}
