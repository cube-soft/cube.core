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
using System.Collections.ObjectModel;
using System.Linq;

namespace Cube.Collections.Mixin
{
    /* --------------------------------------------------------------------- */
    ///
    /// EnumerableExtension
    ///
    /// <summary>
    /// Provides functions for a sequence.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class EnumerableExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetOrDefault(T)
        ///
        /// <summary>
        /// Returns the specified object or empty IEnumerable(T) object.
        /// </summary>
        ///
        /// <param name="src">Source collection.</param>
        ///
        /// <returns>Self or empty collection.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<T> GetOrDefault<T>(this IEnumerable<T> src) =>
            src ?? Enumerable.Empty<T>();

        /* ----------------------------------------------------------------- */
        ///
        /// AddOrSet(T, U)
        ///
        /// <summary>
        /// Adds or sets the specified key-value pair.
        /// </summary>
        ///
        /// <param name="src">Dictionary collection.</param>
        /// <param name="key">Key element to be set.</param>
        /// <param name="value">Value element to be set.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void AddOrSet<T, U>(this IDictionary<T, U> src, T key, U value) =>
            src.AddOrSet(key, value, (x, y) => y);

        /* ----------------------------------------------------------------- */
        ///
        /// AddOrSet(T, U)
        ///
        /// <summary>
        /// Adds or sets the specified key-value pair.
        /// </summary>
        ///
        /// <param name="src">Dictionary collection.</param>
        /// <param name="key">Key element to be set.</param>
        /// <param name="value">Value element to be set.</param>
        /// <param name="selector">
        /// Function object to select which value is set.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public static void AddOrSet<T, U>(this IDictionary<T, U> src, T key, U value, Func<U, U, U> selector)
        {
            if (src.TryGetValue(key, out var current)) src[key] = selector(current, value);
            else src.Add(key, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Compact
        ///
        /// <summary>
        /// Removes null objects in the specified sequence.
        /// </summary>
        ///
        /// <param name="src">Source sequence.</param>
        ///
        /// <returns>Removed sequence.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<T> Compact<T>(this IEnumerable<T> src) => src.OfType<T>();

        /* ----------------------------------------------------------------- */
        ///
        /// Flatten
        ///
        /// <summary>
        /// 木構造を一次元配列に変換します。
        /// 変換時には幅優先探索アルゴリズムが用いられます。
        /// </summary>
        ///
        /// <param name="src">ツリー構造オブジェクト</param>
        /// <param name="children">
        /// 子要素を選択するための関数オブジェクト
        /// </param>
        ///
        /// <returns>変換後のオブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> src,
            Func<T, IEnumerable<T>> children) => src.Flatten((e, s) => children(e));

        /* ----------------------------------------------------------------- */
        ///
        /// Flatten
        ///
        /// <summary>
        /// 木構造を一次元配列に変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static IEnumerable<T> Flatten<T>(this IEnumerable<T> src,
            Func<T, IEnumerable<T>, IEnumerable<T>> func) => src.Concat(
                src.Where(e => func(e, src) != null)
                   .SelectMany(e => func(e, src).Flatten(func))
            );

        /* ----------------------------------------------------------------- */
        ///
        /// ToObservable(T)
        ///
        /// <summary>
        /// ObservableCollection に変換します。
        /// </summary>
        ///
        /// <param name="src">変換前のコレクション</param>
        ///
        /// <returns>ObservableCollection オブジェクト</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> src) =>
            new ObservableCollection<T>(src);

        #endregion
    }
}
