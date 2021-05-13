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
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Cube.Mixin.Generics
{
    /* --------------------------------------------------------------------- */
    ///
    /// Extension
    ///
    /// <summary>
    /// Provides extended methods of generic classes.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Extension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// TryCast
        ///
        /// <summary>
        /// Tries to cast the specified object to the specified type.
        /// </summary>
        ///
        /// <typeparam name="T">Type to be cast.</typeparam>
        ///
        /// <param name="src">Source object.</param>
        ///
        /// <returns>Casted object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T TryCast<T>(this object src) => TryCast(src, default(T));

        /* ----------------------------------------------------------------- */
        ///
        /// TryCast
        ///
        /// <summary>
        /// Tries to cast the specified object to the specified type.
        /// </summary>
        ///
        /// <typeparam name="T">Type to be cast.</typeparam>
        ///
        /// <param name="src">Source object.</param>
        /// <param name="error">
        /// Returned object when the cast is failed.
        /// </param>
        ///
        /// <returns>Casted object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T TryCast<T>(this object src, T error) => src is T dest ? dest : error;

        /* ----------------------------------------------------------------- */
        ///
        /// Copy
        ///
        /// <summary>
        /// Creates a new instance of the type T class and copies values
        /// from public properties and fields of the specified object.
        /// </summary>
        ///
        /// <typeparam name="T">Type of object to be created.</typeparam>
        ///
        /// <param name="src">Object to be copied.</param>
        ///
        /// <returns>Created object.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static T Copy<T>(this T src) where T : new()
        {
            if (typeof(T).IsSerializable) return CopyWithBinaryFormatter(src);

            var dest = new T();
            Assign(dest, src);
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Set
        ///
        /// <summary>
        /// Sets the specified value to the specified field if they are
        /// not equal.
        /// </summary>
        ///
        /// <typeparam name="T">
        /// Type of object to compare and set.
        /// </typeparam>
        ///
        /// <param name="src">Function to compare.</param>
        /// <param name="field">Reference to the target field.</param>
        /// <param name="value">Value being set.</param>
        ///
        /// <returns>True for done; false for cancel.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool Set<T>(this IEqualityComparer<T> src, ref T field, T value)
        {
            if (src.Equals(field, value)) return false;
            field = value;
            return true;
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Assign
        ///
        /// <summary>
        /// Copies the public properties and fields.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void Assign<T>(this T dest, T src)
        {
            var t = src.GetType();

            foreach (var p in t.GetProperties())
            {
                if (p.GetGetMethod() == null || p.GetSetMethod() == null) continue;
                p.SetValue(dest, p.GetValue(src, null), null);
            }

            foreach (var f in t.GetFields()) f.SetValue(dest, f.GetValue(src));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CopyWithBinaryFormatter
        ///
        /// <summary>
        /// Copies values of properties and fields from the specified object
        /// with the BinaryFormatter object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static T CopyWithBinaryFormatter<T>(T src)
        {
            using var ms = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, src);
            ms.Position = 0;
            return (T)formatter.Deserialize(ms);
        }

        #endregion
    }
}
