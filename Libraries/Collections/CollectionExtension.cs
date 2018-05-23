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
using Cube.Differences;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Cube.Collections
{
    /* --------------------------------------------------------------------- */
    ///
    /// CollectionOperator
    ///
    /// <summary>
    /// Collection クラスの拡張メソッド用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class CollectionExtension
    {
        #region Methods

        #region Diff

        /* ----------------------------------------------------------------- */
        ///
        /// Diff(T)
        ///
        /// <summary>
        /// 差分を検出します。
        /// </summary>
        ///
        /// <param name="newer">変更後のオブジェクト</param>
        /// <param name="older">変更前のオブジェクト</param>
        ///
        /// <returns>
        /// 差分の結果を保持するオブジェクト
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<Result<T>> Diff<T>(
            this IEnumerable<T> newer,
            IEnumerable<T> older) =>
            Diff(newer, older, Condition.DiffOnly);

        /* ----------------------------------------------------------------- */
        ///
        /// Diff(T)
        ///
        /// <summary>
        /// 差分を検出します。
        /// </summary>
        ///
        /// <param name="newer">変更後のオブジェクト</param>
        /// <param name="older">変更前のオブジェクト</param>
        /// <param name="mask">結果のフィルタリング用 Mask</param>
        ///
        /// <returns>
        /// 差分の結果を保持するオブジェクト
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<Result<T>> Diff<T>(
            this IEnumerable<T> newer,
            IEnumerable<T> older,
            Condition mask) =>
            new OnpAlgorithm<T>().Compare(older, newer, mask);


        /* ----------------------------------------------------------------- */
        ///
        /// Diff(T)
        ///
        /// <summary>
        /// 差分を検出します。
        /// </summary>
        ///
        /// <param name="newer">変更後のオブジェクト</param>
        /// <param name="older">変更前のオブジェクト</param>
        /// <param name="comparer">比較用オブジェクト</param>
        ///
        /// <returns>
        /// 差分の結果を保持するオブジェクト
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<Result<T>> Diff<T>(
            this IEnumerable<T> newer,
            IEnumerable<T> older,
            IEqualityComparer<T> comparer) =>
            Diff(newer, older, comparer, Condition.DiffOnly);

        /* ----------------------------------------------------------------- */
        ///
        /// Diff(T)
        ///
        /// <summary>
        /// 差分を検出します。
        /// </summary>
        ///
        /// <param name="newer">変更後のオブジェクト</param>
        /// <param name="older">変更前のオブジェクト</param>
        /// <param name="comparer">比較用オブジェクト</param>
        /// <param name="mask">結果のフィルタリング用 Mask</param>
        ///
        /// <returns>
        /// 差分の結果を保持するオブジェクト
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<Result<T>> Diff<T>(
            this IEnumerable<T> newer,
            IEnumerable<T> older,
            IEqualityComparer<T> comparer,
            Condition mask) =>
            new OnpAlgorithm<T>(comparer).Compare(older, newer, mask);

        /* ----------------------------------------------------------------- */
        ///
        /// Diff(T)
        ///
        /// <summary>
        /// 差分を検出します。
        /// </summary>
        ///
        /// <param name="newer">変更後のオブジェクト</param>
        /// <param name="older">変更前のオブジェクト</param>
        /// <param name="compare">比較関数</param>
        ///
        /// <returns>
        /// 差分の結果を保持するオブジェクト
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<Result<T>> Diff<T>(
            this IEnumerable<T> newer,
            IEnumerable<T> older,
            Func<T, T, bool> compare) =>
            Diff(newer, older, compare, Condition.DiffOnly);

        /* ----------------------------------------------------------------- */
        ///
        /// Diff(T)
        ///
        /// <summary>
        /// 差分を検出します。
        /// </summary>
        ///
        /// <param name="newer">変更後のオブジェクト</param>
        /// <param name="older">変更前のオブジェクト</param>
        /// <param name="compare">比較関数</param>
        /// <param name="mask">結果のフィルタリング用 Mask</param>
        ///
        /// <returns>
        /// 差分の結果を保持するオブジェクト
        /// </returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<Result<T>> Diff<T>(
            this IEnumerable<T> newer,
            IEnumerable<T> older,
            Func<T, T, bool> compare,
            Condition mask) =>
            Diff(older, newer, new GenericEqualityComparer<T>(compare), mask);

        #endregion

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

        /* ----------------------------------------------------------------- */
        ///
        /// Clamp(T)
        ///
        /// <summary>
        /// 指定されたインデックスを [0, Count) の範囲に補正します。
        /// </summary>
        ///
        /// <param name="src">コレクション</param>
        /// <param name="index">インデックス</param>
        ///
        /// <returns>補正後のインデックス</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static int Clamp<T>(this IList<T> src, int index) =>
            Math.Min(Math.Max(index, 0), LastIndex(src));

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndex(T)
        ///
        /// <summary>
        /// 最後のインデックスを取得します。
        /// </summary>
        ///
        /// <param name="src">コレクション</param>
        ///
        /// <returns>最後のインデックス</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static int LastIndex<T>(this IList<T> src) =>
            src == null || src.Count == 0 ? 0 : src.Count - 1;

        /* ----------------------------------------------------------------- */
        ///
        /// GetOrDefault(T)
        ///
        /// <summary>
        /// 自身かまたは空の IEnumerable(T) オブジェクトを取得します。
        /// </summary>
        ///
        /// <param name="src">コレクション</param>
        ///
        /// <returns>自身かまたは空のコレクション</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<T> GetOrDefault<T>(this IEnumerable<T> src) =>
            src ?? Enumerable.Empty<T>();

        #endregion

        #region Implementations

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

        #endregion
    }
}
