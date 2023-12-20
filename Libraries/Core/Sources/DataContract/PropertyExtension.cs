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
namespace Cube.DataContract.Internal;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

/* ------------------------------------------------------------------------- */
///
/// PropertyExtension
///
/// <summary>
/// Provides extended methods of the Type and PropertyInfo classes.
/// </summary>
///
/* ------------------------------------------------------------------------- */
internal static class PropertyExtension
{
    #region Type

    /* --------------------------------------------------------------------- */
    ///
    /// Parse
    ///
    /// <summary>
    /// Converts the specified value according to the specified type.
    /// </summary>
    ///
    /// <param name="src">Type of the converted object.</param>
    /// <param name="value">Object to be converted.</param>
    ///
    /// <returns>Converted object.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static object Parse(this Type src, object value) =>
        value is null ? null :
        src.IsEnum ? value :
        src == typeof(DateTime) ? DateTime.Parse(value as string).ToLocalTime() :
        Convert.ChangeType(value, src);

    /* --------------------------------------------------------------------- */
    ///
    /// IsObject
    ///
    /// <summary>
    /// Determines whether the specified type is generic object type.
    /// </summary>
    ///
    /// <param name="src">Target type.</param>
    ///
    /// <returns>true for generic object type.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static bool IsObject(this Type src) => Type.GetTypeCode(src) == TypeCode.Object;

    /* --------------------------------------------------------------------- */
    ///
    /// IsGenericList
    ///
    /// <summary>
    /// Determines whether the specified type is List(T) or IList(T).
    /// </summary>
    ///
    /// <param name="src">Target type.</param>
    ///
    /// <returns>true for generic list.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static bool IsGenericList(this Type src)
    {
        if (!src.IsGenericType) return false;
        var gtd = src.GetGenericTypeDefinition();
        return gtd == typeof(List<>) || gtd == typeof(IList<>);
    }

    #endregion

    #region PropertyInfo

    /* --------------------------------------------------------------------- */
    ///
    /// GetPropertyType
    ///
    /// <summary>
    /// Gets the type of specified object.
    /// </summary>
    ///
    /// <param name="src">Property information.</param>
    ///
    /// <returns>Type object.</returns>
    ///
    /// <remarks>
    /// Returns the type of T if the specified object represents the
    /// Nullable(T) type.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public static Type GetPropertyType(this PropertyInfo src)
    {
        var pt = src.PropertyType;
        return pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>) ?
               pt.GetGenericArguments()[0] :
               pt;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetDataMemberName
    ///
    /// <summary>
    /// Gets the name of property that has the DataMember attribute.
    /// </summary>
    ///
    /// <param name="info">Property information.</param>
    ///
    /// <returns>Name of property.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static string GetDataMemberName(this PropertyInfo info)
    {
        if (!Attribute.IsDefined(info, typeof(DataMemberAttribute))) return null;

        var obj = info.GetCustomAttributes(typeof(DataMemberAttribute), false);
        if (obj.Length == 0) return info.Name;

        var attr = obj[0] as DataMemberAttribute;
        return attr?.Name ?? info.Name;
    }

    #endregion
}
