﻿/* ------------------------------------------------------------------------- */
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
using System.Collections.Generic;
using System.Xml.Linq;

namespace Cube.Xml
{
    /* --------------------------------------------------------------------- */
    ///
    /// XmlOperator
    ///
    /// <summary>
    /// XElement クラスの拡張メソッド用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class XmlOperator
    {
        #region GetElement

        /* ----------------------------------------------------------------- */
        ///
        /// GetElement
        ///
        /// <summary>
        /// XElement オブジェクトを取得します。
        /// 取得の際には既定の名前空間を設定します。
        /// </summary>
        ///
        /// <param name="e">XML オブジェクト</param>
        /// <param name="name">要素名</param>
        ///
        /// <returns>XElement オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static XElement GetElement(this XElement e, string name) =>
            GetElement(e, string.Empty, name);

        /* ----------------------------------------------------------------- */
        ///
        /// GetElement
        ///
        /// <summary>
        /// XElement オブジェクトを取得します。
        /// 名前空間が空文字の場合、既定の名前空間を使用します。
        /// </summary>
        ///
        /// <param name="e">XML オブジェクト</param>
        /// <param name="ns">名前空間</param>
        /// <param name="name">要素名</param>
        ///
        /// <returns>XElement オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static XElement GetElement(this XElement e, string ns, string name) =>
            !string.IsNullOrEmpty(ns) ?
            e.Element(e.GetNamespaceOfPrefix(ns) + name) :
            e.Element(e.GetDefaultNamespace() + name);

        #endregion

        #region GetElements

        /* ----------------------------------------------------------------- */
        ///
        /// GetElements
        ///
        /// <summary>
        /// XElement オブジェクト一覧を取得します。
        /// 取得の際には既定の名前空間を設定します。
        /// </summary>
        ///
        /// <param name="e">XML オブジェクト</param>
        /// <param name="name">要素名</param>
        ///
        /// <returns>XElements オブジェクトの配列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<XElement> GetElements(this XElement e, string name) =>
            GetElements(e, string.Empty, name);

        /* ----------------------------------------------------------------- */
        ///
        /// GetElements
        ///
        /// <summary>
        /// XElement オブジェクト一覧を取得します。
        /// 名前空間が空文字の場合、既定の名前空間を使用します。
        /// </summary>
        ///
        /// <param name="e">XML オブジェクト</param>
        /// <param name="ns">名前空間</param>
        /// <param name="name">要素名</param>
        ///
        /// <returns>XElements オブジェクトの配列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<XElement> GetElements(this XElement e, string ns, string name) =>
            !string.IsNullOrEmpty(ns) ?
            e.Elements(e.GetNamespaceOfPrefix(ns) + name) :
            e.Elements(e.GetDefaultNamespace() + name);

        #endregion

        #region GetDecendants

        /* ----------------------------------------------------------------- */
        ///
        /// GetDecendants
        ///
        /// <summary>
        /// XElement オブジェクト一覧を取得します。
        /// 取得の際には既定の名前空間を設定します。
        /// </summary>
        ///
        /// <param name="e">XML オブジェクト</param>
        /// <param name="name">要素名</param>
        ///
        /// <returns>XElements オブジェクトの配列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<XElement> GetDecendants(this XElement e, string name) =>
            GetDecendants(e, string.Empty, name);

        /* ----------------------------------------------------------------- */
        ///
        /// GetDecendants
        ///
        /// <summary>
        /// XElement オブジェクト一覧を取得します。
        /// 名前空間が空文字の場合、既定の名前空間を使用します。
        /// </summary>
        ///
        /// <param name="e">XML オブジェクト</param>
        /// <param name="ns">名前空間</param>
        /// <param name="name">要素名</param>
        ///
        /// <returns>XElements オブジェクトの配列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<XElement> GetDecendants(this XElement e, string ns, string name) =>
            !string.IsNullOrEmpty(ns) ?
            e.Descendants(e.GetNamespaceOfPrefix(ns) + name) :
            e.Descendants(e.GetDefaultNamespace() + name);

        #endregion

        #region GetValueOrAttribute

        /* ----------------------------------------------------------------- */
        ///
        /// GetValueOrAttribute
        ///
        /// <summary>
        /// 値を取得します。Value が存在しない場合はヒントに指定された
        /// 名前に対応する属性値を取得します。
        /// </summary>
        ///
        /// <param name="e">XML オブジェクト</param>
        /// <param name="name">要素名</param>
        /// <param name="hint">ヒントとなる属性名</param>
        ///
        /// <returns>文字列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetValueOrAttribute(this XElement e,
            string name, string hint) =>
            e.GetValueOrAttribute(string.Empty, name, hint);

        /* ----------------------------------------------------------------- */
        ///
        /// GetValueOrAttribute
        ///
        /// <summary>
        /// 値を取得します。Value が存在しない場合はヒントに指定された
        /// 名前に対応する属性値を取得します。
        /// </summary>
        ///
        /// <param name="e">XML オブジェクト</param>
        /// <param name="ns">名前空間</param>
        /// <param name="name">要素名</param>
        /// <param name="hint">ヒントとなる属性名</param>
        ///
        /// <returns>文字列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetValueOrAttribute(this XElement e,
            string ns, string name, string hint) =>
            e.GetElement(ns, name).GetValueOrAttribute(hint);

        /* ----------------------------------------------------------------- */
        ///
        /// GetValueOrAttribute
        ///
        /// <summary>
        /// 値を取得します。Value が存在しない場合はヒントに指定された
        /// 名前に対応する属性値を取得します。
        /// </summary>
        ///
        /// <param name="e">XML オブジェクト</param>
        /// <param name="hint">ヒントとなる属性名</param>
        ///
        /// <returns>文字列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetValueOrAttribute(this XElement e, string hint)
        {
            if (e == null) return string.Empty;
            if (!string.IsNullOrEmpty(e.Value)) return e.Value;
            if (string.IsNullOrEmpty(hint)) return string.Empty;
            return (string)e.Attribute(hint) ?? string.Empty;
        }

        #endregion
    }
}
