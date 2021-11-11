﻿/* ------------------------------------------------------------------------- */
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

namespace Cube.Mixin.Collections.Generic
{
    /* --------------------------------------------------------------------- */
    ///
    /// Extension
    ///
    /// <summary>
    /// Provides extended methods about the IEnumerable(T) class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Extension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToEnumerable(T)
        ///
        /// <summary>
        /// Converts the specified type T object to IEnumerable(T) object.
        /// </summary>
        ///
        /// <param name="src">Source value.</param>
        ///
        /// <returns>Collection that has only the specified value.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<T> ToEnumerable<T>(this T src) { yield return src; }

        /* ----------------------------------------------------------------- */
        ///
        /// Concat
        ///
        /// <summary>
        /// Combines the specified items to the end of the specified source.
        /// </summary>
        ///
        /// <param name="src">First item.</param>
        /// <param name="items">Items to be combined.</param>
        ///
        /// <returns>Combined sequence.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<T> Concat<T>(this T src, params T[] items)
        {
            yield return src;
            foreach (var e in items) yield return e;
        }

        #endregion
    }
}
