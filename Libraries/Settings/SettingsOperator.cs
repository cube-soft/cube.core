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
using Cube.Serializations;
using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Cube.Settings
{
    /* --------------------------------------------------------------------- */
    ///
    /// SettingsOperator
    ///
    /// <summary>
    /// ユーザ設定を扱うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class SettingsOperator
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
        public static T Load<T>(this RegistryKey src) =>
            new RegistryDeserializer().Invoke<T>(src);

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
                case SettingsType.Xml:
                    return LoadXml<T>(src);
                case SettingsType.Json:
                    return LoadJson<T>(src);
                case SettingsType.Registry:
                    using (var sk = Registry.CurrentUser.OpenSubKey(src, false)) return sk.Load<T>();
                default:
                    throw Error(type, "wrong type");
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// 指定されたファイルから値を読み込み、オブジェクトに設定します。
        /// </summary>
        ///
        /// <param name="type">設定データのフォーマット</param>
        /// <param name="src">読み込み元ストリーム</param>
        ///
        /// <returns>設定オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Load<T>(this SettingsType type, Stream src)
        {
            switch (type)
            {
                case SettingsType.Xml:      return LoadXml<T>(src);
                case SettingsType.Json:     return LoadJson<T>(src);
                case SettingsType.Registry: throw Error(type, "cannot save to stream");
                default:                    throw Error(type, "wrong type");
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
        public static void Save<T>(this RegistryKey dest, T src) =>
            RegistrySettings.Save(dest, src);

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// 指定されたファイルに、オブジェクトの値を保存します。
        /// </summary>
        ///
        /// <param name="type">設定データのフォーマット</param>
        /// <param name="dest">保存ファイル</param>
        /// <param name="src">設定情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Save<T>(this SettingsType type, string dest, T src)
        {
            switch (type)
            {
                case SettingsType.Xml:
                    SaveXml(src, dest);
                    break;
                case SettingsType.Json:
                    SaveJson(src, dest);
                    break;
                case SettingsType.Registry:
                    RegistrySettings.Save(dest, src);
                    break;
                default:
                    throw Error(type, "wrong type");
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// 指定されたファイルに、オブジェクトの値を保存します。
        /// </summary>
        ///
        /// <param name="type">設定データのフォーマット</param>
        /// <param name="dest">保存先ストリーム</param>
        /// <param name="src">設定情報</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Save<T>(this SettingsType type, Stream dest, T src)
        {
            switch (type)
            {
                case SettingsType.Xml:
                    SaveXml(src, dest);
                    break;
                case SettingsType.Json:
                    SaveJson(src, dest);
                    break;
                case SettingsType.Registry:
                    throw Error(type, "cannot save to stream");
                default:
                    throw Error(type, "wrong type");
            }
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Error
        ///
        /// <summary>
        /// エラー用オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static Exception Error(SettingsType type, string message) =>
            new ArgumentException($"{type}:{message}");

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
            using (var ss = File.OpenRead(src)) return LoadXml<T>(ss);
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
        private static T LoadXml<T>(Stream src)
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
        private static T LoadJson<T>(string src)
        {
            using (var ss = File.OpenRead(src)) return LoadJson<T>(ss);
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
        private static T LoadJson<T>(Stream src)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var dest = (T)serializer.ReadObject(src);
            return dest;
        }

        #endregion

        #region Save

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// ファイルに保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void Save(string dest, Action<Stream> action)
        {
            using (var ms = new MemoryStream())
            {
                action(ms);
                using (var ds = File.Create(dest))
                {
                    ms.Position = 0;
                    ms.CopyTo(ds);
                }
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
        private static void SaveXml<T>(T src, string dest) =>
            Save(dest, e => SaveXml(src, e));

        /* ----------------------------------------------------------------- */
        ///
        /// SaveXml
        ///
        /// <summary>
        /// ストリームに XML 形式で保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SaveXml<T>(T src, Stream dest)
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
        private static void SaveJson<T>(T src, string dest) =>
            Save(dest, e => SaveJson(src, e));

        /* ----------------------------------------------------------------- */
        ///
        /// SaveJson
        ///
        /// <summary>
        /// ストリームに JSON 形式で保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SaveJson<T>(T src, Stream dest)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(dest, src);
        }

        #endregion

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// SettingsType
    ///
    /// <summary>
    /// Settings クラスで読み込み、および保存可能なデータ形式一覧を
    /// 表した列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum SettingsType
    {
        /// <summary>レジストリ</summary>
        Registry,
        /// <summary>XML</summary>
        Xml,
        /// <summary>JSON</summary>
        Json,
        /// <summary>不明</summary>
        Unknown = -1
    }
}
