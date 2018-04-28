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
        /// 指定されたレジストリ・サブキー下に内容を保存します。
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
        /// 指定されたレジストリ・サブキー下に内容を保存します。
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
                else if (pt.IsGenericList()) using (var sk = dest.CreateSubKey(key)) SetList(pt, sk, (IList)value);
                else if (pt.IsObject()) using (var sk = dest.CreateSubKey(key)) Set(pt, sk, value);
                else Set(pt, dest, key, value);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Set
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下に内容を保存します。
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
        /// SetList
        ///
        /// <summary>
        /// 指定されたレジストリ・サブキー下にリストの内容を保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void SetList(Type type, RegistryKey dest, IList src)
        {
            var t = type.GetGenericArguments()[0];
            var n = (src.Count == 0) ? 1 : ((int)Math.Log10(src.Count) + 1);

            for (var i = 0; i < src.Count; ++i)
            {
                using (var sk = dest.CreateSubKey(i.ToString($"D{n}"))) Set(t, sk, src[i]);
            }
        }

        #endregion
    }
}
