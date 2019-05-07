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
using Cube.Net35;
using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace Cube.DataContract
{
    /* --------------------------------------------------------------------- */
    ///
    /// Format
    ///
    /// <summary>
    /// Specifies formats that can be serialized and deserialized by the
    /// DataContract module.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum Format
    {
        /// <summary>Registry</summary>
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
    /// Provides functionality to serialize and deserialize the DataContract
    /// objects.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Formatter
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// DefaultKey
        ///
        /// <summary>
        /// Gets or sets the default registry subkey when serializing or
        /// deserializing the registry.
        /// </summary>
        ///
        /// <remarks>
        /// シリアライズまたはデシリアライズ時に明示的にサブキーを指定しな
        /// かった場合、このサブキーが利用されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static RegistryKey DefaultKey
        {
            get => _defaultKey ?? (_defaultKey = Registry.CurrentUser.OpenSubKey("Software", true));
            set => _defaultKey = value;
        }

        #endregion

        #region Methods

        #region Serialize

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize
        ///
        /// <summary>
        /// Serializes objects to the specified location.
        /// </summary>
        ///
        /// <param name="format">Serialization format.</param>
        /// <param name="dest">Saving location.</param>
        /// <param name="src">Object to be serialized.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Serialize<T>(this Format format, string dest, T src)
        {
            if (format == Format.Registry)
            {
                using (var k = DefaultKey.CreateSubKey(dest)) Serialize(k, src);
            }
            else Serialize(dest, e => Serialize(format, e, src));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize
        ///
        /// <summary>
        /// Serializes objects to the specified stream.
        /// </summary>
        ///
        /// <param name="format">Serialization format.</param>
        /// <param name="dest">Saving stream.</param>
        /// <param name="src">Object to be serialized.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Serialize<T>(this Format format, Stream dest, T src)
        {
            switch (format)
            {
                case Format.Xml:
                    SerializeXml(dest, src);
                    break;
                case Format.Json:
                    SerializeJson(dest, src);
                    break;
                default: throw new ArgumentException($"{format}:cannot serialize to stream");
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize
        ///
        /// <summary>
        /// Serializes objects to the specified registry subkey.
        /// </summary>
        ///
        /// <param name="dest">Registry subkey</param>
        /// <param name="src">Object to be serialized.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Serialize<T>(this RegistryKey dest, T src) =>
            new RegistrySerializer().Invoke(dest, src);

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize
        ///
        /// <summary>
        /// Serializes objects to the specified file.
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
        /// Serializes objects to the specified stream as XML format.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SerializeXml<T>(Stream dest, T src)
        {
            var settings = new XmlWriterSettings { Indent = true };
            using (var obj = XmlWriter.Create(dest, settings))
            {
                new DataContractSerializer(typeof(T)).WriteObject(obj, src);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SerializeJson
        ///
        /// <summary>
        /// Serializes objects to the specified stream as JSON format.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void SerializeJson<T>(Stream dest, T src)
        {
            using (var obj = JsonReaderWriterFactory.CreateJsonWriter(dest, Encoding.UTF8, false))
            {
                new DataContractJsonSerializer(typeof(T)).WriteObject(obj, src);
            }
        }

        #endregion

        #region Deserialize

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize
        ///
        /// <summary>
        /// Deserializes contents of the specified location.
        /// </summary>
        ///
        /// <param name="format">Serialization format.</param>
        /// <param name="src">Location to be loaded.</param>
        ///
        /// <returns>Deserialized object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Deserialize<T>(this Format format, string src)
        {
            if (format == Format.Registry)
            {
                using (var k = DefaultKey.OpenSubKey(src, false)) return Deserialize<T>(k);
            }
            else return Deserialize(src, e => Deserialize<T>(format, e));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize
        ///
        /// <summary>
        /// Deserializes contents of the specified stream.
        /// </summary>
        ///
        /// <param name="format">Serialization format.</param>
        /// <param name="src">Stream to be loaded.</param>
        ///
        /// <returns>Deserialized object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Deserialize<T>(this Format format, Stream src)
        {
            switch (format)
            {
                case Format.Xml:  return DeserializeXml<T>(src);
                case Format.Json: return DeserializeJson<T>(src);
                default: throw new ArgumentException($"{format}:cannot deserialize from stream");
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize
        ///
        /// <summary>
        /// Deserializes contents of the specified subkey.
        /// </summary>
        ///
        /// <param name="src">Registry subkey to be loaded.</param>
        ///
        /// <returns>Deserialized object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Deserialize<T>(this RegistryKey src) =>
            new RegistryDeserializer().Invoke<T>(src);

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize
        ///
        /// <summary>
        /// Deserializes contents of the specified file.
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
        /// Deserializes contents of the specified stream as XML format.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static T DeserializeXml<T>(Stream src) =>
            (T)new DataContractSerializer(typeof(T)).ReadObject(src);

        /* ----------------------------------------------------------------- */
        ///
        /// DeserializeJson
        ///
        /// <summary>
        /// Deserializes contents of the specified stream as JSON format.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static T DeserializeJson<T>(Stream src) =>
            (T)new DataContractJsonSerializer(typeof(T)).ReadObject(src);

        #endregion

        #endregion

        #region Fields
        private static RegistryKey _defaultKey;
        #endregion
    }
}
