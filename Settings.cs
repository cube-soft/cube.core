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
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.Win32;

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
        public static void Save<T>(T src, RegistryKey root) { }

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
                var items = type.GetProperties().Where(item =>
                {
                    return Attribute.IsDefined(item, typeof(DataMemberAttribute));
                });

                foreach (var item in items)
                {
                    var info = type.GetProperty(item.Name);
                    if (Type.GetTypeCode(info.PropertyType) != TypeCode.Object)
                    {
                        var value = root.GetValue(item.Name, null);
                        if (value == null) continue;

                        var changed = Convert.ChangeType(value, info.PropertyType);
                        info.SetValue(dest, changed, null);
                    }
                    else
                    {
                        using (var subkey = root.OpenSubKey(item.Name))
                        {
                            var obj = LoadRegistry(subkey, info.PropertyType);
                            info.SetValue(obj, obj, null);
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

        #endregion
    }
}
