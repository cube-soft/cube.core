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

namespace Cube.Collections.Mixin
{
    /* --------------------------------------------------------------------- */
    ///
    /// EnumerableIndex
    ///
    /// <summary>
    /// Provides functionality to get an index in the sequence.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class EnumerableIndex
    {
        #region Methods

        #region IEnumerable<T>

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

        #region IEnumerable<int>

        /* ----------------------------------------------------------------- */
        ///
        /// OrderBy
        ///
        /// <summary>
        /// Sorts the elements of a sequence in ascending order.
        /// </summary>
        ///
        /// <param name="src">Source sequence.</param>
        ///
        /// <returns>
        /// IEnumerable(int) whose elements are sorted.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<int> OrderBy(this IEnumerable<int> src) =>
            src.OrderBy(i => i);

        /* ----------------------------------------------------------------- */
        ///
        /// OrderByDescending
        ///
        /// <summary>
        /// Sorts the elements of a sequence in decending order.
        /// </summary>
        ///
        /// <param name="src">Source sequence.</param>
        ///
        /// <returns>
        /// IEnumerable(int) whose elements are sorted.
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<int> OrderByDescending(this IEnumerable<int> src) =>
            src.OrderByDescending(i => i);

        /* ----------------------------------------------------------------- */
        ///
        /// Within
        ///
        /// <summary>
        /// Gets the elements of a sequence that is within the range of
        /// [begin, end).
        /// </summary>
        ///
        /// <param name="src">Source sequence.</param>
        /// <param name="begin">Lower limit of the range.</param>
        /// <param name="end">
        /// Upper limit of the range (The value is not included).
        /// </param>
        ///
        /// <returns>
        /// IEnumerable(int) whose elements are within the [begin, end).
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<int> Within(this IEnumerable<int> src, int begin, int end) =>
            src.Where(i => i >= begin && i < end);

        /* ----------------------------------------------------------------- */
        ///
        /// Within
        ///
        /// <summary>
        /// Gets the elements of a sequence that is within the range of
        /// [0, n).
        /// </summary>
        ///
        /// <param name="src">Source sequence.</param>
        /// <param name="n">
        /// Upper limit of the range (The value is not included).
        /// </param>
        ///
        /// <returns>
        /// IEnumerable(int) whose elements are within the [0, n).
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<int> Within(this IEnumerable<int> src, int n) =>
            src.Within(0, n);

        #endregion

        #endregion
    }
}
