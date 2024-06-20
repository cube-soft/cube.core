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

using System;
using System.Collections;
using Cube.DataContract.Internal;
using Cube.Text.Extensions;
using Microsoft.Win32;

/* ------------------------------------------------------------------------- */
///
/// RegistrySerializer
///
/// <summary>
/// Provides functionality to serialize to the registry.
/// </summary>
///
/* ------------------------------------------------------------------------- */
internal class RegistrySerializer
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the serialization to the specified registry key.
    /// </summary>
    ///
    /// <param name="dest">Root registry key.</param>
    /// <param name="src">Object to be serialized.</param>
    ///
    /* --------------------------------------------------------------------- */
    public void Invoke<T>(RegistryKey dest, T src) => Set(typeof(T), dest, src);

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Set
    ///
    /// <summary>
    /// Sets the specified object to the specified registry key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void Set(Type type, RegistryKey dest, object src)
    {
        if (dest is null || src is null) return;
        foreach (var pi in type.GetProperties())
        {
            var key = pi.GetDataMemberName();
            if (!key.HasValue()) continue;

            var value = pi.GetValue(src, null);
            if (value is null) continue;

            var pt = pi.GetPropertyType();
            if (pt.IsEnum) dest.SetValue(key, (int)value);
            else if (pt.IsGenericList()) Create(dest, key, e => SetList(pt, e, (IList)value));
            else if (pt.IsArray) Create(dest, key, e => SetArray(pt, e, (Array)value));
            else if (pt.IsObject()) Create(dest, key, e => Set(pt, e, value));
            else Set(pt, dest, key, value);
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Set
    ///
    /// <summary>
    /// Sets the specified object to the specified registry key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void Set(Type type, RegistryKey dest, string key, object value)
    {
        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Boolean:
                dest.SetValue(key, ((bool)value) ? 1 : 0);
                break;
            case TypeCode.DateTime:
                dest.SetValue(key, ((DateTime)value).ToUniversalTime().ToString("o"));
                break;
            default:
                dest.SetValue(key, value);
                break;
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// SetArray
    ///
    /// <summary>
    /// Sets the specified array object to the specified registry key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void SetArray(Type type, RegistryKey dest, Array src)
    {
        if (src.Rank != 1) return;

        var t = type.GetElementType();
        var n = Digit(src.Length);

        foreach (var name in dest.GetSubKeyNames()) dest.DeleteSubKeyTree(name);
        for (var i = 0; i < src.Length; ++i)
        {
            var capture = i;
            Create(dest, i.ToString($"D{n}"), e => SetListElement(t, e, src.GetValue(capture)));
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// SetList
    ///
    /// <summary>
    /// Sets the specified list object to the specified registry key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void SetList(Type type, RegistryKey dest, IList src)
    {
        var ga = type.GetGenericArguments();
        if (ga.Length != 1) return;

        var n = Digit(src.Count);

        foreach (var name in dest.GetSubKeyNames()) dest.DeleteSubKeyTree(name);
        for (var i = 0; i < src.Count; ++i)
        {
            var capture = i;
            Create(dest, i.ToString($"D{n}"), e => SetListElement(ga[0], e, src[capture]));
        }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// SetListElement
    ///
    /// <summary>
    /// Sets the specified list object to the specified registry key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void SetListElement(Type type, RegistryKey dest, object src)
    {
        if (type.IsObject()) Set(type, dest, src);
        else Set(type, dest, "", src);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Create
    ///
    /// <summary>
    /// Creates a new registry key with the specified registry key
    /// and name, and invokes the specified action.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static void Create(RegistryKey dest, string name, Action<RegistryKey> callback)
    {
        using var e = dest.CreateSubKey(name);
        if (e is not null) callback(e);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Digit
    ///
    /// <summary>
    /// Gets the digit number of the specified value.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static int Digit(int n) => n.ToString().Length;

    #endregion
}
