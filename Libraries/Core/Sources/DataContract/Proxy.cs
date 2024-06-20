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
namespace Cube.DataContract;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Xml;
using Cube.FileSystem;
using Microsoft.Win32;

/* ------------------------------------------------------------------------- */
///
/// Proxy
///
/// <summary>
/// Provides extended methods to serialize and deserialize the DataContract
/// objects.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Proxy
{
    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// RootRegistryKey
    ///
    /// <summary>
    /// Gets or sets the root registry subkey when serializing or
    /// deserializing the registry.
    /// </summary>
    ///
    /// <remarks>
    /// If you do not explicitly specify a subkey when serializing or
    /// deserializing, this subkey will be used.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public static RegistryKey RootRegistryKey
    {
        get => _root ??= Registry.CurrentUser.OpenSubKey("Software", true);
        set => Interlocked.Exchange(ref _root, value)?.Dispose();
    }

    #endregion

    #region Serialize

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static void Serialize<T>(this Format format, string dest, T src)
    {
        switch (format)
        {
            case Format.Xml:
                IoEx.Save(dest, e => SerializeXml(e, src));
                break;
            case Format.Json:
                IoEx.Save(dest, e => SerializeJson(e, src));
                break;
            case Format.Registry:
                using (var e = RootRegistryKey.CreateSubKey(dest)) Serialize(e, src);
                break;
        }
    }

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static void Serialize<T>(this RegistryKey dest, T src) =>
        new RegistrySerializer().Invoke(dest, src);

    /* --------------------------------------------------------------------- */
    ///
    /// SerializeXml
    ///
    /// <summary>
    /// Serializes objects to the specified stream as XML format.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void SerializeXml<T>(Stream dest, T src)
    {
        var settings = new XmlWriterSettings { Indent = true };
        using var obj = XmlWriter.Create(dest, settings);
        new DataContractSerializer(typeof(T)).WriteObject(obj, src);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// SerializeJson
    ///
    /// <summary>
    /// Serializes objects to the specified stream as JSON format.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void SerializeJson<T>(Stream dest, T src)
    {
        using var obj = JsonReaderWriterFactory.CreateJsonWriter(dest, Encoding.UTF8, false, true);
        new DataContractJsonSerializer(typeof(T)).WriteObject(obj, src);
    }

    #endregion

    #region Deserialize

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static T Deserialize<T>(this Format format, string src)
    {
        switch (format)
        {
            case Format.Xml:
                return IoEx.Load(src, e => (T)new DataContractSerializer(typeof(T)).ReadObject(e));
            case Format.Json:
                return IoEx.Load(src, e => (T)new DataContractJsonSerializer(typeof(T)).ReadObject(e));
            case Format.Registry:
                using (var e = RootRegistryKey.OpenSubKey(src, false)) return Deserialize<T>(e);
        }
        return default;
    }

    /* --------------------------------------------------------------------- */
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
    /* --------------------------------------------------------------------- */
    public static T Deserialize<T>(this RegistryKey src) => new RegistryDeserializer().Invoke<T>(src);

    #endregion

    #region Others

    /* --------------------------------------------------------------------- */
    ///
    /// Exists
    ///
    /// <summary>
    /// Gets a value indicating whether the specified location exists.
    /// </summary>
    ///
    /// <param name="format">Serialization format.</param>
    /// <param name="src">Location of serialized data.</param>
    ///
    /// <returns>true for exists.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static bool Exists(this Format format, string src)
    {
        switch (format)
        {
            case Format.Registry:
                using (var e = RootRegistryKey.OpenSubKey(src, false)) return e is not null;
            default:
                return Io.Exists(src);
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Delete
    ///
    /// <summary>
    /// Deletes the specified location where serialized data is stored.
    /// </summary>
    ///
    /// <param name="format">Serialization format.</param>
    /// <param name="src">Location of serialized data.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Delete(this Format format, string src)
    {
        switch (format)
        {
            case Format.Registry:
                RootRegistryKey.DeleteSubKeyTree(src, false);
                break;
            default:
                if (Io.Exists(src)) Io.Delete(src);
                break;
        }
    }

    #endregion

    #region Fields
    private static RegistryKey _root;
    #endregion
}
