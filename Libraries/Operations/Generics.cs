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
using IoEx = System.IO;

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
            var type = src.GetType();

            foreach (var property in type.GetProperties())
            {
                if (property.GetGetMethod() == null ||
                    property.GetSetMethod() == null) continue;
                var value = property.GetValue(src, null);
                property.SetValue(dest, value, null);
            }

            foreach (var field in type.GetFields())
            {
                var value = field.GetValue(src);
                field.SetValue(dest, value);
            }
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
        private static T CopyWithBinaryFormatter<T>(this T src)
        {
            object dest = null;
            using (var stream = new IoEx.MemoryStream())
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
