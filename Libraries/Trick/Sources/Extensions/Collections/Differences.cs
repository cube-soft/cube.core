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
namespace Cube.Collections.Extensions;

using System;
using System.Collections.Generic;
using Cube.Collections;
using Cube.Collections.Differences;

/* ------------------------------------------------------------------------- */
///
/// DiffMethods
///
/// <summary>
/// Provides extended methods to get diff between the two sequences.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class DiffMethods
{
    /* --------------------------------------------------------------------- */
    ///
    /// Diff(T)
    ///
    /// <summary>
    /// Returns diff between the two sequences.
    /// </summary>
    ///
    /// <param name="newer">Newer sequence.</param>
    /// <param name="older">Older sequence.</param>
    ///
    /// <returns>Diff result.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<Result<T>> Diff<T>(
        this IEnumerable<T> newer,
        IEnumerable<T> older) =>
        Diff(newer, older, Condition.DiffOnly);

    /* --------------------------------------------------------------------- */
    ///
    /// Diff(T)
    ///
    /// <summary>
    /// Returns diff between the two sequences.
    /// </summary>
    ///
    /// <param name="newer">Newer sequence.</param>
    /// <param name="older">Older sequence.</param>
    /// <param name="mask">Mask to filter the results.</param>
    ///
    /// <returns>Diff result.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<Result<T>> Diff<T>(
        this IEnumerable<T> newer,
        IEnumerable<T> older,
        Condition mask) =>
        new OnpAlgorithm<T>().Compare(older, newer, mask);

    /* --------------------------------------------------------------------- */
    ///
    /// Diff(T)
    ///
    /// <summary>
    /// Returns diff between the two sequences.
    /// </summary>
    ///
    /// <param name="newer">Newer sequence.</param>
    /// <param name="older">Older sequence.</param>
    /// <param name="comparer">Comparer object.</param>
    ///
    /// <returns>Diff result.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<Result<T>> Diff<T>(
        this IEnumerable<T> newer,
        IEnumerable<T> older,
        IEqualityComparer<T> comparer) =>
        Diff(newer, older, comparer, Condition.DiffOnly);

    /* --------------------------------------------------------------------- */
    ///
    /// Diff(T)
    ///
    /// <summary>
    /// Returns diff between the two sequences.
    /// </summary>
    ///
    /// <param name="newer">Newer sequence.</param>
    /// <param name="older">Older sequence.</param>
    /// <param name="comparer">Comparer object.</param>
    /// <param name="mask">Mask to filter the results.</param>
    ///
    /// <returns>Diff result.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<Result<T>> Diff<T>(
        this IEnumerable<T> newer,
        IEnumerable<T> older,
        IEqualityComparer<T> comparer,
        Condition mask) =>
        new OnpAlgorithm<T>(comparer).Compare(older, newer, mask);

    /* --------------------------------------------------------------------- */
    ///
    /// Diff(T)
    ///
    /// <summary>
    /// Returns diff between the two sequences.
    /// </summary>
    ///
    /// <param name="newer">Newer sequence.</param>
    /// <param name="older">Older sequence.</param>
    /// <param name="compare">Function to compare each element.</param>
    ///
    /// <returns>Diff result.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<Result<T>> Diff<T>(
        this IEnumerable<T> newer,
        IEnumerable<T> older,
        Func<T, T, bool> compare) =>
        Diff(newer, older, compare, Condition.DiffOnly);

    /* --------------------------------------------------------------------- */
    ///
    /// Diff(T)
    ///
    /// <summary>
    /// Returns diff between the two sequences.
    /// </summary>
    ///
    /// <param name="newer">Newer sequence.</param>
    /// <param name="older">Older sequence.</param>
    /// <param name="compare">Function to compare each element.</param>
    /// <param name="mask">Mask to filter the results.</param>
    ///
    /// <returns>Diff result.</returns>
    ///
    /* --------------------------------------------------------------------- */
    public static IEnumerable<Result<T>> Diff<T>(
        this IEnumerable<T> newer,
        IEnumerable<T> older,
        Func<T, T, bool> compare,
        Condition mask) =>
        Diff(older, newer, new LambdaEqualityComparer<T>(compare), mask);
}
