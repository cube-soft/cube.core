/* ------------------------------------------------------------------------- */
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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cube.Collections
{
    /* --------------------------------------------------------------------- */
    ///
    /// Collections.Operations
    /// 
    /// <summary>
    /// Collection クラスの拡張メソッド用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Operations
    {
        #region IEnumerable(T)

        /* ----------------------------------------------------------------- */
        ///
        /// ToObservable
        /// 
        /// <summary>
        /// ObservableCollection に変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> src)
            => new ObservableCollection<T>(src);

        /* ----------------------------------------------------------------- */
        ///
        /// Difference
        /// 
        /// <summary>
        /// 差分を検出します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<Cube.Differences.Result<T>> Difference<T>(
            this IEnumerable<T> newer, IEnumerable<T> older, bool diffonly = true)
            => new Cube.Differences.OnpAlgorithm<T>().Compare(older, newer, diffonly);

        /* ----------------------------------------------------------------- */
        ///
        /// Difference
        /// 
        /// <summary>
        /// 差分を検出します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<Cube.Differences.Result<T>> Difference<T>(
            this IEnumerable<T> newer, IEnumerable<T> older,
            IEqualityComparer<T> comparer, bool diffonly = true)
            => new Cube.Differences.OnpAlgorithm<T>(comparer).Compare(older, newer, diffonly);

        #endregion

        #region IList(T)

        /* ----------------------------------------------------------------- */
        ///
        /// Clamp
        /// 
        /// <summary>
        /// 指定されたインデックスを [0, IList(T).Count) の範囲に丸めます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static int Clamp<T>(this IList<T> src, int index)
            => Math.Min(Math.Max(index, 0), LastIndex(src));

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndex
        /// 
        /// <summary>
        /// 最後のインデックスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static int LastIndex<T>(this IList<T> src)
            => (src == null || src.Count == 0) ? 0 : src.Count - 1;

        #endregion
    }
}
