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
using Microsoft.Win32;
using System;
using System.Collections;

namespace Cube.DataContract
{
    /* --------------------------------------------------------------------- */
    ///
    /// RegistrySserializer
    ///
    /// <summary>
    /// レジストリに Serialize するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class RegistrySerializer
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// レジストリ・サブキー下に内容を保存します。
        /// </summary>
        ///
        /// <param name="dest">レジストリ・サブキー</param>
        /// <param name="src">保存するオブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Invoke<T>(RegistryKey dest, T src) => Set(typeof(T), dest, src);

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Set
        ///
        /// <summary>
        /// レジストリ・サブキー下に内容を保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Set(Type type, RegistryKey dest, object src)
        {
            if (dest == null || src == null) return;
            foreach (var pi in type.GetProperties())
            {
                var key = pi.GetDataMemberName();
                if (string.IsNullOrEmpty(key)) continue;

                var value = pi.GetValue(src, null);
                if (value == null) continue;

                var pt = pi.GetPropertyType();
                if (pt.IsEnum) dest.SetValue(key, (int)value);
                else if (pt.IsGenericList()) Create(dest, key, e => SetList(pt, e, (IList)value));
                else if (pt.IsArray) Create(dest, key, e => SetArray(pt, e, (Array)value));
                else if (pt.IsObject()) Create(dest, key, e => Set(pt, e, value));
                else Set(pt, dest, key, value);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Set
        ///
        /// <summary>
        /// レジストリ・サブキー下に内容を保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Set(Type type, RegistryKey dest, string key, object value)
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

        /* ----------------------------------------------------------------- */
        ///
        /// SetArray
        ///
        /// <summary>
        /// レジストリ・サブキー下に配列の内容を保存します。
        /// </summary>
        ///
        /// <remarks>
        /// 1 次元配列のみに対応しています。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void SetArray(Type type, RegistryKey dest, Array src)
        {
            if (src.Rank != 1) return;

            var t = type.GetElementType();
            var n = Digit(src.Length);

            foreach (var name in dest.GetSubKeyNames()) dest.DeleteSubKeyTree(name);
            for (var i = 0; i < src.Length; ++i)
            {
                Create(dest, i.ToString($"D{n}"), e => SetListElement(t, e, src.GetValue(i)));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetList
        ///
        /// <summary>
        /// レジストリ・サブキー下にリストの内容を保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SetList(Type type, RegistryKey dest, IList src)
        {
            var ga = type.GetGenericArguments();
            if (ga == null || ga.Length != 1) return;

            var n = Digit(src.Count);

            foreach (var name in dest.GetSubKeyNames()) dest.DeleteSubKeyTree(name);
            for (var i = 0; i < src.Count; ++i)
            {
                Create(dest, i.ToString($"D{n}"), e => SetListElement(ga[0], e, src[i]));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SetListElement
        ///
        /// <summary>
        /// レジストリ・サブキー下にリスト要素の内容を保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SetListElement(Type type, RegistryKey dest, object src)
        {
            if (type.IsObject()) Set(type, dest, src);
            else Set(type, dest, "", src);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// レジストリ・サブキーを生成して処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Create(RegistryKey dest, string name, Action<RegistryKey> callback)
        {
            using (var e = dest.CreateSubKey(name))
            {
                if (e != null) callback(e);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Digit
        ///
        /// <summary>
        /// 整数値の桁数を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private int Digit(int n) => n.ToString().Length;

        #endregion
    }
}
