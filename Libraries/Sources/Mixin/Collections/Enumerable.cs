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
using System.Linq;

namespace Cube.Mixin.Collections
{
    /* --------------------------------------------------------------------- */
    ///
    /// EnumerableExtension
    ///
    /// <summary>
    /// Provides extended methods of the IEnumerable(T) class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class EnumerableExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetOrEmpty(T)
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
        public static IEnumerable<T> GetOrEmpty<T>(this IEnumerable<T> src) =>
            src ?? Enumerable.Empty<T>();

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

        #region FirstIndex

        /* ----------------------------------------------------------------- */
        ///
        /// FirstIndex(T)
        ///
        /// <summary>
        /// Returns the first index of a sequence that satisfies a
        /// specified condition.
        /// </summary>
        ///
        /// <param name="src">Source sequence.</param>
        /// <param name="predicate">
        /// Function to test each element for a condition.
        /// </param>
        ///
        /// <returns>
        /// First index in the sequence that passes the test in the
        /// predicate function.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static int FirstIndex<T>(this IEnumerable<T> src, Func<T, bool> predicate) =>
            src.Select((Value, Index) => new { Value, Index })
               .First(e => predicate(e.Value))
               .Index;

        /* ----------------------------------------------------------------- */
        ///
        /// FirstIndexOf(T)
        ///
        /// <summary>
        /// Returns the first index of a sequence that satisfies a
        /// specified condition.
        /// </summary>
        ///
        /// <param name="src">Source sequence.</param>
        /// <param name="predicate">
        /// Function to test each element for a condition.
        /// </param>
        ///
        /// <returns>
        /// -1 if the sequence is empty or if no elements pass the test
        /// in the predicate function; otherwise, the first index that
        /// passes the test in the predicate function.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static int FirstIndexOf<T>(this IEnumerable<T> src, Func<T, bool> predicate) =>
            src?.Select((Value, Index) => new { Value, Index })
                .FirstOrDefault(e => predicate(e.Value))
               ?.Index ?? -1;

        #endregion

        #region LastIndex

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndex(T)
        ///
        /// <summary>
        /// Returns the last index of the specified sequence.
        /// </summary>
        ///
        /// <param name="src">Source sequence.</param>
        ///
        /// <returns>
        /// Last index in the sequence.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static int LastIndex<T>(this IEnumerable<T> src)
        {
            if (src is ICollection<T> s0 && s0.Count > 0) return s0.Count - 1;
            if (src is IReadOnlyCollection<T> s1 && s1.Count > 0) return s1.Count - 1;
            return src.LastIndex(e => true);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndex(T)
        ///
        /// <summary>
        /// Returns the last index of a sequence that satisfies a
        /// specified condition.
        /// </summary>
        ///
        /// <param name="src">Source sequence.</param>
        /// <param name="predicate">
        /// Function to test each element for a condition.
        /// </param>
        ///
        /// <returns>
        /// Last index in the sequence that passes the test in the
        /// predicate function.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static int LastIndex<T>(this IEnumerable<T> src, Func<T, bool> predicate) =>
            src.Select((Value, Index) => new { Value, Index })
               .Last(e => predicate(e.Value))
               .Index;

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndexOf(T)
        ///
        /// <summary>
        /// Returns the last index of the specified sequence.
        /// </summary>
        ///
        /// <param name="src">Source sequence.</param>
        ///
        /// <returns>
        /// -1 if the sequence is empty; otherwise, the last index in the
        /// sequence.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static int LastIndexOf<T>(this IEnumerable<T> src)
        {
            if (src is ICollection<T> s0) return s0.Count - 1;
            if (src is IReadOnlyCollection<T> s1) return s1.Count - 1;
            return src.LastIndexOf(e => true);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndexOf(T)
        ///
        /// <summary>
        /// Returns the last index of a sequence that satisfies a
        /// specified condition.
        /// </summary>
        ///
        /// <param name="src">Source sequence.</param>
        /// <param name="predicate">
        /// Function to test each element for a condition.
        /// </param>
        ///
        /// <returns>
        /// -1 if the sequence is empty or if no elements pass the test
        /// in the predicate function; otherwise, the Last index that
        /// passes the test in the predicate function.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static int LastIndexOf<T>(this IEnumerable<T> src, Func<T, bool> predicate) =>
            src?.Select((Value, Index) => new { Value, Index })
                .LastOrDefault(e => predicate(e.Value))
               ?.Index ?? -1;

        /* ----------------------------------------------------------------- */
        ///
        /// Clamp(T)
        ///
        /// <summary>
        /// Normalizes the specified index with the range of [0, Count).
        /// </summary>
        ///
        /// <param name="src">Source sequence.</param>
        /// <param name="index">Index value.</param>
        ///
        /// <returns>Normalized index.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static int Clamp<T>(this IEnumerable<T> src, int index) =>
            Math.Max(Math.Min(index, src.LastIndexOf()), 0);

        #endregion

        #region Flatten

        /* ----------------------------------------------------------------- */
        ///
        /// Flatten
        ///
        /// <summary>
        /// Convert a tree structure to a one-dimensional sequence with
        /// breadth first search..
        /// </summary>
        ///
        /// <param name="src">Source sequence.</param>
        /// <param name="children">Conversion function.</param>
        ///
        /// <returns>Converted sequence.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> src,
            Func<T, IEnumerable<T>> children) => src.Flatten((e, s) => children(e));

        /* ----------------------------------------------------------------- */
        ///
        /// Flatten
        ///
        /// <summary>
        /// Convert a tree structure to a one-dimensional sequence with
        /// breadth first search..
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static IEnumerable<T> Flatten<T>(this IEnumerable<T> src,
            Func<T, IEnumerable<T>, IEnumerable<T>> func) => src.Concat(
                src.Where(e => func(e, src) != null)
                   .SelectMany(e => func(e, src).Flatten(func))
            );

        #endregion

        #endregion
    }
}
