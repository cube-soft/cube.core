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
        public static T Load<T>(this RegistryKey src) => RegistrySettings.Load<T>(src);

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
                    return RegistrySettings.Load<T>(src);
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
            => RegistrySettings.Save(dest, src);

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
                    RegistrySettings.Save(dest, src);
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

        #region Load

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

        #region Save

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

        #endregion
    }
}
