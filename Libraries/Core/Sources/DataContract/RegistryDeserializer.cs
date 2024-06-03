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
using System.Collections.Generic;
using Cube.DataContract.Internal;
using Cube.Text.Extensions;
using Microsoft.Win32;

/* ------------------------------------------------------------------------- */
///
/// RegistryDeserializer
///
/// <summary>
/// Provides functionality to deserialize from the registry.
/// </summary>
///
/* ------------------------------------------------------------------------- */
internal class RegistryDeserializer
{
    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Invoke
    ///
    /// <summary>
    /// Invokes the deserialization from the specified registry key.
    /// </summary>
    ///
    /// <param name="src">Root registry key.</param>
    ///
    /// <returns>Deserialized object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public T Invoke<T>(RegistryKey src) => (T)Get(typeof(T), src);

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Gets the object corresponding to the specified type and
    /// registry key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static object Get(Type type, RegistryKey src)
    {
        var dest = Activator.CreateInstance(type);
        if (src is null) return dest;

        foreach (var pi in type.GetProperties())
        {
            var name = pi.GetDataMemberName();
            if (!name.HasValue()) continue;
            var obj = Get(pi.GetPropertyType(), src, name);
            if (obj is not null) pi.SetValue(dest, obj, null);
        }
        return dest;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Get
    ///
    /// <summary>
    /// Gets the object corresponding to the specified type, registry
    /// key, and name.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static object Get(Type type, RegistryKey src, string name)
    {
        if (type.IsGenericList()) return OpenGet(src, name, e => GetList(type, e));
        if (type.IsArray) return OpenGet(src, name, e => GetArray(type, e));
        if (type.IsObject()) return OpenGet(src, name, e => Get(type, e));
        return type.Parse(src.GetValue(name, null));
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetArray
    ///
    /// <summary>
    /// Gets the array object corresponding to the specified type and
    /// registry key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static Array GetArray(Type type, RegistryKey src)
    {
        if (type.GetArrayRank() != 1) return null;

        var elem = type.GetElementType();
        if (elem is null) return null;

        var obj  = GetListCore(elem, src);
        var dest = Array.CreateInstance(elem, obj.Count);
        obj.CopyTo(dest, 0);
        return dest;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetList
    ///
    /// <summary>
    /// Gets the list object corresponding to the specified type and
    /// registry key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static IList GetList(Type type, RegistryKey src)
    {
        var ga = type.GetGenericArguments();
        return ga.Length == 1 ? GetListCore(ga[0], src) : null;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetListCore
    ///
    /// <summary>
    /// Gets the list object corresponding to the specified type and
    /// registry key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static IList GetListCore(Type type, RegistryKey src)
    {
        var dest = Activator.CreateInstance(typeof(List<>)
                            .MakeGenericType(type)) as IList;
        if (dest is null) return null;

        foreach (var name in src.GetSubKeyNames())
        {
            var obj = OpenGet(src, name, e => GetListElement(type, e));
            if (obj is not null) _ = dest.Add(obj);
        }

        return dest;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetListElement
    ///
    /// <summary>
    /// Gets the list object corresponding to the specified type and
    /// registry key.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static object GetListElement(Type type, RegistryKey src) =>
        type.IsObject() ? Get(type, src) : type.Parse(src.GetValue("", null));

    /* --------------------------------------------------------------------- */
    ///
    /// OpenGet
    ///
    /// <summary>
    /// Opens the registry from the specified registry key and name,
    /// and invokes the specified action.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static object OpenGet(RegistryKey src, string name, Func<RegistryKey, object> action)
    {
        using var e = src.OpenSubKey(name, false);
        return e is not null ? action(e) : null;
    }

    #endregion
}
