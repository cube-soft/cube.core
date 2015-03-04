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
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
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
        public static T Load<T>(RegistryKey root) { return default(T); }

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
        /// Load
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
