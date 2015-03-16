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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.Win32;
using System.Reflection;

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
            try
            {
                var dest = System.Activator.CreateInstance(type);
                var items = GetDataMembers(type);

                foreach (var item in items)
                {
                    var info = type.GetProperty(item.Name);
                    if (Type.GetTypeCode(info.PropertyType) != TypeCode.Object)
                    {
                        var attribute = (DataMemberAttribute)item.GetCustomAttributes(typeof(DataMemberAttribute), false)[0];
                        var name = string.IsNullOrEmpty(attribute.Name) ? item.Name : attribute.Name;

                        var value = root.GetValue(name, null);
                        if (value == null) continue;

                        var changed = ChangeType(value, info.PropertyType);
                        info.SetValue(dest, changed, null);
                    }
                    else
                    {
                        using (var subkey = root.OpenSubKey(item.Name))
                        {
                            var obj = LoadRegistry(subkey, info.PropertyType);
                            info.SetValue(dest, obj, null);
                        }
                    }
                }
                return dest;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.ToString());
                return null;
            }
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
            var items = GetDataMembers(type);
            foreach (var item in items)
            {
                var attribute = (DataMemberAttribute)item.GetCustomAttributes(typeof(DataMemberAttribute), false)[0];
                var name = string.IsNullOrEmpty(attribute.Name) ? item.Name : attribute.Name;
                var value = item.GetValue(src, null);
                var typecode = Type.GetTypeCode(item.PropertyType);

                if (typecode == TypeCode.DateTime) root.SetValue(name, ToUnixTime((DateTime)value));
                else if (typecode != TypeCode.Object) root.SetValue(name, value);
                else using (var subkey = root.CreateSubKey(name)) { SaveRegistry(value, item.PropertyType, subkey); }
            }
        }

        public static System.Collections.Generic.IEnumerable<PropertyInfo> GetDataMembers(Type type)
        {
            return type.GetProperties().Where(item =>
                {
                    return Attribute.IsDefined(item, typeof(DataMemberAttribute));
                });
        }

        #region Converting methods

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
            if (Type.GetTypeCode(type) == TypeCode.DateTime) return ToDateTime((int)value);
            return Convert.ChangeType(value, type);            
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToDateTime
        /// 
        /// <summary>
        /// UNIX 時刻から DateTime オブジェクトへ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static DateTime ToDateTime(int unix)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unix).ToLocalTime();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToUnixTime
        /// 
        /// <summary>
        /// DateTime オブジェクトから UNIX 時刻へ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static int ToUnixTime(DateTime time)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var utc = time.ToUniversalTime();
            return (int)utc.Subtract(epoch).TotalSeconds;
        }

        #endregion

        #endregion
    }
}
