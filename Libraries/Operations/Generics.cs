/* ------------------------------------------------------------------------- */
///
/// Generics.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System.Runtime.Serialization.Formatters.Binary;

namespace Cube.Generics
{
    /* --------------------------------------------------------------------- */
    ///
    /// Generics.Operations
    /// 
    /// <summary>
    /// クラスに対する汎用的な操作を定義するための拡張メソッド用クラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public static class Operations
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Assign
        ///
        /// <summary>
        /// public なプロパティおよびフィールドの値を代入します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void Assign<T>(this T dest, T src)
        {
            var t = src.GetType();

            foreach (var p in t.GetProperties())
            {
                if (p.GetGetMethod() == null || p.GetSetMethod() == null) continue;
                p.SetValue(dest, p.GetValue(src, null));
            }

            foreach (var f in t.GetFields()) f.SetValue(dest, f.GetValue(src));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Copy
        ///
        /// <summary>
        /// public なプロパティおよびフィールドの値をコピーした
        /// オブジェクトを生成します。
        /// </summary>
        /// 
        /// <remarks>
        /// Serializable 属性を持つクラスの場合、BinaryFormatter を
        /// 利用して値をコピーします。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static T Copy<T>(this T src) where T : new()
        {
            if (typeof(T).IsSerializable) return CopyWithBinaryFormatter(src);

            var dest = new T();
            Assign(dest, src);
            return dest;
        }

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// CopyWithBinaryFormatter
        ///
        /// <summary>
        /// BinaryFormatter を用いてオブジェクトのコピーを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static T CopyWithBinaryFormatter<T>(T src)
        {
            object dest = null;
            using (var stream = new System.IO.MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, src);
                stream.Position = 0;
                dest = formatter.Deserialize(stream);
            }
            return (T)dest;
        }

        #endregion
    }
}
