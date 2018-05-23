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
using Cube.Streams;
using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Cube.DataContract
{
    /* --------------------------------------------------------------------- */
    ///
    /// Format
    ///
    /// <summary>
    /// DataContract オブジェクトをシリアライズ可能なフォーマットを定義
    /// した列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum Format
    {
        /// <summary>レジストリ</summary>
        Registry,
        /// <summary>XML</summary>
        Xml,
        /// <summary>JSON</summary>
        Json,
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Formatter
    ///
    /// <summary>
    /// DataContract オブジェクトをシリアライズおよびデシリアライズする
    /// ためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Formatter
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Target
        ///
        /// <summary>
        /// レジストリを対象にシリアライズまたはデシリアライズする際の
        /// ターゲットとなるサブキーを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static RegistryKey Target { get; set; } = Registry.CurrentUser;

        #endregion

        #region Methods

        #region Serialize

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize
        ///
        /// <summary>
        /// 指定された保存場所にオブジェクトの内容をシリアライズします。
        /// </summary>
        ///
        /// <param name="format">シリアライズ・フォーマット</param>
        /// <param name="dest">保存場所を表す文字列</param>
        /// <param name="src">シリアライズ対象オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Serialize<T>(this Format format, string dest, T src)
        {
            if (format == Format.Registry)
            {
                using (var k = Target.CreateSubKey(dest)) k.Serialize(src);
            }
            else Serialize(dest, e => Serialize(format, e, src));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize
        ///
        /// <summary>
        /// 指定されたストリームにオブジェクトの内容をシリアライズします。
        /// </summary>
        ///
        /// <param name="format">シリアライズ・フォーマット</param>
        /// <param name="dest">保存先ストリーム</param>
        /// <param name="src">シリアライズ対象オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Serialize<T>(this Format format, Stream dest, T src)
        {
            switch (format)
            {
                case Format.Xml:
                    dest.SerializeXml(src);
                    break;
                case Format.Json:
                    dest.SerializeJson(src);
                    break;
                default: throw new ArgumentException($"{format}:cannot serialize to stream");
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize
        ///
        /// <summary>
        /// 指定されたレジストリにオブジェクトの内容をシリアライズします。
        /// </summary>
        ///
        /// <param name="dest">レジストリ・サブキー</param>
        /// <param name="src">シリアライズ対象オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Serialize<T>(this RegistryKey dest, T src) =>
            new RegistrySerializer().Invoke(dest, src);

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize
        ///
        /// <summary>
        /// 指定されたファイルにオブジェクトの内容をシリアライズします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void Serialize(string dest, Action<Stream> callback)
        {
            using (var ms = new MemoryStream())
            {
                callback(ms);
                using (var ds = File.Create(dest))
                {
                    ms.Position = 0;
                    ms.CopyTo(ds);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SerializeXml
        ///
        /// <summary>
        /// 指定されたストリームに XML 形式でオブジェクトの内容を
        /// シリアライズします。
        /// </summary>
        ///
        /// <param name="dest">ストリーム</param>
        /// <param name="src">シリアライズ対象オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        private static void SerializeXml<T>(this Stream dest, T src) =>
            new DataContractSerializer(typeof(T)).WriteObject(dest, src);

        /* ----------------------------------------------------------------- */
        ///
        /// SerializeJson
        ///
        /// <summary>
        /// 指定されたストリームに JSON 形式でオブジェクトの内容を
        /// シリアライズします。
        /// </summary>
        ///
        /// <param name="dest">ストリーム</param>
        /// <param name="src">シリアライズ対象オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        private static void SerializeJson<T>(this Stream dest, T src) =>
            new DataContractJsonSerializer(typeof(T)).WriteObject(dest, src);

        #endregion

        #region Deserialize

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize
        ///
        /// <summary>
        /// 指定された場所の内容をデシリアライズします。
        /// </summary>
        ///
        /// <param name="format">シリアライズ・フォーマット</param>
        /// <param name="src">読み込み場所を表す文字列</param>
        ///
        /// <returns>デシリアライズされたオブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Deserialize<T>(this Format format, string src)
        {
            if (format == Format.Registry)
            {
                using (var k = Target.OpenSubKey(src, false)) return k.Deserialize<T>();
            }
            else return Deserialize<T>(src, e => Deserialize<T>(format, e));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize
        ///
        /// <summary>
        /// 指定されたストリームの内容をデシリアライズします。
        /// </summary>
        ///
        /// <param name="format">シリアライズ・フォーマット</param>
        /// <param name="src">読み込みストリーム</param>
        ///
        /// <returns>デシリアライズされたオブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Deserialize<T>(this Format format, Stream src)
        {
            switch (format)
            {
                case Format.Xml:  return src.DeserializeXml<T>();
                case Format.Json: return src.DeserializeJson<T>();
                default: throw new ArgumentException($"{format}:cannot deserialize from stream");
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下の内容をデシリアライズします。
        /// </summary>
        ///
        /// <param name="src">レジストリ・サブキー</param>
        ///
        /// <returns>デシリアライズされたオブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Deserialize<T>(this RegistryKey src) =>
            new RegistryDeserializer().Invoke<T>(src);

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize
        ///
        /// <summary>
        /// 指定されたファイルの内容をデシリアライズします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static T Deserialize<T>(string src, Func<Stream, T> callback)
        {
            using (var ss = File.OpenRead(src)) return callback(ss);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DeserializeXml
        ///
        /// <summary>
        /// 指定されたレストリームの内容を XML 形式としてデシリアライズ
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static T DeserializeXml<T>(this Stream src) =>
            (T)new DataContractSerializer(typeof(T)).ReadObject(src);

        /* ----------------------------------------------------------------- */
        ///
        /// DeserializeJson
        ///
        /// <summary>
        /// 指定されたレストリームの内容を JSON 形式としてデシリアライズ
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static T DeserializeJson<T>(this Stream src) =>
            (T)new DataContractJsonSerializer(typeof(T)).ReadObject(src);

        #endregion

        #endregion
    }
}
