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
using System.Collections.Generic;

namespace Cube.DataContract
{
    /* --------------------------------------------------------------------- */
    ///
    /// RegistryDeserializer
    ///
    /// <summary>
    /// レジストリから Deserialize するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class RegistryDeserializer
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下に存在する値を読み込みます。
        /// </summary>
        ///
        /// <param name="src">レジストリ・サブキー</param>
        ///
        /// <returns>生成オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public T Invoke<T>(RegistryKey src) => (T)Get(typeof(T), src);

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Get
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下に存在する内容を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private object Get(Type type, RegistryKey src)
        {
            var dest = Activator.CreateInstance(type);
            if (src == null) return dest;

            foreach (var pi in type.GetProperties())
            {
                var name = pi.GetDataMemberName();
                if (string.IsNullOrEmpty(name)) continue;
                var obj = Get(pi.GetPropertyType(), src, name);
                if (obj != null) pi.SetValue(dest, obj, null);
            }
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Get
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下の名前に対応する内容を取得
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private object Get(Type type, RegistryKey src, string name)
        {
            if (type.IsGenericList()) return OpenGet(src, name, e => GetList(type, e));
            else if (type.IsObject()) return OpenGet(src, name, e => Get(type, e));
            else return type.Parse(src.GetValue(name, null));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetList
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下に存在する内容を配列として
        /// 取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private IList GetList(Type type, RegistryKey src)
        {
            var t    = type.GetGenericArguments()[0];
            var dest = Activator.CreateInstance(typeof(List<>)
                                .MakeGenericType(t)) as IList;

            foreach (var name in src.GetSubKeyNames())
            {
                using (var sk = src.OpenSubKey(name, false))
                {
                    var obj = Get(t, sk);
                    if (obj != null) dest.Add(obj);
                }
            }

            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Open
        ///
        /// <summary>
        /// レジストリ・サブキーを開き、内容を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private object OpenGet(RegistryKey src, string name, Func<RegistryKey, object> action)
        {
            using (var e = src.OpenSubKey(name, false))
            {
                if (e != null) return action(e);
            }
            return null;
        }

        #endregion
    }
}
