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
using System.Linq;

namespace Cube.Mixin.Collections
{
    /* --------------------------------------------------------------------- */
    ///
    /// IndicesExtension
    ///
    /// <summary>
    /// Provides extended methods of the IEnumerable(int) class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class IndicesExtension
    {
        #region OrderBy

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
        public static IEnumerable<int> OrderBy(this IEnumerable<int> src) => src.OrderBy(i => i);

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

        #endregion

        #region Within

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
        public static IEnumerable<int> Within(this IEnumerable<int> src, int n) => src.Within(0, n);

        #endregion
    }
}
