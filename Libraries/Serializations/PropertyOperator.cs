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
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Cube.Serializations
{
    /* --------------------------------------------------------------------- */
    ///
    /// PropertyOperator
    ///
    /// <summary>
    /// プロパティの変換に関わる拡張用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class PropertyOperator
    {
        #region Type

        /* ----------------------------------------------------------------- */
        ///
        /// Parse
        ///
        /// <summary>
        /// オブジェクトを指定した型に変換します。
        /// </summary>
        ///
        /// <param name="src">変換後の型</param>
        /// <param name="value">変換元オブジェクト</param>
        ///
        /// <returns>変換後オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static object Parse(this Type src, object value) =>
            value == null ? null :
            src.IsEnum ? (int)value :
            src == typeof(DateTime) ? DateTime.Parse(value as string).ToLocalTime() :
            Convert.ChangeType(value, src);

        /* ----------------------------------------------------------------- */
        ///
        /// IsObject
        ///
        /// <summary>
        /// 一般的なクラスを表す型かどうかを判別します。
        /// </summary>
        ///
        /// <param name="src">対象となる Type オブジェクト</param>
        ///
        /// <returns>一般的なクラスかどうか</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool IsObject(this Type src) =>
            Type.GetTypeCode(src) == TypeCode.Object;

        /* ----------------------------------------------------------------- */
        ///
        /// IsGenericList
        ///
        /// <summary>
        /// List(T) またはそのインターフェースかどうかを判別します。
        /// </summary>
        ///
        /// <param name="src">対象となる Type オブジェクト</param>
        ///
        /// <returns>List(T) またはそのインターフェースかどうか</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool IsGenericList(this Type src)
        {
            if (!src.IsGenericType) return false;
            var gtd = src.GetGenericTypeDefinition();
            return gtd == typeof(List<>)               ||
                   gtd == typeof(IList<>)              ||
                   gtd == typeof(ICollection<>)        ||
                   gtd == typeof(IEnumerable<>)        ||
                   gtd == typeof(IReadOnlyList<>)      ||
                   gtd == typeof(IReadOnlyCollection<>);
        }

        #endregion

        #region PropertyInfo

        /* ----------------------------------------------------------------- */
        ///
        /// GetPropertyType
        ///
        /// <summary>
        /// オブジェクトの型を取得します。
        /// </summary>
        ///
        /// <param name="src">オブジェクト情報</param>
        ///
        /// <returns>Type オブジェクト</returns>
        ///
        /// <remarks>
        /// 指定されたオブジェクトの型が Nullable(T) の場合、T を表す
        /// Type オブジェクトを返します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static Type GetPropertyType(this PropertyInfo src)
        {
            var pt = src.PropertyType;
            return pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>) ?
                   pt.GetGenericArguments()[0] :
                   pt;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetDataMemberName
        ///
        /// <summary>
        /// DataMember 属性の名前を取得します。
        /// </summary>
        ///
        /// <param name="info">オブジェクト情報</param>
        ///
        /// <returns>DataMember 属性の名前</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetDataMemberName(this PropertyInfo info)
        {
            if (!Attribute.IsDefined(info, typeof(DataMemberAttribute))) return null;

            var obj = info.GetCustomAttributes(typeof(DataMemberAttribute), false);
            if (obj == null || obj.Length == 0) return info.Name;

            var attr = obj[0] as DataMemberAttribute;
            return attr?.Name ?? info.Name;
        }

        #endregion
    }
}
